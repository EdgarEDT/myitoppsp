using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace Itop.Client.Common 
{
	/// <summary>
	/// ComponentPrint类的使用举例：
	/// DevExpress.ComponentPrint.ShowPreview(gridControl1, "我的标题");
	/// </summary>
	public class ComponentPrint
	{
		#region 公共方法
		/// <summary>
		/// 显示打印预览窗体
		/// </summary>
		/// <param name="component">可以是GridContronl或者TreeList或者其他</param>
		/// <param name="headerTitle">标题</param>
		public static void ShowPreview(IPrintable component, string headerTitle)
		{
			ShowPreview(component, headerTitle, false);
		}

		/// <summary>
		/// 显示打印预览窗体
		/// </summary>
		/// <param name="component">可以是GridContronl或者TreeList或者其他</param>
		/// <param name="headerTitle">标题</param>
		/// <param name="landscape">true表示横向，false表示纵向</param>
		public static void ShowPreview(IPrintable component, string headerTitle, bool landscape)
		{
			ShowPreview(component, headerTitle, landscape, new Font("宋体", 22, System.Drawing.FontStyle.Bold));
		}

		/// <summary>
		/// 显示打印预览窗体
		/// </summary>
		/// <param name="component">可以是GridContronl或者TreeList或者其他</param>
		/// <param name="headerTitle">标题</param>
		/// <param name="landscape">true表示横向，false表示纵向</param>
		/// <param name="headerFont">标题字体</param>
		public static void ShowPreview(IPrintable component, string headerTitle, bool landscape, Font headerFont)
		{
			PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
			DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
			ps.Links.Add(link);
			link.Component = component;//这里可以是可打印的部件

			PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
			phf.Header.Content.AddRange(new string[]{"", headerTitle, ""});
			phf.Header.Font = headerFont;
			phf.Header.LineAlignment = BrickAlignment.Center;
            phf.Footer.Content.AddRange(new string[] { DateTime.Now.ToString("yyyy-MM-dd"), "", "[Page # of Pages #]" });
			phf.Footer.LineAlignment = BrickAlignment.Center;
			link.Landscape = landscape;
			link.CreateDocument(); //建立文档
            

            //ps.PreviewFormEx.Menu.MenuItems[0].MenuItems.RemoveAt(6) ;
            //ps.PreviewFormEx.Menu.MenuItems[0].MenuItems.RemoveAt(5);
            //ps.PreviewFormEx.Menu.MenuItems[0].MenuItems.RemoveAt(4);
            //ps.PreviewFormEx.PrintPreviewBar.Buttons.RemoveAt(4);
            //ps.PreviewFormEx.PrintPreviewBar.Buttons.RemoveAt(23);
            //ps.PreviewFormEx.PrintPreviewBar.Buttons.RemoveAt(23);lgm
            
           
			ps.PreviewFormEx.Text=headerTitle;

			ps.PreviewFormEx.Show();//进行预览
		}

		#endregion

		#region 保护方法
		protected static void UpdateMenuText(MenuItem mi)
		{
			mi.Text = GetChineseMenuText(mi.Text);
			foreach(MenuItem submi in mi.MenuItems)
			{
				UpdateMenuText(submi);
			}
		}

		protected static string GetChineseMenuText(string english)
		{
			switch(english)
			{
				case "&File":
					return "文件";
				case "Page Set&up...":
					return "页面设置...";
				case "&Print...":
					return "打印...";
				case "P&rint":
					return "打印";
				case "&Export To":
					return "导出";
				case "PDF Document":
					return "PDF文档";
				case "HTML Document":
					return "HTML文档";
				case "Text Document":
					return "文本文档";
				case "CSV Document":
					return "CSV 文档";
				case "MHT Document":
					return "MHT 文档";
				case "Excel Document":
					return "Excel 文档";
				case "Rich Text Document":
					return "RIF 文档";
				case "Graphic Document":
					return "图形文档";
				case "Sen&d As":
					return "另存为";
				case "E&xit":
					return "退出";
				case "&View":
					return "视图";
				case "&Toolbar":
					return "工具栏";
				case "&Statusbar":
					return "状态栏";
				case "Page Layout":
					return "页面布局";
				case "Continous":
					return "连续";
				case "Facing":
					return "单页";
				case "&Background":
					return "背景";
				case "&Color...":
					return "颜色";
				case "&Watermark...":
					return "水印";
				case "Page Width":		//
					return "按页缩放";
				case "Text Width":		//
					return "按文本缩放";
				default:
					return english;
			}
		}

		protected static string GetChineseToolBarTipText(string english)
		{
			switch(english)
			{
				case "Document Map":
					return "文档地图";
				case "Search":
					return "查询";
				case "Hand Tool":
					return "手";
				case "Customize":
					return "自定义";
				case "Print":
					return "打印...";
				case "Print Direct":
					return "打印";
				case "Page Setup":
					return "页面设置";
				case "Header And Footer":
					return "页眉和页脚";
				case "Magnifier":
					return "放大镜";
				case "Zoom In":
					return "放大";
				case "Zoom Out":
					return "缩小";
				case "Zoom":
					return "缩放";
				case "First Page":
					return "第一页";
				case "Previous Page":
					return "前一页";
				case "Next Page":
					return "下一页";
				case "Last Page":
					return "最后一页";
				case "Multiple Pages":
					return "多页视图";
				case "Background":
					return "背景";
				case "Watermark":
					return "水印";
				case "Export Document...":
					return "导出文档";
				case "Send e-mail...":
					return "发送电子邮件";
				case "Close Preview":
					return "关闭预览";
				default:
					return english;
			}
		}
		#endregion
	}
}
