using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;
namespace Automatic_Course_Test_System
{
    public partial class Inquiry : Form
    {
        private string kemu="";
        private string ceshi = "";
        private string score = "";
        private string zhanghao = "";
        private bool Close = true;
        private Form FatherForm = null;
        private string html="";

        BindingSource BS = new BindingSource();

        public Inquiry(Form Sign_in)
        {
            InitializeComponent();
            FatherForm = Sign_in;
            Close = true;
            test();
            //string testvalue = comboBox1.Text.Trim();

            //comboBox2.DisplayMember = "";
            //comboBox2.ValueMember = "specifictest";
            //specifictest(testvalue);
        }

        public void getmessage(string z)
        {
            zhanghao = z;

            string testvalue = comboBox1.Text.Trim();

            comboBox2.DisplayMember = "";
            comboBox2.ValueMember = "specifictest";
            specifictest(testvalue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //kemu = comboBox1.SelectedValue.ToString().Trim();
            //ceshi = comboBox2.SelectedValue.ToString().Trim();
            //查询成绩
            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("请选择一个已考科目！");
            }
            else
            {
                kemu = comboBox1.SelectedValue.ToString().Trim();
                ceshi = comboBox2.SelectedValue.ToString().Trim();

                Close = false;
                try
                {
                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Inquire.ashx?action=score&zhanghao=" + zhanghao + "&specifictest=" + ceshi);
                    HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Inquire.ashx?action=score&zhanghao=" + zhanghao + "&specifictest=" + ceshi);
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
                    score = html;
                }
                catch
                {
                    MessageBox.Show("链接失败");
                }


                Results f = new Results(FatherForm,2);
                f.getscore(kemu, ceshi, score);
                f.getmessage(zhanghao);
                f.Show();
                this.Close();
            }
        }

        private void Inquiry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
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

            //comboBox2.DataSource = null;
            //comboBox2.Items.Clear();

            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Inquire.ashx?action=specifictest&zhanghao=" + zhanghao + "&test=" + test);
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Inquire.ashx?action=specifictest&zhanghao=" + zhanghao + "&test=" + test);
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
                //BindingSource BS = new BindingSource();
                BS.DataSource = dt;
                comboBox2.DataSource = BS;
                BS.ResetBindings(false);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string testvalue = comboBox1.SelectedValue.ToString().Trim();
            specifictest(testvalue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            User f = new User(FatherForm);
            f.getmessage(zhanghao);
            this.Close();
            f.Show();
        }
    }
}
