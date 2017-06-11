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
        private int lei = 0;
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

        public void gettest(string c, string k, string z,int l)
        {

            KeMu = c;
            CeYan = k;
            lei = l;
            zhanghao = z;
            string html = "";
            if (lei == 1)
            {
                try
                {
                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=questionall&specifictest=" + CeYan);
                    HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=questionall&specifictest=" + CeYan);
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
                        textBox2.Text = dt.Rows[0]["choiceanswerA"].ToString();
                        textBox3.Text = dt.Rows[0]["choiceanswerB"].ToString();
                        textBox4.Text = dt.Rows[0]["choiceanswerC"].ToString();
                        textBox5.Text = dt.Rows[0]["choiceanswerD"].ToString();
                        if (dt.Rows[num]["answer"].ToString()[0] == 'A')
                        {
                            radioButton3.Checked = true;
                        }
                        else if (dt.Rows[num]["answer"].ToString()[0] == 'B')
                        {
                            radioButton4.Checked = true;
                        }
                        else if (dt.Rows[num]["answer"].ToString()[0] == 'C')
                        {
                            radioButton5.Checked = true;
                        }
                        else
                        {
                            radioButton6.Checked = true;
                        }
                    }
                    else if (int.Parse(dt.Rows[0]["type"].ToString()) == 2)
                    {
                        radioButton2.Checked = true;
                        groupBox2.Hide();
                        groupBox3.Show();
                        textBox6.Text = dt.Rows[0]["answer"].ToString();
                    }

                }
                catch
                {
                    MessageBox.Show("链接失败123");
                }
            }
            else
            {
                button2.Hide();
                button4.Hide();
                zengjia();
                DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
                dgvcs.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle = dgvcs;
                dataGridView1.DataSource = dtnum;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            string html = "-1";
            string str = "http://1725r3a792.iask.in:28445/Server_Test.ashx?action=questionin&specifictest=" + CeYan;
            for (int i = 0; i < zongshu; i++)
            {
                str = str + "&";
                str = str + "testnumber" + i + "=";
                str = str + dt.Rows[i]["testnumber"].ToString();
                str = str + "&";
                str = str + "question" + i + "=";
                str = str + dt.Rows[i]["question"].ToString();
                str = str + "&";
                str = str + "type" + i + "=";
                str = str + dt.Rows[i]["type"].ToString();
                str = str + "&";
                str = str + "choiceanswerA" + i + "=";
                str = str + dt.Rows[i]["choiceanswerA"].ToString();
                str = str + "&";
                str = str + "choiceanswerB" + i + "=";
                str = str + dt.Rows[i]["choiceanswerB"].ToString();
                str = str + "&";
                str = str + "choiceanswerC" + i + "=";
                str = str + dt.Rows[i]["choiceanswerC"].ToString();
                str = str + "&";
                str = str + "choiceanswerD" + i + "=";
                str = str + dt.Rows[i]["choiceanswerD"].ToString();
                str = str + "&";
                str = str + "choiceanswer" + i + "=";
                if (dt.Rows[i]["type"].ToString() == "1")
                    str = str + dt.Rows[i]["answer"].ToString();
                str = str + "&";
                str = str + "answer" + i + "=";
                if (dt.Rows[i]["type"].ToString() != "1")
                    str = str + dt.Rows[i]["answer"].ToString();

            }

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes(str);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create(str);
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
                MessageBox.Show("链接失败123");
            }

            if (html == "1")
            {
                if (MessageBox.Show("修改成功", "修改成功",MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Close = false;
                    QuestionBank f = new QuestionBank(this.FatherForm);
                    f.getmessage(zhanghao);
                    f.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("修改失败");
                textBox1.Text = html;
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

        private void button3_Click(object sender, EventArgs e)
        {
            Close = false;
            QuestionBank f = new QuestionBank(this.FatherForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dt.Rows[num]["question"] = textBox1.Text;
            if (radioButton1.Checked == true)
            {
                dt.Rows[num]["type"] = "1";
                dt.Rows[num]["choiceanswerA"] = textBox2.Text;
                dt.Rows[num]["choiceanswerB"] = textBox3.Text;
                dt.Rows[num]["choiceanswerC"] = textBox4.Text;
                dt.Rows[num]["choiceanswerD"] = textBox5.Text;
                if (radioButton3.Checked == true)
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (lei == 1)
            {
                string str = "";
                str = dataGridView1.CurrentRow.Cells[0].ToString();
                str = str.Remove(0, str.Length - 3);
                str = str.Remove(1, 2);
                num = Convert.ToInt32(str);
                if (num >= zongshu)
                {
                    return;
                }
                label2.Text = "第" + (num + 1) + "题目";
                //显示题目
                textBox1.Text = dt.Rows[num]["question"].ToString();
                if (dt.Rows[num]["type"].ToString() == "1")
                {

                    radioButton1.Checked = true;
                    groupBox2.Show();
                    groupBox3.Hide();
                    textBox2.Text = dt.Rows[num]["choiceanswerA"].ToString();
                    textBox3.Text = dt.Rows[num]["choiceanswerB"].ToString();
                    textBox4.Text = dt.Rows[num]["choiceanswerC"].ToString();
                    textBox5.Text = dt.Rows[num]["choiceanswerD"].ToString();

                    if (dt.Rows[num]["answer"].ToString()[0] == 'A')
                    {
                        radioButton3.Checked = true;
                    }
                    else if (dt.Rows[num]["answer"].ToString()[0] == 'B')
                    {
                        radioButton4.Checked = true;
                    }
                    else if (dt.Rows[num]["answer"].ToString()[0] == 'C')
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
                //StringBuilder stext = new StringBuilder();
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

        private void zengjia()
        {
            dt = null;
            dt = DataTableColumn(); //具体题目表

            //题数表
            DataColumn dc1 = new DataColumn("testnumber", typeof(string));
            dtnum.Columns.Add(dc1);
            for (int i = 0; i < 20; ++i)
            {
                DataRow dr = dt.NewRow();
                DataRow drnum = dtnum.NewRow();
                
                
                drnum["testnumber"] = "第" + i+1 + "题";
                
                dt.Rows.Add(dr);
                dtnum.Rows.Add(drnum);
            }
            zongshu = 20;
        }

    }
}
