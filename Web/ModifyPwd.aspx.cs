using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModifyPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "修改密码";
        if (Session["name"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='Default.aspx'; </script>");
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string name = Session["name"].ToString();
        string oldPwd = this.txtOldPwd.Text;
        string newPwd = this.txtNewPwd.Text;
        string newPwd2 = this.txtNewPwd2.Text;
        if (oldPwd == "" || oldPwd == null || newPwd == "" || newPwd == null || newPwd2 == "" || newPwd2 == null)
        {
            Response.Write(" <script>alert('请填写完整信息！');</script>");
        }
        else
        {
            if (newPwd != newPwd2)
            {
                Response.Write(" <script>alert('两次输入密码不一致，请重新检查！');</script>");
            }
            else
            {
                string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection connection = new SqlConnection(strconn);
                SqlCommand cmd = new SqlCommand();
                connection.Open();
                cmd.Connection = connection;
                //MD5 加密
                string MD5oldPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPwd, "MD5");
                string sqlGetUser = "select count(*) from [User] where loginID = @loginID and LoginPwd = @loginPwd";
                cmd.Parameters.Add(new SqlParameter("@loginID", name));
                cmd.Parameters.Add(new SqlParameter("@loginPwd", MD5oldPwd));
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlGetUser;
                int g = Convert.ToInt32(cmd.ExecuteScalar());
                if (g > 0)
                {
                    /*
                     * 修改密码
                     */
                    //MD5 加密
                    string MD5newPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(newPwd, "MD5");
                    string sqlUpdatePwd = "update [User] set loginPwd = @Pwd where loginID = @ID";
                    cmd.Parameters.Add(new SqlParameter("@ID", name));
                    cmd.Parameters.Add(new SqlParameter("@Pwd", MD5newPwd));
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlUpdatePwd;
                    int d = Convert.ToInt32(cmd.ExecuteScalar());
                    if (d > 0)
                    {
                        Response.Write(" <script>alert('修改成功，请记住新密码！');window.location.href='Index.aspx'; </script>");
                    }
                    else
                    {
                        Response.Write(" <script>alert('修改失败，请重新修改！');window.location.href='ModifyPwd.aspx'; </script>");
                    }
                }
                else
                {
                    Response.Write(" <script>alert('原密码输入错误，请重新输入！');window.location.href='ModifyPwd.aspx'; </script>");
                }
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.txtOldPwd.Text = "";
        this.txtNewPwd.Text = "";
        this.txtNewPwd2.Text = "";
        this.txtOldPwd.Focus();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href='Index.aspx'; </script>");
    }
}
