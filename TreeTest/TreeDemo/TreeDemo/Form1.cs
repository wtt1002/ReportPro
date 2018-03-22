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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string rootPath = @"E:\现场勘验\case20180320060917\采集报告\DESKTOP-FRJ5C8B_20180320_061335\index.html";
        private void Form1_Load(object sender, EventArgs e)
        {
            //webBrowser1.DocumentText = "<div style='color:red;'>大家晚上好，123123</div><br><h1>标准化术语</h1>";  
            //string text = webBrowser1.DocumentText()
            
            //richTextBox1.Text = convert.Convert("<div style='color:red;'>大家晚上好，123123</div><br><h1>标准化术语</h1>");
          
           
            //testDe();
        }

        private void testDe()
        {
            HtmlToText convert = new HtmlToText();
            string strLine;
            try
            {
                FileStream aFile = new FileStream(rootPath, FileMode.Open);
                StreamReader sr = new StreamReader(aFile);
                strLine = sr.ReadToEnd();
                //richTextBox1.Text = convert.Convert(strLine);
               
                sr.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //选择案例的输出文件夹
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"E:\现场勘验\case20180320060917\采集报告\DESKTOP-FRJ5C8B_20180320_061335";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir=new DirectoryInfo(fbd.SelectedPath);
                foreach(FileInfo f in dir.GetFiles()){
                    this.label1.Text = f.FullName;
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add(label1.Text.ToString());
            CopyDir(list, "E:\\");
        }
        private void CopyDir(List<string> fileList, string dest)
        {
            string CurrentPath;
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);

            }
            //string source=@"C:\Users\zhy\Desktop\现场勘验\现场勘验";
            //CurrentPath = dest + "\\";
            //Files = Directory.GetFiles(source);
          
            foreach (string s in fileList)
            {
                if (File.Exists(s))
                {
                    File.Copy(s, dest + "\\test.xls", true);
                    //System.Diagnostics.Process.Start(dest + "\\test.xls");
                    //ConvertExcel(dest + "\\test.xls");
                }
                else
                {
                    MessageBox.Show("文件不存在");
                }

            }
            MessageBox.Show("导出成功！");
            
        }

        public void ConvertExcel(string savePath)
        {
            //将xml文件转换为标准的Excel格式 
            Object Nothing = Missing.Value;//由于yongCOM组件很多值需要用Missing.Value代替   
            Microsoft.Office.Interop.Excel.Application ExclApp = new Microsoft.Office.Interop.Excel.Application(); // 初始化
            Microsoft.Office.Interop.Excel.Workbook ExclDoc = ExclApp.Workbooks.Open(savePath, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);//打开Excl工作薄   
            try
            {
                Object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal;//获取Excl 2007文件格式   
                ExclApp.DisplayAlerts = false;
                ExclDoc.SaveAs(savePath, format, Nothing, Nothing, Nothing, Nothing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);//保存为Excl 2007格式   
            }
            catch (Exception ex) { }
            ExclDoc.Close(Nothing, Nothing, Nothing);
            ExclApp.Quit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExcelOptions excelOptions = new ExcelOptions();
            System.Data.DataTable dt = excelOptions.GetExcelData(@"E:\test.xls");
            dataGridView1.DataSource = dt;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        protected void ExportExcel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            xlApp.Visible = true;
        }

        private void testXLs(){
            OpenFileDialog ofd = new OpenFileDialog();//首先根据打开文件对话框，选择excel表格
            ofd.Filter = "表格|*.xls";//打开文件对话框筛选器
            
            string strPath;//文件完整的路径名
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    strPath = ofd.FileName;
                    MessageBox.Show(strPath);
                    //ConvertExcel(strPath);
                    string strCon = "provider=microsoft.jet.oledb.4.0;data source=" + strPath + ";extended properties=excel 8.0";//关键是红色区域
                    OleDbConnection Con = new OleDbConnection(strCon);//建立连接
                    string strSql = "select * from [Sheet1$]";//表名的写法也应注意不同，对应的excel表为sheet1，在这里要在其后加美元符号$，并用中括号
                    OleDbCommand Cmd = new OleDbCommand(strSql, Con);//建立要执行的命令
                    OleDbDataAdapter da = new OleDbDataAdapter(Cmd);//建立数据适配器
                    DataSet ds = new DataSet();//新建数据集
                    da.Fill(ds, "shyman");//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                    //指定datagridview1的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]

                    //dataGridView1.DataSource = ds.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);//捕捉异常
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string savePath=@"E:\test.xls";
            ExcelOptions excelOptions = new ExcelOptions();
            excelOptions.DeleteRows(savePath, 0, 1, -1);//删除前两行
        }
    }
}
