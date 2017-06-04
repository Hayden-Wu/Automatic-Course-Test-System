using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;

namespace Automatic_Course_Test_System
{
    public partial class Specific_Test : Form
    {
        private List<Class_Test> ctest = new List<Class_Test>();
        private string ceshiming;
        private string zhanghao;
        private string kaoshiming;
        private Form FatherForm = null;
        private bool Close = true;
        private string score;
        private int num = 0;
        private int nownum = 0;
        List<Class_Upload> anwser = new List<Class_Upload>();
        DataTable dt = new DataTable();
        public Specific_Test(Form Sign_in)
        {
            InitializeComponent();
            this.FatherForm = Sign_in;
            Close = true;
            
            
        }
        public void gettest(string c, string k,string z)
        {
            ceshiming = c;
            kaoshiming = k;
            zhanghao = z;
            string html = "";
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=question&specifictest=" + kaoshiming);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=question&specifictest=" + kaoshiming);
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

                label1.Text = kaoshiming + "考试";
                label2.Text = "第" + dt.Rows[0]["testnumber"].ToString() +"题";
                nownum = 0;
                textBox1.Text = dt.Rows[0]["question"].ToString();
                if (int.Parse(dt.Rows[0]["type"].ToString()) == 1)
                {

                    textBox2.Hide();
                    radioButton1.Text = "A" + dt.Rows[0]["choiceanswerA"].ToString();
                    radioButton2.Text = "B" + dt.Rows[0]["choiceanswerB"].ToString();
                    radioButton3.Text = "C" + dt.Rows[0]["choiceanswerC"].ToString();
                    radioButton4.Text = "D" + dt.Rows[0]["choiceanswerD"].ToString();
                }
                else
                {
                    radioButton1.Hide();
                    radioButton2.Hide();
                    radioButton3.Hide();
                    radioButton4.Hide();
                    textBox2.Show();

                }
            }
            catch
            {
                MessageBox.Show("链接失败");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Close = false;
            Results f = new Results(FatherForm);
            //提交

            //接受成绩
            f.getscore(ceshiming, kaoshiming, score);
            f.Show();
            this.Close();
        }

        private void Specific_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nownum == num)
            {
                Class_Upload c = new Class_Upload();
                if (radioButton1.Checked)
                {
                    c.Choice_answer = "A";
                }
                else if (radioButton2.Checked)
                {
                    c.Choice_answer = "B";
                }
                else if (radioButton3.Checked)
                {
                    c.Choice_answer = "C";
                }
                else if (radioButton4.Checked)
                {
                    c.Choice_answer = "D";
                }
                else
                {
                    c.Answer = textBox2.Text;
                }
                
                c.Subject = ceshiming;
                c.Test = kaoshiming;
                c.Testnumber = Convert.ToString(num);
                num++;
                nownum++;
                anwser.Add(c);
            }
            else
            {
                Class_Upload c = new Class_Upload();
                if (radioButton1.Checked)
                {
                    c.Choice_answer = "A";
                }
                else if (radioButton2.Checked)
                {
                    c.Choice_answer = "B";
                }
                else if (radioButton3.Checked)
                {
                    c.Choice_answer = "C";
                }
                else if (radioButton4.Checked)
                {
                    c.Choice_answer = "D";
                }
                else
                {
                    c.Answer = textBox2.Text;
                }
                //c.Subject = ceshiming;
                c.Test = kaoshiming;
                c.Testnumber = Convert.ToString(num);
                anwser[num].copyto(c);
                num++;
                
            }
            if(num==ctest.Count)
            {
                MessageBox.Show("已完成所有试题，请交卷！");
                return ;
            }
            label2.Text = "第" + (num + 1) + "题目";
            //显示题目
            textBox1.Text = ctest[num].Question;
            if (ctest[num].Type == 1)
            {
                radioButton1.Show();
                radioButton2.Show();
                radioButton3.Show();
                radioButton4.Show();
                textBox2.Hide();
                radioButton1.Text = ctest[num].Choice_answerA;
                radioButton2.Text = ctest[num].Choice_answerB;
                radioButton3.Text = ctest[num].Choice_answerC;
                radioButton4.Text = ctest[num].Choice_answerD;
            }
            else
            {
                radioButton1.Hide();
                radioButton2.Hide();
                radioButton3.Hide();
                radioButton4.Hide();
                textBox2.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class_Upload c = new Class_Upload();
            if (radioButton1.Checked)
            {
                c.Choice_answer = "A";
            }
            else if (radioButton2.Checked)
            {
                c.Choice_answer = "B";
            }
            else if (radioButton3.Checked)
            {
                c.Choice_answer = "C";
            }
            else if (radioButton4.Checked)
            {
                c.Choice_answer = "D";
            }
            else if(c.Test!="")
            {
                c.Answer = textBox2.Text;
            }
            else
            {
                MessageBox.Show("请填写答案");
            }
            c.Subject = ceshiming;
            c.Test = kaoshiming;
            c.Testnumber = Convert.ToString(num);
            anwser[num].copyto(c);
           
            num--;
            label2.Text = "第" + (num + 1) + "题目";
            //显示题目
            textBox1.Text = ctest[num].Question;
            if (ctest[num].Type == 1)
            {
                radioButton1.Show();
                radioButton2.Show();
                radioButton3.Show();
                radioButton4.Show();
                textBox2.Hide();
                radioButton1.Text = ctest[num].Choice_answerA;
                radioButton2.Text = ctest[num].Choice_answerB;
                radioButton3.Text = ctest[num].Choice_answerC;
                radioButton4.Text = ctest[num].Choice_answerD;
                if(anwser[num].Choice_answer=="A")
                {
                    radioButton1.Checked = true;
                }
                else if(anwser[num].Choice_answer == "B")
                {
                    radioButton2.Checked = true;
                }
                else if (anwser[num].Choice_answer == "C")
                {
                    radioButton3.Checked = true;
                }
                else if (anwser[num].Choice_answer == "D")
                {
                    radioButton4.Checked = true;
                }
            }
            else
            {
                radioButton1.Hide();
                radioButton2.Hide();
                radioButton3.Hide();
                radioButton4.Hide();
                textBox2.Show();
                textBox2.Text = anwser[num].Answer;
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
                dt = DataTableColumn();

                for(int i = 0; i < nodelist.Count; ++i)
                {
                    DataRow dr = dt.NewRow();
                    XmlNode node = nodelist[i];
                    dr[dt.Columns[0].ColumnName] = node.Attributes["testnumber"].InnerText;
                    for(int j = 1; j < dt.Columns.Count; ++j)
                    {
                        dr[dt.Columns[j].ColumnName] = node.ChildNodes[j - 1].InnerText;
                    }
                    dt.Rows.Add(dr);
                }

                num = nodelist.Count;
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
