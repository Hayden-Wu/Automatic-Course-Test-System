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

    public partial class Sign_in : Form
    {
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
            if (textBox1.Text == "test" && textBox2.Text == "test")
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
