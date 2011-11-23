/***********************************************************************
 * Module:  ResourceHelper.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.Resource.ResourceHelper
 ***********************************************************************/

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ItopVector.Resource
{
   public class ResourceHelper
   {
      /// Methods
      public ResourceHelper()
      {
      }
      
      public static Bitmap LoadBitmap(Type assemblyType, string imageName)
      {
          return ResourceHelper.LoadBitmap(assemblyType, imageName, false, new Point(0, 0));
      }
      
      public static Bitmap LoadBitmap(Type assemblyType, string imageName, Point transparentPixel)
      {
          return ResourceHelper.LoadBitmap(assemblyType, imageName, true, transparentPixel);
      }
      
      public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize)
      {
          return ResourceHelper.LoadBitmapStrip(assemblyType, imageName, imageSize, false, new Point(0, 0));
      }
      
      public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize, Point transparentPixel)
      {
          return ResourceHelper.LoadBitmapStrip(assemblyType, imageName, imageSize, true, transparentPixel);
      }
      
      public static Cursor LoadCursor(Type assemblyType, string cursorName)
      {
          Assembly assembly1 = Assembly.GetAssembly(assemblyType);
          Stream stream1 = assembly1.GetManifestResourceStream(cursorName);
          if (stream1 == null)
          {
              return Cursors.Default;
          }
          return new Cursor(stream1);
      }
      
      public static Icon LoadIcon(Type assemblyType, string iconName)
      {
          Assembly assembly1 = Assembly.GetAssembly(assemblyType);
          Stream stream1 = assembly1.GetManifestResourceStream(iconName);
          return new Icon(stream1);
      }
      
      public static Icon LoadIcon(Type assemblyType, string iconName, Size iconSize)
      {
          Icon icon1 = ResourceHelper.LoadIcon(assemblyType, iconName);
          return new Icon(icon1, iconSize);
      }
   
      protected static Bitmap LoadBitmap(Type assemblyType, string imageName, bool makeTransparent, Point transparentPixel)
      {
          Assembly assembly1 = Assembly.GetAssembly(assemblyType);
          Stream stream1 = assembly1.GetManifestResourceStream(imageName);
          Bitmap bitmap1 = new Bitmap(stream1);
          if (makeTransparent)
          {
              Color color1 = bitmap1.GetPixel(transparentPixel.X, transparentPixel.Y);
              bitmap1.MakeTransparent(color1);
          }
          return bitmap1;
      }
      
      protected static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize, bool makeTransparent, Point transparentPixel)
      {
          ImageList list1 = new ImageList();
          list1.ImageSize = imageSize;
          Assembly assembly1 = Assembly.GetAssembly(assemblyType);
          Stream stream1 = assembly1.GetManifestResourceStream(imageName);
          if (stream1 == null)
          {
              return null;
          }
          Bitmap bitmap1 = new Bitmap(stream1);
          if (makeTransparent)
          {
              Color color1 = bitmap1.GetPixel(transparentPixel.X, transparentPixel.Y);
              bitmap1.MakeTransparent(color1);
          }
          list1.Images.AddStrip(bitmap1);
          return list1;
      }
   
   }
}