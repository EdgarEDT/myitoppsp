using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Common.Logging
{
    /// <summary>
    /// 日志工厂接口
    /// </summary>
    public interface ILoggerFactory {
        /// <summary>
        /// 创建一个日志记录者
        /// </summary>
        /// <typeparam name="T">日志记录者类型</typeparam>
        /// <param name="loggingPath">日志记录路径</param>
        /// <returns></returns>
        ILogger CreateLogger<T>(string loggingPath);

        ILogger CreateLogger<T>();

        ILogger CreateLogger(Type type, string loggingPath);

        ILogger CreateLogger(Type type);
    }

    /// <summary>
    /// 日志记录者接口
    /// </summary>
    public interface ILogger {
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Debug(string message);

        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常</param>
        void Debug(string message, Exception ex);

        /// <summary>
        /// 记录消息级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Info(string message);

        /// <summary>
        /// 记录消息级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常</param>
        void Info(string message, Exception ex);

        /// <summary>
        /// 记录警告级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Warn(string message);

        /// <summary>
        /// 记录警告级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常</param>
        void Warn(string message, Exception ex);

        /// <summary>
        /// 记录错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Error(string message);

        /// <summary>
        /// 记录错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常</param>
        void Error(string message, Exception ex);

        /// <summary>
        /// 记录致命错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        void Fatal(string message);

        /// <summary>
        /// 记录致命错误L级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常</param>
        void Fatal(string message, Exception ex);
    }
}
