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
    public partial class Examinee : Form
    {
        private bool Close = true;
        private string zhanghao = null;
        private Form FatherForm = null;
        public Examinee(Form Admin)
        {
            InitializeComponent();
            Close = true;
            FatherForm = Admin;
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }
    }
}
