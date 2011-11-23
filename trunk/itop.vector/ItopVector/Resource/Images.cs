/***********************************************************************
 * Module:  Images.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.Resource.Images
 ***********************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ItopVector.Resource
{
   public class Images
   {
      /// Methods
      public Images()
      {
      }
   
      /// Properties
      public static Image AboutImage
      {
         get
         {
             return ResourceHelper.LoadBitmap(Type.GetType("ItopVector.Resource.Images"), "ItopVector.Resource.Main.bmp");
         }
      }
      
      public static ImageList FolderImages
      {
         get
         {
             Type type1 = Type.GetType("ItopVector.Resource.Images");
             return ResourceHelper.LoadBitmapStrip(type1, "ItopVector.Resource.Folder.bmp", new Size(0x10, 0x10), new Point(0, 0));
         }
      }
      
      public static ImageList MenuImages
      {
         get
         {
             Type type1 = Type.GetType("ItopVector.Resource.Images");
             return ResourceHelper.LoadBitmapStrip(type1, "ItopVector.Resource.MenuImages.bmp", new Size(0x10, 0x10), new Point(0, 0));
         }
      }
      
      public static ImageList PanelImages
      {
         get
         {
             return ResourceHelper.LoadBitmapStrip(Type.GetType("ItopVector.Resource.Images"), "ItopVector.Resource.PanelImages.bmp", new Size(0x10, 0x10), new Point(0, 0));
         }
      }
   
   }
}