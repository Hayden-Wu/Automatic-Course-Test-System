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
    public partial class Administrator : Form
    {
        private Form FatherForm = null;
        private bool Close = true;
        private string zhanghao;
        public Administrator(Form SignIn)
        {
            InitializeComponent();
            Close = true;

            this.FatherForm = SignIn;
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            QuestionBank f = new QuestionBank(this.FatherForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            Examinee f = new Examinee(this.FatherForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
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

        private void Administrator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }
    }
}
