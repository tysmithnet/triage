using System;
using System.ComponentModel.Composition;
using System.IO;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace Triage.Mortician.Analyzers
{
    [Export(typeof(IExcelPostProcessor))]
    internal sealed class S3UploaderPostProcessor : IExcelPostProcessor
    {
        public void PostProcess(FileInfo reportFile)
        {
            // note: this looks for ~/.aws/credentials for a profile named default
            // see https://docs.aws.amazon.com/AmazonS3/latest/dev/walkthrough1.html#walkthrough1-add-users
            // todo: move this out to a decoupled component
            var creds = new StoredProfileAWSCredentials("default");
            using (var client = new AmazonS3Client(creds, RegionEndpoint.USEast1))
            using (var fs = File.OpenRead(reportFile.FullName))
            {
                Console.WriteLine("Uploading an object");
                var putReportRequest = new PutObjectRequest
                {
                    // todo: this sould be a setting
                    BucketName = "reports.triage",
                    Key = reportFile.Name,
                    InputStream = fs
                };

                //PutObjectResponse putObjectResponse = client.PutObject(putReportRequest);         
            }
        }
    }
}