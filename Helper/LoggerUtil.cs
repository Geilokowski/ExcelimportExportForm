using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProExcelImportExport.Helper
{
    class LoggerUtil
    {
        public static void InitLogger()
        {
            DeleteLogFile();
            LoggingConfiguration config = new LoggingConfiguration();

            NLog.Targets.ConsoleTarget consoleTarget = new NLog.Targets.ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);

            NLog.Targets.FileTarget fileTarget = new NLog.Targets.FileTarget("logfile");
            fileTarget.FileName = "log.txt";
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }

        public static void DeleteLogFile()
        {
            String startVerzeichnis = System.IO.Directory.GetCurrentDirectory() + "\\log.txt";
            if (File.Exists(startVerzeichnis))
            {
                File.Delete(startVerzeichnis);
            }
        }
    }
}
