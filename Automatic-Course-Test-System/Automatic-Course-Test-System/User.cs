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
    public partial class User : Form
    {
        private Form FatherForm = null;
        private string zhanghao;
        private bool Close = true;

        public User(Form SignIn)
        {
            InitializeComponent();
            Close = true;

            this.FatherForm = SignIn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            Test f = new Test(this.FatherForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            Inquiry f = new Inquiry(this.FatherForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }

        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close = false;
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
        }
    }
}
