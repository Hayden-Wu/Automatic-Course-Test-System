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
        public Inquiry()
        {
            InitializeComponent();
        }

        private void Inquiry_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
