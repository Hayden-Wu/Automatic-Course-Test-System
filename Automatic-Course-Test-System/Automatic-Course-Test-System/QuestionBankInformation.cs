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
    public partial class QuestionBankInformation : Form
    {
        private bool Close = true;
        private string zhanghao = null;
        private string KeMu = null;
        private string CeYan = null;
        private Form FatherForm = null;
        private int num = 0;
        private int nownum = 0;
        private int zongshu;
        DataTable dt = new DataTable();
        DataTable dtnum = new DataTable();
        public QuestionBankInformation(Form Admin)
        {
            InitializeComponent();
            FatherForm = Admin;
            Close = true;
            groupBox2.Hide();
            groupBox3.Hide();
        }

        public void gettest(string c, string k, string z)
        {

            KeMu = c;
            CeYan = k;
            zhanghao = z;
            string html = "";
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=question&specifictest=" + CeYan);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=question&specifictest=" + CeYan);
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
                dataGridView1.DataSource = dtnum;


                label1.Text = "第" + dt.Rows[0]["testnumber"].ToString() + "题";
                nownum = 0;
                textBox1.Text = dt.Rows[0]["question"].ToString();
                if (int.Parse(dt.Rows[0]["type"].ToString()) == 1)
                {

                    groupBox3.Hide();
                    groupBox2.Show();
                    radioButton1.Text = "A." + dt.Rows[0]["choiceanswerA"].ToString();
                    radioButton2.Text = "B." + dt.Rows[0]["choiceanswerB"].ToString();
                    radioButton3.Text = "C." + dt.Rows[0]["choiceanswerC"].ToString();
                    radioButton4.Text = "D." + dt.Rows[0]["choiceanswerD"].ToString();
                }
                else
                {
                    groupBox2.Hide();
                    groupBox3.Show();

                }
            }
            catch
            {
                MessageBox.Show("链接失败123");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close = false;
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }


        private void QuestionBankInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close == true)
            {
                Application.Exit();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                groupBox3.Hide();
                groupBox2.Show();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                groupBox2.Hide();
                groupBox3.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

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
                DataColumn dc1 = new DataColumn("testnumber", typeof(string));
                dtnum.Columns.Add(dc1);

                for (int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    DataRow drnum = dtnum.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["testnumber"].InnerText;
                    drnum["testnumber"] = "第" + node.Attributes["testnumber"].InnerText + "题";
                    for (int j = 1; j < dt.Columns.Count; ++j)
                    {
                        dr[dt.Columns[j].ColumnName] = node.ChildNodes[j - 1].InnerText.Trim();
                    }
                    dt.Rows.Add(dr);
                    dtnum.Rows.Add(drnum);
                }
                zongshu = nodelist.Count;


            }
        }
        private DataTable DataTableColumn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("testnumber");
            dt.Columns.Add("question");
            dt.Columns.Add("type");
            dt.Columns.Add("choiceanswerA");
            dt.Columns.Add("choiceanswerB");
            dt.Columns.Add("choiceanswerC");
            dt.Columns.Add("choiceanswerD");
            return dt;
        }
    }
}
