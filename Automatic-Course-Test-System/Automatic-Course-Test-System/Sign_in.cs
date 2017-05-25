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

    public partial class Sign_in : Form
    {
        private string zhanghao="";
        private string mima="";
        
        public Sign_in()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registered f = new Registered(this);
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zhanghao = textBox1.Text;
            mima = textBox2.Text;
            string html="";
            try
            {
                

                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] getWeatherUrl =encoding.GetBytes("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=sign&username=" + zhanghao + "&password=" + mima);
                HttpWebRequest webReq = (HttpWebRequest)HttpWebRequest.Create("http://1725r3a792.iask.in:28445/Server_Sign.ashx?action=sign&username=" + zhanghao + "&password=" + mima);
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


            if ( html == "1")
            {
                User f = new User();
                f.getmessage(zhanghao);
                this.Hide();
                f.Show();
            }
            else if(html =="2")
            {
                Administrator f = new Administrator();
                f.getmessage(zhanghao);
                this.Hide();
                f.Show();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
                textBox2.Select();
                textBox2.Clear();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void Sign_in_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
