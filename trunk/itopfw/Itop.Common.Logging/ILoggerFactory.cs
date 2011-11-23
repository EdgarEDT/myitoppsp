using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Common.Logging
{
    /// <summary>
    /// ��־�����ӿ�
    /// </summary>
    public interface ILoggerFactory {
        /// <summary>
        /// ����һ����־��¼��
        /// </summary>
        /// <typeparam name="T">��־��¼������</typeparam>
        /// <param name="loggingPath">��־��¼·��</param>
        /// <returns></returns>
        ILogger CreateLogger<T>(string loggingPath);

        ILogger CreateLogger<T>();

        ILogger CreateLogger(Type type, string loggingPath);

        ILogger CreateLogger(Type type);
    }

    /// <summary>
    /// ��־��¼�߽ӿ�
    /// </summary>
    public interface ILogger {
        /// <summary>
        /// ��¼���Լ������־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        void Debug(string message);

        /// <summary>
        /// ��¼���Լ������־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        /// <param name="ex">�쳣</param>
        void Debug(string message, Exception ex);

        /// <summary>
        /// ��¼��Ϣ�������־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        void Info(string message);

        /// <summary>
        /// ��¼��Ϣ�������־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        /// <param name="ex">�쳣</param>
        void Info(string message, Exception ex);

        /// <summary>
        /// ��¼���漶�����־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        void Warn(string message);

        /// <summary>
        /// ��¼���漶�����־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        /// <param name="ex">�쳣</param>
        void Warn(string message, Exception ex);

        /// <summary>
        /// ��¼���󼶱����־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        void Error(string message);

        /// <summary>
        /// ��¼���󼶱����־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        /// <param name="ex">�쳣</param>
        void Error(string message, Exception ex);

        /// <summary>
        /// ��¼�������󼶱����־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        void Fatal(string message);

        /// <summary>
        /// ��¼��������L�������־
        /// </summary>
        /// <param name="message">��־��Ϣ</param>
        /// <param name="ex">�쳣</param>
        void Fatal(string message, Exception ex);
    }
}
