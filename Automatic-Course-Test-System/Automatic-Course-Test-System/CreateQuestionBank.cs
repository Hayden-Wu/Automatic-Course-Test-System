using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Automatic_Course_Test_System
{
    public partial class CreateQuestionBank : Form
    {
        private bool Close = true;
        private Form FatherForm;
        private string zhanghao = null;
        string html = "";
        public CreateQuestionBank(Form Admin)
        {
            InitializeComponent();
            FatherForm = Admin;
            Close = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string KeMu = textBox1.Text.Trim();
            string CeYan = textBox2.Text.Trim();
            Close = false;
            QuestionBankInformation f = new QuestionBankInformation(FatherForm);
            f.getmessage_bank(zhanghao, KeMu, CeYan);
            f.Show();
            this.Close();
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void CreateQuestionBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

    }
}
