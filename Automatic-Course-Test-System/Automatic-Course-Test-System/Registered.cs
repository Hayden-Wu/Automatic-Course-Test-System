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
        private Form FatherForm = null;

        public Registered(Form Sign_in)
        {
            InitializeComponent();

            this.FatherForm = Sign_in;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
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
                textBox2.Select();
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("注册失败");
                textBox1.Select();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
