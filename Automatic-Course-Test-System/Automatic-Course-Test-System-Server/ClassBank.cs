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

    public class Class_SpecificTest
    {
        private int testnumber;
        private string question;
        private int type;
        private string choiceanswerA;
        private string choiceanswerB;
        private string choiceanswerC;
        private string choiceanswerD;

        public Class_SpecificTest()
        { }

        public int Testnumber
        {
            get
            {
                return testnumber;
            }

            set
            {
                testnumber = value;
            }
        }

        public string Question
        {
            get
            {
                return question;
            }

            set
            {
                question = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string ChoiceanswerA
        {
            get
            {
                return choiceanswerA;
            }

            set
            {
                choiceanswerA = value;
            }
        }

        public string ChoiceanswerB
        {
            get
            {
                return choiceanswerB;
            }

            set
            {
                choiceanswerB = value;
            }
        }

        public string ChoiceanswerC
        {
            get
            {
                return choiceanswerC;
            }

            set
            {
                choiceanswerC = value;
            }
        }

        public string ChoiceanswerD
        {
            get
            {
                return choiceanswerD;
            }

            set
            {
                choiceanswerD = value;
            }
        }
    }

    public class Class_answer
    {
        private int testnumber;
        private int type;
        private string choiceanswer;
        private string answer;

        public Class_answer()
        {
        }

        public int Testnumber
        {
            get
            {
                return testnumber;
            }

            set
            {
                testnumber = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string Choiceanswer
        {
            get
            {
                return choiceanswer;
            }

            set
            {
                choiceanswer = value;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }

            set
            {
                answer = value;
            }
        }
    }

    public class Class_ChangeTest
    {
        private int testnumber;
        private string question;
        private int type;
        private string choiceanswerA;
        private string choiceanswerB;
        private string choiceanswerC;
        private string choiceanswerD;
        private string choiceanswer;
        private string answer;

        public Class_ChangeTest()
        {
        }

        public int Testnumber
        {
            get
            {
                return testnumber;
            }

            set
            {
                testnumber = value;
            }
        }

        public string Question
        {
            get
            {
                return question;
            }

            set
            {
                question = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string ChoiceanswerA
        {
            get
            {
                return choiceanswerA;
            }

            set
            {
                choiceanswerA = value;
            }
        }

        public string ChoiceanswerB
        {
            get
            {
                return choiceanswerB;
            }

            set
            {
                choiceanswerB = value;
            }
        }

        public string ChoiceanswerC
        {
            get
            {
                return choiceanswerC;
            }

            set
            {
                choiceanswerC = value;
            }
        }

        public string ChoiceanswerD
        {
            get
            {
                return choiceanswerD;
            }

            set
            {
                choiceanswerD = value;
            }
        }

        public string Choiceanswer
        {
            get
            {
                return choiceanswer;
            }

            set
            {
                choiceanswer = value;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }

            set
            {
                answer = value;
            }
        }
    }
}