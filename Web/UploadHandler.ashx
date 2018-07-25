<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public class UploadHandler : IHttpHandler, IRequiresSessionState
{

    int fileMaxSize = 50 * 1024 * 1024;//单位 B
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        HttpPostedFile file = context.Request.Files[0];
        string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";
        //获取缓存中的设备信息
        string realVersion = context.Session["realVersion"].ToString();//获取版本号
        string realChuanhao = context.Session["realChuanhao"].ToString();//获取串号
                                                                         //自定义累加变量，判断是否重复
        int countReapeat = 0;
        if (file != null)
        {
            //获取文件总数
            HttpFileCollection files = context.Request.Files;
            //判断文件夹是否存在，若不存在首先创建文件夹
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            int fileSize = file.ContentLength;
            //获取文件名（包含文件名以及扩展名）
            string fileName = file.FileName.ToString();
            //获取扩展名（包含“.”）
            string fileExtension = file.FileName.Substring(fileName.LastIndexOf(',')).ToLower().ToString();
            if (fileSize > fileMaxSize)
            {
                context.Response.Write("上传的文件" + fileName + "超过大小限制！");
                return;
            }
            if (fileExtension != ".wav")
            {
                context.Response.Write("上传失败！文件格式不正确。。。");
                return;
            }
            //文件名中不包含扩展名的部分
            string manageNO = fileName.Substring(0, fileName.IndexOf("."));
            manageNO = Convert.ToInt32(manageNO).ToString();
            /*
             * 对上传文件重新命名
             * 命名规则：时间 + 设备号 + 串号
             */
            string timeName = DateTime.Now.ToString("yyyyMMdd HHmmss");
            timeName = timeName.Replace("-", "");
            timeName = timeName.Replace(":", "");
            timeName = timeName.Replace(" ", "");
            string newName = timeName + "-" + realChuanhao.ToString() + "-" + fileName;
            //作为和数据库存储的路径对比
            string soundURL = @"upload/sounds/" + newName;

            //验证数据库中是否存在该音频
            string sqlGetSoundUrl = "select count(*) from [ReportData] where soundURL = @soundURL";
            string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
            SqlConnection cnn = new SqlConnection(strconn);
            SqlCommand cmdCheck = new SqlCommand(sqlGetSoundUrl, cnn);
            cmdCheck.Parameters.Add(new SqlParameter("@soundURL", soundURL));
            cnn.Open();
            int getEqualCount = Convert.ToInt32(cmdCheck.ExecuteScalar());
            cnn.Close();
            if (getEqualCount > 0)
            {
                countReapeat++;
                //判断是否全部重复
                if (countReapeat == files.Count)
                {
                    return;
                }
            }
            else
            {
                //获取 PID
                string sqlGetPID = "select PID from [Product] where pIMEI = @pIMEI";
                SqlCommand cmdGetPID = new SqlCommand(sqlGetPID, cnn);
                cmdGetPID.Parameters.Add(new SqlParameter("@pIMEI", realChuanhao));
                cnn.Open();
                int pid = Convert.ToInt32(cmdGetPID.ExecuteScalar());
                cnn.Close();

                //获取 ReportDataID
                string sqlGetReportDataID = "select ReportDataID from [ReportData] where PID = @PID and ManageNO = @ManageNO";
                SqlCommand cmdGetReportDataID = new SqlCommand(sqlGetReportDataID, cnn);
                cmdGetReportDataID.Parameters.Add(new SqlParameter("@PID", pid));
                cmdGetReportDataID.Parameters.Add(new SqlParameter("@ManagerNO", manageNO));
                cnn.Open();
                int ReportDataID = Convert.ToInt32(cmdGetReportDataID.ExecuteScalar());
                cnn.Close();

                //根据解析的文件名进行数据库相关操作
                string sql = "update [ReportData] set soundURL = @soundURL where ReportDataID = @ReportDataID";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.Add(new SqlParameter("@soundURL", soundURL));
                cmd.Parameters.Add(new SqlParameter("@ReportDataID", ReportDataID));
                cnn.Open();
                int up = Convert.ToInt32(cmd.ExecuteScalar());
                if (up > 0)
                {
                    int i;
                    i = 1;
                }
                cnn.Close();
                //提前保存文件
                file.SaveAs(HttpContext.Current.Request.MapPath("upfile/sounds/") + newName);
                return;
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
