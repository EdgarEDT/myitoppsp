using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using ItopVector.Core.Document;
using DevComponents.DotNetBar;
using System.IO;
using ItopVector.Desiger;
using ItopVector;
using ItopVector.Core.Paint;
using System.Xml; 
using System.Text.RegularExpressions;
using System.Reflection;
using ItopVector.Core.Interface.Figure;
using System.Configuration;
namespace ItopVectorDraw
{
	/// <summary>
	/// MainFrame 的摘要说明。
	/// </summary>
	/// 

	public class MainFrame : System.Windows.Forms.Form
	{
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;
		private DevComponents.DotNetBar.DockSite barLeftDockSite;
		private DevComponents.DotNetBar.DockSite barRightDockSite;
		private DevComponents.DotNetBar.DockSite barTopDockSite;
		private DevComponents.DotNetBar.DockSite barBottomDockSite;
		private DevComponents.DotNetBar.TabStrip tabStrip1;
		//private LibraryControl librarygridsys;
		
		private ItopVector.Selector.ShapeSelector shapeSelector;
		private ItopVector.Selector.SymbolSelector symbolSelector;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.ComponentModel.IContainer components;
		private ItopVector.Core.Document.SvgDocument preSvgDocument;
		private ComboBoxItem scaleBox;
		private ComboBoxItem fontnameBox;
		private ComboBoxItem fontsizeBox;
		private Struct.TextStyle ratFont;
//		private SvgFileManage svgFileManager;
		private bool notifyFontChanged;

		private ButtonItem operationButton;
		private ButtonItem orderButton;
		private ButtonItem alignButton;
		private ButtonItem rotateButton;

		private ItopVector.MiniatureView miniatureView;


		public MainFrame()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			ratFont=new ItopVector.Struct.TextStyle("宋体",12,false,false,false);
			notifyFontChanged =true;
			ItopVector.SpecialCursors.LoadCursors();//加载光标资源			
			
			//创建停靠控件
			CreateDockControl();

			InitializeComponent();
			this.WindowState=FormWindowState.Maximized;
			this.dotNetBarManager1.Images=ItopVector.Resource.ResourceHelper.LoadBitmapStrip(base.GetType(),"ItopVectorDraw.ToolbarImages.bmp",new Size(16,16),new Point(0,0));
			
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dotNetBarManager1 = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.barBottomDockSite = new DevComponents.DotNetBar.DockSite();
            this.barLeftDockSite = new DevComponents.DotNetBar.DockSite();
            this.barRightDockSite = new DevComponents.DotNetBar.DockSite();
            this.barTopDockSite = new DevComponents.DotNetBar.DockSite();
            this.tabStrip1 = new DevComponents.DotNetBar.TabStrip();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "图形文件(*.svg)|*.svg";
            // 
            // dotNetBarManager1
            // 
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManager1.BottomDockSite = this.barBottomDockSite;
            this.dotNetBarManager1.DefinitionName = "MainFrame.dotNetBarManager1.xml";
            this.dotNetBarManager1.LeftDockSite = this.barLeftDockSite;
            this.dotNetBarManager1.MenuDropShadow = DevComponents.DotNetBar.eMenuDropShadow.Show;
            this.dotNetBarManager1.ParentForm = this;
            this.dotNetBarManager1.RightDockSite = this.barRightDockSite;
            this.dotNetBarManager1.ShowResetButton = true;
            this.dotNetBarManager1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.dotNetBarManager1.ThemeAware = true;
            this.dotNetBarManager1.TopDockSite = this.barTopDockSite;
            this.dotNetBarManager1.ContainerLoadControl += new System.EventHandler(this.dotNetBarManager1_ContainerLoadControl);
            this.dotNetBarManager1.ItemClick += new System.EventHandler(this.dotNetBarManager1_ItemClick);
            this.dotNetBarManager1.PopupContainerLoad += new System.EventHandler(this.dotNetBarManager1_PopupContainerLoad);
            this.dotNetBarManager1.PopupContainerUnload += new System.EventHandler(this.dotNetBarManager1_PopupContainerUnload);
            // 
            // barBottomDockSite
            // 
            this.barBottomDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barBottomDockSite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barBottomDockSite.Location = new System.Drawing.Point(0, 595);
            this.barBottomDockSite.Name = "barBottomDockSite";
            this.barBottomDockSite.NeedsLayout = false;
            this.barBottomDockSite.Size = new System.Drawing.Size(760, 18);
            this.barBottomDockSite.TabIndex = 3;
            this.barBottomDockSite.TabStop = false;
            // 
            // barLeftDockSite
            // 
            this.barLeftDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barLeftDockSite.Dock = System.Windows.Forms.DockStyle.Left;
            this.barLeftDockSite.Location = new System.Drawing.Point(0, 96);
            this.barLeftDockSite.Name = "barLeftDockSite";
            this.barLeftDockSite.NeedsLayout = false;
            this.barLeftDockSite.Size = new System.Drawing.Size(188, 499);
            this.barLeftDockSite.TabIndex = 0;
            this.barLeftDockSite.TabStop = false;
            // 
            // barRightDockSite
            // 
            this.barRightDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barRightDockSite.Dock = System.Windows.Forms.DockStyle.Right;
            this.barRightDockSite.Location = new System.Drawing.Point(760, 96);
            this.barRightDockSite.Name = "barRightDockSite";
            this.barRightDockSite.NeedsLayout = false;
            this.barRightDockSite.Size = new System.Drawing.Size(0, 499);
            this.barRightDockSite.TabIndex = 1;
            this.barRightDockSite.TabStop = false;
            // 
            // barTopDockSite
            // 
            this.barTopDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barTopDockSite.Dock = System.Windows.Forms.DockStyle.Top;
            this.barTopDockSite.Location = new System.Drawing.Point(0, 0);
            this.barTopDockSite.Name = "barTopDockSite";
            this.barTopDockSite.NeedsLayout = false;
            this.barTopDockSite.Size = new System.Drawing.Size(760, 96);
            this.barTopDockSite.TabIndex = 2;
            this.barTopDockSite.TabStop = false;
            // 
            // tabStrip1
            // 
            this.tabStrip1.CanReorderTabs = true;
            this.tabStrip1.CloseButtonVisible = true;
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabStrip1.Location = new System.Drawing.Point(188, 96);
            this.tabStrip1.MdiTabbedDocuments = true;
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.SelectedTab = null;
            this.tabStrip1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabStrip1.Size = new System.Drawing.Size(572, 24);
            this.tabStrip1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2003;
            this.tabStrip1.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Top;
            this.tabStrip1.TabIndex = 4;
            this.tabStrip1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabStrip1.Text = "tabStrip1";
            this.tabStrip1.DoubleClick += new System.EventHandler(this.tabStrip1_DoubleClick);
            this.tabStrip1.TabItemClose += new DevComponents.DotNetBar.TabStrip.UserActionEventHandler(this.tabStrip1_TabItemClose);
            this.tabStrip1.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabStrip1_SelectedTabChanged);
            // 
            // MainFrame
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(760, 613);
            this.Controls.Add(this.tabStrip1);
            this.Controls.Add(this.barLeftDockSite);
            this.Controls.Add(this.barRightDockSite);
            this.Controls.Add(this.barTopDockSite);
            this.Controls.Add(this.barBottomDockSite);
            this.IsMdiContainer = true;
            this.Name = "MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItopVectorDraw";
            this.Closed += new System.EventHandler(this.MainFrame_Closed);
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.ResumeLayout(false);

		}
		#endregion
		

		private void CreateDockControl()
		{

			//形状库			
			LoadShape();
			//属性
			this.propertyGrid =new PropertyGrid();

			this.miniatureView = new MiniatureView();

		}
		private void MainFrame_Load(object sender, System.EventArgs e)
		{
			
			if(File.Exists("layout.xml"))
			{
				try
				{
//					this.dotNetBarManager1.LoadLayout("layout.xml");
				}
				catch{}
			}
			tabStrip1.MdiForm=this;
			CreateComboBox();
			this.alignButton = this.dotNetBarManager1.GetItem("mAlign") as ButtonItem;
			this.orderButton = this.dotNetBarManager1.GetItem("mOrder") as ButtonItem;
			this.rotateButton = this.dotNetBarManager1.GetItem("mRotate") as ButtonItem;

			CreateContentMenu();
#if !Link
			this.dotNetBarManager1.GetItem("mLink").Visible=false;
//			this.dotNetBarManager1.GetItem("mLayerManager").Visible=false;
#endif
		}
		private void CreateComboBox()
		{
			//字体
			fontnameBox =this.dotNetBarManager1.GetItem("FontFamily") as ComboBoxItem;
			
			if (fontnameBox!=null)
			{
				fontnameBox.ComboBoxEx.Items.Clear();
				fontnameBox.ComboBoxEx.DropDownWidth=200;
				fontnameBox.ComboBoxEx.DropDownHeight=200;

				FontFamily[] fonts =FontFamily.Families;
				foreach(FontFamily font in fonts)
				{
					fontnameBox.Items.Add(font.Name);
				}
				fontnameBox.ComboBoxEx.Text="宋体";
				fonts = null;
				fontnameBox.ComboBoxEx.SelectedIndexChanged+=new EventHandler(fontnameBox_SelectedIndexChanged);

			}
			//字号
			fontsizeBox =this.dotNetBarManager1.GetItem("FontSize") as ComboBoxItem;
			if(fontsizeBox!=null)
			{
				object[] singles = new object[] { 8f, 9f, 10f, 11f, 12f, 13f, 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 36f, 48f, 
												  72f, 80f, 88f, 96f, 128f, 168f
											  } ;
				fontsizeBox.Items.AddRange(singles);
			
				fontsizeBox.ComboBoxEx.Text="12";
				fontsizeBox.ComboBoxEx.SelectedIndexChanged+=new EventHandler(fontsizeBox_SelectedIndexChanged);
			}
			//缩放大小
			scaleBox =this.dotNetBarManager1.GetItem("ScaleBox") as ComboBoxItem;
			if(scaleBox!=null)
			{
//				scaleBox.Items.AddRange(new object[] {
//													  "2500%",
//													  "2000%",
//													  "1500%",
//													  "1000%",
//													  "500%",
//													  "300%",
//													  "200%",
//													  "150%",
//													  "120%",
//													  "100%",
//													  "80%",
//													  "60%",
//													  "50%",
//													  "30%",
//													  "10%"});
                if (ConfigurationManager.AppSettings["maptype"] == "baidu")
                    scaleBox.Items.AddRange(Itop.MapView.MapViewObj.ScaleRanges);
                else {
                    //scaleBox.Items.AddRange(new object[] { "800%", "400%", "200%", "100%", "40%", "20%", "10%", "4%", "2%", "1%" });
                    scaleBox.Items.AddRange(Itop.MapView.MapViewGoogle.ScaleRanges);
                }
				scaleBox.ComboBoxEx.Text="100%";
				scaleBox.ComboBoxEx.SelectedIndexChanged+=new EventHandler(scaleBox_SelectedIndexChanged);

			}			
		}
		

		private void LoadShape()
		{
			this.symbolSelector=new ItopVector.Selector.SymbolSelector(Application.StartupPath+"\\symbol\\symbol.xml");
//			this.symbolSelector.Load(Application.StartupPath+"\\symbol\\symbolxlt.xml");
//			this.symbolSelector.Load(Application.StartupPath+"\\symbol\\symbol.xml");
this.symbolSelector.Load(Application.StartupPath+"\\symbol\\symbol_3.xml");
			this.symbolSelector.Size=new Size(150,200);
			this.symbolSelector.SelectedChanged+=new EventHandler(symbolSelector_SelectedChanged);
			
		}

		private void CreateContentMenu()
		{
			Stream stream1 = null;
			Assembly assembly1 = Assembly.GetAssembly(Type.GetType("ItopVectorDraw.MainFrame"));
			
			if (assembly1 != null)
			{
				stream1= assembly1.GetManifestResourceStream("ItopVectorDraw.ContentMenu.xml");
			}
			
			if (stream1 != null)
			{
				XmlDocument document1 = new XmlDocument();
				try
				{ 
					document1.Load(stream1);

					XmlNodeList nodelist = document1.GetElementsByTagName("popupmenu");
					foreach(XmlNode node in nodelist)
					{
						XmlElement element1 =node as XmlElement;
						ButtonItem popupmenu =new ButtonItem(element1.GetAttribute("name"),element1.GetAttribute("lable"));
						
						CreateChild(popupmenu,element1);
//						this.dotNetBarManager1.RegisterPopup(popupmenu);
						this.dotNetBarManager1.ContextMenus.Add(popupmenu);

					} 
				}
				catch (Exception)
				{
				}
			}
		}

		private void CreateChild(ButtonItem bitem,XmlElement element)
		{
			foreach(XmlNode node2 in element.ChildNodes)
			{
				if(node2 is XmlElement)
				{
					XmlElement element1 =node2 as XmlElement;
					ButtonItem subitem=new ButtonItem(element1.GetAttribute("name"),element1.GetAttribute("lable"));
					subitem.BeginGroup = element1.GetAttribute("begingroup")=="true";
					if(element1.GetAttribute("imageindex")!=string.Empty)
					{ 
						subitem.ImageIndex = int.Parse(element1.GetAttribute("imageindex"));
					}
					CreateChild(subitem, element1); 
					bitem.SubItems.Add(subitem);
				}
			}
		}


		#region 文件操作
		private void NewFile()
		{
			frmDocument frm=new frmDocument();
			
			frm.Text= CreateFileName();
			frm.MdiParent=this;
			//frm.Bounds=this.ClientRectangle;
			frm.Bounds = new Rectangle(0,0,this.Width-this.barLeftDockSite.Width-10,this.Height - this.barTopDockSite.Height -50);
			frm.Show();
			frm.documentControl1.NewFile();			
			
			frm.documentControl1.SVGDocument.FileName = frm.Text;
			frm.documentControl1.ScaleChanged+=new EventHandler(documentControl1_ScaleChanged);
			frm.documentControl1.OnTipEvent+=new ItopVector.Core.Interface.OnTipEventHandler(ShowStatusInfo);
			frm.documentControl1.OnTrackPopup +=new ItopVector.Core.Interface.TrackPopupEventHandler(documentControl1_OnTrackPopup);

		}
		private string CreateFileName()
		{
			string filename="未命名-";
			int count=0;

			Regex regex1 =new Regex(@"^未命名-(?<num>\d+$)");
			foreach(Form frm in this.MdiChildren)
			{
				string key=string.Empty;
				Match match1 = regex1.Match(frm.Text);
				if (match1.Success)
				{
					key = match1.Groups["num"].Value;
					int num1=int.Parse(key);
					count = Math.Max(count,num1);
				}
				
			}
			count++;
			filename +=count;
			return filename;
		}
		private void OpenFile()
		{
			if ((this.openFileDialog1.ShowDialog(this) == DialogResult.OK) && (this.openFileDialog1.FileName != string.Empty))
			{
				this.Cursor=Cursors.WaitCursor;
				frmDocument frm=new frmDocument();
				string fileName=this.openFileDialog1.FileName;
				frm.MdiParent=this;
				frm.Show();
				try
				{
					ShowStatusInfo(this,"正在加载文件...",1);
					if (frm.documentControl1.OpenFile(fileName))
					{
						string str=ItopVector.Core.Func.CodeFunc.FormatXmlDocumentString(frm.documentControl1.SVGDocument);
						StreamWriter sw = new StreamWriter(@"c:\svg2.svg");
						sw.Write(str);
						sw.Close();

						ShowStatusInfo(this,"加载成功",1);
						frm.Text=frm.documentControl1.SVGDocument.FileName;
						frm.documentControl1.ScaleChanged+=new EventHandler(documentControl1_ScaleChanged);
						frm.documentControl1.OnTipEvent+=new ItopVector.Core.Interface.OnTipEventHandler(ShowStatusInfo);		
					}
					else
					{
						MessageBox.Show("加载文件失败！");		
					}
				}
				catch(Exception e)
				{
					MessageBox.Show(e.Message);
				}
				finally
				{
					this.Cursor=Cursors.Default;
				}
			}
		}
		public void Save(frmDocument frm)
		{
			if(frm.documentControl1.SVGDocument.SvgdataUid!=string.Empty)
			{
//				this.svgFileManager.Save(frm.documentControl1.SVGDocument);
			}
			else
			{
				frm.documentControl1.Save();
				frm.Text=frm.documentControl1.SVGDocument.FileName;
			}
		}
		private void SaveAll()
		{
			foreach(Form frm in this.MdiChildren)
			{
                if(frm is frmDocument)
                {
                	this.Save(frm as frmDocument);
                }
			}
		}
		private void CloseAll()
		{
			foreach(Form frm in this.MdiChildren)
			{
				frm.Close();
				
			}
		}

		private void ManagerMdiChildren()
		{
			FormManager dlg =new FormManager();
			dlg.Documents = this.MdiChildren ;
			dlg.FileManagerEvent+=new OnFileManagerEventHandler(dlg_FileManagerEvent);
			dlg.ShowDialog(this);
		}
		#endregion
		
		#region 菜单处理
		private void dotNetBarManager1_ItemClick(object sender, System.EventArgs e)
		{
			DevComponents.DotNetBar.ButtonItem btItem = sender as DevComponents.DotNetBar.ButtonItem;
			 
			frmDocument frm =this.ActiveMdiChild as frmDocument;
			if (btItem !=null )
			{
//				DevComponents.DotNetBar.ButtonItem btItem  =btItem2.Clone() as ButtonItem;
				bool flag=false;
				bool flag2=false;
				switch(btItem.Name)
				{
					case "mNewFile":
						NewFile();
						flag=true;
						break;
					case "mOpen":
						OpenFile();
						flag=true;
					
						break;

					case "mExit":
						this.Close();
						break;
					case "mAbout":
						this.symbolSelector.SelectIndex(2);
						
						string str =frm.documentControl1.SelectToDoc();
						frm=new frmDocument();
			
						
						frm.MdiParent=this;
						//frm.Bounds=this.ClientRectangle;
						frm.Bounds = new Rectangle(0,0,this.Width-this.barLeftDockSite.Width-10,this.Height - this.barTopDockSite.Height -50);
						frm.Show();
						SvgDocument doc =new SvgDocument();
						doc.LoadXml(str);
						frm.documentControl1.SVGDocument=doc;
						frm.documentControl1.ScaleChanged+=new EventHandler(documentControl1_ScaleChanged);
						frm.documentControl1.OnTipEvent+=new ItopVector.Core.Interface.OnTipEventHandler(ShowStatusInfo);
						frm.documentControl1.OnTrackPopup +=new ItopVector.Core.Interface.TrackPopupEventHandler(documentControl1_OnTrackPopup);

//						frm.documentControl1.AddShape( symbolSelector.SelectedItem,new Point(0,0));
//						Form1 f=new Form1();
//						f.Owner=this;
//						f.miniatureView1.VectorControl=frm.documentControl1;
//						
//						f.Show();
						
						
//						ArrayList list=frm.documentControl1.SVGDocument.getLayerList();
//						IEnumerator It=list.GetEnumerator();
//						while(It.MoveNext())
//						{
//							ItopVector.Core.Figure.Layer l=It.Current as ItopVector.Core.Figure.Layer;
//						}
						//frm.documentControl1.Operation=ToolOperation.AreaPolygon;
						//frm.documentControl1.ClipScreen(true);
						//frm.documentControl1.FitWindow();
						//frm.documentControl1.CurrentOperation=ToolOperation.ConnectLine_Rightangle;
//						frmAbout dlg=new frmAbout();
//						dlg.ShowDialog();
//						ItopVector.Core.SvgElement s= frm.documentControl1.SVGDocument.RootElement;
//						System.Xml.XmlNodeList l1= frm.documentControl1.SVGDocument.SelectNodes("svg/*");
//						IEnumerator eum= l1.GetEnumerator();
//						while(eum.MoveNext()){
//							XmlNode node=(XmlNode)eum.Current;
//							System.Xml.XmlElement e1=(System.Xml.XmlElement)node;
//							e1.SetAttribute("layer","layer00001");
//						}
						//frm.documentControl1.Operation=ToolOperation.WindowZoom;
							break;
					case "mInDxf":
						this.ImportDxf();
						break;
					case "mShapeManager":
						ItopVector.Dialog.SymbolManagerDialog dlg2 =new ItopVector.Dialog.SymbolManagerDialog();
						dlg2.SymbolSelector = this.symbolSelector;
						dlg2.ShowDialog(this);
						break;
				}
				if (flag)
				{
					if (this.MdiChildren.Length==1)
					{
						frm =this.ActiveMdiChild as frmDocument;
						if (frm==null)return;
						frm.documentControl1.PropertyGrid=this.propertyGrid;
						this.miniatureView.VectorControl = frm.documentControl1;
						frm.documentControl1.TextStyle =this.ratFont;
						UpdateToolBottom(frm.documentControl1.Operation);
					}
				}
				if (frm==null)return;
				
			Lable_switch:
				flag2=false;
				switch(btItem.Name)
				{
						#region 文件
					case "mSave":
						Save( frm);
						break;
					case "mSaveAll":
						SaveAll();
						break;

					case "mSaveAs":
						frm.documentControl1.SaveAs();
						frm.Text=frm.documentControl1.SVGDocument.FileName;
						break;
					case "mOutImage":
						frm.documentControl1.ExportImage();
//						ItopVectorControl tl=new ItopVectorControl();
//						tl.OpenFile(frm.documentControl1.SVGDocument.FilePath);
//						tl.ExportImage();
						break;
					case "mOutSymbol":
						frm.documentControl1.SymbolSelector = this.symbolSelector;
						frm.documentControl1.ExportSymbol(/*false,true,false,"symbol"*/);
						break;
					case "mClose":
						frm.Close();	
						break;
					case "mCloseAll":
						this.CloseAll();
						break;
					case "mSetup":
						frm.documentControl1.PaperSetup();
						break;
					case "mPrint":												
						frm.documentControl1.Print();
						break;
						#endregion
						#region 编辑
						
					case "mUndo":
						frm.documentControl1.Undo();
						break;
					case"mRedo":
						frm.documentControl1.Redo();
						break;
					case "mCopy":
						frm.documentControl1.Copy();
						break;
					case "mCut":
						frm.documentControl1.Cut();
						break;
					case "mPaste":
						frm.documentControl1.Paste();
						break;
					case "mDelete":
						frm.documentControl1.Delete();
						break;
					case"mSelectAll":
						frm.documentControl1.SelectCuurentLay();
						break;
					case "mClearSelects":
						frm.documentControl1.SelectNone();
						break;
					case "mLink":
						frm.documentControl1.Link();
						break;

						#endregion
						#region View
					case "mLayerManager":
						frm.documentControl1.LayerManager();
						break;
					
						#endregion
					case "mGroup":
						frm.documentControl1.Group();
						break;
					case "mUnGroup":
						frm.documentControl1.UnGroup();
						break;
					case "mShowRule":
						frm.documentControl1.IsShowRule=!frm.documentControl1.IsShowRule;
						break;
					case"mShowGrid":
						frm.documentControl1.IsShowGrid=!frm.documentControl1.IsShowGrid;
						break;
					case"mPasteGrid":
						frm.documentControl1.IsPasteGrid=!frm.documentControl1.IsPasteGrid;
						break;

						#region 顺序

					case "mOrder":
						if(btItem.Tag  is ButtonItem)
						{
							btItem = btItem.Tag as ButtonItem;
							flag2=true;
						}
						else
						{
							frm.documentControl1.ChangeLevel(LevelType.Top);
						}
						break;
					case"mGoTop":
						frm.documentControl1.ChangeLevel(LevelType.Top);
						this.orderButton.Tag = btItem;
						this.orderButton.ImageIndex = btItem.ImageIndex;
						break;
					case"mGoUp":
						frm.documentControl1.ChangeLevel(LevelType.Up);
						this.orderButton.Tag = btItem;
						this.orderButton.ImageIndex = btItem.ImageIndex;
						break;
					case"mGoDown":
						frm.documentControl1.ChangeLevel(LevelType.Down);
						this.orderButton.Tag = btItem;
						this.orderButton.ImageIndex = btItem.ImageIndex;
						break;
					case"mGoBottom":
						frm.documentControl1.ChangeLevel(LevelType.Bottom);
						this.orderButton.Tag = btItem;
						this.orderButton.ImageIndex = btItem.ImageIndex;
						break;
						#endregion
						#region 操作
					case "mDecreaseView":
						frm.documentControl1.Operation=ToolOperation.DecreaseView;
						UpdateToolBottom(btItem);
						break;
					case "mIncreaseView":
						frm.documentControl1.Operation=ToolOperation.IncreaseView;
						UpdateToolBottom(btItem);
						break;
					case "mRoam":
						frm.documentControl1.Operation=ToolOperation.Roam;
						UpdateToolBottom(btItem);
						break;
					case "mSelect":
						frm.documentControl1.Operation=ToolOperation.Select;
						UpdateToolBottom(btItem);
						break;
					case "mFreeTransform":
						frm.documentControl1.Operation=ToolOperation.FreeTransform;
						UpdateToolBottom(btItem);
						break; 
					case "mFreeLines"://锁套
						frm.documentControl1.Operation=ToolOperation.FreeLines;
						UpdateToolBottom(btItem);
						break;
					case "mFreePath":
						frm.documentControl1.Operation=ToolOperation.FreePath;
						UpdateToolBottom(btItem);
						break;
					case "mShapeTransform":
						frm.documentControl1.Operation=ToolOperation.ShapeTransform;
						UpdateToolBottom(btItem);
						break;
					case "mAngleRectangle":
						frm.documentControl1.Operation=ToolOperation.AngleRectangle;
						UpdateToolBottom(btItem);
						break;
					case "mEllipse":
						frm.documentControl1.Operation=ToolOperation.Ellipse;
						UpdateToolBottom(btItem);
						break;
					case "mLine":
						frm.documentControl1.Operation=ToolOperation.Line;
						UpdateToolBottom(btItem);
						break;		
					case "mConnectLine":
						frm.documentControl1.Operation=ToolOperation.ConnectLine;
						UpdateToolBottom(btItem);
						break;	
					case "mPolyline":
						frm.documentControl1.Operation=ToolOperation.PolyLine;
						UpdateToolBottom(btItem);
						break;
					case "mPolygon":
						frm.documentControl1.Operation=ToolOperation.Polygon;
						UpdateToolBottom(btItem);
						break;
					case "mImage":
						frm.documentControl1.Operation=ToolOperation.Image;
						UpdateToolBottom(btItem);
						break;
					case "mText":
						frm.documentControl1.Operation=ToolOperation.Text;
						UpdateToolBottom(btItem);
						break;
					case "mBezier":
						frm.documentControl1.Operation=ToolOperation.Bezier;
						UpdateToolBottom(btItem);
						break;
					case "mEnclosure":
						frm.documentControl1.Operation=ToolOperation.Enclosure;
						UpdateToolBottom(btItem);
						break;
					case "mPie":
						frm.documentControl1.Operation=ToolOperation.Pie;
						UpdateToolBottom(btItem);
						break;
					case "mArc":
						frm.documentControl1.Operation=ToolOperation.Arc;
						UpdateToolBottom(btItem);
						break;
					case "mStar"://等边形
						frm.documentControl1.Operation=ToolOperation.EqualPolygon;
						UpdateToolBottom(btItem);
						break;
						#endregion
						#region 文字

					case "mBold":
						btItem.Checked =!btItem.Checked;
						this.ratFont.Bold =btItem.Checked;
						frm.documentControl1.TextStyle =this.ratFont;
						break;
					case "mItalic":
						btItem.Checked =!btItem.Checked;
						this.ratFont.Italic =btItem.Checked;
						frm.documentControl1.TextStyle =this.ratFont;
						break;
					case "mUnderline":
						btItem.Checked =!btItem.Checked;
						this.ratFont.Underline =btItem.Checked;
						frm.documentControl1.TextStyle =this.ratFont;
						break;
						#endregion
					case "FontFamily":
						break;

					case "mOption"://选项
						frm.documentControl1.SetOption();
						break;

					case "mFormManager":
						this.ManagerMdiChildren();
						break;
					
						#region 翻转
					case "mRotate":
						if(btItem.Tag  is ButtonItem)
						{
							btItem = btItem.Tag as ButtonItem;
							flag2=true;
						}
						else
						{
							frm.documentControl1.FlipX();
							
						}
						break;
					case "mToH":
						
						frm.documentControl1.FlipX();
						this.rotateButton.Tag = btItem;
						this.rotateButton.ImageIndex = btItem.ImageIndex;
						break;
					case "mToV":
						frm.documentControl1.FlipY();
						this.rotateButton.Tag = btItem;
						this.rotateButton.ImageIndex = btItem.ImageIndex;
						break;
					case "mToLeft":
						frm.documentControl1.RotateSelection(-90f);
						this.rotateButton.Tag = btItem;
						this.rotateButton.ImageIndex = btItem.ImageIndex;
						break;
					case "mToRight":
						frm.documentControl1.RotateSelection(90f);
						this.rotateButton.Tag = btItem;
						this.rotateButton.ImageIndex = btItem.ImageIndex;
						break;
					
						#endregion
						#region 对齐
					case "mAlign":
						if(btItem.Tag is ButtonItem)
						{
							btItem = btItem.Tag as ButtonItem;
							flag2=true;
						}
						else
						{
							frm.documentControl1.Align(AlignType.Left);
						}
						break;
					case "mAlignLeft":
						frm.documentControl1.Align(AlignType.Left);
						this.alignButton.ImageIndex = btItem.ImageIndex;
						this.alignButton.Tag =btItem;
						break;
					case "mAlignRight":
						frm.documentControl1.Align(AlignType.Right);
						this.alignButton.ImageIndex = btItem.ImageIndex;
						this.alignButton.Tag =btItem;
						break;
					case "mAlignTop":
						frm.documentControl1.Align(AlignType.Top);
						this.alignButton.ImageIndex = btItem.ImageIndex;
						this.alignButton.Tag =btItem;
						break;
					case "mAlignBottom":
						frm.documentControl1.Align(AlignType.Bottom);
						this.alignButton.ImageIndex = btItem.ImageIndex;
						this.alignButton.Tag =btItem;
						break;
					case "mAlignHorizontalCenter":
						frm.documentControl1.Align(AlignType.HorizontalCenter);
						this.alignButton.ImageIndex = btItem.ImageIndex;
						this.alignButton.Tag =btItem;
						break;
					case "mAlignVerticalCenter":
						frm.documentControl1.Align(AlignType.VerticalCenter);
						this.alignButton.ImageIndex = btItem.ImageIndex;
						this.alignButton.Tag =btItem;
						break;
						#endregion
					default:
						break;
				}
				if(flag2)
				{
					goto Lable_switch;
				}
				
			}
		}
		#endregion

		

		#region tabStrip

		private void tabStrip1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
		{
			frmDocument frm;

			if(e.OldTab!=null)
			{
				frm=e.OldTab.AttachedControl as frmDocument;
				if (frm!=null)
				{
					frm.documentControl1.PropertyGrid=null;
				}
			}
			TabItem tab=e.NewTab;
			if (tab==null)
			{
				return;
			}
			frm =tab.AttachedControl as frmDocument;
			if (frm==null)return;
			documentControl1_ScaleChanged(frm.documentControl1,EventArgs.Empty);
			frm.documentControl1.PropertyGrid=this.propertyGrid;
			this.miniatureView.VectorControl = frm.documentControl1;
			this.TextStyle=frm.documentControl1.TextStyle;

			UpdateToolBottom(frm.documentControl1.Operation);
			
		}
		
		private void tabStrip1_DoubleClick(object sender, System.EventArgs e)
		{
			frmDocument frm = this.ActiveMdiChild as frmDocument;
			if (frm!=null)
				frm.Close(); 
		}
		private void tabStrip1_TabItemClose(object sender, DevComponents.DotNetBar.TabStripActionEventArgs e)
		{
//			
//			TabItem tabitem =null;
//			if(sender is TabStrip)
//			{
//				tabitem=((TabStrip)sender).SelectedTab as TabItem;
//			}
//			else
//			{
//				tabitem = sender as TabItem;
//			}
//			frmDocument frm = tabitem.AttachedControl as frmDocument ;
//			if(frm==null)return;
//
//			if(frm.documentControl1.IsModified)
//			{
//				string str =string.Format("文件\"{0}\"已经发生修改，是否保存？",frm.documentControl1.SVGDocument.FileName);
//				DialogResult result= MessageBox.Show(str,"保存",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
//				if(result==DialogResult.Yes)
//				{
//					this.Save(frm);
//				}
//				else if(result==DialogResult.Cancel)
//				{
//					e.Cancel =true;
//				}
//			}

		}
		#endregion
		#region dotNetBarmanger
		

		private void dotNetBarManager1_ContainerLoadControl(object sender, System.EventArgs e)
		{
			BaseItem item=sender as BaseItem;
			DockContainerItem dockitem=null;
			if(item==null)
				return;
			if(item.Name=="DockContainerShape")
			{
				dockitem=item as DockContainerItem;	

				dockitem.Control=this.symbolSelector;
			}
			
			if(item.Name == "DockContainerItem1")
			{
				dockitem = item as DockContainerItem;
				dockitem.Control = this.miniatureView;
			}
            if (item.Name == "DockContainerProperty") {
                dockitem = item as DockContainerItem;

                dockitem.Control = this.propertyGrid;
            }
			if(item.Name=="DockContainerFile")
			{
				dockitem=item as DockContainerItem;
//				svgFileManager= new ItopVectorDraw.SvgFileManage();
//				dockitem.Control=svgFileManager;
//				svgFileManager.OnOpenSvgDocument+=new ItopVectorDraw.OnOpenDocumenthandler(svgfilemanager_OnOpenSvgDocument);
			}
		}
		private void clr_BackColorChanged(object sender, EventArgs e)
		{
			ColorPicker clr = sender as ColorPicker;
			PopupContainerControl ctrl = clr.Parent as PopupContainerControl;
			if (ctrl == null)
				return;
			
			ButtonItem btn = ctrl.ParentItem as ButtonItem;
			Bitmap bmp = new Bitmap(btn.ImageList.Images[btn.ImageIndex], btn.ImageList.ImageSize);
			Graphics g = Graphics.FromImage(bmp);
			g.DrawImageUnscaled(btn.ImageList.Images[btn.ImageIndex], 0, 0);
			using (SolidBrush brush = new SolidBrush(clr.SelectedColor))
				g.FillRectangle(brush, 0, 12, 16, 4);
			g.Dispose();

			DotNetBarManager manager = null;
			if (btn.ContainerControl is Bar)
				manager = ((Bar) btn.ContainerControl).Owner as DotNetBarManager;
			else if (btn.ContainerControl is MenuPanel)
				manager = ((MenuPanel) btn.ContainerControl).Owner as DotNetBarManager;
			if (manager != null && btn.Name != "")
			{
				ArrayList items = manager.GetItems(btn.Name, true);
				foreach (ButtonItem item in items)
					item.Image = bmp;
			}
			else
				btn.Image = bmp;
			
			BaseItem topItem = ctrl.ParentItem;
			while (topItem.Parent is ButtonItem)
				topItem = topItem.Parent;
			topItem.Expanded = false;
			if (topItem.Parent != null)
				topItem.Parent.AutoExpand = false;
		}
		private void dotNetBarManager1_PopupContainerLoad(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;
			if(item.Name=="mForecolor" || item.Name=="mBackcolor" || item.Name=="mTextcolor")
			{
				DevComponents.DotNetBar.PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;
				ColorPicker clr=new ColorPicker();
				if(item.Name=="mBackcolor")
				{
					clr.SelectedColor=Color.White;
				}
				else
				{
					clr.SelectedColor=Color.Black;
				}
				container.Controls.Add(clr);

				clr.BackColor=dotNetBarManager1.Bars[0].ColorScheme.BarBackground2;
//				clr.tabStrip1.Style=eTabStripStyle.Office2003;
				clr.BackColorChanged+=new EventHandler(clr_BackColorChanged);

				clr.Location=container.ClientRectangle.Location;
				
				container.ClientSize=clr.Size;
			}
			if(item.Name == "mStar")
			{
				DevComponents.DotNetBar.PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;

				StarOption star =new StarOption();
				frmDocument activedocument=this.ActiveMdiChild as frmDocument;
				if(activedocument!=null)
				{
					star.IndentPicker.Value = (Decimal)activedocument.documentControl1.DrawArea.Indent*100;
					star.radialPicker.Value = activedocument.documentControl1.DrawArea.LineCount;
				}
				container.Controls.Add(star);
				star.Location = container.ClientRectangle.Location;
				container.ClientSize = star.Size;
			}
		}

		private void dotNetBarManager1_PopupContainerUnload(object sender, System.EventArgs e)
		{
			ButtonItem item=sender as ButtonItem;
			if(item.Name=="mForecolor") 
			{
				DevComponents.DotNetBar.PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;
				ColorPicker clr=container.Controls[0] as ColorPicker;
				if(clr.SelectedColor!=Color.Empty)
				{
					frmDocument activedocument=this.ActiveMdiChild as frmDocument;
					if(activedocument!=null)
					{
							ItopVector.Core.Paint.Stroke stroke1= new Stroke(new SolidColor(clr.SelectedColor));

						activedocument.documentControl1.Stroke = stroke1;
					}
				}
			}	
			else if(item.Name=="mBackcolor")
			{
				DevComponents.DotNetBar.PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;
				ColorPicker clr=container.Controls[0] as ColorPicker;
				if(clr.SelectedColor!=Color.Empty)
				{
					frmDocument activedocument=this.ActiveMdiChild as frmDocument;
					if(activedocument!=null)
					{
						SolidColor fill=new SolidColor(clr.SelectedColor);
						activedocument.documentControl1.Fill=fill;
					}
				}
			}
			else if(item.Name == "mStar")
			{
				DevComponents.DotNetBar.PopupContainerControl container=item.PopupContainerControl as PopupContainerControl;
				StarOption star = container.Controls[0] as StarOption;
				frmDocument activedocument=this.ActiveMdiChild as frmDocument;
				if(activedocument!=null)
				{
					
					activedocument.documentControl1.DrawArea.LineCount = (int)star.radialPicker.Value;
					activedocument.documentControl1.DrawArea.Indent = (float)star.IndentPicker.Value/100f;

				}
			}

		}
		#endregion
		private void scaleBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frmDocument frm=((frmDocument)this.ActiveMdiChild);
			if (frm!=null)
			{
				string text1=this.scaleBox.SelectedItem.ToString();
				float f1 = ItopVector.Core.Func.Number.ParseFloatStr(text1);
				frm.documentControl1.ScaleRatio=f1;
			}
		}
		/// <summary>
		/// 字体改变
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fontnameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frmDocument frm=((frmDocument)this.ActiveMdiChild);
			if (frm!=null && this.notifyFontChanged)
			{
				string text1=this.fontnameBox.SelectedItem.ToString();
				this.ratFont.FontName=text1;

				frm.documentControl1.TextStyle=this.ratFont;
			}
		}

		/// <summary>
		/// 字号改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fontsizeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			frmDocument frm=((frmDocument)this.ActiveMdiChild);
			if (frm!=null && this.notifyFontChanged)
			{
				string text1=this.fontsizeBox.SelectedItem.ToString();
				this.ratFont.Size=float.Parse( text1) ;
				frm.documentControl1.TextStyle=this.ratFont;
			}
		}
		private void documentControl1_ScaleChanged(object sender, EventArgs e)
		{
			string text1=(((ItopVector.ItopVectorControl)sender).ScaleRatio*100)+"%";

			scaleBox.ComboBoxEx.SelectedIndexChanged-=new EventHandler(scaleBox_SelectedIndexChanged);
			this.scaleBox.ComboBoxEx.Text=text1;
			scaleBox.ComboBoxEx.SelectedIndexChanged+=new EventHandler(scaleBox_SelectedIndexChanged);
		}

		private void MainFrame_Closed(object sender, EventArgs e)
		{
			//保存布局
			this.dotNetBarManager1.SaveLayout(Application.StartupPath+@"\layout.xml");
		}
		private void UpdateProperty()
		{
			if (File.Exists(Application.StartupPath + @"\Preference\preference.xml"))
			{
				try
				{
					XmlDocument document1 = new XmlDocument();
					document1.Load(Application.StartupPath + @"\Preference\preference.xml");
//					foreach (SvgDocument document2 in this.documentList)
//					{
//						XmlNode node1 = document1.DocumentElement.SelectSingleNode("//*[@id='AutoRecordAnim']");
//						if (node1 != null)
//						{
//							document2.RecordAnim = node1.Attributes["Value"].Value.Trim() == "true";
//						}
//						node1 = document1.DocumentElement.SelectSingleNode("//*[@id='AutoExpandAnims']");
//						if (node1 != null)
//						{
//							document2.AutoShowAnim = node1.Attributes["Value"].Value.Trim() == "true";
//						}
//						node1 = document1.DocumentElement.SelectSingleNode("//*[@id='HighQuality']");
//						if (node1 != null)
//						{
//							document2.SmoothingMode = (node1.Attributes["Value"].Value.Trim() == "true") ? SmoothingMode.HighQuality : SmoothingMode.HighSpeed;
//						}
//						node1 = document1.DocumentElement.SelectSingleNode("//*[@id='AntiAlias']");
//						if (node1 != null)
//						{
//							document2.TextRenderingHint = (node1.Attributes["Value"].Value.Trim() == "true") ? TextRenderingHint.AntiAlias : TextRenderingHint.SystemDefault;
//						}
//					}
				}
				catch (Exception)
				{
				}
			}
		}
		private void ShowStatusInfo(object sender, string tooltip, byte type)
		{
			if (type == 0)
			{
				LabelItem item= this.dotNetBarManager1.GetItem("plCoord") as LabelItem;
				if(item!=null)
					item.Text="坐标 "+tooltip;
			}
			else if (type == 1)
			{
				LabelItem item= this.dotNetBarManager1.GetItem("plTip") as LabelItem;
				if(item!=null)
					item.Text=tooltip;
			}
			else if (type == 2)
			{
				LabelItem item= this.dotNetBarManager1.GetItem("plColumn") as LabelItem;
				if(item!=null)
					item.Text=tooltip;
			}
		}
		public  void UpdateToolBottom(ToolOperation operation)
		{
			string str1="m"+operation.ToString();
			ButtonItem button =this.dotNetBarManager1.GetItem(str1) as ButtonItem;
			if(button!=null)
			{
				this.UpdateToolBottom(button);
			}
				
		}
		private void UpdateToolBottom(ButtonItem button)
		{
			
			if(operationButton==null)
			{
				operationButton = this.dotNetBarManager1.GetItem("mRoam") as ButtonItem;
			}
			operationButton.Checked =false;

			button.Checked =true;

			operationButton = button;
		}

		private void svgfilemanager_OnOpenSvgDocument(object sender, SvgDocument document)
		{
			//检查是否已打开
			
			string fileuid=document.SvgdataUid;
			if(fileuid!=string.Empty)
			{
				foreach(frmDocument frm1 in this.MdiChildren)
				{
					if(frm1.documentControl1.SVGDocument.SvgdataUid==fileuid)
					{
						frm1.Activate();
						return;
					}
				}
			}			
			frmDocument frm=new frmDocument();
			frm.MdiParent=this;
			frm.Show();
			frm.documentControl1.SVGDocument=document;
			frm.Text =document.FileName;
			frm.documentControl1.ScaleChanged+=new EventHandler(documentControl1_ScaleChanged);
			frm.documentControl1.OnTipEvent+=new ItopVector.Core.Interface.OnTipEventHandler(ShowStatusInfo);
			if(this.MdiChildren.Length==1)
			{
				frm.documentControl1.PropertyGrid=this.propertyGrid;
				this.miniatureView.VectorControl = frm.documentControl1;
				frm.documentControl1.TextStyle = this.ratFont;
				UpdateToolBottom(frm.documentControl1.Operation);
			}
		}
		private void ImportDxf()
		{
			OpenFileDialog dlg=new OpenFileDialog();
			dlg.Filter="CAD文件(*.dxf,*.dwg)|*.dxf;*.dwg";
			if(dlg.ShowDialog(this)==DialogResult.OK)
			{
				
				svgfilemanager_OnOpenSvgDocument(this,new Itop.CADConvert.CADConvertHelper().ConvertToSvg(dlg.FileName));
				
			}
		}		
	
		private Struct.TextStyle TextStyle
		{
			set
			{
				if(this.ratFont==value )return;

				this.notifyFontChanged =false;
				this.fontnameBox.ComboBoxEx.Text = value.FontName;
				this.fontsizeBox.ComboBoxEx.Text = value.Size.ToString();
				this.ratFont =value;

				ButtonItem bitem =this.dotNetBarManager1.GetItem("mBold") as ButtonItem;
				if(bitem!=null)bitem.Checked =value.Bold;
				bitem =this.dotNetBarManager1.GetItem("mItalic") as ButtonItem;
				if(bitem!=null)bitem.Checked =value.Italic;
				bitem =this.dotNetBarManager1.GetItem("mUnderline") as ButtonItem;
				if(bitem!=null)bitem.Checked =value.Underline;
				this.notifyFontChanged =true;
			}
		}
		private void dlg_FileManagerEvent(object sender, object[] pathlist, Action action)
		{

		}

		private void documentControl1_OnTrackPopup(object sender, Point screenPoint)
		{
			
			
			if(this.dotNetBarManager1.ContextMenus.Contains("menu1"))
			{
//				BaseItem popupmenu =this.dotNetBarManager1.ContextMenus["menu1"];
//				(popupmenu as ButtonItem).PopupMenu(screenPoint);
			}
			

		}

		private void symbolSelector_SelectedChanged(object sender, EventArgs e)
		{
			frmDocument frm =this.ActiveMdiChild as frmDocument;
            if (frm == null) return;
            try {
                frm.documentControl1.DrawArea.PreGraph = this.symbolSelector.SelectedItem.CloneNode(true) as IGraph;
            } catch { }
		}

		
	}
}
