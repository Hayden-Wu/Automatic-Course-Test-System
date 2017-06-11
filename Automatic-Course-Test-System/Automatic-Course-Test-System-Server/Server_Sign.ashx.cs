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
            string classroom = httpContext.Request.QueryString["classroom"];
            string name = httpContext.Request.QueryString["name"];
            string administrstorpassword = httpContext.Request.QueryString["administrstorpassword"];

            if (action == "sign")
                Sign(username, password);
            else if (action == "registereduser")
                RegisteredUser(username, password, classroom, name);
            else if (action == "registeredadministrstor")
                RegisteredAdministrator(username, password, administrstorpassword);
            else if (action == "classroom")
                ClassRoom();
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

        protected void RegisteredUser(string username, string password, string classroom, string name)
        {
            int RegisteredReturn = -1;

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            try
            {

                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select * from CourseTestUser "
                    + "where username = '" + username.Trim() + "'";

                SqlCommand SC = new SqlCommand(sqlstr, conn);
                SqlDataReader SDR = SC.ExecuteReader();

                if (SDR.Read())
                {
                    SDR.Close();
                    RegisteredReturn = 2;
                }
                else
                {
                    SDR.Close();
                    string sqlstr2 = "insert into CourseTestUser(username,password,type)"
                        + "values('" + username.Trim() + "','" + password.Trim() + "','1')";
                    SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                    int mark = SC2.ExecuteNonQuery();

                    string sqlstr3 = "insert into CourseTestExaminee(username,name,class)"
                        + "values('" + username.Trim() + "','" + name.Trim() + "','" + classroom.Trim() + "')";
                    SqlCommand SC3 = new SqlCommand(sqlstr3, conn);
                    int mark2 = SC3.ExecuteNonQuery();

                    RegisteredReturn = 1;
                }

                conn.Close();

            }
            catch (Exception ex)
            {
                RegisteredReturn = -1;
            }
            finally
            { }

            httpContext.Response.Write(RegisteredReturn);
        }

        protected void RegisteredAdministrator(string username, string password, string administrstorpassword)
        {
            int RegisteredReturn = -1;

            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            try
            {

                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select * from CourseTestUser "
                    + "where username = '" + username.Trim() + "'";

                SqlCommand SC = new SqlCommand(sqlstr, conn);
                SqlDataReader SDR = SC.ExecuteReader();

                if (SDR.Read())
                {
                    SDR.Close();
                    RegisteredReturn = 2;
                }
                else
                {
                    SDR.Close();
                    if (administrstorpassword == "123456")
                    {
                        string sqlstr2 = "insert into CourseTestUser(username,password,type)"
                            + "values('" + username.Trim() + "','" + password.Trim() + "','2')";
                        SqlCommand SC2 = new SqlCommand(sqlstr2, conn);
                        int mark = SC2.ExecuteNonQuery();

                        RegisteredReturn = 1;
                    }
                    else
                        RegisteredReturn = 3;
                }

                conn.Close();

            }
            catch (Exception ex)
            {
                RegisteredReturn = -1;
            }
            finally
            { }

            httpContext.Response.Write(RegisteredReturn);
        }

        protected void ClassRoom()
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
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string sqlstr = "select class from CourseTestClass";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();


                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    string Classroom = ds.Tables[0].Rows[i]["class"].ToString();
                    XmlNode root = xmlDoc.SelectSingleNode("informations");
                    XmlElement xe1 = xmlDoc.CreateElement("information");
                    xe1.SetAttribute("classroom", "" + Classroom + "");
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
    }
}