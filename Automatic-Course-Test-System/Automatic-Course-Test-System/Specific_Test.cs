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
    public partial class Specific_Test : Form
    {
        private Form FatherForm = null;
        private bool Close = true;
        public Specific_Test(Form Sign_in)
        {
            InitializeComponent();
            this.FatherForm = Sign_in;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close = false;
            Results f = new Results(FatherForm);
            f.Show();
            this.Close();
        }

        private void Specific_Test_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }
    }
}
