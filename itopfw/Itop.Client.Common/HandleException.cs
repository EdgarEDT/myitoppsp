using System;
using System.Collections.Generic;
using System.Text;
using Itop;
using Itop.Common; 

namespace Itop.Client.Common 
{
    public class HandleException
    {
        public static void TryCatch(Exception e)
        {
            string bugInfo = "";
            if(ProgramDebug.ShowDebugInfo)
            {
                bugInfo += "\n类型: " + e.GetType().ToString() + "\n";
                bugInfo += "详细信息: " + e.Message + "\n\n";
                bugInfo += "堆栈跟踪：\n";
                int nMaxLength = 500;
                if (e.StackTrace.Length > nMaxLength)
                {
                    bugInfo += e.StackTrace.Substring(0, nMaxLength) + "\n\n.......";
                }
                else
                {
                    bugInfo += e.StackTrace;
                }
            }

            try
            {
                throw e;
            }
            catch (System.Runtime.Remoting.RemotingException)
            {
                MsgBox.Show(Strings.ConnectServerFail + bugInfo);
            }
            catch (System.Net.WebException)
            {
                MsgBox.Show(Strings.ConnectServerFail + bugInfo);
            }
            catch (System.Net.Sockets.SocketException)
            {
                MsgBox.Show(Strings.ConnectServerFail + bugInfo);
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MsgBox.Show(Strings.AccessDatabaseFail + bugInfo);
            }
            catch (ItopClientException exc)
            {
                MsgBox.Show(exc.Message + bugInfo);
            }
            catch (System.Exception)
            {
                MsgBox.Show(Strings.Exception + bugInfo);
            }
        }
    }
}
