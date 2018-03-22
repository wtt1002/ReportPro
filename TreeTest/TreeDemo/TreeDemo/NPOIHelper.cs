using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Reflection;
//using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
namespace TreeDemo
{
    public class ExcelOptions  
    {  
        //private Stopwatch wath = new Stopwatch();  
   
        /// <summary>  
        /// 使用COM读取Excel  
        /// </summary>  
        /// <param name="excelFilePath">路径</param>  
        /// <returns>DataTabel</returns>  
        public System.Data.DataTable GetExcelData(string excelFilePath)  
        {  
            Excel.Application app = new Excel.Application();  
            Excel.Sheets sheets;  
            Excel.Workbook workbook = null;  
            object oMissiong = System.Reflection.Missing.Value;  
            System.Data.DataTable dt = new System.Data.DataTable();  
   
            //wath.Start();  
   
            try 
            {
                if (app == null)  
                {  
                    return null;  
                }  
   
                workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);  
   
                //将数据读入到DataTable中——Start    
   
                sheets = workbook.Worksheets;  
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);//读取第一张表  
                if (worksheet == null)  
                    return null;  
   
                string cellContent;  
                int iRowCount = worksheet.UsedRange.Rows.Count;  
                int iColCount = worksheet.UsedRange.Columns.Count;  
                Excel.Range range;  
   
                //负责列头Start  
                DataColumn dc;  
                int ColumnID = 1;  
                range = (Excel.Range)worksheet.Cells[1, 1];  

                while (range.Text.ToString().Trim() != "")  
                {  
                    dc = new DataColumn();  
                    dc.DataType = System.Type.GetType("System.String");  
                    dc.ColumnName = range.Text.ToString().Trim();
                    //MessageBox.Show(dc.ToString());
                    dt.Columns.Add(dc);  
   
                    range = (Excel.Range)worksheet.Cells[1, ++ColumnID];  
                }  
                //End  
   
                for (int iRow = 2; iRow <= iRowCount; iRow++)  
                {
                    if (iRow > 10)
                    {
                        break;//只显示前十行
                    }
                    DataRow dr = dt.NewRow();  
   
                    for (int iCol = 1; iCol <= iColCount; iCol++)  
                    {  
                        range = (Excel.Range)worksheet.Cells[iRow, iCol];  
   
                        cellContent = (range.Value2 == null) ? "" : range.Text.ToString();  
   
                        //if (iRow == 1)  
                        //{  
                        //    dt.Columns.Add(cellContent);  
                        //}  
                        //else  
                        //{  
                            dr[iCol - 1] = cellContent;  
                        //}  
                    }  
   
                    //if (iRow != 1)  
                    //MessageBox.Show(dr.ToString());
                    dt.Rows.Add(dr);  
                }  
   
                //wath.Stop();  
                //TimeSpan ts = wath.Elapsed;  
   
                //将数据读入到DataTable中——End  
                return dt;  
            }  
            catch 
            {  
                   
                return null;  
            }  
            finally 
            {  
                workbook.Close(false, oMissiong, oMissiong);  
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);  
                workbook = null;  
                app.Workbooks.Close();  
                app.Quit();  
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);  
                app = null;  
                GC.Collect();  
                GC.WaitForPendingFinalizers();  
            }  
        }


        /// <summary>  
        /// 删除Excel行  
        /// </summary>  
        /// <param name="excelFilePath">Excel路径</param>  
        /// <param name="rowStart">开始行</param>  
        /// <param name="rowEnd">结束行</param>  
        /// <param name="designationRow">指定行</param>  
        /// <returns></returns>  
        public string DeleteRows(string excelFilePath, int rowStart, int rowEnd, int designationRow)
        {
            string result = "";
            Excel.Application app = new Excel.Application();
            Excel.Sheets sheets;
            Excel.Workbook workbook = null;
            object oMissiong = System.Reflection.Missing.Value;
            try
            {
                if (app == null)
                {
                    MessageBox.Show("分段读取Excel失败");
                    return "分段读取Excel失败";
                }

                workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                sheets = workbook.Worksheets;
                Excel.Worksheet worksheet = (Excel.Worksheet)sheets.get_Item(1);//读取第一张表  
                if (worksheet == null)
                {
                    MessageBox.Show("读到空");
                    return result;
                }
                    
                Excel.Range range;

                //先删除指定行，一般为列描述  
                if (designationRow != -1)
                {
                    range = (Excel.Range)worksheet.Rows[designationRow, oMissiong];
                    range.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                }
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                //Excel.Range mes=(Excel.Range)worksheet.Cells[1, 2];
                //MessageBox.Show(mes.Text.ToString());
                range = (Excel.Range)worksheet.Rows[1, oMissiong];//删除第一行
                range.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                range = (Excel.Range)worksheet.Rows[2, oMissiong];//删除第二行
                range.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                MessageBox.Show("已执行删除");
                /**int i = rowStart;
                for (int iRow = rowStart; iRow <= rowEnd; iRow++, i++)
                {
                    MessageBox.Show("删除");
                    range = (Excel.Range)worksheet.Rows[rowStart, oMissiong];
                    range.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                }**/

                //sw.Stop();
                //TimeSpan ts = sw.Elapsed;
                workbook.Save();

                //将数据读入到DataTable中——End  
                return result;
            }
            catch
            {

                return "分段读取Excel失败";
            }
            finally
            {
                workbook.Close(false, oMissiong, oMissiong);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
                app.Workbooks.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }  
   
        public void ToExcelSheet(DataSet ds, string fileName)  
        {  
            Excel.Application appExcel = new Excel.Application();  
            Excel.Workbook workbookData = null;  
            Excel.Worksheet worksheetData;  
            Excel.Range range;  
            try 
            {  
                workbookData = appExcel.Workbooks.Add(System.Reflection.Missing.Value);  
                appExcel.DisplayAlerts = false;//不显示警告  
                //xlApp.Visible = true;//excel是否可见  
                //  
                //for (int i = workbookData.Worksheets.Count; i > 0; i--)  
                //{  
                //    Microsoft.Office.Interop.Excel.Worksheet oWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbookData.Worksheets.get_Item(i);  
                //    oWorksheet.Select();  
                //    oWorksheet.Delete();  
                //}  
   
                for (int k = 0; k < ds.Tables.Count; k++)  
                {  
                    worksheetData = (Excel.Worksheet)workbookData.Worksheets.Add(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);  
                    // testnum--;  
                    if (ds.Tables[k] != null)  
                    {  
                        worksheetData.Name = ds.Tables[k].TableName;  
                        //写入标题  
                        for (int i = 0; i < ds.Tables[k].Columns.Count; i++)  
                        {  
                            worksheetData.Cells[1, i + 1] = ds.Tables[k].Columns[i].ColumnName;  
                            range = (Excel.Range)worksheetData.Cells[1, i + 1];  
                            //range.Interior.ColorIndex = 15;  
                            range.Font.Bold = true;  
                            range.NumberFormatLocal = "@";//文本格式  
                            range.EntireColumn.AutoFit();//自动调整列宽  
                            // range.WrapText = true; //文本自动换行    
                            range.ColumnWidth = 15;  
                        }  
                        //写入数值  
                        for (int r = 0; r < ds.Tables[k].Rows.Count; r++)  
                        {  
                            for (int i = 0; i < ds.Tables[k].Columns.Count; i++)  
                            {  
                                worksheetData.Cells[r + 2, i + 1] = ds.Tables[k].Rows[r][i];  
                                //Range myrange = worksheetData.get_Range(worksheetData.Cells[r + 2, i + 1], worksheetData.Cells[r + 3, i + 2]);  
                                //myrange.NumberFormatLocal = "@";//文本格式  
                                //// myrange.EntireColumn.AutoFit();//自动调整列宽  
                                ////   myrange.WrapText = true; //文本自动换行    
                                //myrange.ColumnWidth = 15;  
                            }  
                            //  rowRead++;  
                            //System.Windows.Forms.Application.DoEvents();  
                        }  
                    }  
                    worksheetData.Columns.EntireColumn.AutoFit();  
                    workbookData.Saved = true;  
                }  
            }  
            catch (Exception ex) { }  
            finally 
            {  
                workbookData.SaveCopyAs(fileName);  
                workbookData.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);  
                appExcel.Quit();  
                GC.Collect();  
            }  
        }  
       
    }
}
