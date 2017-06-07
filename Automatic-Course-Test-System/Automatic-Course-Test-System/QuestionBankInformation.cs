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
    public partial class QuestionBankInformation : Form
    {
        private bool Close = true;
        private string zhanghao = null;
        private string KeMu = null;
        private string CeYan = null;
        private Form FatherForm = null;
        public QuestionBankInformation(Form Admin)
        {
            InitializeComponent();
            FatherForm = Admin;
            Close = true;
            groupBox2.Hide();
            groupBox3.Hide();
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
        public void getmessage(string z)
        {
            zhanghao = z;
        }

        public void getmessage_bank(string z, string kemu, string ceyan)
        {
            zhanghao = z;
            KeMu = kemu;
            CeYan = ceyan;
        }

        private void QuestionBankInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close == true)
            {
                Application.Exit();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                groupBox3.Hide();
                groupBox2.Show();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                groupBox2.Hide();
                groupBox3.Show();
            }
        }
    }
}
