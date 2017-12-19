using CommandLine;

namespace Triage.Mortician
{
    /// <summary>
    /// The command line options for the default running options
    /// </summary>
    [Verb("run", HelpText = "Run mortician on the provided dump file")]
    public class DefaultOptions
    {
        [Option('d', "dumpfile", HelpText = "The dump file to operate on", SetName = "LocalFile")]
        public string DumpFile { get; set; }

        [Option("s3-bucket", HelpText = "Bucket where the dump file is located", SetName = "S3")]
        public string S3DumpFileBucket { get; set; }

        [Option("s3-key", HelpText = "Bucket where the dump file is located", SetName = "S3")]
        public string S3DumpFileKey { get; set; }
    }
}