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
        public Specific_Test(Form Sign_in)
        {
            InitializeComponent();
            this.FatherForm = Sign_in;
            Close = true;
           
            
        }
        public void gettest(string c,string k,string z)
        {
            ceshiming = c;
            kaoshiming = k;
            zhanghao = z;
            string html = "";
            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=test&kemu=" + ceshiming + "&kaoshi=" + kaoshiming);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=test&kemu=" + ceshiming + "&kaoshi=" + kaoshiming);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                anwser[num].copyto(c);
                num++;
            }

            //显示题目
        }

        private void button1_Click(object sender, EventArgs e)
        {
            num--;
            //显示题目
        }
        public void jiexi(string x)
        {
            int subject = 0;
            int test = 0;
            int testnumber = 0;
            int question = 0;
            int type = 0;
            int choice_answerA = 0;
            int choice_answerB = 0;
            int choice_answerC = 0;
            int choice_answerD = 0;
            int mark = 0;
            char[] real_subject = new char[100];
            char[] real_test = new char[100];
            char[] real_testnumber = new char[100];
            char[] real_question = new char[100];
            char[] real_type = new char[100];
            char[] real_choice_answerA = new char[100];
            char[] real_choice_answerB = new char[100];
            char[] real_choice_answerC = new char[100];
            char[] real_choice_answerD = new char[100];
            while (x.IndexOf("subject=") != -1)
            {
                Class_Test c = new Class_Test();
                subject = x.IndexOf("subject=");
                subject = subject + 8;
                mark = x.IndexOf(";");
                x.CopyTo(subject , real_subject, 0, mark - subject);
                x.Remove(subject-8, mark - subject + 9);

                test = x.IndexOf("test=");
                test = test + 5;
                mark = x.IndexOf(";");
                x.CopyTo(test , real_test, 0, mark - test);
                x.Remove(test - 5, mark - test + 6);

                testnumber = x.IndexOf("testnumber=");
                testnumber = testnumber + 11;
                mark = x.IndexOf(";");
                x.CopyTo(testnumber , real_testnumber, 0, mark - testnumber);
                x.Remove(testnumber - 11, mark - testnumber + 12);

                question = x.IndexOf("question=");
                question = question + 9;
                mark = x.IndexOf(";");
                x.CopyTo(question, real_question, 0, mark - question);
                x.Remove(question - 9, mark - question + 10);

                type = x.IndexOf("type=");
                type = type + 5;
                mark = x.IndexOf(";");
                x.CopyTo(type , real_type, 0, mark - type);
                x.Remove(type - 5, mark - type + 6);

                choice_answerA = x.IndexOf("choice_answerA=");
                choice_answerA = choice_answerA + 15;
                mark = x.IndexOf(";");
                x.CopyTo(choice_answerA , real_choice_answerA, 0, mark - choice_answerA);
                x.Remove(choice_answerA - 15, mark - choice_answerA + 16);

                choice_answerB = x.IndexOf("choice_answerB=");
                choice_answerB = choice_answerB + 15;
                mark = x.IndexOf(";");
                x.CopyTo(choice_answerB , real_choice_answerB, 0, mark - choice_answerB);
                x.Remove(choice_answerB - 15, mark - choice_answerB + 16);

                choice_answerC = x.IndexOf("choice_answerC=");
                choice_answerC = choice_answerC + 15;
                mark = x.IndexOf(";");
                x.CopyTo(choice_answerC , real_choice_answerC, 0, mark - choice_answerC);
                x.Remove(choice_answerC - 15, mark - choice_answerC + 16);

                choice_answerD = x.IndexOf("choice_answerD=");
                choice_answerD = choice_answerD + 15;
                mark = x.IndexOf(";");
                x.CopyTo(choice_answerD , real_choice_answerD, 0, mark - choice_answerD);
                x.Remove(choice_answerD - 15, mark - choice_answerD + 16);
                c.Question=real_question.ToString();
                c.Subject = real_subject.ToString();
                c.Test = real_test.ToString();
                c.Testnumber = real_testnumber.ToString();
                c.Type = Convert.ToInt32( real_type.ToString());
                c.Choice_answerA = real_choice_answerA.ToString();
                c.Choice_answerB = real_choice_answerB.ToString();
                c.Choice_answerC = real_choice_answerC.ToString();
                c.Choice_answerD = real_choice_answerD.ToString();
                ctest.Add(c);
            }
        }
    }
}
