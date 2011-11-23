using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Columns;


namespace Itop.Client.Forecast
{
    public class GridTreePrint
    {
        public static void Exception(TreeList xTreeList,string title)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "��Ŀ";


            GridControl gc = new GridControl();
            GridView gv = new GridView();


            gc.Dock = System.Windows.Forms.DockStyle.Fill;
            gc.EmbeddedNavigator.Name = "";
            gc.Location = new System.Drawing.Point(0, 34);
            gc.MainView = gv;
            gc.Name = "gridControl1";
            gc.Size = new System.Drawing.Size(506, 272);
            gc.TabIndex = 0;
            gc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gv });


            gv.GridControl = gc;
            gv.Name = "gridView1";
            gv.OptionsBehavior.Editable = false;
            gv.OptionsCustomization.AllowFilter = false;
            gv.OptionsCustomization.AllowGroup = false;
            gv.OptionsCustomization.AllowSort = false;
            gv.OptionsView.EnableAppearanceOddRow = true;
            gv.OptionsView.ShowGroupPanel = false;
            gv.GroupPanelText = title;


            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("y") >= 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }
            }

            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID);
            }


            for (int i = 0; i < listColID.Count; i++)
            {
                GridColumn gridCol = new GridColumn();
                gridCol.FieldName = listColID[i];
                gridCol.VisibleIndex = i;
                gridCol.Visible = true;


                //this.bandedGridColumn1.AppearanceHeader.Options.UseTextOptions = true;
                //this.bandedGridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //this.bandedGridColumn1.Caption = "��Ŀ����";
                //this.bandedGridColumn1.ColumnEdit = this.repositoryItemMemoEdit1;
                //this.bandedGridColumn1.FieldName = "Title";
                //this.bandedGridColumn1.Name = "bandedGridColumn1";
                if (listColID[i] == "Title")
                {
                    gridCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gridCol.Width = 150;
                    gridCol.Caption = "��Ŀ";
                }
                else
                {
                    gridCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridCol.Width = 150;
                    gridCol.Caption = gridCol.FieldName.Replace("y", "") + "��";
                }
                gv.Columns.Add(gridCol);
            }






            gc.DataSource = dt;



            //FileClass.ExportExcel(title, "", gc);
            ForecastFileClass.ExportExcel(title,"",gc);
        }



        private static void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //���������ڶ��㼰�Ժ���ǰ��ӿո�
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "����" + node[colID];
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }

            ////�����������ӿ���

            //if (node.ParentNode == null && dt.Rows.Count > 0)
            //{
            //    dt.Rows.Add(new object[] { });
            //}

            dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }

        //////����ѡ���ͳ����ݣ�����ͳ�ƽ�����ݱ�

        ////private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        ////{
        ////    DataTable dtReturn = new DataTable();
        ////    dtReturn.Columns.Add("Title", typeof(string));
        ////    foreach (ChoosedYears choosedYear in listChoosedYears)
        ////    {
        ////        dtReturn.Columns.Add(choosedYear.Year + "��", typeof(double));
        ////        if (choosedYear.WithIncreaseRate)
        ////        {
        ////            dtReturn.Columns.Add(choosedYear.Year + "������", typeof(double)).Caption = "������";
        ////        }
        ////    }

        ////    #region �������
        ////    for (int i = 0; i < sourceDataTable.Rows.Count; i++)
        ////    {
        ////        DataRow newRow = dtReturn.NewRow();
        ////        DataRow sourceRow = sourceDataTable.Rows[i];
        ////        foreach (DataColumn column in dtReturn.Columns)
        ////        {
        ////            if (column.Caption != "������")
        ////            {
        ////                newRow[column.ColumnName] = sourceRow[column.ColumnName];
        ////            }
        ////        }
        ////        dtReturn.Rows.Add(newRow);
        ////    }
        ////    #endregion

        ////    #region ����������

        ////    DataColumn columnBegin = null;
        ////    foreach (DataColumn column in dtReturn.Columns)
        ////    {
        ////        if (column.ColumnName.IndexOf("��") > 0)
        ////        {
        ////            if (columnBegin == null)
        ////            {
        ////                columnBegin = column;
        ////            }
        ////        }
        ////        else if (column.ColumnName.IndexOf("������") > 0)
        ////        {
        ////            DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 1];

        ////            foreach (DataRow row in dtReturn.Rows)
        ////            {
        ////                object numerator = row[columnEnd];
        ////                object denominator = row[columnBegin];

        ////                if (numerator != DBNull.Value && denominator != DBNull.Value)
        ////                {
        ////                    try
        ////                    {
        ////                        int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("��", ""))
        ////                            - Convert.ToInt32(columnBegin.ColumnName.Replace("��", ""));
        ////                        row[column] = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
        ////                    }
        ////                    catch
        ////                    {
        ////                    }
        ////                }
        ////            }

        ////            //���μ��������ʵ�����Ϊ�´ε���ʼ��

        ////            columnBegin = columnEnd;
        ////        }
        ////    }
        ////    #endregion

        ////    return dtReturn;
        ////}
    }
}
