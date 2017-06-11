using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automatic_Course_Test_System
{
    public partial class Examinee : Form
    {
        private bool Close = true;
        private string zhanghao = null;
        private Form FatherForm = null;
        public Examinee(Form Admin)
        {
            InitializeComponent();
            Close = true;
            FatherForm = Admin;
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            InquiryByTest f = new InquiryByTest(FatherForm);
            f.Show();
            f.getmessage(zhanghao);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            InquiryByExaminee f = new InquiryByExaminee(FatherForm);
            f.Show();
            f.getmessage(zhanghao);
            this.Close();
        }

        private void Examinee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close == true)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close = false;
            Administrator f = new Administrator(FatherForm);
            f.getmessage(zhanghao);
            this.Close();
            f.Show();
        }
    }
}
