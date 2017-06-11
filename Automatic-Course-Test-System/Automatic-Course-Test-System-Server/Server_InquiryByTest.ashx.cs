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
    /// Server_InquiryByTest 的摘要说明
    /// </summary>
    public class Server_InquiryByTest : IHttpHandler
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

            if (action == "studenttest")
                StudentTest(specifictest);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void StudentTest(string specifictest)
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
                string sqlstr = "select username,score from CourseTestResults "
                    + "where specifictest='" + specifictest + "'";

                SqlDataAdapter SD = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                SD.Fill(ds);

                conn.Close();


                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    string username = ds.Tables[0].Rows[i]["test"].ToString();
                    string score = ds.Tables[0].Rows[i]["score"].ToString();
                    XmlNode root = xmlDoc.SelectSingleNode("informations");
                    XmlElement xe1 = xmlDoc.CreateElement("information");
                    xe1.SetAttribute("username", "" + username + "");
                    XmlElement xeSub1 = xmlDoc.CreateElement("score");
                    xeSub1.InnerText = "" + score + "";
                    xe1.AppendChild(xeSub1);
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