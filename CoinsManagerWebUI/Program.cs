using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Common;
using System;
using System.IO;

namespace CoinsManagerWebUI
{
    public class Program
    {
        private static NLog.Logger logger;

        public static void Main(string[] args)
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
            // Manually setup your desired logger.
            // I've used NLog for demonstration purposes but you can use your own preferred logger.
            var today = DateTime.Today.ToString("yyyy-MM-dd");
            var binDirectory = GetBinDirectory<Program>();
            var nLogInternalFullPath = Path.Combine(binDirectory, "NLog", $"{today}-internal.log");

            InternalLogger.LogFile = nLogInternalFullPath;

            // Assign logger. Make sure you have the file NLog.config
            // https://github.com/NLog/NLog/wiki/File-target
            logger = NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Unhandled error");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                    {
                        var builtConfig = config.Build();
                        var secretClient = new SecretClient(
                            new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                            new DefaultAzureCredential());
                        config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static string GetBinDirectory<T>()
        {
            return Path.GetDirectoryName(typeof(T).Assembly.Location);
        }
    }
}
