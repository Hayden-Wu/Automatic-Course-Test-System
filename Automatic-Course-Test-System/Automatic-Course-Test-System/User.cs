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
        private string zhanghao;
        private bool Close = true;
        public User()
        {
            InitializeComponent();
            Close = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test f = new Test(this);
            f.getmessage(zhanghao);
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inquiry f = new Inquiry(this);
            f.getmessage(zhanghao);
            f.Show();
            this.Hide();
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
    }
}
