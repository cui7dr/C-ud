using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class DrawPicture : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Draw();
        }
    }
    private void Draw()
    {
        Response.ContentType = "image/jpeg";
        Response.Clear();
        int ReportDataID = Convert.ToInt32(Session["RPID"]);
        //int ReportDataID = 9;

        //从数据库中取出数据
        string str = "select DataSerial from [ReportData] where ReportDataID=@ReportDataID";

        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection conn = new SqlConnection(strconn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(str, conn);
        cmd.Parameters.Add(new SqlParameter("@ReportDataID", ReportDataID));

        string GetLuBoData = cmd.ExecuteScalar().ToString();
        int num = GetLuBoData.Length;
        int[] arr = new int[num];

        int c = 0;
        for (int i = 0; i <= num - 2; i = i + 2)
        {
            int rLuBoData = Convert.ToInt32(GetLuBoData.Substring(i, 2), 16);
            //放到数组中
            arr[c] = rLuBoData;
            c++;
        }

        Bitmap img = new Bitmap(num + 30, 200);//创建Bitmap对象 
        for (int i = 0; i < num + 30; i++)
            for (int j = 0; j < 200; j++)
            {
                img.SetPixel(i, j, Color.Transparent);
            }
        Graphics g = Graphics.FromImage(img);//创建Graphics对象
        g.Clear(Color.White);//清空背景色
        Pen Bp = new Pen(Color.Black); //定义画笔 
        Pen Rp = new Pen(Color.Red);//红色画笔 

        String[] n = { "200", "150", "100", " 50", "  0" };
        Font font = new Font("Arial", 10, FontStyle.Bold);
        int y = 50;
        for (int i = 0; i < 5; i++)
        {
            g.DrawString(n[i].ToString(), font, Brushes.Red, -3, y); //设置文字内容及输出位置

            y = y + 30;
        }

        AdjustableArrowCap aac;  //定义箭头帽    
        aac = new AdjustableArrowCap(4, 4);
        Rp.CustomStartCap = aac;  //开始端箭头帽 

        g.DrawLine(Rp, 20, 20, 20, 180);//纵坐标 
        g.DrawLine(Rp, num+32, 180, 20, 180);//横坐标
        for (int i = 0; i < num / 2; i++)
        {
            g.DrawLine(Bp, 20 + i * (2 + 1 / 2), 180 - arr[i], 20 + (i + 1) * (2 + 1 / 2), 180 - arr[i + 1]);
        }
        img.Save(Response.OutputStream, ImageFormat.Jpeg);
        //释放掉Graphics对象和位图所使用的内存空间
        g.Dispose();
        img.Dispose();
        //把输出结果发送到客户端
        //Response.Flush();
    }
}