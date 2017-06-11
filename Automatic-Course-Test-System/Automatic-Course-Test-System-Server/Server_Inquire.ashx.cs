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
    /// Server_Inquire 的摘要说明
    /// </summary>
    public class Server_Inquire : IHttpHandler
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

            if (action == "specifictest")
                Specific_Test(username, test);
            else if (action == "score")
                Score(username, specifictest);
            else if (action == "creat")
                Ifcreat(specifictest);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected void Specific_Test(string username, string test)
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
                string sqlstr = "select specifictest from CourseTestResults "
                    + "where specifictest in "
                    + "(select specifictest from CourseTestTest "
                    + "where test = '" + test.Trim() 
                    + "') and username = '" + username.Trim() + "'";

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

        protected void Score(string username, string specifictest)
        {
            int result = -1;
            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            string sqlstr = "select * from CourseTestResults "
                + "where username = '" + username.Trim()
                + "' and specifictest = '" + specifictest.Trim() + "'";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                result = Int32.Parse(ds.Tables[0].Rows[0][2].ToString());

            httpContext.Response.Write(result);
            conn.Close();
        }
        protected void Ifcreat( string specifictest)
        {
            string constr = "server=.;database=CourseTest;Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            string sqlstr = "select * from CourseTestSpecificTest "
                    + "where specifictest = '" + specifictest.Trim() + "'";

            SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
            DataSet ds = new DataSet();
            SD.Fill(ds);
            if(ds.Tables[0].Rows[0][2].ToString()=="")
            {
                result = "0";
            }
            else
            {
                result = "1";
            }

            httpContext.Response.Write(result);
            conn.Close();
        }
    }
}