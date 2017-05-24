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
    public partial class User : Form
    {
        private bool Close = true;
        public User()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close = false;
            Test f = new Test();
            f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close = false;
            Inquiry f = new Inquiry();
            f.Show();
            this.Close();
        }

        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close == true)
                Application.Exit();
        }
    }
}
