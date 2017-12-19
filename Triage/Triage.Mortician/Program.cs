﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using CommandLine;
using Common.Logging;

namespace Triage.Mortician
{
    /// <summary>
    ///     Entry point class
    /// </summary>
    internal class Program
    {
        internal static ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        ///     Entry point to the application
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Log.Trace("Hello world");

            Parser.Default.ParseArguments<DefaultOptions, ConfigOptions>(args).MapResult(
                (DefaultOptions opts) => DefaultExecution(opts),
                (ConfigOptions opts) => ConfigExecution(opts),
                errs => -1
            );     
        }

        /// <summary>
        ///     Executes the configuration module
        /// </summary>
        /// <param name="configOptions">The the configuration command line options</param>
        /// <returns>Program status code</returns>
        private static int ConfigExecution(ConfigOptions configOptions)
        {
            if (configOptions.ShouldList)
                try
                {
                    var configText = File.ReadAllText("mortician.config.json");
                    Console.WriteLine(configText);
                    return 0;
                }
                catch (IOException e)
                {
                    Console.WriteLine(
                        "Could not load mortician.config.json, use config -k \"some key\" -v \"some value\"");
                    return 1;
                }

            var settings = Settings.GetSettings();

            if (configOptions.KeysToDelete != null && configOptions.KeysToDelete.Any())
            {
                foreach (var key in configOptions.KeysToDelete)
                    if (settings.ContainsKey(key))
                        settings.Remove(key);
                Settings.SaveSettings(settings);
                return 0;
            }

            var pairs = configOptions.Keys.Zip(configOptions.Values, (s, s1) => (s, s1))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            var newSet = settings.Union(pairs);
            settings = newSet
                .GroupBy(kvp => kvp.Key)
                .Select(group => new KeyValuePair<string, string>(group.Key, group.Last().Value))
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            Settings.SaveSettings(settings);

            return 0;
        }

        /// <summary>
        ///     Perform the default execution
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Program status code</returns>
        private static int DefaultExecution(DefaultOptions options)
        {
            if (options.DumpFile == null && options.S3DumpFileBucket != null && options.S3DumpFileKey != null)
            {
                Log.Trace("Attempting to download dump from s3");
                var creds = new StoredProfileAWSCredentials("default");
                using (var client = new AmazonS3Client(creds, RegionEndpoint.USEast1))
                {
                    GetObjectResponse response;
                    try
                    {
                         response = client.GetObject(options.S3DumpFileBucket, options.S3DumpFileKey);
                    }
                    catch (Exception e)
                    {
                        Log.Fatal($"Unable to get file from bucket: {e.Message}. Exiting");
                        return -1;
                    }
                    
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        try
                        {
                            Log.Trace("Response came back OK, will save to disk");
                            string fileName = $"{DateTime.UtcNow:yyyy_MM_dd_mm_HH_ss}.dmp";
                            response.WriteResponseStreamToFile(fileName);

                            options.DumpFile = fileName;
                        }
                        catch (Exception e)
                        {
                            Log.Fatal($"Unable to write dump file to disk: {e}", e);
                            return -1;
                        }      
                    }
                    else
                    {
                        Log.Fatal($"Could not retrieve dump file from S3");
                        return -1;
                    }
                }
            }
            var executionLocation = Assembly.GetEntryAssembly().Location;
            var morticianAssemblyFiles =
                Directory.EnumerateFiles(Path.GetDirectoryName(executionLocation), "Triage.Mortician.*.dll");
            var toLoad =
                morticianAssemblyFiles.Except(AppDomain.CurrentDomain.GetAssemblies().Select(x => x.Location));
            foreach (var assembly in toLoad)
                Assembly.LoadFile(assembly);

            var aggregateCatalog = new AggregateCatalog(AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("Triage.Mortician")).Select(x => new AssemblyCatalog(x)));
            var compositionContainer = new CompositionContainer(aggregateCatalog);

            var repositoryFactory = new CoreComponentFactory(compositionContainer, new FileInfo(options.DumpFile));
            repositoryFactory.RegisterRepositories();

            var engine = compositionContainer.GetExportedValue<Engine>();
            engine.Process().Wait();

            return 0;
        }
    }
}