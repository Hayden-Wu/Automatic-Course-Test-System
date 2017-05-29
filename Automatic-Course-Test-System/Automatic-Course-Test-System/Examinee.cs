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
            InquiryByTest f = new InquiryByTest(FatherForm,this);
            f.Show();
            f.getmessage(zhanghao);
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            InquiryByExaminee f = new InquiryByExaminee(FatherForm,this);
            f.Show();
            f.getmessage(zhanghao);
            this.Hide();
        }

        private void Examinee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close == true)
            {
                Application.Exit();
            }
        }
    }
}
