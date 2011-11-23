/***********************************************************************
 * Module:  DrawAreaConfig.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.Resource.DrawAreaConfig
 ***********************************************************************/

using System;
using System.IO;
using System.Xml;

namespace ItopVector.Resource
{
   public class DrawAreaConfig
   {
      public DrawAreaConfig()
      {
      }
      
      public static string GetLabelForName(string name)
      {
          if (DrawAreaConfig.configdocument != null)
          {
              string text1 = "//*[@name='" + name.Trim().ToLower() + "']";
              XmlNode node1 = DrawAreaConfig.configdocument.SelectSingleNode(text1);
              if (node1 is XmlElement)
              {
                  return ((XmlElement) node1).GetAttribute("label");
              }
          }
          return string.Empty;
      }
   
      /// Methods
      static DrawAreaConfig()
      {
          DrawAreaConfig.configdocument = null;
          Stream stream1 = Files.GetFileStream("ItopVector.Resource.DrawArea.drawareaconfig.xml");
          if (stream1 != null)
          {
              DrawAreaConfig.configdocument = new XmlDocument();
              try
              {
                  DrawAreaConfig.configdocument.Load(stream1);
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