using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace Automatic_Course_Test_System_Server
{
    /// <summary>
    /// Server_Sign 的摘要说明
    /// </summary>
    public class Server_Sign : IHttpHandler
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
            string username = httpContext.Request.QueryString["username"];
            string password = httpContext.Request.QueryString["password"];

            if (action == "sign")
                Sign(username, password);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected void Sign(string username, string password)
        {
            int login = 0;

            //SqlConnectionStringBuilder constr = new SqlConnectionStringBuilder();
            //constr.DataSource = @"(local)";
            //constr.IntegratedSecurity = true;
            //constr.InitialCatalog = "CourseTest";
            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            try
            {
                //SqlConnection conn = new SqlConnection(constr.ConnectionString);
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select type from CourseTestUser "
                    + "where username = '" + username.Trim()
                    + "' and password = '" + password.Trim() + "'";


                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();

                if (ds.Tables[0].Rows[0][0].ToString() != null)
                {
                    login = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }

            }
            catch(Exception ex)
            {
                login = 0;
            }
            finally
            { }
            //SqlCommand SC = new SqlCommand(sqlstr, conn);
            //object judge = SC.ExecuteScalar();

            httpContext.Response.Write(login);
        }
    }
}