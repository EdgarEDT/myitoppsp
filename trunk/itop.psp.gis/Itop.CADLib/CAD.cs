using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Win32;
using Autodesk.AutoCAD;
using Autodesk.AutoCAD.Interop;

using Autodesk.AutoCAD.Interop.Common;

using System.Runtime.InteropServices;


using ItopVector.Core.Figure;
using ItopVector.Core;
using ItopVector.Core.Interface.Figure;
using System.Drawing;
using System.Xml;
using System.Configuration;








namespace Itop.CADLib
{
    public class CAD
    {
      
       
      
        public ItopVector.ItopVectorControl tlVectorControl1;
        FlashWindow f = new FlashWindow();
        string str500kv= ConfigurationSettings.AppSettings.Get("500kv");
        string str220kv=ConfigurationSettings.AppSettings.Get("220kv");
        string str110kv=ConfigurationSettings.AppSettings.Get("110kv");
        string str66kv = ConfigurationSettings.AppSettings.Get("66kv");
        string str33kv = ConfigurationSettings.AppSettings.Get("35kv");
        string CadFullType = ConfigurationSettings.AppSettings.Get("CadFullType");

        int int_cir1 =Convert.ToInt32( ConfigurationSettings.AppSettings.Get("cir1"));
        int int_cir2 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("cir2"));
        int int_cir3 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("cir3"));
        int int_cir4 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("cir4"));
        int int_cir5 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("cir5"));
        int TextSize = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("TextSize"));
        int w_500 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("500Width"));
        int w_220 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("220Width"));
        int w_110 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("110Width"));
        int w_66 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("66Width"));
        int w_35 = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("35Width"));
        int offX = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("offX"));
        int offY = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("offY"));
        int TextLength = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("TextLength"));

       
        public void WriteDwg(string larlist)
        {
            try
            {
                AcadApplication cad;
                f.SetText("处理中请等待......");
                f.Show();

                string[] strlar = larlist.Split(',');
                cad=AutoCADConnector();
                if (cad!=null)
                {
                    
                    //cad.ActiveDocument.Linetypes.Load("DASHEDX2", "acadiso.lin");
                    //cad.ActiveDocument.Linetypes.Load("ACAD_ISO03W100", "acadiso.lin");

                    SvgElementCollection col = new SvgElementCollection();

                    if (larlist == "ALL")
                    {
                        ArrayList Layerlist = tlVectorControl1.SVGDocument.getLayerList();
                        try
                        {

                            for (int i = 0; i < Layerlist.Count; i++)
                            {
                                string str = ((Layer)Layerlist[i]).Label.Replace(",", "");
                                str = str.Replace("(", "");
                                str = str.Replace(")", "");
                                str = str.Replace(" ", "");
                                str = str.Replace("－", "");
                                str = str.Replace("，", "");
                                str = str.Replace("/", "");
                                str = str.Replace("\\", "");
                                if (str == "") { str = ((Layer)Layerlist[i]).ID; }
                                AcadLayer layer = cad.ActiveDocument.Layers.Add(str);
                            }
                        }
                        catch (Exception e1)
                        {

                        }
                        foreach (Layer lar in tlVectorControl1.SVGDocument.Layers)
                        {
                            string str = lar.Label.Replace(",", "");
                            str = str.Replace("(", "");
                            str = str.Replace(")", "");
                            str = str.Replace(" ", "");
                            str = str.Replace("－", "");
                            str = str.Replace("，", "");
                            str = str.Replace("/", "");
                            str = str.Replace("\\", "");
                            if (str == "") { str = lar.ID; }
                            col.AddRange(lar.GraphList);


                            f.SetText("正在处理" + str + "层，请等待......");
                            CallCAD(col, str, Layerlist.Count,cad);
                            col.Clear();
                        }
                    }
                    else
                    {
                        ArrayList Layerlist = tlVectorControl1.SVGDocument.getLayerList();
                        int temp = 0;
                        try
                        {

                            for (int i = 0; i < Layerlist.Count; i++)
                            {
                                for (int j = 0; j < strlar.Length; j++)
                                {
                                    if (strlar[j] == ((Layer)Layerlist[i]).ID)
                                    {
                                        temp = temp + 1;
                                        string str = ((Layer)Layerlist[i]).Label.Replace(",", "");
                                        str = str.Replace("(", "");
                                        str = str.Replace(")", "");
                                        str = str.Replace(" ", "");
                                        str = str.Replace("－", "");
                                        str = str.Replace("，", "");
                                        str = str.Replace("/", "");
                                        str = str.Replace("\\", "");
                                        if (str == "") { str = ((Layer)Layerlist[i]).ID; }
                                        AcadLayer layer = cad.ActiveDocument.Layers.Add(str);
                                        continue;
                                    }
                                }
                            }
                        }
                        catch (Exception e1)
                        {

                        }
                        foreach (Layer lar in tlVectorControl1.SVGDocument.Layers)
                        {
                            for (int n = 0; n < strlar.Length; n++)
                            {
                                if (strlar[n] == lar.ID)
                                {
                                    string str = lar.Label.Replace(",", "");
                                    str = str.Replace("(", "");
                                    str = str.Replace(")", "");
                                    str = str.Replace(" ", "");
                                    str = str.Replace("－", "");
                                    str = str.Replace("，", "");
                                    str = str.Replace("/", "");
                                    str = str.Replace("\\", "");
                                    if (str == "") { str = lar.ID; }
                                    col.AddRange(lar.GraphList);
                                    f.SetText("正在处理" + str + "层，请等待......");
                                    CallCAD(col, str, temp,cad);
                                    col.Clear();
                                    continue;
                                }
                            }

                        }
                    }
                    short[] filtertype = new short[4];
                    filtertype[0] = -4;
                    filtertype[1] = 0;
                    filtertype[2] = 62;
                    filtertype[3] = -4;
                    object[] filterDate = new object[4];
                    filterDate[0] = "<AND";
                    filterDate[1] = "HATCH";
                    filterDate[2] = "7";
                    filterDate[3] = "AND>";



                    cad.Visible = true;
                    cad.ActiveDocument.SendCommand("Z e ");
                    cad.ActiveDocument.SendCommand("audit y ");
                    //AcadSelectionSet s = cad.ActiveDocument.SelectionSets.Add("HatchSel");
                    //s.SelectOnScreen((object)filtertype, (object)filterDate);
                    //cad.Application.ActiveDocument.
                    //int kk = s.Count;
                    //cad.ActiveDocument.SendCommand("all ");
                    //cad.ActiveDocument.SendCommand("draworder b ");
                    f.Close();
                }
                else
                {
                    f.Close();
                }
            }
            catch(Exception e2)
            {
                MessageBox.Show("请安装AutoCAD2006或以上版本。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
        }
        public AcadApplication AutoCADConnector()
        {
            AcadApplication cad;
            try
            {
               
                // Upon creation, attempt to retrieve running instance
                cad = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application.18");
                cad.Visible = false;
            }
            catch
            {
                try
                {
                    // Create an instance and set flag to indicate this
                    cad = new AcadApplicationClass();
                    cad.Visible = false;
                    return cad;
                }
                catch (Exception e2)
                {
                    MessageBox.Show("请安装AutoCAD2006或以上版本。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return null;
                }
            }
            return cad;
        }
        public void CallCAD(SvgElementCollection _col, string lab, int count, AcadApplication cad)
        {
            int errid = 0;

            AcadEntity[] en = new AcadEntity[2];
            AcadHatch wt = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid", true, 0);
            wt.color = ACAD_COLOR.acWhite;
            AcadHatch h;
            AcadHatch h2;
            AcadHatch h3;
            AcadHatch h4;
            AcadHatch h5;
           
            //h = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid",/* "JIS_LC_20",*/ true, 0);
            if (CadFullType == "1")
            {
                h = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid", true, 0);
                h2 = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid", true, 0);
                h3 = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid", true, 0);
                h4 = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid", true, 0);
                h5 = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid", true, 0);
            }
            else
            {
                h = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);//JIS_LC_20 //JIS_LC_20
                h2 = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
                h3 = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
                h4 = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
                h5 = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
            }
           
            if (CadFullType == "2")
            {
                h.PatternScale = 0.25;
                h.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
            }
            Color c_500 = ColorTranslator.FromHtml(str500kv);
            AcadAcCmColor color500 = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18"); //2006 -"AutoCAD.AcCmColor.16"
            color500.SetRGB(c_500.R, c_500.G, c_500.B);
            h.TrueColor = color500;

            if (CadFullType == "2")
            {
                h2.PatternScale = 0.25;
                h2.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
            }
            Color c_220 = ColorTranslator.FromHtml(str220kv);
            AcadAcCmColor color220 = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18");
            color220.SetRGB(c_220.R, c_220.G, c_220.B);
            h2.TrueColor = color220;

            if (CadFullType == "2")
            {
                h3.PatternScale = 0.25;
                h3.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
            }
            Color c_110 = ColorTranslator.FromHtml(str110kv);
            AcadAcCmColor color110 = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18");
            color110.SetRGB(c_110.R, c_110.G, c_110.B);
            h3.TrueColor = color110;

            if (CadFullType == "2")
            {
                h4.PatternScale = 0.25;
                h4.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
            }
            Color c_66 = ColorTranslator.FromHtml(str66kv);
            AcadAcCmColor color66 = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18");
            color66.SetRGB(c_66.R, c_66.G, c_66.B);
            h4.TrueColor = color66;

            if (CadFullType == "2")
            {
                h5.PatternScale = 0.25;
                h5.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
            }
            Color c_33 = ColorTranslator.FromHtml(str33kv);
            AcadAcCmColor color33 = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18");
            color33.SetRGB(c_33.R, c_33.G, c_33.B);
            h5.TrueColor = color33;

          

         

            for (int i = 0; i < _col.Count; i++)
            {
                SvgElement ele = (SvgElement)_col[i];
                f.SetText("共有" + count + "个图层。正在处理" + lab + "图层。\r\n 此图层共" + _col.Count + "个图元，正在处理第" + i + "个，请等待......");
                if (ele.Name == "rect")
                {
                    try
                    {
                        RectangleElement rect = ele as RectangleElement;
                        PointF[] points = new PointF[1];
                        points[0].X = rect.X;
                        points[0].Y = rect.Y;
                        rect.Transform.Matrix.TransformPoints(points);
                        double[] col = new double[15];
                        col[0] = points[0].X;
                        col[1] = -points[0].Y;
                        col[2] = 0;

                        col[3] = rect.X;
                        col[4] = -(rect.Y + rect.Height);
                        col[5] = 0;

                        col[6] = rect.X + rect.Width;
                        col[7] = -(rect.Y + rect.Height);
                        col[8] = 0;

                        col[9] = rect.X + rect.Width;
                        col[10] = -rect.Y;
                        col[11] = 0;

                        col[12] = rect.X;
                        col[13] = -rect.Y;
                        col[14] = 0;

                        AcadPolyline pl = cad.ActiveDocument.ModelSpace.AddPolyline(col);
                        string color_str = rect.GetAttribute("style");
                        int sub_i = color_str.IndexOf("stroke:");
                        if (sub_i == -1)
                        {
                            color_str = "#000000";
                        }
                        else
                        {
                            color_str = color_str.Substring(sub_i + 7, 7);
                           // color_str = color_str.Replace("000000", "FFFFFF");
                        }
                        Color c1 = ColorTranslator.FromHtml(color_str);
                        AcadAcCmColor color = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18");
                        color.SetRGB(c1.R, c1.G, c1.B);
                        pl.TrueColor = color;

                        pl.Layer = lab;
                        cad.Application.Update();
                    }
                    catch { }
                }
                if (ele.Name == "ellipse")
                {
                    //Ellips eli = ele as Ellips;

                    //DxfEllipse eli = new DxfEllipse(new Point3D(eli.RX, eli.RY, 0), new Vector3D(1, 1, 0), 1);
                    //model.Entities.Add(eli);
                }
                if (ele.Name == "polyline")
                {
                    try
                    {
                        Polyline p1 = ele as Polyline;
                        PointF[] points = p1.Points;
                        if (points.Length > 1)
                        {
                            p1.Transform.Matrix.TransformPoints(points);
                            double[] col = new double[points.Length * 3];
                            int k = 0;
                            for (int n = 0; n < points.Length; n++)
                            {
                                col[k] = points[n].X;
                                col[k + 1] = -points[n].Y;
                                col[k + 2] = 0;
                                k = k + 3;
                            }
                            string color_str = p1.GetAttribute("style");
                            int gh_line = color_str.IndexOf("stroke-dasharray:");
                            int sub_i = color_str.IndexOf("stroke:");
                            if (sub_i == -1)
                            {
                                //color_str = "#FFFFF";
                                color_str = "#000000";
                            }
                            else
                            {
                                color_str = color_str.Substring(sub_i + 7, 7);
                                //color_str = color_str.Replace("000000", "FFFFFF");
                            }
                            Color c1 = ColorTranslator.FromHtml(color_str);
                            AcadPolyline ple = cad.ActiveDocument.ModelSpace.AddPolyline(col);

                            Layer temp_l = tlVectorControl1.SVGDocument.GetLayerByID(ele.GetAttribute("layer"),false);
                            if (temp_l != null)
                            {
                                if (temp_l.Label.ToLower().Contains("500kv"))
                                {
                                    ple.ConstantWidth = w_500;
                                }
                                if (temp_l.Label.ToLower().Contains("220kv"))
                                {
                                    ple.ConstantWidth = w_220;
                                }
                                if (temp_l.Label.ToLower().Contains("110kv"))
                                {
                                    ple.ConstantWidth = w_110;
                                }
                                if (temp_l.Label.ToLower().Contains("66kv"))
                                {
                                    ple.ConstantWidth = w_66;
                                }
                                if (temp_l.Label.ToLower().Contains("35kv"))
                                {
                                    ple.ConstantWidth = w_35;
                                }
                            }
                            if (gh_line != -1)
                            {
                                ple.Linetype = "DASHEDX2";
                            }

                            ple.Layer = lab;

                            AcadAcCmColor color = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18");
                            color.SetRGB(c1.R, c1.G, c1.B);
                            ple.TrueColor = color;//ACAD_COLOR.acMagenta;

                            cad.Application.Update();
                        }
                    }
                    catch { }
                }
                if (ele.Name == "polygon")
                {
                    try
                    {
                        Polygon p1 = ele as Polygon;
                        PointF[] points = p1.Points;
                        if (points.Length < 2) return;
                        p1.Transform.Matrix.TransformPoints(points);

                        double[] col = new double[points.Length * 3 + 3];
                        int k = 0;
                        for (int n = 0; n < points.Length; n++)
                        {
                            col[k] = points[n].X;
                            col[k + 1] = -points[n].Y;
                            col[k + 2] = 0;
                            k = k + 3;
                        }
                        col[points.Length * 3 + 2] = 0;
                        col[points.Length * 3 + 1] = col[1];
                        col[points.Length * 3] = col[0];
                        AcadPolyline poly = cad.ActiveDocument.ModelSpace.AddPolyline(col);
                        string IsArea = p1.GetAttribute("IsArea");
                        string color_str = p1.GetAttribute("style");
                        int sub_i = color_str.IndexOf("stroke:");
                        int sub_fill = color_str.IndexOf("fill:#");



                        if (sub_i == -1 && sub_fill == -1)
                        {
                            //color_str = "#FFFFF";
                            color_str = "#000000";
                        }
                        else
                        {
                            if (IsArea == "1")
                            {

                                if (sub_fill == -1)
                                {
                                    color_str = color_str.Substring(sub_i + 7, 7);
                                   // color_str = color_str.Replace("000000", "FFFFFF");
                                }
                                else
                                {
                                    color_str = color_str.Substring(sub_fill + 5, 7);
                                    //color_str = color_str.Replace("000000", "FFFFFF");

                                }

                            }
                            else
                            {
                                color_str = color_str.Substring(sub_i + 7, 7);
                                //color_str = color_str.Replace("000000", "FFFFFF");
                            }
                        }
                        Color c1 = ColorTranslator.FromHtml(color_str);
                        AcadAcCmColor color = (AcadAcCmColor)cad.ActiveDocument.Application.GetInterfaceObject("AutoCAD.AcCmColor.18");
                        color.SetRGB(c1.R, c1.G, c1.B);
                        if (IsArea == "1")
                        {
                            AcadHatch solid = cad.ActiveDocument.ModelSpace.AddHatch(0, "solid", true, 0);
                            solid.TrueColor = color;
                            AcadEntity[] obj = new AcadEntity[1];
                            obj[0] = (AcadEntity)poly;
                            solid.AppendOuterLoop(obj);
                            solid.Evaluate();
                            solid.Layer = lab;
                        }
                        poly.TrueColor = color;
                        poly.Layer = lab;
                        cad.Application.Update();
                    }
                    catch { }
                }
                if (ele.Name == "text")
                {
                    try
                    {
                        ItopVector.Core.Figure.Text t1 = ele as ItopVector.Core.Figure.Text;
                        PointF[] points = new PointF[1];
                        points[0].X = t1.X;
                        points[0].Y = t1.Y;
                        t1.Transform.Matrix.TransformPoints(points);

                        double[] ins = new double[3];
                        ins[0] = points[0].X+offX;
                        ins[1] =- points[0].Y+offY;
                        ins[2] = 0;
                        AcadMText text = cad.ActiveDocument.ModelSpace.AddMText(ins, TextLength, t1.TextString);
                        text.Height = TextSize;
                        text.Layer = lab;
                        cad.Application.Update();
                    }
                    catch { }
                }
                if (ele.Name == "use")
                {
                    try
                    {
                        PointF[] cirp = new PointF[1];
                        Use u = ele as Use;
                        PointF[] points = new PointF[1];
                        points[0].X = u.X;
                        points[0].Y = u.Y;
                    
                        XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/defs/symbol [@id='" + u.GraphId + "']");
                        XmlElement e = (XmlElement)list[0];
                        XmlNodeList sonlist = e.ChildNodes;
                        for (int k = 0; k < sonlist.Count; k++)
                        {
                            string str1 = sonlist[k].GetType().ToString();
                            if (str1 == "ItopVector.Core.Figure.Circle")
                            {
                                Circle cir = (Circle)sonlist[k];
                                cirp[0].X = cir.CX;
                                cirp[0].Y = cir.CY;
                                cir.Transform.Matrix.TransformPoints(cirp);
                                break;
                            }
                            if (str1 == "ItopVector.Core.Figure.RectangleElement")
                            {
                                RectangleElement cir = (RectangleElement)sonlist[k];
                                cirp[0].X = cir.X;
                                cirp[0].Y = cir.Y;
                                cir.Transform.Matrix.TransformPoints(cirp);
                                break;
                            }
                        }

                        PointF p = cirp[0];
                        points[0].X = points[0].X + p.X;
                        points[0].Y = points[0].Y + p.Y;

                        u.Transform.Matrix.TransformPoints(points);

                        if (u.GraphId.Contains("Substation500"))
                        {

                            double[] pnt = new double[3];
                            pnt[0] = points[0].X;
                            pnt[1] = -points[0].Y;
                            pnt[2] = 0;
                            AcadCircle cirz = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir1);
                            cirz.Layer = lab;
                            cirz.TrueColor = color500;
                            if (CadFullType == "2")
                            {
                                AcadEntity[] otw = new AcadEntity[1];
                                otw[0] = (AcadEntity)cirz;
                                wt.AppendOuterLoop(otw);
                                wt.Layer = lab;
                            }
                            AcadCircle cir = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir2);
                            cir.Layer = lab;
                            cir.TrueColor = color500;
                            

                            AcadCircle cir1 = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir3);
                            cir1.TrueColor = color500;
                            cir1.Layer = lab;
                            if (CadFullType == "2")
                            {
                                AcadEntity[] ot = new AcadEntity[1];
                                ot[0] = (AcadEntity)cir1;
                                h.AppendOuterLoop(ot);

                                if (u.GraphId.Contains("gh"))
                                {
                                    cir.Linetype = "ACAD_ISO03W100";
                                    cir1.Linetype = "ACAD_ISO03W100";
                                }
                            }
                            if(CadFullType=="1"){
                                if (!u.GraphId.Contains("gh") && !u.GraphId.Contains("user"))
                                {
                                    AcadEntity[] ot = new AcadEntity[1];
                                    ot[0] = (AcadEntity)cir1;
                                    h.AppendOuterLoop(ot);
                                }
                            }
                         
                         
                        }
                        if (u.GraphId.Contains("Substation220"))
                        {

                            double[] pnt = new double[3];
                            pnt[0] = points[0].X;
                            pnt[1] = -points[0].Y;
                            pnt[2] = 0;
                            AcadCircle cir = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir2);
                            cir.TrueColor = color220;
                            cir.Layer = lab;

                            if (CadFullType == "2")
                            {
                                AcadEntity[] otw = new AcadEntity[1];
                                otw[0] = (AcadEntity)cir;
                                wt.AppendOuterLoop(otw);
                                wt.Layer = lab;
                            }
                            AcadCircle cir1 = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir3);
                            cir1.Layer = lab;
                            cir1.TrueColor = color220;

                            if (CadFullType == "2")
                            {
                                if (u.GraphId.Contains("gh"))
                                {
                                    cir.Linetype = "ACAD_ISO03W100";
                                    cir1.Linetype = "ACAD_ISO03W100";
                                }
                               

                                AcadEntity[] ot = new AcadEntity[1];
                                ot[0] = (AcadEntity)cir1;
                                h2.AppendOuterLoop(ot);
                            }
                            if (CadFullType == "1")
                            {
                                if (!u.GraphId.Contains("gh") && !u.GraphId.Contains("user"))
                                {
                                    AcadEntity[] ot = new AcadEntity[1];
                                    ot[0] = (AcadEntity)cir1;
                                    h2.AppendOuterLoop(ot);
                                }
                                if (u.GraphId.Contains("gh-user"))
                                {
                                    double[] stat1 = new double[3];
                                    stat1[0] = pnt[0] - int_cir3 / 2 - int_cir3 / 10;
                                    stat1[1] = pnt[1] - int_cir3 / 2 - int_cir3 / 10;
                                    stat1[2] = 0;
                                    double[] end1 = new double[3];
                                    end1[0] = pnt[0] + int_cir3 / 2 + int_cir3 / 10;
                                    end1[1] = pnt[1] + int_cir3 / 2 + int_cir3 / 10;
                                    end1[2] = 0;
                                    AcadLine line1 = cad.ActiveDocument.ModelSpace.AddLine(stat1, end1);
                                    line1.Layer = lab;
                                    line1.TrueColor = color220;
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir3 / 2 - int_cir3 / 10;
                                    stat2[1] = pnt[1] + int_cir3 / 2 + int_cir3 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir3 / 2 + int_cir3 / 10;
                                    end2[1] = pnt[1] - int_cir3 / 2 - int_cir3 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color220;
                                }
                                if (u.GraphId.Contains("user"))
                                {
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir3 / 2 - int_cir3 / 10;
                                    stat2[1] = pnt[1] + int_cir3 / 2 + int_cir3 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir3 / 2 + int_cir3 / 10;
                                    end2[1] = pnt[1] - int_cir3 / 2 - int_cir3 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color220;
                                }
                            }
                        }
                        if (u.GraphId.Contains("Substation110"))
                        {

                            double[] pnt = new double[3];
                            pnt[0] = points[0].X;
                            pnt[1] = -points[0].Y;
                            pnt[2] = 0;
                            AcadCircle cir = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir3);
                            cir.Layer = lab;
                            cir.TrueColor = color110;
                            if (CadFullType == "2")
                            {
                                AcadEntity[] otw = new AcadEntity[1];
                                otw[0] = (AcadEntity)cir;
                                wt.AppendOuterLoop(otw);
                                wt.Layer = lab;
                            }
                            AcadCircle cir1 = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir4);
                            cir1.Layer = lab;
                            cir1.TrueColor = color110;

                            if (CadFullType == "2")
                            {
                                if (u.GraphId.Contains("gh"))
                                {
                                    cir.Linetype = "ACAD_ISO03W100";
                                    cir1.Linetype = "ACAD_ISO03W100";
                                }

                                AcadEntity[] ot = new AcadEntity[1];
                                ot[0] = (AcadEntity)cir1;
                                h3.AppendOuterLoop(ot);
                            }
                            if (CadFullType == "1")
                            {
                                if (!u.GraphId.Contains("gh") && !u.GraphId.Contains("user"))
                                {
                                    AcadEntity[] ot = new AcadEntity[1];
                                    ot[0] = (AcadEntity)cir1;
                                    h3.AppendOuterLoop(ot);
                                }
                                if (u.GraphId.Contains("gh-user"))
                                {
                                    double[] stat1 = new double[3];
                                    stat1[0] = pnt[0] - int_cir4 / 2 - int_cir4 / 10;
                                    stat1[1] = pnt[1] - int_cir4 / 2 - int_cir4 / 10;
                                    stat1[2] = 0;
                                    double[] end1 = new double[3];
                                    end1[0] = pnt[0] + int_cir4 / 2 + int_cir4 / 10;
                                    end1[1] = pnt[1] + int_cir4 / 2 + int_cir4 / 10;
                                    end1[2] = 0;
                                    AcadLine line1 = cad.ActiveDocument.ModelSpace.AddLine(stat1, end1);
                                    line1.Layer = lab;
                                    line1.TrueColor = color110;
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir4 / 2 - int_cir4 / 10;
                                    stat2[1] = pnt[1] + int_cir4 / 2 + int_cir4 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir4 / 2 + int_cir4 / 10;
                                    end2[1] = pnt[1] - int_cir4 / 2 - int_cir4 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color110;
                                }
                                if (u.GraphId.Contains("user"))
                                {
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir4 / 2 - int_cir4 / 10;
                                    stat2[1] = pnt[1] + int_cir4 / 2 + int_cir4 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir4 / 2 + int_cir4 / 10;
                                    end2[1] = pnt[1] - int_cir4 / 2 - int_cir4 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color110;
                                }
                            }
                        }
                        if (u.GraphId.Contains("Substation66"))
                        {

                            double[] pnt = new double[3];
                            pnt[0] = points[0].X;
                            pnt[1] = -points[0].Y;
                            pnt[2] = 0;

                            AcadCircle cir = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir4);
                            cir.Layer = lab;
                            cir.TrueColor = color66;
                            if (CadFullType == "2")
                            {
                                AcadEntity[] otw = new AcadEntity[1];
                                otw[0] = (AcadEntity)cir;
                                wt.AppendOuterLoop(otw);
                                wt.Layer = lab;
                            }
                            AcadCircle cir1 = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir5);
                            cir1.Layer = lab;
                            cir1.TrueColor = color66;

                            if (CadFullType == "2")
                            {
                                if (u.GraphId.Contains("gh"))
                                {
                                    cir.Linetype = "ACAD_ISO03W100";
                                    cir1.Linetype = "ACAD_ISO03W100";
                                }

                                AcadEntity[] ot = new AcadEntity[1];
                                ot[0] = (AcadEntity)cir1;
                                h4.AppendOuterLoop(ot);
                                h4.Evaluate();
                            }
                            if (CadFullType == "1")
                            {
                                if (!u.GraphId.Contains("gh") && !u.GraphId.Contains("user"))
                                {
                                    AcadEntity[] ot = new AcadEntity[1];
                                    ot[0] = (AcadEntity)cir1;
                                    h4.AppendOuterLoop(ot);
                                }
                                if (u.GraphId.Contains("gh-user"))
                                {
                                    double[] stat1 = new double[3];
                                    stat1[0] = pnt[0] - int_cir5 / 2 - int_cir5/10;
                                    stat1[1] = pnt[1] - int_cir5 / 2 - int_cir5 / 10;
                                    stat1[2] = 0;
                                    double[] end1 = new double[3];
                                    end1[0] = pnt[0] + int_cir5 / 2 + int_cir5 / 10;
                                    end1[1] = pnt[1] + int_cir5 / 2 + int_cir5 / 10;
                                    end1[2] = 0;
                                    AcadLine line1 = cad.ActiveDocument.ModelSpace.AddLine(stat1, end1);
                                    line1.Layer = lab;
                                    line1.TrueColor = color66;
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir5 / 2 - int_cir5 / 10;
                                    stat2[1] = pnt[1] + int_cir5 / 2 + int_cir5 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir5 / 2 + int_cir5 / 10;
                                    end2[1] = pnt[1] - int_cir5 / 2 - int_cir5 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color66;
                                }
                                if (u.GraphId.Contains("user"))
                                {
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir5 / 2 - int_cir5 / 10;
                                    stat2[1] = pnt[1] + int_cir5 / 2 + int_cir5 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir5 / 2 + int_cir5 / 10;
                                    end2[1] = pnt[1] - int_cir5 / 2 - int_cir5 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color66;
                                }
                            }
                        }
                        if (u.GraphId.Contains("Substation35"))
                        {

                            double[] pnt = new double[3];
                            pnt[0] = points[0].X;
                            pnt[1] = -points[0].Y;
                            pnt[2] = 0;

                            AcadCircle cir = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir4);
                            cir.Layer = lab;
                            cir.TrueColor = color33;

                            if (CadFullType == "2")
                            {
                                AcadEntity[] otw = new AcadEntity[1];
                                otw[0] = (AcadEntity)cir;
                                wt.AppendOuterLoop(otw);
                                wt.Layer = lab;
                            }
                            AcadCircle cir1 = cad.ActiveDocument.ModelSpace.AddCircle(pnt, int_cir5);
                            cir1.Layer = lab;
                            cir1.TrueColor = color33;

                            if (CadFullType == "2")
                            {
                                if (u.GraphId.Contains("gh"))
                                {
                                    cir.Linetype = "ACAD_ISO03W100";
                                    cir1.Linetype = "ACAD_ISO03W100";
                                }
                            
                                AcadEntity[] ot = new AcadEntity[1];
                                ot[0] = (AcadEntity)cir1;
                                h5.AppendOuterLoop(ot);
                            }
                            if (CadFullType == "1")
                            {
                                if (!u.GraphId.Contains("gh") && !u.GraphId.Contains("user"))
                                {
                                    AcadEntity[] ot = new AcadEntity[1];
                                    ot[0] = (AcadEntity)cir1;
                                    h5.AppendOuterLoop(ot);
                                }
                                if (u.GraphId.Contains("gh-user"))
                                {
                                    double[] stat1 = new double[3];
                                    stat1[0] = pnt[0] - int_cir5 / 2 - int_cir5 / 10;
                                    stat1[1] = pnt[1] - int_cir5 / 2 - int_cir5 / 10;
                                    stat1[2] = 0;
                                    double[] end1 = new double[3];
                                    end1[0] = pnt[0] + int_cir5 / 2 + int_cir5 / 10;
                                    end1[1] = pnt[1] + int_cir5 / 2 + int_cir5 / 10;
                                    end1[2] = 0;
                                    AcadLine line1 = cad.ActiveDocument.ModelSpace.AddLine(stat1, end1);
                                    line1.Layer = lab;
                                    line1.TrueColor = color33;
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir5 / 2 - int_cir5 / 10;
                                    stat2[1] = pnt[1] + int_cir5 / 2 + int_cir5 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir5 / 2 + int_cir5 / 10;
                                    end2[1] = pnt[1] - int_cir5 / 2 - int_cir5 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color33;
                                }
                                if (u.GraphId.Contains("user"))
                                {
                                    double[] stat2 = new double[3];
                                    stat2[0] = pnt[0] - int_cir5 / 2 - int_cir5 / 10;
                                    stat2[1] = pnt[1] + int_cir5 / 2 + int_cir5 / 10;
                                    stat2[2] = 0;
                                    double[] end2 = new double[3];
                                    end2[0] = pnt[0] + int_cir5 / 2 + int_cir5 / 10;
                                    end2[1] = pnt[1] - int_cir5 / 2 - int_cir5 / 10;
                                    end2[2] = 0;
                                    AcadLine line2 = cad.ActiveDocument.ModelSpace.AddLine(stat2, end2);
                                    line2.Layer = lab;
                                    line2.TrueColor = color33;
                                }
                            }
                            //h3.Evaluate();
                        }
                        if (u.GraphId.Contains("Substation-hdc") )
                        {
                            double[] col = new double[15];
                            col[0] = points[0].X;
                            col[1] = -points[0].Y;
                            col[2] = 0;

                            col[3] = points[0].X;
                            col[4] = -(points[0].Y + 60);
                            col[5] = 0;

                            col[6] = points[0].X + 180;
                            col[7] = -(points[0].Y + 60);
                            col[8] = 0;

                            col[9] = points[0].X + 180;
                            col[10] = -points[0].Y;
                            col[11] = 0;

                            col[12] = points[0].X;
                            col[13] = -points[0].Y;
                            col[14] = 0;

                            AcadPolyline pl = cad.ActiveDocument.ModelSpace.AddPolyline(col);

                            if (u.GraphId.Contains("gh")) { }
                            else
                            {
                                AcadHatch solid = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0); //JIS_LC_20//JIS_LC_20
                                solid.PatternScale = 0.25;
                                solid.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
                                solid.color = ACAD_COLOR.acRed;
                                AcadEntity[] obj = new AcadEntity[1];
                                obj[0] = (AcadEntity)pl;
                                solid.AppendOuterLoop(obj);
                                solid.Evaluate();
                                solid.Layer = lab;
                            }
                            pl.Layer = lab;
                            pl.color = ACAD_COLOR.acRed;

                            double[] col2 = new double[15];
                            col2[0] = points[0].X;
                            col2[1] = -points[0].Y+60;
                            col2[2] = 0;

                            col2[3] = points[0].X;
                            col2[4] = -points[0].Y ;
                            col2[5] = 0;

                            col2[6] = points[0].X + 180;
                            col2[7] = -points[0].Y;
                            col2[8] = 0;

                            col2[9] = points[0].X + 180;
                            col2[10] = -points[0].Y+60;
                            col2[11] = 0;

                            col2[12] = points[0].X;
                            col2[13] = -points[0].Y+60;
                            col2[14] = 0;
                            AcadPolyline pl2 = cad.ActiveDocument.ModelSpace.AddPolyline(col2);
                            pl2.Layer = lab;
                            pl2.color = ACAD_COLOR.acRed;
                            //AcadEntity[] otw = new AcadEntity[1];
                            //otw[0] = (AcadEntity)pl2;
                            //wt.AppendOuterLoop(otw);
                            //wt.Layer = lab;
                        }
                        if ( u.GraphId.Contains("Substation-rdc"))
                        {
                            double[] col = new double[15];
                            col[0] = points[0].X;
                            col[1] = -points[0].Y;
                            col[2] = 0;

                            col[3] = points[0].X;
                            col[4] = -(points[0].Y + 120);
                            col[5] = 0;

                            col[6] = points[0].X + 90;
                            col[7] = -(points[0].Y + 120);
                            col[8] = 0;

                            col[9] = points[0].X + 90;
                            col[10] = -points[0].Y;
                            col[11] = 0;

                            col[12] = points[0].X;
                            col[13] = -points[0].Y;
                            col[14] = 0;

                            AcadPolyline pl = cad.ActiveDocument.ModelSpace.AddPolyline(col);
                            if (u.GraphId.Contains("gh")) { }
                            else
                            {
                                AcadHatch solid = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
                                solid.PatternScale = 0.25;
                                solid.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
                                solid.color = ACAD_COLOR.acWhite;
                                AcadEntity[] obj = new AcadEntity[1];
                                obj[0] = (AcadEntity)pl;
                                solid.AppendOuterLoop(obj);
                                solid.Evaluate();
                                solid.Layer = lab;
                            }

                            pl.Layer = lab;

                            double[] col2 = new double[15];
                            col2[0] = points[0].X+90;
                            col2[1] = -points[0].Y;
                            col2[2] = 0;

                            col2[3] = points[0].X+90;
                            col2[4] = -(points[0].Y + 120);
                            col2[5] = 0;

                            col2[6] = points[0].X + 90+90;
                            col2[7] = -(points[0].Y + 120);
                            col2[8] = 0;

                            col2[9] = points[0].X + 90+90;
                            col2[10] = -points[0].Y;
                            col2[11] = 0;

                            col2[12] = points[0].X+90;
                            col2[13] = -points[0].Y;
                            col2[14] = 0;
                            AcadPolyline pl2 = cad.ActiveDocument.ModelSpace.AddPolyline(col2);
                            pl2.Layer = lab;
                            AcadEntity[] otw = new AcadEntity[1];
                            otw[0] = (AcadEntity)pl2;
                            wt.AppendOuterLoop(otw);
                            wt.Layer = lab;
                            
                        }
                        if (u.GraphId.Contains("水电站"))
                        {

                            double[] col2 = new double[12];
                            col2[0] = points[0].X;
                            col2[1] = -points[0].Y;
                            col2[2] = 0;

                            col2[3] = points[0].X;
                            col2[4] = -points[0].Y + 120;
                            col2[5] = 0;

                            col2[6] = points[0].X + 180;
                            col2[7] = -(points[0].Y) + 120;
                            col2[8] = 0;

                            col2[9] = points[0].X;
                            col2[10] = -points[0].Y;
                            col2[11] = 0;
                          
                            AcadPolyline pl = cad.ActiveDocument.ModelSpace.AddPolyline(col2);
                            AcadHatch solid = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
                            solid.PatternScale = 0.25;
                            solid.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
                            solid.color = ACAD_COLOR.acWhite;
                            AcadEntity[] obj = new AcadEntity[1];
                            obj[0] = (AcadEntity)pl;
                            solid.AppendOuterLoop(obj);
                            solid.Evaluate();
                            solid.Layer = lab;
                            pl.Layer = lab;

                            double[] col = new double[12];
                            col[0] = points[0].X;
                            col[1] = -points[0].Y;
                            col[2] = 0;

                            col[3] = points[0].X + 180;
                            col[4] = -points[0].Y;
                            col[5] = 0;

                            col[6] = points[0].X + 180;
                            col[7] = -(points[0].Y) + 120;
                            col[8] = 0;

                            col[9] = points[0].X;
                            col[10] = -points[0].Y;
                            col[11] = 0;

                            AcadPolyline pl2 = cad.ActiveDocument.ModelSpace.AddPolyline(col);
                            pl2.Layer = lab;
                            AcadEntity[] otw = new AcadEntity[1];
                            otw[0] = (AcadEntity)pl2;
                            wt.AppendOuterLoop(otw);
                            wt.Layer = lab;
                        }
                        if (u.GraphId.Contains("gh-Substation-sdz"))
                        {

                            double[] col2 = new double[12];
                            col2[0] = points[0].X;
                            col2[1] = -points[0].Y;
                            col2[2] = 0;

                            col2[3] = points[0].X;
                            col2[4] = -points[0].Y + 120;
                            col2[5] = 0;

                            col2[6] = points[0].X + 180;
                            col2[7] = -(points[0].Y) + 120;
                            col2[8] = 0;

                            col2[9] = points[0].X;
                            col2[10] = -points[0].Y;
                            col2[11] = 0;
                            AcadPolyline pl = cad.ActiveDocument.ModelSpace.AddPolyline(col2);
                            pl.Layer = lab;

                            double[] col = new double[12];
                            col[0] = points[0].X;
                            col[1] = -points[0].Y;
                            col[2] = 0;

                            col[3] = points[0].X + 180;
                            col[4] = -points[0].Y;
                            col[5] = 0;

                            col[6] = points[0].X + 180;
                            col[7] = -(points[0].Y) + 120;
                            col[8] = 0;

                            col[9] = points[0].X;
                            col[10] = -points[0].Y;
                            col[11] = 0;

                            AcadPolyline pl2 = cad.ActiveDocument.ModelSpace.AddPolyline(col);
                            pl2.Layer = lab;
                            AcadEntity[] otw = new AcadEntity[1];
                            otw[0] = (AcadEntity)pl2;
                            wt.AppendOuterLoop(otw);
                            wt.Layer = lab;
                        }
                        if (u.GraphId.Contains("Substation-csxn"))
                        {
                            double[] col = new double[15];
                            col[0] = points[0].X;
                            col[1] = -points[0].Y;
                            col[2] = 0;

                            col[3] = points[0].X;
                            col[4] = -(points[0].Y + 120);
                            col[5] = 0;

                            col[6] = points[0].X + 180;
                            col[7] = -(points[0].Y + 120);
                            col[8] = 0;

                            col[9] = points[0].X + 180;
                            col[10] = -points[0].Y;
                            col[11] = 0;

                            col[12] = points[0].X;
                            col[13] = -points[0].Y;
                            col[14] = 0;

                            AcadPolyline pl = cad.ActiveDocument.ModelSpace.AddPolyline(col);
                            //AcadEntity[] otw = new AcadEntity[1];
                            //otw[0] = (AcadEntity)pl;
                            //wt.AppendOuterLoop(otw);
                            //wt.Layer = lab;

                            double[] col2 = new double[15];
                            col2[0] = points[0].X;
                            col2[1] = -points[0].Y-30;
                            col2[2] = 0;

                            col2[3] = points[0].X;
                            col2[4] = -(points[0].Y)- 120;
                            col2[5] = 0;

                            col2[6] = points[0].X + 90;
                            col2[7] = -(points[0].Y)- 120;
                            col2[8] = 0;

                            col2[9] = points[0].X + 90;
                            col2[10] = -points[0].Y-30;
                            col2[11] = 0;

                            col2[12] = points[0].X;
                            col2[13] = -points[0].Y-30;
                            col2[14] = 0;

                            AcadPolyline pl2 = cad.ActiveDocument.ModelSpace.AddPolyline(col2);
                            double[] col3 = new double[15];
                            col3[0] = points[0].X+90;
                            col3[1] = -points[0].Y-60;
                            col3[2] = 0;

                            col3[3] = points[0].X+90;
                            col3[4] = -(points[0].Y )- 120;
                            col3[5] = 0;

                            col3[6] = points[0].X + 180;
                            col3[7] = -(points[0].Y) - 120;
                            col3[8] = 0;

                            col3[9] = points[0].X + 180;
                            col3[10] = -points[0].Y-60;
                            col3[11] = 0;

                            col3[12] = points[0].X+90;
                            col3[13] = -points[0].Y-60;
                            col3[14] = 0;

                            AcadPolyline pl3 = cad.ActiveDocument.ModelSpace.AddPolyline(col3);
                            if (u.GraphId.Contains("gh")) { }
                            else
                            {
                                AcadHatch solid = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
                                solid.PatternScale = 0.25;
                                solid.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
                                solid.color = ACAD_COLOR.acWhite;
                                AcadEntity[] obj = new AcadEntity[1];
                                obj[0] = (AcadEntity)pl2;
                                solid.AppendOuterLoop(obj);
                                solid.Evaluate();
                                solid.Layer = lab;
                                AcadHatch solid2 = cad.ActiveDocument.ModelSpace.AddHatch(0, "JIS_LC_20", true, 0);
                                solid2.PatternScale = 0.25;
                                solid2.Lineweight = ACAD_LWEIGHT.acLnWtByLwDefault;
                                solid2.color = ACAD_COLOR.acWhite;
                                AcadEntity[] obj2 = new AcadEntity[1];
                                obj2[0] = (AcadEntity)pl3;
                                solid2.AppendOuterLoop(obj2);
                                solid2.Evaluate();
                                solid2.Layer = lab;
                            }

                            pl.Layer = lab;
                            pl2.Layer = lab;
                            pl3.Layer = lab;

                          
                        }
                    }
                    catch { }
                }
                

            }
            //wt.color = ACAD_COLOR.acWhite;
            
      
            try
            {
                wt.Evaluate();
                h.Evaluate();
                h.Layer = lab;
            }
            catch(Exception e2) { }
          
            try
            {
                h2.Evaluate();
                h2.Layer = lab;
            }
            catch(Exception e3) { }
           
           
            try
            {
                h3.Evaluate();
                h3.Layer = lab;
            }
            catch { }
            try
            {
                h4.Evaluate();
                h4.Layer = lab;
            }
            catch { }
            try
            {
                h5.Evaluate();
                h5.Layer = lab;
            }
            catch { }
            cad.Application.Update();
            //cad.ActiveDocument.SendCommand("Z e ");
        }
    }
   
}
