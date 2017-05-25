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
        private Form FatherForm = null;
        public QuestionBankInformation(Form Admin)
        {
            InitializeComponent();
            FatherForm = Admin;
            Close = true;
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

        private void QuestionBankInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Close == true)
            {
                Application.Exit();
            }

        }
    }
}
