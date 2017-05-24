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
        public User()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test f = new Test();
            f.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inquiry f = new Inquiry();
            f.Show();
            this.Hide();
        }

        private void User_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
