using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "地图展示";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            //string Longitude = Request["Longitude"].ToString();
            //string Latitude = Request["Latitude"].ToString();
            if (!IsPostBack)
            {
                if (Session["Longitude"] == null || Session["Latitude"] == null)
                {
                    Response.Write(" <script>alert('您没有登录或您的请求不合法，请检查后重新操作！');window.location.href='../Admin.aspx'; </script>");
                }
            }
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'UserManager.aspx'; </script>");
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'ProductManager.aspx'; </script>");
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'RecordManager.aspx'; </script>");
    }

}