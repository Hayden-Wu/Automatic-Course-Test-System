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
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=questionall&specifictest=" + CeYan);
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
                    radioButton1.Checked = true;
                    groupBox3.Hide();
                    groupBox2.Show();
                    radioButton1.Text = "A." + dt.Rows[0]["choiceanswerA"].ToString();
                    radioButton2.Text = "B." + dt.Rows[0]["choiceanswerB"].ToString();
                    radioButton3.Text = "C." + dt.Rows[0]["choiceanswerC"].ToString();
                    radioButton4.Text = "D." + dt.Rows[0]["choiceanswerD"].ToString();
                }
                else if(int.Parse(dt.Rows[0]["type"].ToString()) == 2)
                {
                    radioButton2.Checked = true;
                    groupBox2.Hide();
                    groupBox3.Show();
                    textBox6.Text= dt.Rows[0]["answer"].ToString();
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
            dt.Rows[num]["question"] = textBox1.Text;
            if (radioButton1.Checked == true)
            {
                dt.Rows[num]["type"] = "1";
                dt.Rows[num]["choiceanswerA"] = radioButton3.Text;
                dt.Rows[num]["choiceanswerB"] = radioButton4.Text;
                dt.Rows[num]["choiceanswerC"] = radioButton5.Text;
                dt.Rows[num]["choiceanswerD"] = radioButton6.Text;
                if(radioButton3.Checked == true)
                   dt.Rows[num]["answer"] = "A";
                else if (radioButton4.Checked == true)
                   dt.Rows[num]["answer"] = "B";
                else if (radioButton5.Checked == true)
                    dt.Rows[num]["answer"] = "C";
                else if (radioButton6.Checked == true)
                    dt.Rows[num]["answer"] = "D";
            }
            else
            {
                dt.Rows[num]["type"] = "2";
                dt.Rows[num]["answer"] = textBox6.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            dt.Rows[num]["question"] = "";
            dt.Rows[num]["type"] = "";
            dt.Rows[num]["choiceanswerA"] = "";
            dt.Rows[num]["choiceanswerB"] = "";
            dt.Rows[num]["choiceanswerC"] = "";
            dt.Rows[num]["choiceanswerD"] = "";
            dt.Rows[num]["answer"] = "";
            radioButton2.Checked = true;
            groupBox3.Show();
            textBox1.Text = "";
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string str = "";
            str = dataGridView1.CurrentRow.Cells[0].ToString();
            str = str.Remove(0, str.Length - 3);
            str = str.Remove(1, 2);
            num = Convert.ToInt32(str);
            label2.Text = "第" + (num + 1) + "题目";
            //显示题目
            textBox1.Text = dt.Rows[num]["question"].ToString();
            if (dt.Rows[num]["type"].ToString() == "1")
            {

                radioButton1.Checked = true;
                groupBox2.Show();
                groupBox3.Hide();
                radioButton3.Text = "A." + dt.Rows[num]["choiceanswerA"].ToString();
                radioButton4.Text = "B." + dt.Rows[num]["choiceanswerB"].ToString();
                radioButton5.Text = "C." + dt.Rows[num]["choiceanswerC"].ToString();
                radioButton6.Text = "D." + dt.Rows[num]["choiceanswerD"].ToString();

                if(dt.Rows[num]["answer"].ToString()=="A")
                {
                    radioButton3.Checked = true;
                }
                else if(dt.Rows[num]["answer"].ToString() == "B")
                {
                    radioButton4.Checked = true;
                }
                else if(dt.Rows[num]["answer"].ToString() == "C")
                {
                    radioButton5.Checked = true;
                }
                else
                {
                    radioButton6.Checked = true;
                }
                
            }
            else
            {
                radioButton2.Checked = true;
                groupBox2.Hide();
                groupBox3.Show();
                textBox6.Text = dt.Rows[num]["answer"].ToString();
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
            dt.Columns.Add("answer");
            return dt;
        }
    }
}
