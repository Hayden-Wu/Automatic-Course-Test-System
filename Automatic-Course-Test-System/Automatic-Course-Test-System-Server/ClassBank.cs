using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Automatic_Course_Test_System_Server
{
    public class Class_Test
    {
        private string test;
        private string specifictest;

        public Class_Test()
        { }

        public string Test
        {
            get
            {
                return test;
            }

            set
            {
                test = value;
            }
        }

        public string Specifictest
        {
            get
            {
                return specifictest;
            }

            set
            {
                specifictest = value;
            }
        }
    }
}