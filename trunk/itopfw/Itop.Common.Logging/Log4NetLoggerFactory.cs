using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net.Config;

namespace Itop.Common.Logging
{
    public class Log4NetLoggerFactory {
        static Log4NetLoggerFactory() {

            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
        }

        public static ILogger CreateLogger<T>(string group) {
            return new Log4NetLogger(typeof(T), group);
        }

        public static ILogger CreateLogger<T>() {
            return new Log4NetLogger(typeof(T), String.Empty);
        }

        public static ILogger CreateLogger(Type type, string loggingPath) {
            return new Log4NetLogger(type, String.Empty);
        }

        public static ILogger CreateLogger(Type type) {
            return new Log4NetLogger(type, String.Empty);
        }
    }
       
}
