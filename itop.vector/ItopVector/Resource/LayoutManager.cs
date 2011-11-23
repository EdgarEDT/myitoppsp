/***********************************************************************
 * Module:  LayoutManager.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.Resource.LayoutManager
 ***********************************************************************/

using System;
using System.IO;
using System.Xml;

namespace ItopVector.Resource
{
   public class LayoutManager
   {
      public LayoutManager()
      {
      }
      
      public static string GetLabelForName(string name)
      {
          if (LayoutManager.configdocument != null)
          {
              string text1 = "//*[@name='" + name.Trim().ToLower() + "']";
              XmlNode node1 = LayoutManager.configdocument.SelectSingleNode(text1);
              if (node1 is XmlElement)
              {
                  return ((XmlElement) node1).GetAttribute("label");
              }
          }
          return string.Empty;
      }
   
      /// Methods
      static LayoutManager()
      {
          LayoutManager.configdocument = null;
          Stream stream1 = Files.GetFileStream("ItopVector.Resource.LayoutManager.ControlConfig.xml");
          if (stream1 != null)
          {
              LayoutManager.configdocument = new XmlDocument();
              try
              {
                  LayoutManager.configdocument.Load(stream1);
              }
              catch (Exception)
              {
              }
          }
      }
   
      /// Fields
      private static readonly XmlDocument configdocument;
   
   }
}