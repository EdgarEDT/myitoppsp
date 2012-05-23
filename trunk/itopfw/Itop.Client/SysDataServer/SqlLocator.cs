using System;

namespace Itop.Client
{
	/// <summary>
	/// SqlLocator ��ժҪ˵����
	/// </summary>
	public class SqlLocator
	{
		public SqlLocator()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}



		[System.Runtime.InteropServices.DllImport("odbc32.dll")]

		private static extern short SQLAllocHandle(short hType, IntPtr inputHandle, out IntPtr outputHandle);

		[System.Runtime.InteropServices.DllImport("odbc32.dll")]

		private static extern short SQLSetEnvAttr(IntPtr henv, int attribute, IntPtr valuePtr, int strLength);

		[System.Runtime.InteropServices.DllImport("odbc32.dll")]

		private static extern short SQLFreeHandle(short hType, IntPtr handle); 

		[System.Runtime.InteropServices.DllImport("odbc32.dll",CharSet= System.Runtime.InteropServices.CharSet.Ansi)]

		private static extern short SQLBrowseConnect(IntPtr hconn, System.Text.StringBuilder inString, 

			short inStringLength, System.Text.StringBuilder outString, short outStringLength,

			out short outLengthNeeded);

 

		private const short SQL_HANDLE_ENV = 1;

		private const short SQL_HANDLE_DBC = 2;

		private const int SQL_ATTR_ODBC_VERSION = 200;

		private const int SQL_OV_ODBC3 = 3;

		private const short SQL_SUCCESS = 0;

 

		private const short SQL_NEED_DATA = 99;

		private const short DEFAULT_RESULT_SIZE = 1024;

		private const string SQL_DRIVER_STR = "DRIVER=SQL SERVER";

 

		/// <summary>

		/// ��ȡ���ڵ����ݿ���������ƣ���һ���ַ������顣

		/// </summary>

		/// <returns></returns>

		public static string[] GetServers()

		{

			string list = string.Empty;

			IntPtr henv = IntPtr.Zero;

			IntPtr hconn = IntPtr.Zero;

			System.Text.StringBuilder inString = new System.Text.StringBuilder(SQL_DRIVER_STR);

			System.Text.StringBuilder outString = new System.Text.StringBuilder(DEFAULT_RESULT_SIZE);

			short inStringLength = (short) inString.Length;

			short lenNeeded = 0;

 

			try

			{

				if (SQL_SUCCESS == SQLAllocHandle(SQL_HANDLE_ENV, henv, out henv))

				{

					if (SQL_SUCCESS == SQLSetEnvAttr(henv,SQL_ATTR_ODBC_VERSION,(IntPtr)SQL_OV_ODBC3,0))

					{

						if (SQL_SUCCESS == SQLAllocHandle(SQL_HANDLE_DBC, henv, out hconn))

						{

							if (SQL_NEED_DATA ==  SQLBrowseConnect(hconn, inString, inStringLength, outString, 

								DEFAULT_RESULT_SIZE, out lenNeeded))

							{

								if (DEFAULT_RESULT_SIZE < lenNeeded)

								{

									outString.Capacity = lenNeeded;

									if (SQL_NEED_DATA != SQLBrowseConnect(hconn, inString, inStringLength, outString, 

										lenNeeded,out lenNeeded))

									{

										throw new ApplicationException("Unabled to aquire SQL Servers from ODBC driver.");

									}    

								}

								list = outString.ToString();

								int start = list.IndexOf("{") + 1;

								int len = list.IndexOf("}") - start;

								if ((start > 0) && (len > 0))

								{

									list = list.Substring(start,len);

								}

								else

								{

									list = string.Empty;

								}

							}                           

						}

					}

				}

			}

			catch

			{

				list = string.Empty;

			}

			finally

			{

				if (hconn != IntPtr.Zero)

				{

					SQLFreeHandle(SQL_HANDLE_DBC,hconn);

				}

				if (henv != IntPtr.Zero)

				{

					SQLFreeHandle(SQL_HANDLE_ENV,hconn);

				}

			}

 

			string[] array = null;

 

			if (list.Length > 0)

			{

				array = list.Split(',');

			}

 

			return array;

		}


	}
}
