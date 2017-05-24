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
        private bool Close = true;
        private Form FatherForm = null;
        public Inquiry(Form Sign_in)
        {
            InitializeComponent();
            FatherForm = Sign_in;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            Results f = new Results(FatherForm);
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
