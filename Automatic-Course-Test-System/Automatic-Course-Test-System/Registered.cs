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
    public partial class Registered : Form
    {
        public Registered()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sign_in f = new Sign_in();
            this.Close();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && (textBox2.Text == textBox3.Text))
            {
                MessageBox.Show("注册成功");
                this.Close();
            }
            else if(textBox2.Text!=textBox3.Text)
            {
                MessageBox.Show("请输入相同密码");
                textBox3.Select();
            }
            else
            {
                MessageBox.Show("注册失败");
                textBox1.Select();
            }
        }
    }
}
