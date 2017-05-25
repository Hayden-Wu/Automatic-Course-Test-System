using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            if (username == "123456" && password == "123456")
            {
                login = 1;
            }
            else if (username == "admin" && password == "admin")
            {
                login = 2;
            }
            httpContext.Response.Write(login);
        }
    }
}