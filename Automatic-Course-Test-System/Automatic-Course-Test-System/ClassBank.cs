using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic_Course_Test_System
{
    class Class_Examinee
    {
        private string examineenumber;
        private string name;
        private string classroom;

        public Class_Examinee()
        { }

        public string Examineenumber
        {
            get
            {
                return examineenumber;
            }

            set
            {
                examineenumber = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Classroom
        {
            get
            {
                return classroom;
            }

            set
            {
                classroom = value;
            }
        }
    }

    class Class_Test
    {
        private string subject;
        private string test;
        private string testnumber;
        private string question;
        private int type;
        private string choice_answerA;
        private string choice_answerB;
        private string choice_answerC;
        private string choice_answerD;

        public Class_Test()
        { }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

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

        public string Testnumber
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

        public string Choice_answerA
        {
            get
            {
                return choice_answerA;
            }

            set
            {
                choice_answerA = value;
            }
        }

        public string Choice_answerB
        {
            get
            {
                return choice_answerB;
            }

            set
            {
                choice_answerB = value;
            }
        }

        public string Choice_answerC
        {
            get
            {
                return choice_answerC;
            }

            set
            {
                choice_answerC = value;
            }
        }

        public string Choice_answerD
        {
            get
            {
                return choice_answerD;
            }

            set
            {
                choice_answerD = value;
            }
        }
    }

    class Class_Result
    {
        private string examineenumber;
        private string subject;
        private string test;
        private string score;

        public Class_Result()
        { }

        public string Examineenumber
        {
            get
            {
                return examineenumber;
            }

            set
            {
                examineenumber = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

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

        public string Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }
    }

    class Class_Upload
    {
        private string examineenumber;
        private string subject;
        private string test;
        private string testnumber;
        private string choice_answerA;
        private string answer;

        public Class_Upload()
        { }

        public string Examineenumber
        {
            get
            {
                return examineenumber;
            }

            set
            {
                examineenumber = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

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

        public string Testnumber
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

        public string Choice_answerA
        {
            get
            {
                return choice_answerA;
            }

            set
            {
                choice_answerA = value;
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
