using System;
using System.Security;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.Collections;



public class EncryptString     //加密解密类   
{


    ///   <summary>   
    ///   转换string到Byte树组   
    ///   </summary>   
    ///   <param   name="s">要转换的字符串</param>   
    ///   <returns>转换的Byte数组</returns>   
    public  Byte[] StringToByteArray(String s) {
        /*   
        Char[]   ca   =   s.ToCharArray();   
        Byte[]   ba   =   new   Byte[ca.Length];   
        for(int   i=0;   i<ba.Length;   i++)   ba[i]   =   (Byte)ca[i];   
        return   ba;*/

        return Encoding.UTF8.GetBytes(s);
    }

    ///   <summary>   
    ///   转换Byte数组到字符串   
    ///   </summary>   
    ///   <param   name="a_arrByte">Byte数组</param>   
    ///   <returns>字符串</returns>   
    public string ByteArrayToString(Byte[] a_arrByte) {
        /*   
        //char[]   ca   =   new   char[a_arrByte.Length]   ;   
        for(int   i   =   0   ;   i   <   a_arrByte.Length   ;   i   ++)   
        {   
        result   +=   (char)a_arrByte[i]   ;   
        }*/

        return Encoding.UTF8.GetString(a_arrByte);
    }


    ///   <summary>   
    ///   3des加密字符串   
    ///   </summary>   
    ///   <param   name="a_strString">要加密的字符串</param>   
    ///   <param   name="a_strKey">密钥</param>   
    ///   <returns>加密后并经base64编码的字符串</returns>   
    ///   <remarks>静态方法，采用默认ascii编码</remarks>   
    public string Encrypt3DES(string a_strString, string a_strKey) {
        TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

        DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(a_strKey));
        DES.Mode = CipherMode.ECB;

        ICryptoTransform DESEncrypt = DES.CreateEncryptor();

        byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(a_strString);
        return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
    }//end   method   

    ///   <summary>   
    ///   3des加密字符串   
    ///   </summary>   
    ///   <param   name="a_strString">要加密的字符串</param>   
    ///   <param   name="a_strKey">密钥</param>   
    ///   <param   name="encoding">编码方式</param>   
    ///   <returns>加密后并经base63编码的字符串</returns>   
    ///   <remarks>重载，指定编码方式</remarks>   
    public string Encrypt3DES(string a_strString, string a_strKey, Encoding encoding) {
        TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

        DES.Key = hashMD5.ComputeHash(encoding.GetBytes(a_strKey));
        DES.Mode = CipherMode.ECB;

        ICryptoTransform DESEncrypt = DES.CreateEncryptor();


        byte[] Buffer = encoding.GetBytes(a_strString);
        return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
    }


    ///   <summary>   
    ///   3des解密字符串   
    ///   </summary>   
    ///   <param   name="a_strString">要解密的字符串</param>   
    ///   <param   name="a_strKey">密钥</param>   
    ///   <returns>解密后的字符串</returns>   
    ///   <exception   cref="">密钥错误</exception>   
    ///   <remarks>静态方法，采用默认ascii编码</remarks>   
    public string Decrypt3DES(string a_strString, string a_strKey) {
        TripleDESCryptoServiceProvider DES = new
        TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

        DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(a_strKey));
        DES.Mode = CipherMode.ECB;

        ICryptoTransform DESDecrypt = DES.CreateDecryptor();

        string result = "";
        try {
            byte[] Buffer = Convert.FromBase64String(a_strString);
            result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        } catch (Exception e) {
#if   DEBUG   
  Console.WriteLine("错误：{0}"   ,   e)   ;   
#endif//DEBUG
            throw (new Exception("不是有效的 base64  字符串", e));
        }

        return result;
    }//end   method   

    ///   <summary>   
    ///   3des解密字符串   
    ///   </summary>   
    ///   <param   name="a_strString">要解密的字符串</param>   
    ///   <param   name="a_strKey">密钥</param>   
    ///   <param   name="encoding">编码方式</param>   
    ///   <returns>解密后的字符串</returns>   
    ///   <exception   cref="">密钥错误</exception>   
    ///   <remarks>静态方法，指定编码方式</remarks>   
    public string Decrypt3DES(string a_strString, string a_strKey, Encoding encoding) {
        TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

        DES.Key = hashMD5.ComputeHash(encoding.GetBytes(a_strKey));
        DES.Mode = CipherMode.ECB;

        ICryptoTransform DESDecrypt = DES.CreateDecryptor();

        string result = "";
        try {
            byte[] Buffer = Convert.FromBase64String(a_strString);
            result = encoding.GetString(DESDecrypt.TransformFinalBlock
            (Buffer, 0, Buffer.Length));
        } catch (Exception e) {
#if   DEBUG   
  Console.WriteLine("错误：{0}"   ,   e)   ;   
#endif//DEBUG
            throw (new Exception("不是有效的 base64  字符串", e));
        }

        return result;
    }//end   method   


}
/// <summary>
/// SymmCrypto 的摘要说明。
/// SymmCrypto类实现.NET框架下的加密和解密服务。
/// 原作者： Frank Fang : fangfrank@hotmail.com
/// </summary>
public class SymmCrypto {
    public enum SymmProvEnum : int {
        DES, RC2, Rijndael
    }

    private SymmetricAlgorithm mobjCryptoService;

    /// <remarks>
    /// 使用.Net SymmetricAlgorithm 类的构造器.
    /// </remarks>
    public SymmCrypto(SymmProvEnum NetSelected) {
        switch (NetSelected) {
            case SymmProvEnum.DES:
                mobjCryptoService = new DESCryptoServiceProvider();
                break;
            case SymmProvEnum.RC2:
                mobjCryptoService = new RC2CryptoServiceProvider();
                break;
            case SymmProvEnum.Rijndael:
                mobjCryptoService = new RijndaelManaged();
                break;
        }
    }

    /// <remarks>
    /// 使用自定义SymmetricAlgorithm类的构造器.
    /// </remarks>
    public SymmCrypto(SymmetricAlgorithm ServiceProvider) {
        mobjCryptoService = ServiceProvider;
    }

    /// <remarks>
    /// Depending on the legal key size limitations of 
    /// a specific CryptoService provider and length of 
    /// the private key provided, padding the secret key 
    /// with space character to meet the legal size of the algorithm.
    /// </remarks>
    private byte[] GetLegalKey(string Key) {
        string sTemp;
        if (mobjCryptoService.LegalKeySizes.Length > 0) {
            int lessSize = 0, moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
            // key sizes are in bits
            while (Key.Length * 8 > moreSize) {
                lessSize = moreSize;
                moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
            }
            sTemp = Key.PadRight(moreSize / 8, ' ');
        } else
            sTemp = Key;

        // convert the secret key to byte array
        return ASCIIEncoding.ASCII.GetBytes(sTemp);
    }

    public string Encrypting(string Source, string Key) {
        byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
        // create a MemoryStream so that the process can be done without I/O files
        System.IO.MemoryStream ms = new System.IO.MemoryStream();

        byte[] bytKey = GetLegalKey(Key);

        // set the private key
        mobjCryptoService.Key = bytKey;
        mobjCryptoService.IV = bytKey;

        // create an Encryptor from the Provider Service instance
        ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

        // create Crypto Stream that transforms a stream using the encryption
        CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

        // write out encrypted content into MemoryStream
        cs.Write(bytIn, 0, bytIn.Length);
        cs.FlushFinalBlock();

        // get the output and trim the '\0' bytes
        byte[] bytOut = ms.GetBuffer();
        int i = 0;
        for (i = 0; i < bytOut.Length; i++)
            if (bytOut[i] == 0)
                break;

        // convert into Base64 so that the result can be used in xml
        return System.Convert.ToBase64String(bytOut, 0, i);
    }

    public string Decrypting(string Source, string Key) {
        // convert from Base64 to binary
        byte[] bytIn = System.Convert.FromBase64String(Source);
        // create a MemoryStream with the input
        System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

        byte[] bytKey = GetLegalKey(Key);

        // set the private key
        mobjCryptoService.Key = bytKey;
        mobjCryptoService.IV = bytKey;

        // create a Decryptor from the Provider Service instance
        ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

        // create Crypto Stream that transforms a stream using the decryption
        CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

        // read out the result from the Crypto Stream
        System.IO.StreamReader sr = new System.IO.StreamReader(cs);
        return sr.ReadToEnd();
    }
}
