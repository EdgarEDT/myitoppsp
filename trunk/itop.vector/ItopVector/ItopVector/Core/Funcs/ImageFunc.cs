namespace ItopVector.Core.Func
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Net;
	using System.Text.RegularExpressions;
	using System.Collections;
	using System.Text;
	using System.Drawing.Imaging;
	using System.Xml;

    public class ImageFunc
    {
		static Hashtable imagetable;
		static Regex regex;
		
        // Methods
        public ImageFunc()
        {
        }
		static ImageFunc()
		{
			imagetable =new Hashtable();
			regex = new Regex("^data:image\\/[a-zA-Z0-9]+;base64,", RegexOptions.IgnoreCase);

		}

        public static Bitmap GetImageForURL(string sURL, SvgElement element)
        {
            Bitmap bitmap1 = null;
            try
            {
                if ((sURL.StartsWith("http://") || sURL.StartsWith("https://")) || sURL.StartsWith("file://"))
                {
                    WebRequest request1 = WebRequest.Create(sURL);
                    WebResponse response1 = request1.GetResponse();
                    Stream stream1 = response1.GetResponseStream();
                    MemoryStream stream2 = new MemoryStream();
                    for (int num1 = stream1.ReadByte(); num1 != -1; num1 = stream1.ReadByte())
                    {
                        stream2.WriteByte((byte) num1);
                    }
                    if (!response1.ContentType.StartsWith("image/svg+xml"))
                    {
                        bitmap1 = (Bitmap) System.Drawing.Image.FromStream(stream2);
                    }
                    stream1.Close();
                    return bitmap1;
                }
                string text1 = Path.GetExtension(sURL);
                if (text1.Trim().ToLower() == ".svg")
                {
                    SvgDocument document1 = SvgDocumentFactory.CreateDocumentFromFile(sURL);
                    if ((document1 != null) && (document1.DocumentElement is SVG))
                    {
                        SVG svg1 = (SVG) document1.DocumentElement;
                        bitmap1 = new Bitmap((int) svg1.Width, (int) svg1.Height);
                        Graphics graphics1 = Graphics.FromImage(bitmap1);
                        svg1.Draw(graphics1, element.OwnerDocument.ControlTime);
                        graphics1.Dispose();
                    }
                    return bitmap1;
                }				
				if (regex.IsMatch(sURL))//Base64±àÂë×ÊÔ´
				{
					string text2 = sURL.Substring(regex.Match(sURL).Length);
					Stream stream1 = FromBase64String(text2);
					if (stream1 != null)
					{
						bitmap1 = (Bitmap)System.Drawing.Image.FromStream(stream1);
					}
					return bitmap1;
				}
//				System.Drawing.Image a= System.Drawing.Image.FromFile(sURL,true);
//				if(a.GetType().FullName=="System.Drawing.Imaging.Metafile")
//				{
//					
//					Metafile wmf=(Metafile)Metafile.FromFile(sURL);
//					int aa=wmf.Width;
//					int bb=wmf.Height;
//					Bitmap bm=new Bitmap(wmf.Width,wmf.Height); 
//					Graphics gr=Graphics.FromImage(bm);
//					RectangleF dest_bounds=new RectangleF(0,0,wmf.Width,wmf.Height);
//					RectangleF source_bounds=new RectangleF(0,0,wmf.Width,wmf.Height);
//					gr.DrawImage(wmf, dest_bounds, source_bounds, GraphicsUnit.Pixel);
//
////					MemoryStream stream4=new MemoryStream() ;
////					bm.Save(stream4,bm.RawFormat);
//					bm.Save("C:\\1.bmp",ImageFormat.Bmp);
//				
//					bitmap1 = (Bitmap) System.Drawing.Image.FromFile("C:\\1.bmp");
//					MemoryStream stream3=new MemoryStream() ;
//					bitmap1.Save(stream3,bitmap1.RawFormat);				
//					if (stream3 != null)
//					{
//						string text3 = ToBase64String(stream3);
//					
//						(element as XmlElement).SetAttribute("href", SvgDocument.XLinkNamespace, text3);
//					}
//					 gr.Dispose();
//				}
				else
				{
                    try {
                        bitmap1 = (Bitmap)System.Drawing.Image.FromFile(sURL);
                    } catch {
                        bitmap1 = (Bitmap)System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\"+sURL);
                    }
                    if (bitmap1!=null && !SvgDocument.BkImageLoad) {
                        MemoryStream stream3 = new MemoryStream();
                        bitmap1.Save(stream3, bitmap1.RawFormat);
                        if (stream3 != null) {
                            string text3 = ToBase64String(stream3);

                            (element as XmlElement).SetAttribute("href", SvgDocument.XLinkNamespace, text3);
                        }
                    }
				
				}
               
				
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return bitmap1;
        }
		internal static string ToBase64String(MemoryStream stream)
		{
			if (stream != null)
			{
				byte[] buffer1 = stream.GetBuffer();
				if (buffer1 != null)
				{
					StringBuilder text1 =new StringBuilder(Convert.ToBase64String(buffer1));
//					string text2 = string.Empty;
//					StringBuilder text3 = new StringBuilder();
//					int n=0;
//					while (true)
//					{
//						if (text1.ToString().Length < 2000)
//						{
////							text2 = text2 + text1;
//							text3.Append(text1.ToString());
//							break;
//						}
////						text2 = text2 + text1.Substring(0, 1000) + "\r\n";
//
//						text3.Append(text1.ToString(0,2000)+"\r\n");
//						text1.Remove(0,2000);
//
//						n++;
//						if(n==1000)
//						{
//							n=0;
//						}
//						
//					}
//					if (text2.Trim().Length > 0)
//					{
//						text2 = "data:image/jpg;base64," + text2.Trim();
//					}
//					return text2;
					if(text1.ToString().Trim().Length>0)
					{
						imagetable[text1.ToString()] = stream;
						text1.Insert(0,"data:image/jpg;base64,");
					}
					return text1.ToString();
				}
			}
			return string.Empty;
		}
		internal static  Stream FromBase64String(string base64String)
		{
			try
			{
				if (!imagetable.ContainsKey(base64String))
				{
					imagetable[base64String] = new MemoryStream(Convert.FromBase64String(base64String));
				}
			}
			catch
			{
			}
			return (imagetable[base64String] as Stream);
		}

    }

}

