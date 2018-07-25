using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Suggestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "意见反馈";
        if (Session["name"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！'); window.location.href='Default.aspx?url='+'Suggestion.aspx'; </script>");
        }
        else
        {
            this.txtSugName.Text = Session["name"].ToString();
            this.txtSugTime.Text = (DateTime.Now).ToString();
        }
    }

    protected void btnPostSug_Click(object sender, EventArgs e)
    {
        string sugName = Session["name"].ToString();
        DateTime sugTime = Convert.ToDateTime(this.txtSugTime.Text);
        string sugContent = this.txtSugContent.Text;
        //数据库读写
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string sqlGetUser = "select UID from [User] where loginID = @loginID";
        cmd.Parameters.Add(new SqlParameter("@loginID", sugName));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetUser;
        int UID = Convert.ToInt32(cmd.ExecuteScalar());
        //添加记录
        string sqlDeleteMessage = "inner into [Suggestion] value (@UID, @STime, @SContent, 0);";
        cmd.Parameters.Add(new SqlParameter("@UID", UID));
        cmd.Parameters.Add(new SqlParameter("@STime", sugTime));
        cmd.Parameters.Add(new SqlParameter("@SContent", sugContent));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlDeleteMessage;
        int d = cmd.ExecuteNonQuery();
        if (d > 0)
        {
            Response.Write(" <script>alert('提交成功！感谢您的反馈，您的反馈将帮助我们更好的改进！'); window.location.href='Index.aspx'; </script>");
        }
        else
        {
            Response.Write(" <script>alert('提交失败！请稍后重试，感谢您的关注!'); window.location.href='Index.aspx'; </script>");
        }
    }

    protected void btnCancle_Click(object sender,EventArgs e)
    {
        Response.Write("<script>window.location.href='Index.aspx'; </script>");
    }
}
