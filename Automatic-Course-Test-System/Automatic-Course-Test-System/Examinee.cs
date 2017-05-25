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
        private Form FatherForm = null;
        private string zhanghao = null;
        public Examinee(Form Admin)
        {
            InitializeComponent();
            FatherForm = Admin;
            Close = true;
        }

        public void getmessage(string z)
        {
            zhanghao = z;
        }
    }
}
