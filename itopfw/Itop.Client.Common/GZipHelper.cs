using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Core;
using System.IO;

namespace Itop.Client.Common {
    public enum OLECMDID {
        OLECMDID_OPEN = 1,
        OLECMDID_NEW = 2,
        OLECMDID_SAVE = 3,
        OLECMDID_SAVEAS = 4,
        OLECMDID_SAVECOPYAS = 5,
        OLECMDID_PRINT = 6,
        OLECMDID_PRINTPREVIEW = 7,
        OLECMDID_PAGESETUP = 8,
        OLECMDID_SPELL = 9,
        OLECMDID_PROPERTIES = 10,
        OLECMDID_CUT = 11,
        OLECMDID_COPY = 12,
        OLECMDID_PASTE = 13,
        OLECMDID_PASTESPECIAL = 14,
        OLECMDID_UNDO = 15,
        OLECMDID_REDO = 16,
        OLECMDID_SELECTALL = 17,
        OLECMDID_CLEARSELECTION = 18,
        OLECMDID_ZOOM = 19,
        OLECMDID_GETZOOMRANGE = 20,
        OLECMDID_UPDATECOMMANDS = 21,
        OLECMDID_REFRESH = 22,
        OLECMDID_STOP = 23,
        OLECMDID_HIDETOOLBARS = 24,
        OLECMDID_SETPROGRESSMAX = 25,
        OLECMDID_SETPROGRESSPOS = 26,
        OLECMDID_SETPROGRESSTEXT = 27,
        OLECMDID_SETTITLE = 28,
        OLECMDID_SETDOWNLOADSTATE = 29,
        OLECMDID_STOPDOWNLOAD = 30,
        OLECMDID_ONTOOLBARACTIVATED = 31,
        OLECMDID_FIND = 32,
        OLECMDID_DELETE = 33,
        OLECMDID_HTTPEQUIV = 34,
        OLECMDID_HTTPEQUIV_DONE = 35,
        OLECMDID_ENABLE_INTERACTION = 36,
        OLECMDID_ONUNLOAD = 37,
        OLECMDID_PROPERTYBAG2 = 38,
        OLECMDID_PREREFRESH = 39
    }	
    public enum OLECMDEXECOPT
    {	OLECMDEXECOPT_DODEFAULT	= 0,
	    OLECMDEXECOPT_PROMPTUSER	= 1,
	    OLECMDEXECOPT_DONTPROMPTUSER	= 2,
	    OLECMDEXECOPT_SHOWHELP	= 3
    }	

    /// <summary>
    /// 压缩解压单文件类
    /// </summary>
    public class GZipHelper {
        public static string Compress(string sourceFile){
            byte[] dataBuffer = new byte[4096];
            string destFile = sourceFile + ".gz";
            using (Stream gs = new GZipOutputStream(File.Create(destFile))) {
                using (FileStream fs = File.OpenRead(sourceFile)) {
                    StreamUtils.Copy(fs, gs, dataBuffer);
                }
            }
            return destFile;
        }
        public static string UnCompress(string gzipFile) {
            byte[] dataBuffer = new byte[4096];
            string destFile =Path.GetDirectoryName(gzipFile)+"\\"+ Path.GetFileNameWithoutExtension(gzipFile);
            using (GZipInputStream gs = new GZipInputStream(File.OpenRead(gzipFile))) {
                using (FileStream fs = File.Create(destFile)) {
                    StreamUtils.Copy(gs, fs, dataBuffer);
                }
            }
            return destFile;
        }
    }
}
