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
        string S_name = "-1";
        string html = "-1";
        int beforeform = -1;
        DataTable dt = new DataTable();
        DataTable dt_ceyan = new DataTable();
        DataTable dt_chengji = new DataTable();
        public ExamineeInformation(Form Admin,int BeforeForm,string s_name)
        {
            InitializeComponent();
            FatherForm = Admin;
            S_name = s_name;
            beforeform = BeforeForm;
            Close = true;
            textBox1.Text = S_name;

            classroom(S_name);

            textBox2.Text = html;

            Score(S_name);

            DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
            dgvcs.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle = dgvcs;
            dataGridView1.DataSource = dt;
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            Administrator f = new Administrator(FatherForm);
            f.getmessage(zhanghao);
            this.Close();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            if(beforeform == 1)
            {
                InquiryByTest f = new InquiryByTest(FatherForm);
                f.Show();
                f.getmessage(zhanghao);
            }
            else
            {
                InquiryByExaminee f = new InquiryByExaminee(FatherForm);
                f.Show();
                f.getmessage(zhanghao);
            }
            this.Close();
        }

        private void ExamineeInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        void classroom(string name)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=class&username=" + name.Trim());
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=class&username=" + name.Trim());

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
                MessageBox.Show("链接失败1");
            }
        }

        void Score(string name)
        {
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=results&username=" + name.Trim());
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Examinee.ashx?action=results&username=" + name.Trim());

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
                MessageBox.Show("链接失败1");
            }

            jiexi(html);
            
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

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["specifictest"].InnerText;
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
            dt.Columns.Add("考试");
            dt.Columns.Add("成绩");
            return dt;
        }
    }
}
