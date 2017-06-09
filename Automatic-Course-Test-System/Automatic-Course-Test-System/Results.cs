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
    public partial class Results : Form
    {
        private Form HomeForm = null;
        private string zhanghao;
        private bool Close = true;

        public Results(Form Sign_in,int which)
        {
            InitializeComponent();
            HomeForm = Sign_in;
            Close = true;
            if(which == 1)
                button1.Hide();
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        public void getscore(string kemu,string test,string score)
        {
            textBox1.Text = kemu;
            textBox2.Text = test;
            textBox3.Text = score;
        }

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;

            Inquiry f = new Inquiry(HomeForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            User f = new User(HomeForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }
    }
}
