using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Automatic_Course_Test_System_Server
{
    /// <summary>
    /// Server_Test 的摘要说明
    /// </summary>
    public class Server_Test : IHttpHandler
    {
        HttpContext httpContext;

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
            string username = httpContext.Request.QueryString["zhanghao"];
            string test = httpContext.Request.QueryString["test"];
            string specifictest = httpContext.Request.QueryString["specifictest"];
            string[] answer = new string[20];
            answer[0] = httpContext.Request.QueryString["1"];
            answer[1] = httpContext.Request.QueryString["2"];
            answer[2] = httpContext.Request.QueryString["3"];
            answer[3] = httpContext.Request.QueryString["4"];
            answer[4] = httpContext.Request.QueryString["5"];
            answer[5] = httpContext.Request.QueryString["6"];
            answer[6] = httpContext.Request.QueryString["7"];
            answer[7] = httpContext.Request.QueryString["8"];
            answer[8] = httpContext.Request.QueryString["9"];
            answer[9] = httpContext.Request.QueryString["10"];
            answer[10] = httpContext.Request.QueryString["11"];
            answer[11] = httpContext.Request.QueryString["12"];
            answer[12] = httpContext.Request.QueryString["13"];
            answer[13] = httpContext.Request.QueryString["14"];
            answer[14] = httpContext.Request.QueryString["15"];
            answer[15] = httpContext.Request.QueryString["16"];
            answer[16] = httpContext.Request.QueryString["17"];
            answer[17] = httpContext.Request.QueryString["18"];
            answer[18] = httpContext.Request.QueryString["19"];
            answer[19] = httpContext.Request.QueryString["20"];

            if (action == "test")
                Test();
            else if (action == "specific_test")
                Specific_Test(test);
            else if (action == "question")
                Question(specifictest);
            else if (action == "answer")
                Answer(username, specifictest, answer);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected void Test()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclar;
            xmlDeclar = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclar);
            XmlElement xmlElement = xmlDoc.CreateElement("", "informations", "");
            xmlDoc.AppendChild(xmlElement);

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            try
            {
                //SqlConnection conn = new SqlConnection(constr.ConnectionString);
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select DISTINCT test from CourseTestTest";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();

                Automatic_Course_Test_System_Server.Class_Test model = new Class_Test();

                for(int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    model.Test = ds.Tables[0].Rows[i]["test"].ToString();
                    XmlNode root = xmlDoc.SelectSingleNode("informations");
                    XmlElement xe1 = xmlDoc.CreateElement("information");
                    xe1.SetAttribute("test", "" + model.Test + "");
                    root.AppendChild(xe1);
                }

            }
            catch (Exception ex)
            {
                XmlNode root = xmlDoc.SelectSingleNode("informations");
                XmlElement xe1 = xmlDoc.CreateElement("warn");
                XmlElement xeSub1 = xmlDoc.CreateElement("success");
                xeSub1.InnerText = "false2";
                xe1.AppendChild(xeSub1);
                XmlElement xeSub2 = xmlDoc.CreateElement("err_msg");
                xeSub2.InnerText = "" + ex.Message + "";
                xe1.AppendChild(xeSub2);
                root.AppendChild(xe1);
            }

            httpContext.Response.Write(xmlDoc.InnerXml);
        }

        protected void Specific_Test(string test)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclar;
            xmlDeclar = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclar);
            XmlElement xmlElement = xmlDoc.CreateElement("", "informations", "");
            xmlDoc.AppendChild(xmlElement);

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            try
            {
                //SqlConnection conn = new SqlConnection(constr.ConnectionString);
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select specifictest from CourseTestTest "
                    + "where test = '" + test.Trim() + "'";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();

                Automatic_Course_Test_System_Server.Class_Test model = new Class_Test();

                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    model.Specifictest = ds.Tables[0].Rows[i]["specifictest"].ToString();
                    XmlNode root = xmlDoc.SelectSingleNode("informations");
                    XmlElement xe1 = xmlDoc.CreateElement("information");
                    xe1.SetAttribute("specifictest", "" + model.Specifictest + "");
                    root.AppendChild(xe1);
                }

            }
            catch (Exception ex)
            {
                XmlNode root = xmlDoc.SelectSingleNode("informations");
                XmlElement xe1 = xmlDoc.CreateElement("warn");
                XmlElement xeSub1 = xmlDoc.CreateElement("success");
                xeSub1.InnerText = "false2";
                xe1.AppendChild(xeSub1);
                XmlElement xeSub2 = xmlDoc.CreateElement("err_msg");
                xeSub2.InnerText = "" + ex.Message + "";
                xe1.AppendChild(xeSub2);
                root.AppendChild(xe1);
            }

            httpContext.Response.Write(xmlDoc.InnerXml);
        }

        protected void Question(string specifictest)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclar;
            xmlDeclar = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclar);
            XmlElement xmlElement = xmlDoc.CreateElement("", "informations", "");
            xmlDoc.AppendChild(xmlElement);

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            try
            {
                //SqlConnection conn = new SqlConnection(constr.ConnectionString);
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select testnumber,question,type,choiceanswerA,choiceanswerB,choiceanswerC,choiceanswerD from CourseTestSpecificTest "
                    + "where specifictest = '" + specifictest.Trim() + "'";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();

                Automatic_Course_Test_System_Server.Class_SpecificTest model = new Class_SpecificTest();

                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    model.Testnumber = int.Parse(ds.Tables[0].Rows[i]["testnumber"].ToString());
                    model.Question = ds.Tables[0].Rows[i]["question"].ToString();
                    model.Type = int.Parse(ds.Tables[0].Rows[i]["type"].ToString());
                    model.ChoiceanswerA = ds.Tables[0].Rows[i]["choiceanswerA"].ToString();
                    model.ChoiceanswerB = ds.Tables[0].Rows[i]["choiceanswerB"].ToString();
                    model.ChoiceanswerC = ds.Tables[0].Rows[i]["choiceanswerC"].ToString();
                    model.ChoiceanswerD = ds.Tables[0].Rows[i]["choiceanswerD"].ToString();

                    XmlNode root = xmlDoc.SelectSingleNode("informations");
                    XmlElement xe1 = xmlDoc.CreateElement("information");
                    xe1.SetAttribute("testnumber", "" + model.Testnumber + "");
                    XmlElement xeSub1 = xmlDoc.CreateElement("question");
                    xeSub1.InnerText = "" + model.Question.Trim() + "";
                    xe1.AppendChild(xeSub1);
                    XmlElement xeSub2 = xmlDoc.CreateElement("type");
                    xeSub2.InnerText = "" + model.Type + "";
                    xe1.AppendChild(xeSub2);
                    XmlElement xeSub3 = xmlDoc.CreateElement("choiceanswerA");
                    xeSub3.InnerText = "" + model.ChoiceanswerA.Trim() + "";
                    xe1.AppendChild(xeSub3);
                    XmlElement xeSub4 = xmlDoc.CreateElement("choiceanswerB");
                    xeSub4.InnerText = "" + model.ChoiceanswerA.Trim() + "";
                    xe1.AppendChild(xeSub4);
                    XmlElement xeSub5 = xmlDoc.CreateElement("choiceanswerC");
                    xeSub5.InnerText = "" + model.ChoiceanswerA.Trim() + "";
                    xe1.AppendChild(xeSub5);
                    XmlElement xeSub6 = xmlDoc.CreateElement("choiceanswerD");
                    xeSub6.InnerText = "" + model.ChoiceanswerA.Trim() + "";
                    xe1.AppendChild(xeSub6);
                    root.AppendChild(xe1);
                }

            }
            catch (Exception ex)
            {
                XmlNode root = xmlDoc.SelectSingleNode("informations");
                XmlElement xe1 = xmlDoc.CreateElement("warn");
                XmlElement xeSub1 = xmlDoc.CreateElement("success");
                xeSub1.InnerText = "false2";
                xe1.AppendChild(xeSub1);
                XmlElement xeSub2 = xmlDoc.CreateElement("err_msg");
                xeSub2.InnerText = "" + ex.Message + "";
                xe1.AppendChild(xeSub2);
                root.AppendChild(xe1);
            }

            httpContext.Response.Write(xmlDoc.InnerXml);
        }

        protected void Answer(string username, string specifictest, string[] answer)
        {
            int results = -1;

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            try
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select testnumber,type,choiceanswer,answer from CourseTestSpecificTest "
                    + "where specifictest = '" + specifictest.Trim() + "'";


                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();

                results = 0;
                Automatic_Course_Test_System_Server.Class_answer model = new Class_answer();

                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    model.Testnumber = int.Parse(ds.Tables[0].Rows[i]["testnumber"].ToString());
                    model.Type = int.Parse(ds.Tables[0].Rows[i]["type"].ToString());
                    model.Choiceanswer = ds.Tables[0].Rows[i]["choiceanswer"].ToString();
                    model.Answer = ds.Tables[0].Rows[i]["answer"].ToString();

                    if(model.Type == 1)
                    {
                        if (model.Choiceanswer.Trim() == answer[model.Testnumber - 1].Trim())
                            results += 5;
                    }
                    else if(model.Type == 2)
                    {
                        int num = -1;
                        num = answer[model.Testnumber - 1].Trim().IndexOf(model.Answer.Trim());
                        if (num >= 0)
                            results += 5;
                    }
                }

            }
            catch (Exception ex)
            {
                results = -1;
            }
            finally
            { }

            if (results >= 0)
            {
                int oldResults = -1;

                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select * from CourseTestResults "
                    + "where username = '" + username.Trim()
                    + "' and specifictest = '" + specifictest.Trim() + "'";


                SqlCommand SC = new SqlCommand(sqlstr, conn);
                SqlDataReader SDR = SC.ExecuteReader();

                if (SDR.Read())
                {
                    SDR.Close();
                    SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                    DataSet ds = new DataSet();
                    SD.Fill(ds);

                    oldResults = Int32.Parse(ds.Tables[0].Rows[0][2].ToString());
                    if (oldResults < results)
                    {
                        string sqlstr2 = "update CourseTestResults set score = '" + results
                        + "' where username = '" + username.Trim() + "' and specifictest = '" + specifictest.Trim() + "'";
                        SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                        int mark = SC2.ExecuteNonQuery();
                    }
                }
                else
                {
                    SDR.Close();
                    string sqlstr2 = "insert into CourseTestResults(username,specifictest,score)"
                        + "values('" + username.Trim() + "','" + specifictest.Trim() + "','" + results + "')";
                    SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                    int mark = SC2.ExecuteNonQuery();
                }

                conn.Close();
            }

            httpContext.Response.Write(results);
        }
    }
}