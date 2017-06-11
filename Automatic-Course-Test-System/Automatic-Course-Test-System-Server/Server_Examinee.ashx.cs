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
    /// Sever_Examinee 的摘要说明
    /// </summary>
    public class Sever_Examinee : IHttpHandler
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
            string banji = httpContext.Request.QueryString["banji"];

            if (action == "test")
                Test();
            else if(action == "specifictest")
                Specific_test(banji);
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
                string sqlstr = "select class from CourseTestClass";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();


                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    string classroom = ds.Tables[0].Rows[i]["class"].ToString();
                    XmlNode root = xmlDoc.SelectSingleNode("informations");
                    XmlElement xe1 = xmlDoc.CreateElement("information");
                    xe1.SetAttribute("classroom", "" + classroom + "");
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

        protected void Specific_test(string banji)
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
                string sqlstr = "select username from CourseTestExaminee where class = '" + banji + "'";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();

                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    string username = ds.Tables[0].Rows[i]["username"].ToString();
                    XmlNode root = xmlDoc.SelectSingleNode("informations");
                    XmlElement xe1 = xmlDoc.CreateElement("information");
                    xe1.SetAttribute("username", "" + username + "");
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