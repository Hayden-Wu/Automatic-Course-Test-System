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
    public partial class Test : Form
    {
        private string zhanghao = "";
        private Form FatherForm = null;
        private bool Close = true;
        //private bool XiaLa = false;
        string html = "";
        public Test(Form Sign_in)
        {
            InitializeComponent();
            this.FatherForm = Sign_in;
            Close = true;

            test();
            string testvalue = comboBox1.Text.Trim();

            comboBox2.DisplayMember = "";
            comboBox2.ValueMember = "specifictest";
            specifictest(testvalue);
            //XiaLa = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string testvalue = comboBox1.SelectedValue.ToString().Trim();
            string specifictestvalue = comboBox2.SelectedValue.ToString().Trim();
            Close = false;
            Specific_Test f = new Specific_Test(FatherForm);
            f.gettest(testvalue, specifictestvalue, zhanghao);
            f.Show();
            this.Close();
        }

        private void Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }
        public void getmessage(string z)
        {
            zhanghao = z;
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

                //comboBox2.DisplayMember = "";
                //comboBox2.ValueMember = "specifictest";
                comboBox2.DataSource = dt;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string testvalue = comboBox1.SelectedValue.ToString().Trim();
            specifictest(testvalue);
        }
    }
}
