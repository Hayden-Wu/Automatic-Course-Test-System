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
            string specifictest = httpContext.Request.QueryString["specifictest"];
            string xml = httpContext.Request.QueryString["xml"];

            if (action == "questionchange")
                QuestionChange(specifictest, xml);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void QuestionChange(string specifictest, string xml)
        {
            string results = "0";

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";

            try
            {
                //SqlConnection conn = new SqlConnection(constr);
                //conn.Open();

                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(xml.Trim());

                //XmlNodeList nodelist = doc.DocumentElement.GetElementsByTagName("information");

                //dt = null;
                //dt = DataTableColumn(); //具体题目表

                //for (int i = 0; i < nodelist.Count; ++i)
                //{
                //    DataRow dr = dt.NewRow();
                //    XmlNode node = nodelist[i];
                //    dr[dt.Columns[0].ColumnName] = node.Attributes["testnumber"].InnerText;
                //    for (int j = 1; j < dt.Columns.Count; ++j)
                //    {
                //        dr[dt.Columns[j].ColumnName] = node.ChildNodes[j - 1].InnerText.Trim();
                //    }
                //    dt.Rows.Add(dr);
                //    string sqlstr2;
                //    if (int.Parse(dt.Rows[i]["type"].ToString()) == 1)
                //    {
                //        sqlstr2 = "update CourseTestSpecificTest set question = '" + dt.Rows[i]["question"].ToString()
                //            + "',type = '1',choiceanswer = '" + dt.Rows[i]["answer"].ToString()
                //            + "',choiceanswerA = '" + dt.Rows[i]["choiceanswerA"].ToString()
                //            + "',choiceanswerB = '" + dt.Rows[i]["choiceanswerB"].ToString()
                //            + "',choiceanswerC = '" + dt.Rows[i]["choiceanswerC"].ToString()
                //            + "',choiceanswerD = '" + dt.Rows[i]["choiceanswerD"].ToString()
                //            + "',answer = '' where specifictest = '" + specifictest.Trim() + "' and testnumber = '" + dt.Rows[i]["testnumber"].ToString() + "'";
                //    }
                //    else
                //    {
                //        sqlstr2 = "update CourseTestSpecificTest set question = '" + dt.Rows[i]["question"].ToString()
                //            + "',type = '2',choiceanswer = '',choiceanswerA = '',choiceanswerB = '',choiceanswerC = '',choiceanswerD = ''"
                //            + ",answer = '" + dt.Rows[i]["answer"].ToString()
                //            + "' where specifictest = '" + specifictest.Trim() + "' and testnumber = '" + dt.Rows[i]["testnumber"].ToString() + "'";
                //    }
                //    SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                //    int mark = SC2.ExecuteNonQuery();
                //}

                //conn.Close();

                results = "1";

            }
            catch (Exception ex)
            {
                results = ex.Message + xml.Trim();
            }
            finally
            { }

            httpContext.Response.Write(results);
        }

        private DataTable DataTableColumn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("testnumber");
            dt.Columns.Add("question");
            dt.Columns.Add("type");
            dt.Columns.Add("choiceanswerA");
            dt.Columns.Add("choiceanswerB");
            dt.Columns.Add("choiceanswerC");
            dt.Columns.Add("choiceanswerD");
            dt.Columns.Add("answer");
            return dt;
        }
    }
}