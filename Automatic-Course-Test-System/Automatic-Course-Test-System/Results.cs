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
    public partial class Results : Form
    {
        private Form FatherForm = null;
        private bool Close = true;
        public Results(Form Sign_in)
        {
            InitializeComponent();
            FatherForm = Sign_in;
        }

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            if (this.FatherForm != null)
            {
                this.FatherForm.Visible = true;
            }
            this.Close();
        }
    }
}
