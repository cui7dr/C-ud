using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Bll;

public partial class Admin : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "管理员登录";
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.txtName.Text = "";
        this.txtPwd.Text = "";
        this.txtName.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Page.Title = "管理员登录";
        string name = this.txtName.Text.ToString();
        string pwd = this.txtPwd.Text.ToString();
        if (name == null || name == "" || pwd == null || pwd == "")
        {
            Response.Write("<script> alert('请填写完整信息'); </script>");
        }
        else
        {
            Models.Admin admin;
            AdminManager am = new AdminManager();
            if (am.AdminLogin(name, pwd, out admin))
            {
                Session["AdminName"] = name;//用户名存入缓存
                if (Request.QueryString["url"] != null)
                {
                    Response.Redirect(Request.QueryString["url"].ToString());
                }
                Response.Write("<script> window.location.href='Admin/UserManager.aspx'; </script>");
            }
            else
            {
                Response.Write("<script> alert('登录失败,请检查用户名或密码！'); </script>");
            }
        }
    }
}
