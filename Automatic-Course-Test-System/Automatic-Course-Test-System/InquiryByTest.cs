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
    public partial class InquiryByTest : Form
    {
        private string zhanghao = null;
        private Form FatherForm = null;
        private Form ChildForm = null;
        private bool Close = true;
        public InquiryByTest(Form Admin,Form Examinee)
        {
            InitializeComponent();
            FatherForm = Admin;
            ChildForm = Examinee;
            Close = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            ExamineeInformation f = new ExamineeInformation(FatherForm,ChildForm);
            f.Show();
            f.getmessage(zhanghao);
            this.Close();
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void InquiryByTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close==true)
            {
                Application.Exit();
            }
        }
    }
}
