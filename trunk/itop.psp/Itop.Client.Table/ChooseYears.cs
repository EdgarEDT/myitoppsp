using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Common;
using Itop.Client.Chen;

namespace Itop.Client.Table
{
    public partial class FormChooseYears : DevExpress.XtraEditors.XtraForm
    {
        private List<int> listYearsForChoose = new List<int>();
        private List<ChoosedYears> listChoosedYears = new List<ChoosedYears>();
        private bool _noIncreaseRate = false;

        private DataTable dataTable = new DataTable();

        public bool NoIncreaseRate
        {
            get { return _noIncreaseRate; }
            set { _noIncreaseRate = value; }
        }

        public List<int> ListYearsForChoose
        {
            get { return listYearsForChoose; }
            set { listYearsForChoose = value; }
        }

        public List<ChoosedYears> ListChoosedYears
        {
          get { return listChoosedYears; }
          set { listChoosedYears = value; }
        }

        public FormChooseYears()
        {
            InitializeComponent();
        }

        private void ChooseYears_Load(object sender, EventArgs e)
        {
            dataTable.Columns.Add("A", typeof(string));
            dataTable.Columns.Add("B", typeof(bool));
            dataTable.Columns.Add("C", typeof(bool));

            foreach(int i in listYearsForChoose)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["A"] = i + "年";
                newRow["B"] = false;
                newRow["C"] = false;
                dataTable.Rows.Add(newRow);
            }

            gridControl1.DataSource = dataTable;

            if (_noIncreaseRate)
            {
                gridView1.Columns["C"].Visible = false;
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //选择增长率时，如果未选择，或者前面的选择数为小于1，则增长率不可选

            if(gridView1.FocusedColumn.FieldName == "C")
            {
                if ((bool)gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "B") == false)
                {
                    e.Cancel = true;
                    return;
                }

                int nSelectedBefore = 0;
                for (int i = 0; i < gridView1.FocusedRowHandle; i++)
                {
                    if((bool)gridView1.GetRowCellValue(i, "B"))
                    {
                        nSelectedBefore++;
                    }
                }

                if (nSelectedBefore < 1)
                {
                    e.Cancel = true;
                }
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //当选择取消后，增长率跟着取消
            if(e.Column.FieldName == "B" && (bool)e.Value == false)
            {
                gridView1.SetRowCellValue(e.RowHandle, "C", false);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listChoosedYears.Clear();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if ((bool)gridView1.GetRowCellValue(i, "B"))
                {
                    ChoosedYears cy = new ChoosedYears();
                    cy.Year = Convert.ToInt32(gridView1.GetRowCellValue(i, "A").ToString().Replace("年", ""));
                    cy.WithIncreaseRate = (bool)gridView1.GetRowCellValue(i, "C");
                    listChoosedYears.Add(cy);
                }
            }

            if (listChoosedYears.Count < 1)
            {
                MsgBox.Show("请至少选择 1 个年份！");
                return;
            }
            
            //在需要增长率的条件下，最后一个年份，自动设置为带增长率

            if(!_noIncreaseRate)
            {
                listChoosedYears[listChoosedYears.Count - 1].WithIncreaseRate = true;
            }

            DialogResult = DialogResult.OK;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "B", checkEdit1.Checked);
                if (!checkEdit1.Checked)
                {
                    gridView1.SetRowCellValue(i, "C", false);
                }
            }
        }
    }
}