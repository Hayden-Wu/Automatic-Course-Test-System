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
    public partial class Registered : Form
    {
        private Form FatherForm = null;
        private bool Close = true;

        private string zhanghao = null;
        private string mima = null;
        private string banji = null;

        string html = "";
        public Registered(Form Sign_in)
        {
            InitializeComponent();

            this.FatherForm = Sign_in;
            Close = true;

            test();
            banji = comboBox1.Text.Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zhanghao = textBox1.Text.Trim();
            mima = textBox2.Text.Trim();
            string html = "";

            
            if (textBox1.Text != "" && textBox2.Text != "" && (textBox2.Text == textBox3.Text))
            {
                try
                {
                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Registered.ashx?action=sign&username=" + zhanghao + "&password=" + mima + "&class=" + banji);
                    HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Registered.ashx?action=sign&username=" + zhanghao + "&password=" + mima + "&class=" + banji);
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
                }
                catch
                {
                    MessageBox.Show("链接失败");
                }
                if (html == "1")
                {
                    MessageBox.Show("注册成功");
                    if (this.FatherForm != null)
                    {
                        this.FatherForm.Visible = true;
                    }
                    this.Close();
                }
                else if (html == "2")
                {
                    MessageBox.Show("用户名已存在，请重新输入");
                    textBox1.Select();
                }
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("请输入相同密码");
                textBox2.Select();
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("注册失败,请输入正确信息");
                textBox1.Select();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void Registered_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void test()
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Registered.ashx?action=class");
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Registered.ashx?action=class");
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
                DataColumn dc1 = new DataColumn("class", typeof(string));
                dt.Columns.Add(dc1);

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["class"].InnerText;
                    dt.Rows.Add(dr);
                }

                comboBox1.DisplayMember = "";
                comboBox1.ValueMember = "class";
                comboBox1.DataSource = dt;
            }
        }
    }
}
