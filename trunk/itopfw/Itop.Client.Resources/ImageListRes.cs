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
            //系统用图片不显示在图标列表中
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

            imageList.Images.Add("添加", Itop.Client.Resources.Properties.Resources.新建);
            imageList.Images.Add("修改", Itop.Client.Resources.Properties.Resources.修改);
            imageList.Images.Add("删除", Itop.Client.Resources.Properties.Resources.删除);
            imageList.Images.Add("打印", Itop.Client.Resources.Properties.Resources.打印);
            imageList.Images.Add("关闭", Itop.Client.Resources.Properties.Resources.关闭);
            imageList.Images.Add("发送", Itop.Client.Resources.Properties.Resources.发送);
            imageList.Images.Add("审核", Itop.Client.Resources.Properties.Resources.审核);
            imageList.Images.Add("审批", Itop.Client.Resources.Properties.Resources.审批);
            imageList.Images.Add("作废", Itop.Client.Resources.Properties.Resources.作废);
            imageList.Images.Add("布局", Itop.Client.Resources.Properties.Resources.布局);
            imageList.Images.Add("同级", Itop.Client.Resources.Properties.Resources.添加同级);
            imageList.Images.Add("下级", Itop.Client.Resources.Properties.Resources.添加下级);
            imageList.Images.Add("刷新", Itop.Client.Resources.Properties.Resources.刷新);
            imageList.Images.Add("授权", Itop.Client.Resources.Properties.Resources.权限);
            imageList.Images.Add("角色", Itop.Client.Resources.Properties.Resources.角色);
            imageList.Images.Add("查询", Itop.Client.Resources.Properties.Resources.查询);
            
            return imageList;
        }


        public static Icon GetImage(string name)
        { 
            Icon bm=null;
            switch(name)
            {
                case "添加":
                    bm = Itop.Client.Resources.Properties.Resources.新建;
                    break;
                case "修改":
                    bm = Itop.Client.Resources.Properties.Resources.修改;
                    break;
                case "删除":
                    bm = Itop.Client.Resources.Properties.Resources.删除;
                    break;
                case "打印":
                    bm = Itop.Client.Resources.Properties.Resources.打印;
                    break;
                case "关闭":
                    bm = Itop.Client.Resources.Properties.Resources.关闭;
                    break;
                case "发送":
                    bm = Itop.Client.Resources.Properties.Resources.发送;
                    break;
                case "审核":
                    bm = Itop.Client.Resources.Properties.Resources.审核;
                    break;
                case "审批":
                    bm = Itop.Client.Resources.Properties.Resources.审批;
                    break;
                case "作废":
                    bm = Itop.Client.Resources.Properties.Resources.作废;
                    break;
                case "布局":
                    bm = Itop.Client.Resources.Properties.Resources.布局;
                    break;
                case "同级":
                    bm = Itop.Client.Resources.Properties.Resources.添加同级;
                    break;
                case "下级":
                    bm = Itop.Client.Resources.Properties.Resources.添加下级;
                    break;
                case "刷新":
                    bm = Itop.Client.Resources.Properties.Resources.刷新;
                    break;
                case "授权":
                    bm = Itop.Client.Resources.Properties.Resources.授权;
                    break;
                case "角色":
                    bm = Itop.Client.Resources.Properties.Resources.角色;
                    break;
                case "查询":
                    bm = Itop.Client.Resources.Properties.Resources.查询;
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
