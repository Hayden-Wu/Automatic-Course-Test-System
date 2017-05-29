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
    public partial class ExamineeInformation : Form
    {
        private bool Close = true;
        private string zhanghao = null;
        private Form FatherForm = null;
        private Form ChildForm = null;
        public ExamineeInformation(Form Admin,Form Examinee)
        {
            InitializeComponent();
            FatherForm = Admin;
            ChildForm = Examinee;
            Close = true;
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            if (this.ChildForm != null)
            {
                this.ChildForm.Visible = true;
            }
            this.Close();
        }

        private void ExamineeInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }
    }
}
