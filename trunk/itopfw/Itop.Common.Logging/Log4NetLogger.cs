using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace Itop.Common.Logging
{
    public class Log4NetLogger : ILogger {
        ILog log;
        string group;

        public Log4NetLogger(Type type, string group) {
            this.group = group;
            log = LogManager.GetLogger(type);
        }

        string GetMessage(string msg) {
            return group + " " + msg;
        }

        public void Debug(string message) {
            log.Debug(GetMessage(message));
        }

        public void Debug(string message, Exception ex) {
            log.Debug(GetMessage(message), ex);
        }

        public void Info(string message) {
            log.Info(GetMessage(message));
        }

        public void Info(string message, Exception ex) {
            log.Info(GetMessage(message), ex);
        }

        public void Warn(string message) {
            log.Warn(GetMessage(message));
        }

        public void Warn(string message, Exception ex) {
            log.Warn(GetMessage(message), ex);
        }

        public void Error(string message) {
            log.Error(GetMessage(message));
        }

        public void Error(string message, Exception ex) {
            log.Error(GetMessage(message), ex);
        }

        public void Fatal(string message) {
            log.Fatal(GetMessage(message));
        }

        public void Fatal(string message, Exception ex) {
            log.Fatal(GetMessage(message), ex);
        }
    }
}
