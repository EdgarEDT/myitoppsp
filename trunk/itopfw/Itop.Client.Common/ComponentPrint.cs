using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;

namespace Itop.Client.Common 
{
	/// <summary>
	/// ComponentPrint���ʹ�þ�����
	/// DevExpress.ComponentPrint.ShowPreview(gridControl1, "�ҵı���");
	/// </summary>
	public class ComponentPrint
	{
		#region ��������
		/// <summary>
		/// ��ʾ��ӡԤ������
		/// </summary>
		/// <param name="component">������GridContronl����TreeList��������</param>
		/// <param name="headerTitle">����</param>
		public static void ShowPreview(IPrintable component, string headerTitle)
		{
			ShowPreview(component, headerTitle, false);
		}

		/// <summary>
		/// ��ʾ��ӡԤ������
		/// </summary>
		/// <param name="component">������GridContronl����TreeList��������</param>
		/// <param name="headerTitle">����</param>
		/// <param name="landscape">true��ʾ����false��ʾ����</param>
		public static void ShowPreview(IPrintable component, string headerTitle, bool landscape)
		{
			ShowPreview(component, headerTitle, landscape, new Font("����", 22, System.Drawing.FontStyle.Bold));
		}

		/// <summary>
		/// ��ʾ��ӡԤ������
		/// </summary>
		/// <param name="component">������GridContronl����TreeList��������</param>
		/// <param name="headerTitle">����</param>
		/// <param name="landscape">true��ʾ����false��ʾ����</param>
		/// <param name="headerFont">��������</param>
		public static void ShowPreview(IPrintable component, string headerTitle, bool landscape, Font headerFont)
		{
			PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
			DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
			ps.Links.Add(link);
			link.Component = component;//��������ǿɴ�ӡ�Ĳ���

			PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
			phf.Header.Content.AddRange(new string[]{"", headerTitle, ""});
			phf.Header.Font = headerFont;
			phf.Header.LineAlignment = BrickAlignment.Center;
            phf.Footer.Content.AddRange(new string[] { DateTime.Now.ToString("yyyy-MM-dd"), "", "[Page # of Pages #]" });
			phf.Footer.LineAlignment = BrickAlignment.Center;
			link.Landscape = landscape;
			link.CreateDocument(); //�����ĵ�
            

            //ps.PreviewFormEx.Menu.MenuItems[0].MenuItems.RemoveAt(6) ;
            //ps.PreviewFormEx.Menu.MenuItems[0].MenuItems.RemoveAt(5);
            //ps.PreviewFormEx.Menu.MenuItems[0].MenuItems.RemoveAt(4);
            //ps.PreviewFormEx.PrintPreviewBar.Buttons.RemoveAt(4);
            //ps.PreviewFormEx.PrintPreviewBar.Buttons.RemoveAt(23);
            //ps.PreviewFormEx.PrintPreviewBar.Buttons.RemoveAt(23);lgm
            
           
			ps.PreviewFormEx.Text=headerTitle;

			ps.PreviewFormEx.Show();//����Ԥ��
		}

		#endregion

		#region ��������
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
					return "�ļ�";
				case "Page Set&up...":
					return "ҳ������...";
				case "&Print...":
					return "��ӡ...";
				case "P&rint":
					return "��ӡ";
				case "&Export To":
					return "����";
				case "PDF Document":
					return "PDF�ĵ�";
				case "HTML Document":
					return "HTML�ĵ�";
				case "Text Document":
					return "�ı��ĵ�";
				case "CSV Document":
					return "CSV �ĵ�";
				case "MHT Document":
					return "MHT �ĵ�";
				case "Excel Document":
					return "Excel �ĵ�";
				case "Rich Text Document":
					return "RIF �ĵ�";
				case "Graphic Document":
					return "ͼ���ĵ�";
				case "Sen&d As":
					return "���Ϊ";
				case "E&xit":
					return "�˳�";
				case "&View":
					return "��ͼ";
				case "&Toolbar":
					return "������";
				case "&Statusbar":
					return "״̬��";
				case "Page Layout":
					return "ҳ�沼��";
				case "Continous":
					return "����";
				case "Facing":
					return "��ҳ";
				case "&Background":
					return "����";
				case "&Color...":
					return "��ɫ";
				case "&Watermark...":
					return "ˮӡ";
				case "Page Width":		//
					return "��ҳ����";
				case "Text Width":		//
					return "���ı�����";
				default:
					return english;
			}
		}

		protected static string GetChineseToolBarTipText(string english)
		{
			switch(english)
			{
				case "Document Map":
					return "�ĵ���ͼ";
				case "Search":
					return "��ѯ";
				case "Hand Tool":
					return "��";
				case "Customize":
					return "�Զ���";
				case "Print":
					return "��ӡ...";
				case "Print Direct":
					return "��ӡ";
				case "Page Setup":
					return "ҳ������";
				case "Header And Footer":
					return "ҳü��ҳ��";
				case "Magnifier":
					return "�Ŵ�";
				case "Zoom In":
					return "�Ŵ�";
				case "Zoom Out":
					return "��С";
				case "Zoom":
					return "����";
				case "First Page":
					return "��һҳ";
				case "Previous Page":
					return "ǰһҳ";
				case "Next Page":
					return "��һҳ";
				case "Last Page":
					return "���һҳ";
				case "Multiple Pages":
					return "��ҳ��ͼ";
				case "Background":
					return "����";
				case "Watermark":
					return "ˮӡ";
				case "Export Document...":
					return "�����ĵ�";
				case "Send e-mail...":
					return "���͵����ʼ�";
				case "Close Preview":
					return "�ر�Ԥ��";
				default:
					return english;
			}
		}
		#endregion
	}
}
