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
    public partial class Inquiry : Form
    {
        private string kemu="";
        private string ceshi = "";
        private string score = "";
        private string zhanghao="";
        private bool Close = true;
        private Form FatherForm = null;
        public Inquiry(Form Sign_in)
        {
            InitializeComponent();
            FatherForm = Sign_in;
            Close = true;
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            kemu = comboBox1.Text;
            ceshi = comboBox2.Text;
            //查询成绩
            Close = false;
            Results f = new Results(FatherForm);
            f.getscore(kemu, ceshi, score);
            f.Show();
            this.Close();
        }

        private void Inquiry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }
    }
}
