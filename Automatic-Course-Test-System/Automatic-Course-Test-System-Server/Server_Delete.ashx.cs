using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Automatic_Course_Test_System_Server
{
    /// <summary>
    /// Server_Delete 的摘要说明
    /// </summary>
    public class Server_Delete : IHttpHandler
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
            string specifictest = httpContext.Request.QueryString["specifictest"];

            if (action == "delete")
                DeleteQuestion(specifictest);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void DeleteQuestion(string specifictest)
        {
            string results = "0";

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";

            try
            {
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();

                string sqlstr = "delete from CourseTestTest where specifictest = '" + specifictest + "'";

                SqlCommand SC = new SqlCommand(sqlstr, conn);
                int mark = SC.ExecuteNonQuery();
                if (mark > 0)
                {
                    string sqlstr2 = "delete from CourseTestSpecificTest where specifictest = '" + specifictest + "'";

                    SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                    int mark2 = SC2.ExecuteNonQuery();
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