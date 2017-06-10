using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Automatic_Course_Test_System
{
    public partial class ExamineeInformation : Form
    {
        private bool Close = true;
        private string zhanghao = null;
        private Form FatherForm = null;
        private Form ChildForm = null;
        string S_class = null;
        string S_name = null;
        string html = null;
        DataTable dt = new DataTable();
        DataTable dt_ceyan = new DataTable();
        DataTable dt_chengji = new DataTable();
        public ExamineeInformation(Form Admin,Form Examinee,string s_class,string s_name)
        {
            InitializeComponent();
            FatherForm = Admin;
            ChildForm = Examinee;
            S_class = s_class;
            S_name = s_name;
            Close = true;
            textBox1.Text = S_name;
            textBox2.Text = S_class;

            test();
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            if (this.ChildForm != null)
            {
                this.ChildForm.Visible = true;
            }
            this.Close();
        }

        private void ExamineeInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        void test()
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_ExamineeInformation.ashx?action=inquiry&s_class=" + S_class + "s_name=" + S_name);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_ExamineeInformation.ashx?action=inquiry&s_class=" + S_class + "s_name=" + S_name);
                webReq.Method = "post";
                webReq.ContentType = "text/xml";

                Stream outstream = webReq.GetRequestStream();
                outstream.Write(getWeatherUrl, 0, getWeatherUrl.Length);
                outstream.Flush();
                outstream.Close();

                webReq.Timeout = 2000;
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                Stream stream = webResp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, encoding);
                html = sr.ReadToEnd();
                sr.Close();
                stream.Close();

                jiexi(html);

                DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
                dgvcs.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle = dgvcs;
                dataGridView1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("链接失败123");
            }
        }
        public void jiexi(string x)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(x.Trim());

            if (doc.SelectSingleNode("informations/warn") != null)
            {
                XmlNode node = doc.SelectSingleNode("informations/warn");
                string success = node.ChildNodes[0].InnerText;
                string err_msg = node.ChildNodes[1].InnerText;

                label2.Text = "success:" + success + "\r\n" + "err_msg:" + err_msg;
            }
            else
            {
                StringBuilder stext = new StringBuilder();
                XmlNodeList nodelist = doc.DocumentElement.GetElementsByTagName("information");

                dt = null;
                dt = DataTableColumn(); //具体题目表

                //题数表
                DataColumn dc1 = new DataColumn("测验", typeof(string));
                DataColumn dc2 = new DataColumn("成绩", typeof(string));
                dt_ceyan.Columns.Add(dc1);
                dt_chengji.Columns.Add(dc2);

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    DataRow dr_ceyan = dt_ceyan.NewRow();
                    DataRow dr_chengji = dt_chengji.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["s_specifictest"].InnerText;
                    dr_ceyan["测验"] = node.Attributes["s_specifictest"].InnerText;
                    dr[dt.Columns[1].ColumnName] = node.Attributes["s_score"].InnerText;
                    dr_chengji["成绩"] = node.Attributes["s_score"].InnerText;
                    for (int j = 1; j < dt.Columns.Count; ++j)
                    {
                        dr[dt.Columns[j].ColumnName] = node.ChildNodes[j - 1].InnerText.Trim();
                    }
                    dt.Rows.Add(dr);
                    dt_ceyan.Rows.Add(dr_ceyan);
                    dt_chengji.Rows.Add(dr_chengji);;
                }
            }
        }
        /// <summary>
        /// datatable含姓名与成绩两列
        /// </summary>
        private DataTable DataTableColumn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("测验");
            dt.Columns.Add("成绩");
            return dt;
        }
    }
}
