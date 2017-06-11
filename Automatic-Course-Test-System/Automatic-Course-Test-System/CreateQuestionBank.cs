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
    public partial class CreateQuestionBank : Form
    {
        private bool Close = true;
        private Form FatherForm;
        private string zhanghao = null;
        string html = "";
        public CreateQuestionBank(Form Admin)
        {
            InitializeComponent();
            FatherForm = Admin;
            Close = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string KeMu = textBox1.Text.Trim();
            string CeYan = textBox2.Text.Trim();
            Close = false;

            try
            {
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl = encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Inquire.ashx?action=creat&&specifictest=" + CeYan);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Inquire.ashx?action=creat&&specifictest=" + CeYan);
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
            if(html == "0")
            {
                MessageBox.Show("创建测验成功，请加入具体题目");
                QuestionBankInformation f = new QuestionBankInformation(FatherForm);
                f.gettest(zhanghao, KeMu, CeYan,0);
                f.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("创建测验失败,请修改测验信息");
            }
            
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void CreateQuestionBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            Administrator f = new Administrator(FatherForm);
            f.getmessage(zhanghao);
            this.Close();
            f.Show();
        }
    }
}
