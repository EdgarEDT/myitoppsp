/***********************************************************************
 * Module:  DomIcon.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.Resource.DomIcon
 ***********************************************************************/

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace ItopVector.Resource
{
   public class DomIcon
   {
      public DomIcon()
      {
      }
      
      public static int GetImageIndexForName(string name)
      {
          if (DomIcon.PropertyDocument != null)
          {
              string text1 = "//*[@name='" + name.ToLower().Trim() + "']";
              XmlNode node1 = DomIcon.PropertyDocument.SelectSingleNode(text1);
              if (node1 is XmlElement)
              {
                  string text2 = ((XmlElement) node1).GetAttribute("imageindex").Trim();
                  int num1 = -1;
                  try
                  {
                      num1 = int.Parse(text2.Trim());
                  }
                  finally
                  {
                  }
                  return num1;
              }
          }
          return -1;
      }
      
      public static void LoadIcon()
      {
      }
   
      /// Methods
      static DomIcon()
      {
          DomIcon.DomIcons = new Hashtable(0x10);
          DomIcon.Images = new ImageList();
          DomIcon.PropertyDocument = new XmlDocument();
          DomIcon.Images = ResourceHelper.LoadBitmapStrip(Type.GetType("ItopVector.Resource.DomIcon"), "ItopVector.Resource.DomIcon.Bitmap1.bmp", new Size(0x10, 0x10), new Point(0, 0));
          Assembly assembly1 = Assembly.GetAssembly(Type.GetType("ItopVector.Resource.DomIcon"));
          string[] textArray1 = assembly1.GetManifestResourceNames();
          string text1 = "ItopVector.Resource.DomIcon.";
          string[] textArray2 = textArray1;
          for (int num1 = 0; num1 < textArray2.Length; num1++)
          {
              string text2 = textArray2[num1];
              if (text2.EndsWith(".ico") && (text2.IndexOf(text1) >= 0))
              {
                  Stream stream1 = assembly1.GetManifestResourceStream(text2);
                  string text3 = text2.Substring(text1.Length, text2.Length - text1.Length);
                  text3 = text3.Substring(0, text3.Length - 4);
                  if (stream1 != null)
                  {
                      Icon icon1 = new Icon(stream1);
                      DomIcon.DomIcons.Add(text3, icon1);
                  }
              }
          }
          assembly1 = Assembly.GetAssembly(Type.GetType("ItopVector.Resource.DomIcon"));
          Stream stream2 = assembly1.GetManifestResourceStream("ItopVector.Resource.DomIcon.domimage.xml");
          if (stream2 != null)
          {
              DomIcon.PropertyDocument.Load(stream2);
          }
      }
   
      /// Fields
      public static readonly Hashtable DomIcons;
      public static readonly ImageList Images;
   
      private static readonly XmlDocument PropertyDocument;
   
      /// Properties
      public static Icon TitleIcon
      {
         get
         {
             Assembly assembly1 = Assembly.GetAssembly(Type.GetType("ItopVector.Resource.DomIcon"));
             Stream stream1 = assembly1.GetManifestResourceStream("ItopVector.Resource.DomIcon.title.ico");
             if (stream1 != null)
             {
                 return new Icon(stream1);
             }
             return null;
         }
      }
   
   }
}