using System;
using System.Collections.Generic;
using System.Text;
using FarPoint.Win;
using System.Drawing;
using System.Collections;

namespace Itop.Client.GYGH
{
    public class fpcommon
    {

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
        /// <param name="onerow">�����к�</param>
        /// <param name="onecol">�����к�</param>
        /// <param name="anothercol">�������к�</param>
        /// <param name="writercol">д�빫ʽ���к�</param>
        /// <param name="rowcount">���ж�����Ҫͳ��</param>
        /// <param name="xiaoshuwei">�������С��λ</param>
        public void Sheet_WriteFormula_OneCol_Anotercol_nopercent(FarPoint.Win.Spread.SheetView obj_sheet, int onerow, int onecol, int anothercol, int writercol, int rowcount,int xiaoshuwei)
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
        /// <summary>
        /// ������������������еĶ�̬��ѹ�б���
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="SxXjName">��Ͻ�ؼ����Ƽ�����б�</param>
        /// <param name="startrow">��̬�б�����ʼ�к�</param>
        /// <param name="obj_DY_List">��̬��ѹ�б�</param>
        public void Sheet_AddItem_ZBonlyDY(FarPoint.Win.Spread.SheetView obj_sheet, List<string[]> SxXjName, int startrow, IList<double> obj_DY_List)
        {
            //����б�������
            int dylength = obj_DY_List.Count;
            if (obj_DY_List.Count > 0)
            {
                for (int i = 0; i < SxXjName.Count; i++)
                {
                    for (int j = 0; j < obj_DY_List.Count; j++)
                    {
                        obj_sheet.SetValue(startrow + i * dylength + j, 2, obj_DY_List[j].ToString());
                    }
                    obj_sheet.AddSpanCell(startrow + i * dylength, 0, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 0, SxXjName[i][0].ToString());
                    obj_sheet.AddSpanCell(startrow + i * dylength, 1, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 1, SxXjName[i][1].ToString());
                }
            }

        }
        /// <summary>
        /// ������������Ӹ����еĶ�̬��ѹ�б��⣬���ҵ�������id��ҪתΪname
        /// </summary>
        /// <param name="obj_sheet"></param>
        /// <param name="area_key_id">areaidΪkey�Ĺ�ϣ��</param>
        /// <param name="startrow">��̬�б�����ʼ�к�</param>
        /// <param name="obj_DY_List">��̬��ѹ�б�</param>
        /// <param name="SXareaid_List">��Ͻ��������areaid�б�</param>
        /// <param name="XJareaid_List">�ؼ���������areaid�б�</param>
        public void Sheet_AddItem_FBonlyDY(FarPoint.Win.Spread.SheetView obj_sheet, Hashtable area_key_id, int startrow, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //д����������
            int dylength = obj_DY_List.Count;
            if (obj_DY_List.Count > 0)
            {
                for (int i = 0; i < (2 + SXareaid_List.Count + XJareaid_List.Count); i++)
                {
                    string areaname = "";
                    if (i == 0 || i == (SXareaid_List.Count + 1))
                    {
                        areaname = "�ϼ�";
                    }
                    else
                    {
                        if (i < SXareaid_List.Count + 1)
                        {
                            if ( area_key_id[SXareaid_List[i - 1].ToString()]!=null)
                            {
                                areaname = area_key_id[SXareaid_List[i - 1].ToString()].ToString();
                            }
                            else
                            {
                                areaname = "";
                            }
                            
                        }
                        else
                        {
                            if (area_key_id[XJareaid_List[i - SXareaid_List.Count - 2].ToString()]!=null)
                            {
                                areaname = area_key_id[XJareaid_List[i - SXareaid_List.Count - 2].ToString()].ToString();
                            }
                            else
                            {
                                areaname = "";
                            }
                        }
                    }
                    for (int j = 0; j < obj_DY_List.Count; j++)
                    {
                        obj_sheet.SetValue(startrow + i * dylength + j, 2, obj_DY_List[j].ToString());
                    }
                    obj_sheet.AddSpanCell(startrow + i * dylength, 1, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 1, areaname);

                }
                //д��һ������
                obj_sheet.AddSpanCell(startrow, 0, (SXareaid_List.Count + 1) * dylength, 1);
                obj_sheet.SetValue(startrow, 0, "��Ͻ������");
                obj_sheet.AddSpanCell(startrow + (SXareaid_List.Count + 1) * dylength, 0, (XJareaid_List.Count + 1) * dylength, 1);
                obj_sheet.SetValue(startrow + (SXareaid_List.Count + 1) * dylength, 0, "�ؼ�������");

            }
            
        }

        /// <summary>
        /// �����ַ���С����С��Ĳ���,����һ������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FindUnits(string str)
        {
            try
            {
                if (str.Contains("(") && str.Contains(")"))
                {
                    return str.Substring(str.IndexOf("(") + 1, str.IndexOf(")") - str.IndexOf("(") - 1);
                }
                else if (str.Contains("��") && str.Contains("��"))
                {
                    return str.Substring(str.IndexOf("��") + 1, str.IndexOf("��") - str.IndexOf("��") - 1);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }
    }
    public struct Columqk
    {
        private string _colname;
        private int _CellType ;
        private int _weishu ;
        public Columqk(string a,int b,int c)
        {
            _colname = a;
            _CellType = b;
            _weishu = c;
        }
        public string colname 
        {
            get
            {
                return _colname;
            }
            set
            {
                _colname = value;
            }
        }
        public int CellType  //���Ϊ�ٷֱ���CellType="1" ����Ϊ"2"
        {
            get
            {
                return _CellType;
            }
            set
            {
                _CellType = value;
            }
        }
        public int weishu       //ΪС��������λ��
        {
            get { return _weishu; }
            set { _weishu = value; }
        }
    }
}
