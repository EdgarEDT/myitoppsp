/***********************************************************************
 * Module:  Preference.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.Resource.Preference
 ***********************************************************************/

using System;
using System.IO;
using System.Xml;

namespace ItopVector.Resource
{
   public class Preference
   {
      public Preference()
      {
      }
      
      public static string GetLabelForName(string name)
      {
          if (Preference.configdocument != null)
          {
              string text1 = "//*[@name='" + name.Trim().ToLower() + "']";
              XmlNode node1 = Preference.configdocument.SelectSingleNode(text1);
              if (node1 is XmlElement)
              {
                  return ((XmlElement) node1).GetAttribute("label");
              }
          }
          return string.Empty;
      }
   
      /// Methods
      static Preference()
      {
          Preference.configdocument = null;
          Stream stream1 = Files.GetFileStream("ItopVector.Resource.Preference.Config.xml");
          if (stream1 != null)
          {
              Preference.configdocument = new XmlDocument();
              try
              {
                  Preference.configdocument.Load(stream1);
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