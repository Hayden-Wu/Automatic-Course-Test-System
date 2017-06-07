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
        private int zongshu;
        private int num = 0;
        private int nownum = 0;
        private string s = "";
        List<Class_Upload> anwser = new List<Class_Upload>();
        DataTable dt = new DataTable();
        DataTable dtnum = new DataTable();
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

                DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
                dgvcs.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.DefaultCellStyle = dgvcs;
                dataGridView1.DataSource = dtnum;
                

                label1.Text = kaoshiming + "考试";
                label2.Text = "第" + dt.Rows[0]["testnumber"].ToString() +"题";
                nownum = 0;
                textBox1.Text = dt.Rows[0]["question"].ToString();
                if (int.Parse(dt.Rows[0]["type"].ToString()) == 1)
                {

                    textBox2.Hide();
                    groupBox1.Show();
                    radioButton1.Text = "A." + dt.Rows[0]["choiceanswerA"].ToString();
                    radioButton2.Text = "B." + dt.Rows[0]["choiceanswerB"].ToString();
                    radioButton3.Text = "C." + dt.Rows[0]["choiceanswerC"].ToString();
                    radioButton4.Text = "D." + dt.Rows[0]["choiceanswerD"].ToString();
                }
                else
                {
                    groupBox1.Hide();
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
            if (num == anwser.Count)
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
            anwser.Add(c);
            }
            string str = "";
            for (int i = 0; i < anwser.Count; ++i)
            {
                if (int.Parse(dt.Rows[i]["type"].ToString()) == 1)
                {
                    str = str + (i+1) + "=" + anwser[i].Choice_answer + "&";
                }
                else
                {
                    str = str + (i+1) + "=" + anwser[i].Answer + "&";
                }
                
            }
            str=str.Remove(str.Length-1, 1);
            MessageBox.Show(str);

            string html = "";
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=answer&zhanghao="+zhanghao+"&specifictest=" + kaoshiming + "&" + str);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Test.ashx?action=answer&zhanghao=" + zhanghao + "&specifictest=" + kaoshiming + "&" + str);
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
            if (num+1 == zongshu)
            {
                MessageBox.Show("已完成所有试题，请交卷！");
                if (dt.Rows[num]["type"].ToString() != "1" && textBox2.Text != "")
                {
                    s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    s = s.Remove(1, s.Length - 1);
                    if (s != "√")
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                    }
                }
                else if (dt.Rows[num]["type"].ToString() == "1" && (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked))
                {
                    s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    s = s.Remove(1, s.Length - 1);
                    if (s != "√")
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                    }
                }
                return;
            }
            if (nownum == num&&num==anwser.Count)
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
                else if(dt.Rows[num]["type"].ToString()!="1")
                {
                    c.Answer = textBox2.Text;
                   
                }

                if (dt.Rows[num]["type"].ToString() != "1" && textBox2.Text != "")
                {
                    s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    s = s.Remove(1, s.Length - 1);
                    if (s != "√")
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                    }
                }
                else if (dt.Rows[num]["type"].ToString() == "1" && (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked))
                {
                    s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    s = s.Remove(1, s.Length - 1);
                    if (s != "√")
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                    }
                }

                c.Subject = ceshiming;
                c.Test = kaoshiming;
                c.Testnumber = Convert.ToString(num);
                num++;
                nownum++;
                anwser.Add(c);

               // MessageBox.Show(anwser[num - 1].Choice_answer);
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

                if (dt.Rows[num]["type"].ToString() != "1" && textBox2.Text != "")
                {
                    s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    s = s.Remove(1, s.Length - 1);
                    if (s != "√")
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                    }
                }
                else if (dt.Rows[num]["type"].ToString() == "1" && (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked))
                {
                    s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    s = s.Remove(1, s.Length - 1);
                    if (s != "√")
                    {
                        dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                    }
                }

                //c.Subject = ceshiming;
                c.Test = kaoshiming;
                c.Testnumber = Convert.ToString(num);
                anwser[num].copyto(c);
                num++;
                
            }
            
            label2.Text = "第" + (num + 1) + "题目";
            //显示题目
            textBox1.Text = dt.Rows[num]["question"].ToString();
            if (dt.Rows[num]["type"].ToString() == "1")
            {
                // radioButton1
                //MessageBox.Show(Convert.ToString( anwser.Count));
                //MessageBox.Show(Convert.ToString(num));
                if (anwser.Count<=num)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else
                {
                    if (anwser[num].Choice_answer == "A")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (anwser[num].Choice_answer == "B")
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
                    else
                    {
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;
                        radioButton4.Checked = false;
                    }
                }
                groupBox1.Show();
                textBox2.Hide();
                radioButton1.Text = "A." + dt.Rows[num]["choiceanswerA"].ToString();
                radioButton2.Text = "B." + dt.Rows[num]["choiceanswerB"].ToString();
                radioButton3.Text = "C." + dt.Rows[num]["choiceanswerC"].ToString();
                radioButton4.Text = "D." + dt.Rows[num]["choiceanswerD"].ToString();
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                groupBox1.Hide();
                textBox2.Show();
            }
            //  MessageBox.Show(Convert.ToString(anwser.Count));
            dataGridView1.Focus();
            dataGridView1.CurrentCell = dataGridView1.Rows[num].Cells[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(num==0)
            {
                MessageBox.Show("已是第一题");
                return;
            }
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
            else if(dt.Rows[num]["type"].ToString() != "1")
            {
                c.Answer = textBox2.Text;
                
            }
            
            c.Subject = ceshiming;
            c.Test = kaoshiming;
            c.Testnumber = Convert.ToString(num);
            if (num == nownum && num == anwser.Count)
            {
                anwser.Add(c);
               
                
            }
            else
            {
                
                anwser[num].copyto(c);
            }

            if (dt.Rows[num]["type"].ToString() != "1" && textBox2.Text != "")
            {
                s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                s = s.Remove(1, s.Length - 1);
                if (s != "√")
                {
                    dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                }
            }
            else if (dt.Rows[num]["type"].ToString() == "1" && (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked))
            {
                s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                s = s.Remove(1, s.Length - 1);
                if (s != "√")
                {
                    dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                }
            }

            num--;
            label2.Text = "第" + (num + 1) + "题目";
            //显示题目
            textBox1.Text = dt.Rows[num]["question"].ToString();
            if (dt.Rows[num]["type"].ToString() == "1")
            {
                groupBox1.Show();
                textBox2.Hide();
                radioButton1.Text = "A."+dt.Rows[num]["choiceanswerA"].ToString();
                radioButton2.Text = "B."+dt.Rows[num]["choiceanswerB"].ToString();
                radioButton3.Text = "C."+dt.Rows[num]["choiceanswerC"].ToString();
                radioButton4.Text = "D."+dt.Rows[num]["choiceanswerD"].ToString();
                if (anwser[num].Choice_answer=="A")
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
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                groupBox1.Hide();
                
                textBox2.Text = anwser[num].Answer;
                textBox2.Show();
            }
            dataGridView1.Focus();
            dataGridView1.CurrentCell = dataGridView1.Rows[num].Cells[0];
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string str="";
            str = dataGridView1.CurrentRow.Cells[0].ToString();
            str = str.Remove(0, str.Length - 3);
            str = str.Remove(1, 2);
            
            if (Convert.ToInt32( str) > nownum)
            {
                MessageBox.Show("请按顺序答题！");
                dataGridView1.Focus();
                dataGridView1.CurrentCell = dataGridView1.Rows[num].Cells[0];
                return;
            }
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
            else if (c.Test != "")
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
            if (num == nownum && num == anwser.Count)
            {
                anwser.Add(c);
            }
            else
            {
               
                anwser[num].copyto(c);
            }
            if (dt.Rows[num]["type"].ToString() != "1" && textBox2.Text != "")
            {
                s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                s = s.Remove(1, s.Length - 1);
                if (s != "√")
                {
                    dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                }
            }
            else if (dt.Rows[num]["type"].ToString() == "1" && (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked))
            {
                s = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                s = s.Remove(1, s.Length - 1);
                if (s != "√")
                {
                    dataGridView1.CurrentRow.Cells[0].Value = "√" + dataGridView1.CurrentRow.Cells[0].Value;
                }
            }
            num = Convert.ToInt32(str);
           
            label2.Text = "第" + (num + 1) + "题目";
            //显示题目
            textBox1.Text = dt.Rows[num]["question"].ToString();
            if (dt.Rows[num]["type"].ToString() == "1")
            {
                // radioButton1
                //MessageBox.Show(Convert.ToString( anwser.Count));
                //MessageBox.Show(Convert.ToString(num));
                if (anwser.Count <= num)
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else
                {
                    if (anwser[num].Choice_answer == "A")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (anwser[num].Choice_answer == "B")
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
                    else
                    {
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        radioButton3.Checked = false;
                        radioButton4.Checked = false;
                    }
                }
                groupBox1.Show();
                textBox2.Hide();
                radioButton1.Text = "A." + dt.Rows[num]["choiceanswerA"].ToString();
                radioButton2.Text = "B." + dt.Rows[num]["choiceanswerB"].ToString();
                radioButton3.Text = "C." + dt.Rows[num]["choiceanswerC"].ToString();
                radioButton4.Text = "D." + dt.Rows[num]["choiceanswerD"].ToString();
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                groupBox1.Hide();
                textBox2.Show();
            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if(textBox2.Text=="简答题")
            {
                textBox2.Text = "";
            }
        }
    }
}
