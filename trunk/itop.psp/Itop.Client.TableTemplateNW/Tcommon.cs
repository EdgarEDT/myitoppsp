using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win;
using System.Drawing;
using System.Collections;
using System.IO;
using FarPoint.Win;
using System.Windows.Forms;
using Itop.Client;
using Itop.Domain.Table;
namespace Itop.Client.TableTemplateNW
{
    class Tcommon
    {
        #region ���ñ���
        /// <summary>
        /// �滮��������
        /// ��User.ini�ļ��ڵ�CityName��
        /// </summary>
        public static string CityName
        {
            get
            {
                string cityname = "";
                FileINI User_Ini = new FileINI(Application.StartupPath + "\\User.ini");
                if (File.Exists(Application.StartupPath + "\\User.ini"))
                {
                    User_Ini = new FileINI(Application.StartupPath + "\\User.ini");
                    cityname = User_Ini.ReadValue("Setting", "CityName");
                    if (cityname == "")
                    {
                        User_Ini.Writue("Setting", "CityName", "��������");
                        cityname = User_Ini.ReadValue("Setting", "CityName");
                    }
                }
                return cityname;
            }
        }

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public static string ProjectID
        {
            get
            {

                return Itop.Client.MIS.ProgUID;
            }
        }
        public static int CurrentYear
        {
            get
            {
                return DateTime.Now.Year;
            }
        }
        /// <summary>
        /// �ļ�����
        /// </summary>
        public static string ExcelDir = "xls";
        /// <summary>
        /// ����·��(�Ѽ�\)
        /// </summary>
        public static string CurrentPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\";
            }
        }
        /// <summary>
        /// �ļ�·����(�Ѽ�\)
        /// </summary>
        public static string ExcelFilePath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.ExcelDir + "\\";
            }
        }
        #endregion
        #region ����������ת����
        private string[] que = new string[60] { "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", 
            "ʮһ","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��",
            "��ʮ��","��ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��",
            "��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ"};

        /// <summary>
        /// ������������תΪ���Ĵ�дһ����...
        /// </summary>
        /// <param name="number">����(0--60)</param>
        /// <returns>����ת�������������</returns>
        public string CHNumberToChar(int number)
        {
            if (number <= 10 && number > 0)
            {
                return que[number - 1];
            }
            else
            {
                return "������Χ";
            }
        }

        #endregion
        #region ���㼸��������
        /// <summary>
        /// ���㼸�������� (lastdouble/basedouble)^1/num-1
        /// </summary>
        /// <param name="basedouble"></param>
        /// <param name="lastdouble"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public double AverageIncreasing(double basedouble, double lastdouble, int num)
        {
            double db = Math.Pow(lastdouble / basedouble, 1.0 / num) - 1;
            if (db.ToString() == "������" || db.ToString() == "�������")
                db = 0;
            return db;

        }
        #endregion
        #region ����⼰���
        /// <summary>
        /// ���ݱ��ʶ���ر������
        /// </summary>
        /// <param name="TableID">���ʶ</param>
        /// <returns>�ַ�</returns>
        public string GetTableTitle(string TableID)
        {
            string con = " TableID='" + TableID + "' and ProjectID='" + Tcommon.ProjectID + "'";
            IList<Ps_Table_Report> templist = Common.Services.BaseService.GetList<Ps_Table_Report>("SelectPs_Table_ReportListByConn", con);
            if (templist.Count > 0)
            {
                return templist[0].TableNewName;
            }
            else
            {
                return "���ޱ������뵽������Ϣ�����Ǽǣ�";
            }

        }
        public int[] getRowList(string str)
        {
            string[] id = str.Split(",".ToCharArray());
            int[] list = new int[id.Length - 1];
            for (int i = 0; i < id.Length - 1; i++)
            {
                if (id[i] != "")
                {
                    list[i] = Convert.ToInt32(id[i]);
                }
            }
            return list;
        }
        /// <summary>
        /// ���ݱ��ʶ���ر����
        /// </summary>
        /// <param name="TableID">���ʶ</param>
        /// <returns>��������</returns>
        public int[] GetTableYears(string TableID)
        {
            int[] intary = null;
            string con = " TableID='" + TableID + "' and ProjectID='" + Tcommon.ProjectID + "'";
            IList<Ps_Table_Report> templist = Common.Services.BaseService.GetList<Ps_Table_Report>("SelectPs_Table_ReportListByConn", con);
            if (templist.Count > 0)
            {

                string[] ary = templist[0].Years.Split('#');
                intary = new int[ary.Length];
                for (int i = 0; i < ary.Length; i++)
                {
                    intary[i] = Convert.ToInt32(ary[i]);
                }
            }
            else
            {
                intary = new int[1];
                intary[0] = CurrentYear;
            }
            return intary;

        }
        #endregion
        #region ����txt�ļ�
        private string rn = "\r\n";
        private static int QuestionNO = 0;
        private static string fileName = "Question" + DateTime.Now.ToShortDateString() + ".txt";
        /// <summary>
        /// ��¼���⵽txt �ĵ����Ա�������鿴
        /// </summary>
        /// <param name="TableNO">����</param>
        /// <param name="QuestionDes">��������</param>
        /// <param name="Reason">����ԭ��</param>
        /// <param name="Remark">��ע</param>
        public void WriteQuestion(string TableNO, string QuestionDes, string Reason, string Remark)
        {
            if (Tcommon.QuestionNO == 0)
            {
                Del_QuestionTxt();
            }
            Tcommon.QuestionNO += 1;

            string tempno = "";
            if (Tcommon.QuestionNO > 0 && Tcommon.QuestionNO < 10)
            {
                tempno = "00000" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 10 && Tcommon.QuestionNO < 100)
            {
                tempno = "0000" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 100 && Tcommon.QuestionNO < 1000)
            {
                tempno = "000" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 1000 && Tcommon.QuestionNO < 10000)
            {
                tempno = "00" + Tcommon.QuestionNO;
            }
            else if (Tcommon.QuestionNO >= 10000 && Tcommon.QuestionNO < 100000)
            {
                tempno = "0" + Tcommon.QuestionNO;
            }
            else
            {
                tempno = Tcommon.QuestionNO.ToString();
            }
            string tempstr = tempno + "      " + TableNO + "       " + QuestionDes + "       " + Reason + "    " + Remark + "  " + rn;
            string txtrar = File.ReadAllText(Tcommon.fileName);
            tempstr = txtrar + tempstr;
            StreamWriter generateTxt = new StreamWriter(Tcommon.fileName, false);
            //д��ֵ
            generateTxt.Write(tempstr);
            //�ر�
            generateTxt.Close();
        }
        private void Del_QuestionTxt()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.fileName))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.fileName);
            }
            try
            {
                StreamWriter generateTxt = new StreamWriter(Tcommon.fileName, false);
                //д��ֵ
                generateTxt.Write("�����嵥" + rn);
                //�ر�
                generateTxt.Close();
            }
            catch (Exception ex)
            {
                Tcommon.fileName = "Question" + DateTime.Now.ToShortDateString() + DateTime.Now.Second.ToString() + ".txt";
                StreamWriter generateTxt = new StreamWriter(Tcommon.fileName, false);
                //д��ֵ
                generateTxt.Write("�����嵥" + rn);
                //�ر�
                generateTxt.Close();

            }
        }
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public void Show_Question()
        {
            if (MessageBox.Show("�Ƿ�鿴������������ĵ���", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            System.Diagnostics.Process.Start(System.Windows.Forms.Application.StartupPath + "\\" + Tcommon.fileName);
        }
        #endregion
        #region farpoint����

        /// <summary>
        /// //����������ȥ�������ж���Ŀ��кͿ���
        /// </summary>
        /// <param name="tempspread"></param>
        public void SpreadRemoveEmptyCells(FarPoint.Win.Spread.FpSpread tempspread)
        {

            //�����޿յ�Ԫ��ģʽ
            FarPoint.Win.Spread.Model.INonEmptyCells nec;
            //����spread�ж��ٸ����
            int sheetscount = tempspread.Sheets.Count;
            //��������
            int rowcount = 0;
            //��������
            int colcount = 0;
            for (int m = 0; m < sheetscount; m++)
            {
                nec = (FarPoint.Win.Spread.Model.INonEmptyCells)tempspread.Sheets[m].Models.Data;
                //�����޿յ�Ԫ������
                colcount = nec.NonEmptyColumnCount;
                //�����޿յ�Ԫ������
                rowcount = nec.NonEmptyRowCount;
                tempspread.Sheets[m].RowCount = rowcount;
                tempspread.Sheets[m].ColumnCount = colcount;
            }
        }
        /// <summary>
        /// �������������fpread�е�sheet���ݣ�����sheetname,�������ݸ���ʱ�û�ʹ�õ�sheet˳�򲻻��
        /// </summary>
        /// <param name="tempspread"></param>
        public void SpreadClearSheet(FarPoint.Win.Spread.FpSpread tempspread)
        {
            //ͨ������������Ϊ�����������
            int sheetscount = tempspread.Sheets.Count;
            for (int i = 0; i < sheetscount; i++)
            {
                tempspread.Sheets[i].RowCount = 0;
                tempspread.Sheets[i].ColumnCount = 0;
            }
        }
        /// <summary>
        /// �������������ù��������е���ʾģʽΪR1C1ģʽ
        /// </summary>
        /// <param name="tempspread"></param>
        public void Sheet_Referen_R1C1(FarPoint.Win.Spread.SheetView obj_sheet)
        {

            obj_sheet.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;


        }
        /// <summary>
        /// �����������趨��Ԫ���ʽ
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row">��</param>
        /// <param name="col">��</param>
        /// <param name="Type">����1Ϊnumber��2ΪPrsent</param>
        /// <param name="DecimalPlaces"></param>
        public void SetSheetCellType(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col, int Type, int DecimalPlaces)
        {

            FarPoint.Win.Spread.CellType.PercentCellType percelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
            percelltype.DecimalPlaces = DecimalPlaces;
            FarPoint.Win.Spread.CellType.NumberCellType numcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
            numcelltype.DecimalPlaces = DecimalPlaces;
            if (Type == 1)
            {
                obj_sheet.Cells[row, col].CellType = numcelltype;
            }
            else if (Type == 2)
            {
                obj_sheet.Cells[row, col].CellType = percelltype;
            }
        }
        /// <summary>
        /// ����������д�뼸��ƽ�������� (lastColnum/baseColnum)^1/num-1
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="baseColnum">������</param>
        /// <param name="lastColnum">������</param>
        /// <param name="num">ƽ�������������У�</param>
        /// <param name="writerow">д����</param>
        /// <param name="writecol">д����</param>
        /// <param name="RowCount">ͳ����</param>
        /// <param name="DecimalPlaces">С��λ��</param>
        /// <param name="Present">�Ƿ���ʾ�ٷֱ�����</param>
        public void Sheet_WriteFormula_RowAveInsering(FarPoint.Win.Spread.SheetView obj_sheet, int baseColnum, int lastColnum, int num, int writerow, int writecol, int RowCount, int DecimalPlaces, bool Present)
        {

            FarPoint.Win.Spread.CellType.PercentCellType percelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
            percelltype.DecimalPlaces = DecimalPlaces;
            FarPoint.Win.Spread.CellType.NumberCellType numcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
            numcelltype.DecimalPlaces = DecimalPlaces;
            for (int i = 0; i < RowCount; i++)
            {
                obj_sheet.Cells[writerow + i, writecol].Formula = " Power(R" + (writerow + i + 1) + "C" + (1 + lastColnum) + "/R" + (writerow + i + 1) + "C" + (baseColnum + 1) + "," + (1.000 / num) + ")-1";
                if (Present)
                {
                    obj_sheet.Cells[writerow + i, writecol].CellType = percelltype;
                }
                else
                {
                    obj_sheet.Cells[writerow + i, writecol].CellType = numcelltype;
                }
            }
        }

        /// <summary>
        /// ����������д������͹�ʽ
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">Ҫ��Ͳ�����ʼ�к�</param>
        /// <param name="startcol">Ҫ��Ͳ�����ʼ�к�</param>
        /// <param name="itemcount">Ҫ��Ͳ��ֵ���Ŀ��</param>
        /// <param name="itemlenth">ÿ����Ŀ����</param>
        /// <param name="writerow">��ʽд�뿪ʼ��</param>
        /// <param name="writecol">��ʽд�뿪ʼ��</param>
        /// <param name="countcol">ͳ������</param>
        public void Sheet_WriteFormula_RowSum(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int itemcount, int itemlenth, int writerow, int writecol, int countcol)
        {

            for (int col = 0; col < countcol; col++)
            {
                for (int n = 0; n < itemlenth; n++)
                {
                    string SumFormula = "";
                    for (int m = 0; m < itemcount; m++)
                    {
                        SumFormula += "," + "R" + (startrow + m * itemlenth + n + 1) + "C" + (startcol + col + 1);
                    }
                    SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                    obj_sheet.Cells[writerow + n, writecol + col].Formula = SumFormula;
                }
            }

        }
        /// <summary>
        /// ����������д������͹�ʽ(ͳ�Ʋ���)
        /// 1   a
        ///     b
        ///     c
        ///     d
        /// 2   a
        ///     b
        ///     c
        ///     d
        ///��  a
        ///     b
        ///     c
        ///     d
        /// itemcount=2
        /// itemlenth=3
        /// 
        /// ��Ҫͳ��c �� ��d ��
        /// ��
        /// startrownum=2
        /// willrowcount=2
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">Ҫ��Ͳ�����ʼ�к�</param>
        /// <param name="startcol">Ҫ��Ͳ�����ʼ�к�</param>
        /// <param name="itemcount">Ҫ��Ͳ��ֵ���Ŀ��</param>
        /// <param name="itemlenth">ÿ����Ŀ����</param>
        /// <param name="writerow">��ʽд�뿪ʼ��</param>
        /// <param name="writecol">��ʽд�뿪ʼ��</param>
        /// <param name="startrownum">ͳ�ƿ�ʼ������Ŀ�е�λ�ã���0��ʼ��</param>
        /// <param name="willrowcount">��Ҫͳ�Ƶ�������С����Ŀ������</param>
        /// <param name="countcol">ͳ������</param
        public void Sheet_WriteFormula_RowSum2(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int itemcount, int itemlenth, int writerow, int writecol, int startrownum, int willrowcount, int countcol)
        {

            for (int col = 0; col < countcol; col++)
            {
                for (int n = 0; n < itemlenth; n++)
                {
                    if (n < startrownum || n > startrownum + willrowcount)
                    {

                    }
                    else
                    {
                        string SumFormula = "";
                        for (int m = 0; m < itemcount; m++)
                        {
                            SumFormula += "," + "R" + (startrow + m * itemlenth + n + 1) + "C" + (startcol + col + 1);
                        }
                        SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                        obj_sheet.Cells[writerow + n, writecol + col].Formula = SumFormula;

                    }
                }
            }

        }
        /// <summary>
        /// ����������д��ָ������͹�ʽ
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="rownum">����кŵ�����</param>
        /// <param name="startcol">��ʼ��</param>
        /// <param name="writerow">д�빫ʽ�к�</param>
        /// <param name="writecol">д�빫ʽ�к�</param>
        /// <param name="countcol">ͳ������</param>
        public void Sheet_WriteFormula_RowSum3(FarPoint.Win.Spread.SheetView obj_sheet, int[] rownum, int startcol, int writerow, int writecol, int countcol)
        {

            for (int col = 0; col < countcol; col++)
            {
                string SumFormula = "";
                for (int m = 0; m < rownum.Length; m++)
                {
                    SumFormula += "," + "R" + (rownum[m] + 1) + "C" + (startcol + col + 1);
                }
                SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                obj_sheet.Cells[writerow, writecol + col].Formula = SumFormula;


            }

        }
        /// <summary>
        /// ��������Ҫ���������ͳ��д�빫ʽ�����ֻ��һ��
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">��ʼ�к�</param>
        /// <param name="startcol">��ʼ�к�</param>
        /// <param name="countrow">ͳ��������</param>
        /// <param name="countcol">ͳ��������</param>
        /// <param name="writecol">д����</param>
        public void Sheet_WriteFormula_ColSum_WritOne(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int countrow, int countcol, int writecol)
        {
            for (int row = 0; row < countrow; row++)
            {
                string SumFormula = "";
                for (int col = 0; col < countcol; col++)
                {
                    SumFormula += "," + "R" + (startrow + row + 1) + "C" + (startcol + col + 1);
                }
                SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                obj_sheet.Cells[startrow + row, writecol].Formula = SumFormula;
            }

        }
        /// <summary>
        /// ��������Ҫ����ָ�������ͳ��д�빫ʽ
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow"></param>
        /// <param name="colnum">������</param>
        /// <param name="countrow"></param>
        /// <param name="writecol"></param>
        public void Sheet_WriteFormula_ColSum_WritOne2(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int[] colnum, int countrow,  int writecol)
        {
            for (int row = 0; row < countrow; row++)
            {
                string SumFormula = "";
                for (int col = 0; col < colnum.Length; col++)
                {
                    SumFormula += "," + "R" + (startrow + row + 1) + "C" + (colnum[col]  + 1);
                }
                SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                obj_sheet.Cells[startrow + row, writecol].Formula = SumFormula;
            }

        }
      
        /// <summary>
        /// ����������д����three=one+two������͹�ʽ
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow"></param>
        /// <param name="onecol"></param>
        /// <param name="twocol"></param>
        /// <param name="threecol"></param>
        /// <param name="rowcount"></param>
        public void Sheet_WriteFormula_OneCol_TwoCol_Threecol_sum(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int twocol, int threecol, int rowcount)
        {
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, threecol].Formula = "SUM(R" + (onerow + row + 1) + "C" + (onecol + 1) + ", R" + (onerow + row + 1) + "C" + (twocol + 1) + ")";
            }
        }
        /// <summary>
        /// ��������Ҫ����д����Ϊһ�������͹�ʽ(�жϺϲ���Ԫ��)
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">��ʼ�к�</param>
        /// <param name="startcol">��ʼ�к�</param>
        /// <param name="rowcount">Ҫͳ�Ƶ�������</param>
        /// <param name="itemcount">Ҫͳ�Ƶ�����</param>
        /// <param name="writerow">д�빫ʽ�к�</param>
        /// <param name="writecol">д�빫ʽ�к�</param>
        /// <param name="itemlenth">ÿ�����������</param>
        public void Sheet_WriteFormula_MoreColsum(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int rowcount, int itemcount, int writerow, int writecol, int itemlenth)
        {

            for (int row = 0; row < rowcount; row++)
            {
                for (int m = 0; m < itemlenth; m++)
                {
                    string SumFormula = "";
                    for (int col = 0; col < itemcount; col++)
                    {
                        //��������������кϲ������
                        if (obj_sheet.Cells[startrow + row, col * itemlenth + startcol + m].Value != null)
                        {
                            SumFormula += "," + "R" + (startrow + row + 1) + "C" + (col * itemlenth + startcol + m + 1);

                        }
                        else
                        {
                            break;
                        }

                    }
                    if (SumFormula != "")
                    {
                        SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                        obj_sheet.Cells[writerow + row, writecol + m].Formula = SumFormula;
                    }

                }

            }


        }
        /// <summary>
        /// ��������Ҫ����д����Ϊһ�������͹�ʽ(���жϺϲ���Ԫ��)
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">��ʼ�к�</param>
        /// <param name="startcol">��ʼ�к�</param>
        /// <param name="rowcount">Ҫͳ�Ƶ�������</param>
        /// <param name="itemcount">Ҫͳ�Ƶ�����</param>
        /// <param name="writerow">д�빫ʽ�к�</param>
        /// <param name="writecol">д�빫ʽ�к�</param>
        /// <param name="itemlenth">ÿ�����������</param>
        public void Sheet_WriteFormula_MoreColsum_NoSpan(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int rowcount, int itemcount, int writerow, int writecol, int itemlenth)
        {

            for (int row = 0; row < rowcount; row++)
            {
                for (int m = 0; m < itemlenth; m++)
                {
                    string SumFormula = "";
                    for (int col = 0; col < itemcount; col++)
                    {
                        SumFormula += "," + "R" + (startrow + row + 1) + "C" + (col * itemlenth + startcol + m + 1);

                    }
                    SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                    obj_sheet.Cells[writerow + row, writecol + m].Formula = SumFormula;
                }

            }
        }
        /// <summary>
        /// ��������Ҫ���ڵ�Ԫ����д������0
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">��ʼ�к�</param>
        /// <param name="startcol">��ʼ�к�</param>
        /// <param name="rowcount">������</param>
        /// <param name="colcount">������</param>
        public void Sheet_WriteZero(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int rowcount, int colcount)
        {
            for (int row = 0; row < rowcount; row++)
            {
                for (int col = 0; col < colcount; col++)
                {
                    obj_sheet.SetValue(startrow + row, startcol + col, 0);
                }
            }
        }
        /// <summary>
        /// ���������ñ��ֻ��
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_ReadOnly(FarPoint.Win.Spread.SheetView obj_sheet)
        {

            obj_sheet.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
        }
        /// <summary>
        /// �������������߿��߲�ͬʱ��Ԫ������ˮƽ��ֱ������
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_GridandCenter(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //����һ���߿���
            LineBorder border = new LineBorder(Color.Black);
            int rowcount = obj_sheet.Rows.Count;
            int colcount = obj_sheet.Columns.Count;

            for (int col = 0; col < colcount; col++)
            {
                for (int row = 0; row < rowcount; row++)
                {
                    //������
                    obj_sheet.Cells[row, col].Border = border;

                }

                //ˮƽ�ʹ�ֱ�����ж���
                obj_sheet.Columns[col].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                obj_sheet.Columns[col].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            }

        }
        /// <summary>
        /// �����кϲ���Ԫ�������
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row">��ʼ�к�</param>
        /// <param name="col">��ʼ�к�</param>
        /// <returns></returns>
        public string Sheet_find_Rownotemptycell(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col)
        {
            //���ںϲ���Ԫ��ı�񣬳��ϲ��еĵ�һ�����ҵ������⣬������Ϊnull
            //���������ڷ��غϲ��е����ݣ��ݹ������ҡ�����ֵΪ�����󡱱�ʾû�ҵ�����
            if (obj_sheet.Cells[row, col].Value != null)
            {
                return obj_sheet.Cells[row, col].Value.ToString();
            }
            else
            {
                if (row != 0)
                {
                    return Sheet_find_Rownotemptycell(obj_sheet, row - 1, col);
                }
                else
                {
                    return "!����";
                }
            }
        }
        /// <summary>
        /// �����кϲ���Ԫ�������
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row">��ʼ�к�</param>
        /// <param name="col">��ʼ�к�</param>
        /// <returns></returns>
        public string Sheet_find_Colnotemptycell(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col)
        {
            //���ںϲ���Ԫ��ı�񣬳��ϲ��еĵ�һ�����ҵ������⣬������Ϊnull
            //���������ڷ��غϲ��е����ݣ��ݹ������ҡ�����ֵΪ�����󡱱�ʾû�ҵ�����
            if (obj_sheet.Cells[row, col].Value != null)
            {
                return obj_sheet.Cells[row, col].Value.ToString();
            }
            else
            {
                if (col != 0)
                {
                    return Sheet_find_Colnotemptycell(obj_sheet, row, col - 1);
                }
                else
                {
                    return "!����";
                }
            }
        }
        /// <summary>
        /// ���ñ���������� SheetName���ϲ���һ�мӱ���
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="rowcount">����</param>
        /// <param name="colcount">����</param>
        /// <param name="title">������</param>
        /// <param name="sheetname">������ǩ��</param>
        public void Sheet_RowCol_Title_Name(FarPoint.Win.Spread.SheetView obj_sheet, int rowcount, int colcount, string title, string sheetname)
        {
            obj_sheet.RowCount = rowcount;
            obj_sheet.ColumnCount = colcount;
            obj_sheet.SheetName = sheetname;
            obj_sheet.AddSpanCell(0, 0, 1, colcount);
            obj_sheet.SetValue(0, 0, title);
            obj_sheet.Rows[0].Height = 35;
        }
        /// <summary>
        /// �����ı��Զ�����
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_Colautoenter(FarPoint.Win.Spread.FpSpread tempspread)
        {
            //FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.GeneralCellType cellType = new FarPoint.Win.Spread.CellType.GeneralCellType();
            cellType.WordWrap = true;
            for (int i = 0; i < tempspread.Sheets.Count; i++)
            {
                for (int col = 0; col < tempspread.Sheets[i].ColumnCount; col++)
                {
                    tempspread.Sheets[i].Columns[col].CellType = cellType;
                }
            }


        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="obj_sheet"></param>
        public void Sheet_Locked(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int rowcount = obj_sheet.RowCount;
            int colcount = obj_sheet.ColumnCount;

            for (int col = 0; col < colcount; col++)
            {
                obj_sheet.Columns[col].Locked = true;
            }
        }
        /// <summary>
        /// ��ָ����Ԫ��������赥Ԫ���ʽΪ��ֵ
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void Sheet_UnLockedandCellNumber(FarPoint.Win.Spread.SheetView obj_sheet, int row, int col)
        {
            obj_sheet.Cells[row, col].Locked = false;
            // obj_sheet.Cells[row, col].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
        }
        /// <summary>
        /// �������������ش�ָ���в���ָ�����ݵ�һ��ƥ����к�
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="col">Ҫ�������ݵ��к�</param>
        /// <param name="strvalue">Ҫ���ҵ��ַ���ֵ</param>
        /// <returns>�������-1��ʾû���ҵ�</returns>
        public int Sheet_Find_Value(FarPoint.Win.Spread.SheetView obj_sheet, int col, string strvalue)
        {
            int flag = 0;
            int num = 0;
            for (int row = 0; row < obj_sheet.RowCount; row++)
            {
                if (obj_sheet.Cells[row, col].Value != null && obj_sheet.Cells[row, col].Value.ToString() == strvalue)
                {
                    flag = 1;
                    num = row;
                    break;
                }
            }
            if (flag != 0)
            {
                return num;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// ����������Ӣ�����ֻ��ŵ��ַ����ĳ��ȣ���������Ϊ2��Ӣ�Ļ�����Ϊ1��
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int Text_Lenght(string str)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                byte[] byte_len = Encoding.Default.GetBytes(str.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2;  //������ȴ���1�������ģ�ռ�����ֽڣ�+2
                else
                    len += 1;  //������ȵ���1����Ӣ�ģ�ռһ���ֽڣ�+1
            }
            return len;
        }

        /// <summary>
        /// ��������Ҫ����д������ʽ������������ռ�����ٷֱ����,���������״3-6��
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="yrow">�����к�</param>
        /// <param name="ycol">�����к�</param>
        /// <param name="span">�������(0��ʾ�޼��)</param>
        /// <param name="wrow">д�빫ʽ��</param>
        /// <param name="wcol">д�빫ʽ��</param>
        /// <param name="itemcout">����Ŀ��</param>
        /// <param name="colcount">���ж�����Ҫд</param>
        /// <param name="perentrow">�����кţ��̶�����һ�У�</param>
        public void Sheet_WriteFormula_Percent_Row(FarPoint.Win.Spread.SheetView obj_sheet, int yrow, int ycol, int span, int wrow, int wcol, int itemcout, int colcount, int perentrow)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int i = 0; i < itemcout; i++)
            {
                for (int j = 0; j < colcount; j++)
                {
                    obj_sheet.Cells[wrow + i * (span + 1), wcol + j].Formula = "R" + (yrow + i * (span + 1) + 1) + "C" + (ycol + j + 1) + "/" + "R" + (perentrow + 1) + "C" + (ycol + j + 1);
                    obj_sheet.Cells[wrow + i * (span + 1), wcol + j].CellType = pct;
                }

            }
        }
        /// <summary>
        /// ��������������֮��İٷֱȣ������������״3-8�еİٷֱ�
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="startrow">��ٷֱȵ��к�</param>
        /// <param name="startcol">��ٷֱȵ��к�</param>
        /// <param name="items">��Ŀ��</param>
        /// <param name="length">ÿ����Ŀ����</param>
        /// <param name="writerow">д�빫ʽ�к�</param>
        /// <param name="writecol">д�빫ʽ�к�</param>
        public void Sheet_WriteFormula_TwoCol_Percent(FarPoint.Win.Spread.SheetView obj_sheet, int startrow, int startcol, int items, int length, int writerow, int writecol)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int i = 0; i < items; i++)
            {
                string SumFormula = "";
                //�������
                for (int j = 0; j < length; j++)
                {
                    SumFormula += "," + "R" + (startrow + i * length + j + 1) + "C" + (startcol + 1);
                }
                SumFormula = "SUM(" + SumFormula.Substring(1, SumFormula.Length - 1) + ")";
                //��д����
                for (int k = 0; k < length; k++)
                {
                    obj_sheet.Cells[writerow + i * length + k, writecol].Formula = "R" + (startrow + i * length + k + 1) + "C" + (startcol + 1) + "/" + SumFormula;
                    obj_sheet.Cells[writerow + i * length + k, writecol].CellType = pct;
                }
            }
        }
        /// <summary>
        /// ������������ĳ���еı�ֵ�� ���д�ڵ������� other/one,�аٷֱȷ���
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow">�����к�</param>
        /// <param name="onecol">�����к�</param>
        /// <param name="anothercol">�������к�</param>
        /// <param name="writercol">д�빫ʽ���к�</param>
        /// <param name="rowcount">���ж�����Ҫͳ��</param>
        public void Sheet_WriteFormula_OneCol_Anotercol_percent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anothercol, int writercol, int rowcount)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, writercol].Formula = "R" + (onerow + row + 1) + "C" + (anothercol + 1) + "/R" + (onerow + row + 1) + "C" + (onecol + 1);
                obj_sheet.Cells[onerow + row, writercol].CellType = pct;
            }
        }
        /// <summary>
        /// ������������ĳ���еı�ֵ�� ���д�ڵ������� other/one,�ްٷֱȷ���
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow"></param>
        /// <param name="onecol"></param>
        /// <param name="anotherrow"></param>
        /// <param name="writerRow"></param>
        /// <param name="colcount"></param>
        public void Sheet_WriteFormula_OneRow_AnoterRow_nopercent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anotherrow, int writerRow, int colcount)
        {
            FarPoint.Win.Spread.CellType.NumberCellType pct = new FarPoint.Win.Spread.CellType.NumberCellType();
            pct.DecimalPlaces = 2;
            for (int col = 0; col < colcount; col++)
            {
                obj_sheet.Cells[writerRow, onecol + col].Formula = "R" + (anotherrow + 1) + "C" + (onecol + col + 1) + "/R" + (onerow + 1) + "C" + (onecol + col + 1);

                obj_sheet.Cells[writerRow, onecol + col].CellType = pct;
            }
        }
        /// <summary>
        /// ������������ĳ���еı�ֵ�� ���д�ڵ������� other/one,�ްٷֱȷ���
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow">�����к�</param>
        /// <param name="onecol">�����к�</param>
        /// <param name="anothercol">�������к�</param>
        /// <param name="writercol">д�빫ʽ���к�</param>
        /// <param name="rowcount">���ж�����Ҫͳ��</param>
        /// <param name="xiaoshuwei">�������С��λ</param>
        public void Sheet_WriteFormula_OneCol_Anotercol_nopercent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anothercol, int writercol, int rowcount, int xiaoshuwei)
        {
            //����5λС��
            FarPoint.Win.Spread.CellType.NumberCellType newcelltype = new FarPoint.Win.Spread.CellType.NumberCellType();
            newcelltype.DecimalPlaces = xiaoshuwei;
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, writercol].Formula = "R" + (onerow + row + 1) + "C" + (anothercol + 1) + "/R" + (onerow + row + 1) + "C" + (onecol + 1);
                obj_sheet.Cells[onerow + row, writercol].CellType = newcelltype;

            }
        }
        /// <summary>
        /// ���������������������one/(two-three)
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="onerow"></param>
        /// <param name="onecol"></param>
        /// <param name="twocol"></param>
        /// <param name="threecol"></param>
        /// <param name="writecol"></param>
        /// <param name="rowcount"></param>
        public void Sheet_WriteFormula_OneCol_Twocol_Threecol_percent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int twocol, int threecol, int writecol, int rowcount)
        {
            FarPoint.Win.Spread.CellType.PercentCellType pct = new FarPoint.Win.Spread.CellType.PercentCellType();
            for (int row = 0; row < rowcount; row++)
            {
                obj_sheet.Cells[onerow + row, writecol].Formula = "R" + (onerow + row + 1) + "C" + (onecol + 1) + "/(R" + (onerow + row + 1) + "C" + (twocol + 1) + "-R" + (onerow + row + 1) + "C" + (threecol + 1) + ")";
                obj_sheet.Cells[onerow + row, writecol].CellType = pct;
            }

        }

        #endregion farpoint����

    }
}
