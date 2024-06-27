using NLog.Config;
using NLog.Targets;
using LogLevel = NLog.LogLevel;

namespace MonitoringDemo.Services
{
    public static class NLogLoggerConfiguration
    {
        static NLogLoggerConfiguration()
        {
            LogFilePath = CreateLogFile();
        }

        public static string LogFilePath { get; }

        private static string CreateLogFile()
        {
            var filename = $"{AppInfo.PackageName}.log";
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(folder))
            {
                folder = Directory.CreateDirectory(folder).FullName;
            }

            var filePath = Path.Combine(folder, filename);
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            return filePath;
        }

        public static LoggingConfiguration GetLoggingConfiguration()
        {
            var config = new LoggingConfiguration();
            var layout = "${longdate:universalTime=True}|${level}|${logger}|${message}|${exception:format=tostring}[EOL]";

            // Console Target
            var consoleTarget = new ConsoleTarget("console");
            consoleTarget.Layout = layout;
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);

            // Debug Target
            var debugTarget = new DebugTarget("debug");
            debugTarget.Layout = layout;
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);

            // File Target
            var fileTarget = new FileTarget();
            fileTarget.FileName = LogFilePath;
            fileTarget.Layout = layout;
            fileTarget.MaxArchiveFiles = 1;
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Rolling;
            fileTarget.ArchiveAboveSize = 1048576; // 1MB
            fileTarget.ConcurrentWrites = true;
            fileTarget.KeepFileOpen = false;
            config.AddTarget("file", fileTarget);

            var fileRule = new LoggingRule("*", LogLevel.Trace, fileTarget);
            config.LoggingRules.Add(fileRule);

            // TODO: AppCenter Replacement
            // AppCenterTarget
            // var appCenterTarget = new AppCenterTarget();
            // appCenterTarget.ReportExceptionAsCrash = true;
            // appCenterTarget.AppSecret = "";
            // appCenterTarget.ContextProperties.Add(new TargetPropertyWithContext
            // {
            //     Name = "logger",
            //     Layout = "${logger}"
            // });
            // appCenterTarget.ContextProperties.Add(new TargetPropertyWithContext
            // {
            //     Name = "loglevel",
            //     Layout = "${level}"
            // });
            // appCenterTarget.ContextProperties.Add(new TargetPropertyWithContext
            // {
            //     Name = "threadid",
            //     Layout = "${threadid}"
            // });
            // config.AddTarget("appCenter", appCenterTarget);
            //
            // var appCenterRule = new LoggingRule("*", LogLevel.Trace, appCenterTarget);
            // config.LoggingRules.Add(appCenterRule);
            //
            // LogManager.Configuration = config;

            return config;
        }
    }
}