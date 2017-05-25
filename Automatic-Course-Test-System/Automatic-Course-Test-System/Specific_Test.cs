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
        private string ceshiming;
        private string kaoshiming;
        private Form FatherForm = null;
        private bool Close = true;
        public Specific_Test(Form Sign_in)
        {
            InitializeComponent();
            this.FatherForm = Sign_in;
        }
        public void gettest(string c,string k)
        {
            ceshiming = c;
            kaoshiming = k;
            string html = "";
            try
            {
                string getWeatherUrl = "http://60.186.67.74/Server_Operational?action=sign&ceshi=" + ceshiming + "&kaoshi=" + kaoshiming;
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
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Close = false;
            Results f = new Results(FatherForm);
           //提交

            //接受成绩

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
    }
}
