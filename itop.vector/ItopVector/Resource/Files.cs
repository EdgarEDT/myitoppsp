/***********************************************************************
 * Module:  Files.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.Resource.Files
 ***********************************************************************/

using System;
using System.IO;
using System.Reflection;

namespace ItopVector.Resource
{
   public class Files
   {
      /// Methods
      public Files()
      {
      }
      
      public static Stream GetFileStream(string filename)
      {
          Assembly assembly1 = Assembly.GetAssembly(Type.GetType("ItopVector.Resource.Files"));
          if (assembly1 != null)
          {
              return assembly1.GetManifestResourceStream(filename);
          }
          return null;
      }
   
   }
}