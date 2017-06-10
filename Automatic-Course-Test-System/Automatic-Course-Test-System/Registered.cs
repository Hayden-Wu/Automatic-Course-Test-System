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
        private string AdministratorPassword;

        string html = "";
        public Registered(Form Sign_in)
        {
            InitializeComponent();

            this.FatherForm = Sign_in;
            Close = true;

            label5.Hide();
            label6.Hide();
            comboBox1.Hide();
            textBox4.Hide();

            comboBox1.DisplayMember = "";
            comboBox1.ValueMember = "classroom";

            //classroom();
            //banji = comboBox1.Text.Trim();
        }

        
        /// <summary>
        /// 注册按钮，“用户名”对应username，“密码”对应password，“班级”对应class
        /// 调用Sever_Registered的register函数
        /// 注册成功返回1，用户名重复返回2
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            zhanghao = textBox1.Text.Trim();
            mima = textBox2.Text.Trim();
            string html = "";

            
            if (textBox1.Text != "" && textBox2.Text != "" && (textBox2.Text == textBox3.Text))
            {
                if (radioButton1.Checked)
                {
                    banji = comboBox1.Text.Trim();

                    try
                    {
                        Encoding encoding = Encoding.GetEncoding("utf-8");
                        byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=registereduser&username=" + zhanghao + "&password=" + mima + "&classroom=" + banji);
                        HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=registereduser&username=" + zhanghao + "&password=" + mima + "&classroom=" + banji);
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
                        Close = false;
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
                    else
                    {
                        MessageBox.Show("注册失败"+ html);
                    }
                }
                else if(radioButton2.Checked)
                {
                    AdministratorPassword = textBox4.Text.Trim();

                    try
                    {
                        Encoding encoding = Encoding.GetEncoding("utf-8");
                        byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=registeredadministrstor&username=" + zhanghao + "&password=" + mima + "&administrstorpassword=" + AdministratorPassword);
                        HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=registeredadministrstor&username=" + zhanghao + "&password=" + mima + "&administrstorpassword=" + AdministratorPassword);
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
                        Close = false;
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
                    else if (html == "3")
                    {
                        MessageBox.Show("管理员口令错误，请重新输入");
                        textBox4.Select();
                    }
                    else
                    {
                        MessageBox.Show("注册失败" + html);
                    }
                }
                else
                {
                    MessageBox.Show("请选择学生或管理员！");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
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

        /// <summary>
        /// 将班级加入下拉框中
        /// 调用Server_Registered的class函数
        /// </summary>
        private void classroom()
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=classroom");
            HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=classroom");
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

                //comboBox1.DisplayMember = "";
                //comboBox1.ValueMember = "classroom";
                comboBox1.DataSource = dt;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                label5.Show();
                comboBox1.Show();
                label6.Hide();
                textBox4.Hide();

                classroom();
                //banji = comboBox1.Text.Trim();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label6.Show();
            textBox4.Show();
            label5.Hide();
            comboBox1.Hide();
        }
    }
}
