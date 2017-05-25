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
        private string zhanghao;
        public Administrator()
        {
            InitializeComponent();
        }
        public void getmessage(string z)
        {
            zhanghao = z;
        }
    }
}
