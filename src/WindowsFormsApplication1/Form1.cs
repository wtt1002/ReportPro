using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
            initLabel();
            initSplitContainer1();
            
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 800;
            this.Height =500;
            this.BackgroundImage = Image.FromFile("C:\\Users\\Administrator\\Desktop\\back_image.jpg");

            /*
            try
             {
                 ///获取文件夹目录
                 string filePath = "C:\\Users\\Administrator\\Desktop\\现场勘验\\case20180320060917";
                 //Console.WriteLine("该文件的目录："+filePath);
                 string dirctoryPath = Path.GetFullPath(filePath);   //-->C:\JiYF\BenXH\BenXHCMS.xml

                 ///父节点
                 string filename = Path.GetFileNameWithoutExtension(dirctoryPath);
                 TreeNode node = new TreeNode(filename);
                     ///给treeview添加节点
                     this.treeView1.Nodes.Add(node);
                     ///调用方法递归出磁盘的所有文件，并将父节点和路径传入
                     expendtree(dirctoryPath, node);

             }
             catch { }
             * */
            
        }
        public void initLabel()
        {
            //label1.Width = 1200;
            //label1.Height = 100;
            //label1.Image = Image.FromFile();
            //label1.BackColor = Color.Green;
            //label1.Text = "abcdefgh";
            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //watch.Start();//开始计时

            //watch.Stop();//停止计时

            //string elapseTime = "耗时:" + (watch.ElapsedMilliseconds);//输出时间 毫秒
            //label1.Text = elapseTime;
        }

        private void initSplitContainer1()
        {
            //splitContainer1.Width = 1200;
           // splitContainer1.Width = 
            
            initComboBoxes();

        }
        private void initComboBoxes()
        {

            ComboBox_peopleKind.Items.Add("嫌疑人");
            ComboBox_peopleKind.Items.Add("证人");

            comboBox_caseReason.Items.Add("口头传唤");
            comboBox_caseReason.Items.Add("投案自首");
            comboBox_caseReason.Items.Add("群众扭送");
            comboBox_caseReason.Items.Add("书面传唤");
            comboBox_caseReason.Items.Add("不显示");
            textBox_startTime.Width = 200;
            textBox_startTime.Text = DateTime.Now.ToString();
            textBox_endTime.Width = 200;
            button_addExaminer.Width = 20;
            button_addExaminer.BackgroundImage = Image.FromFile("C:\\Users\\Administrator\\Desktop\\image_add.png");
            //button_addExaminer.BackgroundImageLayout = 

        }

        private void expendtree(string path, TreeNode tn)
        { 
            try
             {
                 ///获取父节点目录的子目录
                 string[] paths = Directory.GetDirectories(path);
                 ///子节点
                 TreeNode subnode = new TreeNode();
                 ///通过遍历给传进来的父节点添加子节点
                 foreach (string fpath in paths)
                 {
                     string filename = Path.GetFileNameWithoutExtension(fpath);
                     subnode = new TreeNode(filename);
                     tn.Nodes.Add(subnode);
                     ///对文件夹不断递归， 得到所有文件
                     expendtree(fpath, subnode);
                 }  
             }
            catch { } 
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //this.BackgroundImage = Image.FromFile("image/back_image.jpg");
            //this.BackColor = Color.Blue;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //treeView1.ShowLines = true;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Combox_recordKind_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox_peopleKind_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void textBox_CaseNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_caseReason_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox_startTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_endTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_addExaminer_Click(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();

            //设定位置

            var screenPoint = PointToScreen(button_addExaminer.Location);
            tb.Top = screenPoint.X;
            tb.Left = screenPoint.Y + 5;

            //添加控件
            
            this.Controls.Add(tb);

            /*
            string controlMark = Guid.NewGuid().ToString();
            TextBox t1 = new TextBox();
            t1.Name = "txt_" + controlMark;
            //t1.Click += new EventHandler(btnClickEvent);
            t1.Width = 300;
            flowLayoutPanel1.Controls.Add(t1);
          
            Button btnDel = new Button();
            btnDel.Name = "del_" + controlMark;
            btnDel.Text = "删除";
            btnDel.Click += new EventHandler(delFileControl);
            flowLayoutPanel1.Controls.Add(btnDel);
             */
        }

        private void button_submit_Click(object sender, EventArgs e)
        {

        }


    }
}
