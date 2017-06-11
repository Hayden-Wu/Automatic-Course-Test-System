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
    public partial class InquiryByTest : Form
    {
        private string zhanghao = null;
        private Form FatherForm = null;
        private Form ChildForm = null;
        private bool Close = true;
        string html = "";
        string KeMu = null;
        string CeYan = null;
        string S_class = null;
        string S_name = null;
        DataTable dt = new DataTable();
        DataTable dt_class = new DataTable();
        DataTable dt_name = new DataTable();
        DataTable dt_score = new DataTable();

        public InquiryByTest(Form Admin,Form Examinee)
        {
            InitializeComponent();
            FatherForm = Admin;
            ChildForm = Examinee;
            Close = true;

            test();
            KeMu = comboBox1.Text.Trim();

            comboBox2.DisplayMember = "";
            comboBox2.ValueMember = "specifictest";
            specifictest(KeMu);
        }
        /// <summary>
        /// 按测试查询学生成绩
        /// 调用Server_InquiryByTest的inquiry函数
        /// 上传测验“specifictest”
        /// 需接收学生姓名与该测验对应成绩
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            KeMu = comboBox1.Text.Trim();
            CeYan = comboBox2.Text.Trim();
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_InquiryByTest.ashx?action=studenttest&specifictest=" + CeYan);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_InquiryByTest.ashx?action=studenttest&specifictest=" + CeYan);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            Administrator f = new Administrator(FatherForm);
            f.getmessage(zhanghao);
            this.Close();
            f.Show();
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void InquiryByTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close==true)
            {
                Application.Exit();
            }
        }

        private void test()
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=test");
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=test");
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

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(html.Trim());

            if (doc.SelectSingleNode("informations/warn") != null)
            {
                XmlNode node = doc.SelectSingleNode("informations/warn");
                string success = node.ChildNodes[0].InnerText;
                string err_msg = node.ChildNodes[1].InnerText;

                label1.Text = "success:" + success + "\r\n" + "err_msg:" + err_msg;
            }
            else
            {
                StringBuilder stext = new StringBuilder();
                XmlNodeList nodelist = doc.DocumentElement.GetElementsByTagName("information");

                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("test", typeof(string));
                dt.Columns.Add(dc1);

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["test"].InnerText;
                    dt.Rows.Add(dr);
                }

                comboBox1.DisplayMember = "";
                comboBox1.ValueMember = "test";
                comboBox1.DataSource = dt;
            }
        }

        private void specifictest(string testvalue)
        {
            string test = testvalue.Trim();

            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=specific_test&test=" + test);
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=specific_test&test=" + test);
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

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(html.Trim());

            if (doc.SelectSingleNode("informations/warn") != null)
            {
                XmlNode node = doc.SelectSingleNode("informations/warn");
                string success = node.ChildNodes[0].InnerText;
                string err_msg = node.ChildNodes[1].InnerText;

                label1.Text = "success:" + success + "\r\n" + "err_msg:" + err_msg;
            }
            else
            {
                StringBuilder stext = new StringBuilder();
                XmlNodeList nodelist = doc.DocumentElement.GetElementsByTagName("information");

                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("specifictest", typeof(string));
                dt.Columns.Add(dc1);

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["specifictest"].InnerText;
                    dt.Rows.Add(dr);
                }
                comboBox2.DataSource = dt;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            S_class = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString().Trim();
            S_name = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString().Trim();

            Close = false;
            ExamineeInformation f = new ExamineeInformation(FatherForm, ChildForm,S_class,S_name);
            f.Show();
            f.getmessage(zhanghao);
            this.Close();
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

                label1.Text = "success:" + success + "\r\n" + "err_msg:" + err_msg;
            }
            else
            {
                StringBuilder stext = new StringBuilder();
                XmlNodeList nodelist = doc.DocumentElement.GetElementsByTagName("information");

                dt = null;
                dt = DataTableColumn(); //具体题目表

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["username"].InnerText;
                    for (int j = 1; j < dt.Columns.Count; ++j)
                    {
                        dr[dt.Columns[j].ColumnName] = node.ChildNodes[j - 1].InnerText.Trim();
                    }
                    dt.Rows.Add(dr);
                }
            }
        }

        /// <summary>
        /// datatable含姓名与成绩两列
        /// </summary>
        private DataTable DataTableColumn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("姓名");
            dt.Columns.Add("成绩");
            return dt;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string testvalue = comboBox1.SelectedValue.ToString().Trim();
            specifictest(testvalue);
        }
    }
}
