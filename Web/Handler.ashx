<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.IO;

public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "images\\adv");
        string html = string.Empty;
        for(int i = 0; i < files.Length; i++)
        {
            int k = files[i].LastIndexOf("\\") + 1;
            if (i < files.Length - 1)
            {
                html += files[i].Substring(k) + ",";
            }
            else
            {
                html += files[i].Substring(k);
            }
        }
        context.Response.Write(html);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}