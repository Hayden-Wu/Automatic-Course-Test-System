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
    public partial class Test : Form
    {
        private string zhanghao = "";
        private Form FatherForm = null;
        private bool Close = true;
        public Test(Form Sign_in)
        {
            InitializeComponent();
            this.FatherForm = Sign_in;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            Specific_Test f = new Specific_Test(FatherForm);
            f.gettest(comboBox1.Text,comboBox2.Text);
            f.Show();
            this.Close();
        }

        private void Test_FormClosing(object sender, FormClosingEventArgs e)
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
