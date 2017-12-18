using System;
using System.ComponentModel.Composition;
using System.IO;
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
    [Export(typeof(IAnalysisObserver))]
    internal sealed class S3UploaderAnalysisObserver : IAnalysisObserver
    {
        public ILog Log = LogManager.GetLogger(typeof(S3UploaderAnalysisObserver));

        [Import]
        public EventHub EventHub { get; set; }

        [Import]
        public SettingsRepository SettingsRepository { get; set; }

        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Process(CancellationToken cancellationToken)
        {                                                   
            return EventHub.Get<ExcelReportComplete>().ForEachAsync((reportFile) =>
            {
                // note: this looks for ~/.aws/credentials for a profile named default
                // see https://docs.aws.amazon.com/AmazonS3/latest/dev/walkthrough1.html#walkthrough1-add-users
                // todo: move this out to a decoupled component
                var creds = new StoredProfileAWSCredentials("default");

                using (var client = new AmazonS3Client(creds, RegionEndpoint.USEast1))
                using (var fs = File.OpenRead(reportFile.ReportFile))
                {                                             
                    var putReportRequest = new PutObjectRequest
                    {
                        // todo: this sould be a setting
                        BucketName = "reports.triage",
                        Key = reportFile.ReportFile,
                        InputStream = fs
                    };

                    // todo: make a service
                    Log.Info("Attempting to upload report to S3");
                    //PutObjectResponse putObjectResponse = client.PutObject(putReportRequest);         
                }
            });
        }
    }

    public class ExcelReportComplete : Message
    {
        public string ReportFile { get; protected internal set; }

    }
}