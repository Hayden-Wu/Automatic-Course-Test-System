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
    public partial class Administrator : Form
    {
        private bool Close = true;
        private string zhanghao;
        public Administrator()
        {
            InitializeComponent();
            Close = true;
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void Administrator_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuestionBank f = new QuestionBank(this);
            f.getmessage(zhanghao);
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Examinee f = new Examinee(this);
            f.getmessage(zhanghao);
            f.Show();
            this.Hide();
        }
    }
}
