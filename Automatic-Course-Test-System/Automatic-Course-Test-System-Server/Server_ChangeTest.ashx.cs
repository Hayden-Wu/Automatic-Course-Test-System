using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace Automatic_Course_Test_System_Server
{
    /// <summary>
    /// Server_ChangeTest 的摘要说明
    /// </summary>
    public class Server_ChangeTest : IHttpHandler
    {
        HttpContext httpContext;
        DataTable dt = new DataTable();

        public void ProcessRequest(HttpContext context)
        {
            httpContext = context;

            httpContext.Response.Buffer = true;

            httpContext.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(1);

            httpContext.Response.Expires = 0;

            httpContext.Response.CacheControl = "no-cache";

            httpContext.Response.AppendHeader("Pragma", "No-Cache");
            httpContext.Response.ContentType = "text/plain";

            string action = httpContext.Request.QueryString["action"];
            string test = httpContext.Request.QueryString["test"];
            string specifictest = httpContext.Request.QueryString["specifictest"];
            List<Class_ChangeTest> change = new List<Class_ChangeTest>();
            for(int i = 0; i < 10; i++)
            {
                Class_ChangeTest item = new Class_ChangeTest();
                item.Testnumber = int.Parse(httpContext.Request.QueryString["testnumber" + i]);
                item.Question = httpContext.Request.QueryString["question" + i];
                item.Type = int.Parse(httpContext.Request.QueryString["type" + i]);
                item.ChoiceanswerA = httpContext.Request.QueryString["choiceanswerA" + i];
                item.ChoiceanswerB = httpContext.Request.QueryString["choiceanswerB" + i];
                item.ChoiceanswerC = httpContext.Request.QueryString["choiceanswerC" + i];
                item.ChoiceanswerD = httpContext.Request.QueryString["choiceanswerD" + i];
                item.Choiceanswer = httpContext.Request.QueryString["choiceanswer" + i];
                item.Answer = httpContext.Request.QueryString["answer" + i];
                change.Add(item);
            }

            if (action == "questionchange")
                QuestionChange(specifictest, change);
            else if (action == "add")
                AddQuestion(test, specifictest, change);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void QuestionChange(string specifictest, List<Class_ChangeTest> change)
        {
            string results = "0";

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";

            try
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                for (int i = 0; i < change.Count; ++i)
                {
                    string sqlstr2;
                    if (change[i].Type == 1)
                    {
                        sqlstr2 = "update CourseTestSpecificTest set question = '" + change[i].Question
                            + "',type = '1',choiceanswer = '" + change[i].Choiceanswer
                            + "',choiceanswerA = '" + change[i].ChoiceanswerA
                            + "',choiceanswerB = '" + change[i].ChoiceanswerB
                            + "',choiceanswerC = '" + change[i].ChoiceanswerC
                            + "',choiceanswerD = '" + change[i].ChoiceanswerD
                            + "',answer = '' where specifictest = '" + specifictest.Trim() + "' and testnumber = '" + change[i].Testnumber + "'";
                    }
                    else
                    {
                        sqlstr2 = "update CourseTestSpecificTest set question = '" + change[i].Question
                            + "',type = '2',choiceanswer = '',choiceanswerA = '',choiceanswerB = '',choiceanswerC = '',choiceanswerD = ''"
                            + ",answer = '" + change[i].Answer
                            + "' where specifictest = '" + specifictest.Trim() + "' and testnumber = '" + change[i].Testnumber + "'";
                    }
                    SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                    int mark = SC2.ExecuteNonQuery();
                }

                conn.Close();

                results = "1";

            }
            catch (Exception ex)
            {
                results = ex.Message + "123";
            }
            finally
            { }

            httpContext.Response.Write(results);
        }

        private void AddQuestion(string test, string specifictest, List<Class_ChangeTest> change)
        {
            string results = "0";

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";

            try
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                string sqlstr = "insert into CourseTestTest(test,specifictest) "
                    + "values('" + test  + "','" + specifictest + "')";

                SqlCommand SC1 = new SqlCommand(sqlstr, conn);
                int mark = SC1.ExecuteNonQuery();
                if (mark > 0)
                {
                    for (int i = 0; i < change.Count; ++i)
                    {
                        string sqlstr2;

                        sqlstr2 = "insert into CourseTestSpecificTest (specifictest,testnumber,question,type,choiceanswer,choiceanswerA,choiceanswerB,choiceanswerC,choiceanswerD,answer) "
                        + " values('" + specifictest.Trim() 
                        + "','" + change[i].Testnumber
                        + "','" + change[i].Question
                        + "','" + change[i].Type 
                        + "','" + change[i].Choiceanswer 
                        + "','" + change[i].ChoiceanswerA
                        + "','" + change[i].ChoiceanswerB 
                        + "','" + change[i].ChoiceanswerC 
                        + "','" + change[i].ChoiceanswerD 
                        + "','" + change[i].Answer + "')";

                        SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                        int mark2 = SC2.ExecuteNonQuery();
                    }
                }

                conn.Close();

                results = "1";

            }
            catch (Exception ex)
            {
                results = ex.Message + "123";
            }
            finally
            { }

            httpContext.Response.Write(results);
        }
    }
}