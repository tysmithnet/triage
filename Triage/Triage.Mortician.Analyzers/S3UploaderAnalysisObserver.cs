﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Common.Logging;
using Triage.Mortician.Repository;

namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    ///     An object capable of responding to relevant report events by uploading them to S3
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.IAnalysisObserver" />
    [Export(typeof(IAnalysisObserver))]
    internal sealed class S3UploaderAnalysisObserver : IAnalysisObserver
    {
        private const string UploadExcelToS3 = "upload-excel-to-s3";
        private const string ExcelBucketId = "excel-bucket-id";
        public ILog Log = LogManager.GetLogger(typeof(S3UploaderAnalysisObserver));

        /// <summary>
        ///     Gets or sets the event hub.
        /// </summary>
        /// <value>
        ///     The event hub.
        /// </value>
        [Import]
        public EventHub EventHub { get; set; }

        /// <summary>
        ///     Gets or sets the settings repository.
        /// </summary>
        /// <value>
        ///     The settings repository.
        /// </value>
        [Import]
        public SettingsRepository SettingsRepository { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Performs setup
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that when complete will signal the completion of this work</returns>
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs the core execution
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that when complete will signal the completion of this work</returns>
        public Task Process(CancellationToken cancellationToken)
        {
            return EventHub.Get<ExcelReportComplete>().ForEachAsync(message =>
            {
                var shouldUpload = SettingsRepository.GetBool(UploadExcelToS3, fallbackToDefault: true);
                if (!shouldUpload)
                {
                    Log.Trace($"Skipping upload excel report to s3. Is the setting {UploadExcelToS3} set to true?");
                    return;
                }

                // note: this looks for ~/.aws/credentials for a profile named default
                // see https://docs.aws.amazon.com/AmazonS3/latest/dev/walkthrough1.html#walkthrough1-add-users
                // todo: move this out to a decoupled component
                StoredProfileAWSCredentials creds;
                try
                {
                    creds = new StoredProfileAWSCredentials("default");
                }
                catch (Exception e)
                {
                    Log.Error($"Unable to create credentials for S3 client");
                    return;
                }         

                using (var client = new AmazonS3Client(creds, RegionEndpoint.USEast1))
                using (var fs = File.OpenRead(message.ReportFile))
                {
                    var putReportRequest = new PutObjectRequest
                    {
                        // todo: this sould be a setting
                        BucketName = SettingsRepository.Get(ExcelBucketId),
                        Key = message.ReportFile,
                        InputStream = fs
                    };

                    // todo: make a service
                    Log.Info("Attempting to upload report to S3");
                    try
                    {
                        var putObjectResponse = client.PutObject(putReportRequest);
                        if (putObjectResponse.HttpStatusCode == HttpStatusCode.OK)
                            Log.Trace($"Upload of {message.ReportFile} was successful");
                        else
                            throw new ApplicationException("Unknown error");
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Unable to upload {message.ReportFile} to S3: {e.Message}", e);
                    }
                }
            }, cancellationToken);
        }
    }
}