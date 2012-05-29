using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Collections;
using System.Drawing.Imaging;
namespace Itop.Client.Resources
{
    public class ImageListRes
    {
        
        public static ImageList GetimageList(int size,DataTable imageNames)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(size, size);
            imageList.ColorDepth = ColorDepth.Depth32Bit;

            
            foreach (DataRow row in imageNames.Rows)
            {
                try
                {
                    object obj = Itop.Client.Resources.Properties.Resources.ResourceManager.GetObject(row["ProgIco"] as string);
                    if (obj is Icon){
                        imageList.Images.Add(row["ProgIco"] as string, (Icon)obj);
                    }
                    else if (obj is Bitmap)
                    {
                        imageList.Images.Add(row["ProgIco"] as string, (Bitmap)obj);
                    }
                    else
                    { 
                    
                    }
                    
                }
                catch(Exception exc)
                {
                    System.Diagnostics.Debug.WriteLine(exc.GetType().ToString() + " - " + exc.Message);
                }
            }            

         return imageList;
        }


        

        public static ImageList GetimageList(int width,int height, DataTable imageNames)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(width, height);
            imageList.ColorDepth = ColorDepth.Depth32Bit;

            //imageList.Images.Add("selected", (Bitmap)Itop.Client.Resources.Properties.Resources.ResourceManager.GetObject("selected"));
            foreach (DataRow row in imageNames.Rows)
            {
                try
                {
                    object obj = Itop.Client.Resources.Properties.Resources.ResourceManager.GetObject(row["ProgIco"] as string);
                    if (obj is Icon)
                    {
                        imageList.Images.Add(row["ProgIco"] as string, (Icon)obj);
                    }
                    else if (obj is Bitmap)
                    {
                        imageList.Images.Add(row["ProgIco"] as string, (Bitmap)obj);
                    }
                    else
                    {

                    }

                }
                catch (Exception exc)
                {
                    System.Diagnostics.Debug.WriteLine(exc.GetType().ToString() + " - " + exc.Message);
                }
            }

            return imageList;
        }



        static ImageList imagelistall=new ImageList();
        public static Stream GetFileStream(string filename) {
            Assembly assembly1 = Assembly.GetAssembly(typeof(Itop.Client.Resources.ImageListRes));
            if (assembly1 != null) {
                
                return assembly1.GetManifestResourceStream(filename);
            }
            return null;
        }

        public static void GetImageFile(Image metaFile, string filename,int wid,int hei)
        {
            Bitmap bmp = new Bitmap(wid, hei);
               Graphics g = Graphics.FromImage(metaFile);   
               g.DrawImage(bmp, 0, 0, bmp.Width,bmp.Height);
               bmp.Save(filename, ImageFormat.Png);
               // Clear object clearly
               g.Dispose();
               metaFile.Dispose();
               bmp.Dispose();
        }


        public static ImageList GetimageListAll(int size)
        {
            
            ImageList imageList = imagelistall;
            if (imagelistall.Images.Count == 0) {
                imageList.ImageSize = new System.Drawing.Size(size, size);
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                Stream stream = GetFileStream("Itop.Client.Resources.Properties.Resources.resources");

                ResourceReader read = new ResourceReader(stream);
                IDictionaryEnumerator ie = read.GetEnumerator();
                while (ie.MoveNext()) {
                    object obj = ie.Value;
                    try {
                        if (obj is Icon) {
                            imageList.Images.Add(ie.Key as string, (Icon)obj);
                        } else {
                            Bitmap tempbit = (Bitmap)obj;
                            tempbit.Tag = ie.Key as string;
                            imageList.Images.Add(ie.Key as string, tempbit);
                        }
                    } catch { }
                }
            }
            return imageList;
        }
        private static bool Is_In(string[] str, string strvalue)
        {
            bool have = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (strvalue==str[i].ToString())
                {
                    have = true;
                    break;
                }
            }
            return have;
        }
        public static ImageList GetimageListAll(int size,string str)
        {
            //ϵͳ��ͼƬ����ʾ��ͼ���б���
            string[] sysico ={ "a0", "a1", "a2", "a3", "a4", "bottom", "dl1", "dl2", "dl3", "ma", "sz1", "sz2", "sz3","about","maintop","loginwait","loginbg" };
            //ImageList imageList = imagelistall;
            ImageList imageList = new ImageList ();
            
            //if (imagelistall.Images.Count == 0||str!="")
            //{
                imageList.Images.Clear();
                imageList.ImageSize = new System.Drawing.Size(size, size);
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                Stream stream = GetFileStream("Itop.Client.Resources.Properties.Resources.resources");

                ResourceReader read = new ResourceReader(stream);
                IDictionaryEnumerator ie = read.GetEnumerator();
                while (ie.MoveNext())
                {
                    object obj = ie.Value;
                    try
                    {
                        if (obj is Icon)
                        {
                            //imageList.Images.Add(ie.Key as string, (Icon)obj);
                        }
                        else
                        {
                            if (!Is_In(sysico,ie.Key.ToString()))
                            {
                                if (ie.Key.ToString().Contains(str)||str=="")
                                {
                                    Bitmap tempbit = (Bitmap)obj;
                                    imageList.Images.Add(ie.Key as string, tempbit);
                                }
                               
                            }
                           
                        }
                    }
                    catch { }
                }
            //}
            return imageList;
        }
        public static ImageList GetToolBarimageList(int size)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(size, size);
            imageList.ColorDepth = ColorDepth.Depth32Bit;

            imageList.Images.Add("���", Itop.Client.Resources.Properties.Resources.�½�);
            imageList.Images.Add("�޸�", Itop.Client.Resources.Properties.Resources.�޸�);
            imageList.Images.Add("ɾ��", Itop.Client.Resources.Properties.Resources.ɾ��);
            imageList.Images.Add("��ӡ", Itop.Client.Resources.Properties.Resources.��ӡ);
            imageList.Images.Add("�ر�", Itop.Client.Resources.Properties.Resources.�ر�);
            imageList.Images.Add("����", Itop.Client.Resources.Properties.Resources.����);
            imageList.Images.Add("���", Itop.Client.Resources.Properties.Resources.���);
            imageList.Images.Add("����", Itop.Client.Resources.Properties.Resources.����);
            imageList.Images.Add("����", Itop.Client.Resources.Properties.Resources.����);
            imageList.Images.Add("����", Itop.Client.Resources.Properties.Resources.����);
            imageList.Images.Add("ͬ��", Itop.Client.Resources.Properties.Resources.���ͬ��);
            imageList.Images.Add("�¼�", Itop.Client.Resources.Properties.Resources.����¼�);
            imageList.Images.Add("ˢ��", Itop.Client.Resources.Properties.Resources.ˢ��);
            imageList.Images.Add("��Ȩ", Itop.Client.Resources.Properties.Resources.Ȩ��);
            imageList.Images.Add("��ɫ", Itop.Client.Resources.Properties.Resources.��ɫ);
            imageList.Images.Add("��ѯ", Itop.Client.Resources.Properties.Resources.��ѯ);
            
            return imageList;
        }


        public static Icon GetImage(string name)
        { 
            Icon bm=null;
            switch(name)
            {
                case "���":
                    bm = Itop.Client.Resources.Properties.Resources.�½�;
                    break;
                case "�޸�":
                    bm = Itop.Client.Resources.Properties.Resources.�޸�;
                    break;
                case "ɾ��":
                    bm = Itop.Client.Resources.Properties.Resources.ɾ��;
                    break;
                case "��ӡ":
                    bm = Itop.Client.Resources.Properties.Resources.��ӡ;
                    break;
                case "�ر�":
                    bm = Itop.Client.Resources.Properties.Resources.�ر�;
                    break;
                case "����":
                    bm = Itop.Client.Resources.Properties.Resources.����;
                    break;
                case "���":
                    bm = Itop.Client.Resources.Properties.Resources.���;
                    break;
                case "����":
                    bm = Itop.Client.Resources.Properties.Resources.����;
                    break;
                case "����":
                    bm = Itop.Client.Resources.Properties.Resources.����;
                    break;
                case "����":
                    bm = Itop.Client.Resources.Properties.Resources.����;
                    break;
                case "ͬ��":
                    bm = Itop.Client.Resources.Properties.Resources.���ͬ��;
                    break;
                case "�¼�":
                    bm = Itop.Client.Resources.Properties.Resources.����¼�;
                    break;
                case "ˢ��":
                    bm = Itop.Client.Resources.Properties.Resources.ˢ��;
                    break;
                case "��Ȩ":
                    bm = Itop.Client.Resources.Properties.Resources.��Ȩ;
                    break;
                case "��ɫ":
                    bm = Itop.Client.Resources.Properties.Resources.��ɫ;
                    break;
                case "��ѯ":
                    bm = Itop.Client.Resources.Properties.Resources.��ѯ;
                    break;    
            }

            return bm;
        
        }

        public static Bitmap GetLoginWaitPhoto()
        {
            return Itop.Client.Resources.Properties.Resources.loginwait1;
        }
        public static Bitmap GetAboutPhoto()
        {
            return Itop.Client.Resources.Properties.Resources.about;
        }
        public static Bitmap GetLoginPhoto()
        {
            return Itop.Client.Resources.Properties.Resources.loginbg;       
        }

        public static Bitmap GetBannerPhoto()
        {
            return Itop.Client.Resources.Properties.Resources.maintop1; 
        }

        public static Bitmap GetLeftPhoto()
        {
            return Itop.Client.Resources.Properties.Resources.a1;
        }

        public static Bitmap GetLeft2Photo()
        {
            return Itop.Client.Resources.Properties.Resources.a2;
        }

        public static Bitmap GetBottomPhoto()
        {
            //return Itop.Client.Resources.Properties.Resources.a3;
            return Itop.Client.Resources.Properties.Resources.a5;
        }

        public static Bitmap GetMainPhoto()
        {
            return Itop.Client.Resources.Properties.Resources.a4;
        }

    }
}
