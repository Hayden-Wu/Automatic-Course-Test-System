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
    public partial class QuestionBank : Form
    {
        private Form FatherForm = null;
        private bool Close = true;
        private string zhanghao = null;

        public QuestionBank(Form Admin)
        {
            InitializeComponent();
            Close = true;
            FatherForm = Admin;
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void QuestionBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close = false;
            CreateQuestionBank f = new CreateQuestionBank(FatherForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            QuestionBankInformation f = new QuestionBankInformation(FatherForm);
            f.getmessage(zhanghao);
            f.Show();
            this.Close();
        }
    }
}
