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
        private string zhanghao;
        private string mima;
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
                string getWeatherUrl = "http://" + Httpadd.Add + ":8080/pro/load.jsp?name=" + zhanghao + "&passward=" + mima;
                WebRequest webReq = WebRequest.Create(getWeatherUrl);
                webReq.Timeout = 2000;
                WebResponse webResp = webReq.GetResponse();
                Stream stream = webResp.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("GBK"));
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
