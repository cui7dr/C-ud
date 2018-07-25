using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Bll;
using Models;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "用户登录";

    }

    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        string name = this.txtName.Text.ToString().Trim();
        string pwd = this.txtPwd.Text.ToString().Trim();
        //验证输入内容是否为空
        if (name == null || name == "" || pwd == null || pwd == "")
        {
            Response.Write("<script> alert('请填写完整信息'); </script>");
        }
        else
        {
            User user;
            //MD5 加密
            string MD5pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            if (new UserManager().UserLogin(name, MD5pwd, out user))
            {
                Session["name"] = name;
                if (Request.QueryString["url"] != null)
                {
                    Response.Redirect(Request.QueryString["url"].ToString());
                }
                Response.Redirect("Index.aspx?loginID=" + name);
            }
            else
            {
                Response.Write("<script> alert('登录失败,请检查用户名或密码！'); </script>");
            }
        }
    }
}
