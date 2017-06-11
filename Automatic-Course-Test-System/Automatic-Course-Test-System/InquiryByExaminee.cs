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
    public partial class InquiryByExaminee : Form
    {
        private bool Close = true;
        private string zhanghao = null;
        private Form FatherForm = null;
        private Form ChildForm = null;
        string S_class = null;
        string S_name = null;

        string html = "";

        public InquiryByExaminee(Form Admin,Form Examinee)
        {
            InitializeComponent();
            FatherForm = Admin;
            ChildForm = Examinee;
            Close = true;

            test();
            string banji = comboBox1.Text.Trim();

            comboBox2.DisplayMember = "";
            comboBox2.ValueMember = "username";
            specifictest(banji);
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            S_class = comboBox1.Text.ToString().Trim();
            S_name = comboBox2.Text.ToString().Trim();
            ExamineeInformation f = new ExamineeInformation(FatherForm,ChildForm,S_class,S_name);
            f.Show();
            f.getmessage(zhanghao);
            this.Close();
        }

        private void InquiryByExaminee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close==true)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 获取班级操作
        /// 调用Server_Examinee的test函数
        /// </summary>
        private void test()
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=test");
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=test");
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
                DataColumn dc1 = new DataColumn("classroom", typeof(string));
                dt.Columns.Add(dc1);

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["classroom"].InnerText;
                    dt.Rows.Add(dr);
                }

                comboBox1.DisplayMember = "";
                comboBox1.ValueMember = "classroom";
                comboBox1.DataSource = dt;
            }
        }

        /// <summary>
        /// 获取考生姓名，班级设为“banji”上传
        /// 调用Server_Examinee的specific_test函数
        /// </summary>
        /// <param name="testvalue"></param>
        private void specifictest(string testvalue)
        {
            string banji = testvalue.Trim();

            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=specifictest&banji=" + banji);
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=specifictest&banji=" + banji);
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
                DataColumn dc1 = new DataColumn("username", typeof(string));
                dt.Columns.Add(dc1);

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["username"].InnerText;
                    dt.Rows.Add(dr);
                }
                comboBox2.DataSource = dt;
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

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string testvalue = comboBox1.SelectedValue.ToString().Trim();
            specifictest(testvalue);
        }
    }
}
