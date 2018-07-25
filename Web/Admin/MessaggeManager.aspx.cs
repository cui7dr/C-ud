using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class MessageManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "通知管理";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='../Admin.aspx'; </script>");
        }
        else
        { 
            //设置主键字段为SID
            this.GridView.DataKeyNames = new string[] { "MID" };
            if (!IsPostBack)
            {
                HRDataBind();
                GetDropDownList();
            }
        }
    }

    public void HRDataBind()
    {
        //绑定代码
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection connection = new SqlConnection(strconn);
        connection.Open();

        //string sql = "select SID,loginID,companyName,STime,SContent,IsRead from [Suggestion] inner join [User] on [Suggestion].UID=[User].UID";
        string sql = "select MID, [Message].UID, [User].UID, loginID, companyName, companyAddress, MTime, MContent, IsRead from [Message] inner join [User] on [Message].UID = [User].UID";

        //声明数据集
        DataSet dataset = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(sql, strconn);
        //读取数据放入数据集
        adapter.Fill(dataset);
        //关闭数据库
        //connection.Close();
        DataTable dt = dataset.Tables[0];

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            GridView.DataSource = dt;
            GridView.DataBind();
            int columnCount = dt.Columns.Count + 1;
            GridView.Rows[0].Cells.Clear();
            GridView.Rows[0].Cells.Add(new TableCell());
            GridView.Rows[0].Cells[0].ColumnSpan = columnCount;
            GridView.Rows[0].Cells[0].Text = "没有记录";
            GridView.Rows[0].Cells[0].Style.Add("text-align", "center");
            //关闭数据库
            connection.Close();
        }
        else
        {
            GridView.DataSource = dt;
            GridView.DataBind();
            //关闭数据库
            connection.Close();
        }
    }

    public void GetDropDownList()
    {
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection connection = new SqlConnection(strconn);
        string sql = "select distinct loginID from [User]";
        //声明数据集
        DataSet dataset = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(sql, strconn);
        //读取数据放入数据集
        adapter.Fill(dataset);
        //关闭数据库
        //connection.Close();
        DataTable dt = dataset.Tables[0];

        this.ddlName.DataTextField = "loginID";
        //下拉列表绑定查寻的 10 条数据集
        this.ddlName.DataSource = dt;
        this.ddlName.DataBind();
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView.PageIndex = e.NewPageIndex;
        HRDataBind();
        GetDropDownList();
    }

    protected void GridView_RowDataBound(object sender,GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor = this.style.backgroundColor; this.style.backgroundColor = '#6699FF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = currentcolor");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkbtnDel = e.Row.FindControl("lnkbtnDel") as LinkButton;
            lnkbtnDel.Attributes.Add("onclick", "return confirm('删除该条数据将删除与该条数据相关的一系列数据，请谨慎操作！！！你确定要继续删除吗？')");
        }
    }

    protected void GridView_RowDeleting(object cender,GridViewDeleteEventArgs e)
    {
        LinkButton lnkbtnDel = GridView.Rows[e.RowIndex].FindControl("linkbtnDel") as LinkButton;
        string MID = lnkbtnDel.CommandArgument.ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string deleteMessage = "delete from [Message] where MID = @MID";
        cmd.Parameters.Add(new SqlParameter("@MID", MID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = deleteMessage;
        int d = cmd.ExecuteNonQuery();
        if (d > 0)
        {
            Response.Write(" <script>alert('删除成功！'); window.location.href = 'MessageManager.aspx'; </script> ");
        }
        else
        {
            Response.Write(" <script>alert('删除失败！'); window.location.href = 'MessageManager.aspx'; </script> ");
        }
    }

    protected void Button4_Click(object sender,EventArgs e)
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

    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'ReportManager.aspx'; </script>");
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'SuggestionManager.aspx'; </script>");
    }

    protected void btnPostMes_Click(object sender,EventArgs e)
    {
        string loginID = this.ddlName.SelectedValue.ToString();
        DateTime mTime = DateTime.Now;
        string mesContent = this.txtMesContent.Text;
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string sqlGetUID = "select UID from [User] where loginID = @loginID";
        cmd.Parameters.Add(new SqlParameter("@loginID", loginID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetUID;
        int UID = Convert.ToInt32(cmd.ExecuteScalar());
        string sqlPostMessage = "insert into [Message] values (@UID, @MTime, @MContent, 0);";
        cmd.Parameters.Add(new SqlParameter("@UID", UID));
        cmd.Parameters.Add(new SqlParameter("@MTime", mTime));
        cmd.Parameters.Add(new SqlParameter("@MContent", mesContent));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlPostMessage;
        int d = cmd.ExecuteNonQuery();
        if (d > 0)
        {
            Response.Write(" <script>alert('发送成功！'); window.location.href = 'MessageManager.aspx'; </script> ");
        }
        else
        {
            Response.Write(" <script>alert('发送失败！'); window.location.href = 'MessageManager.aspx'; </script> ");
        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'MessaggeManager.aspx'; </script>");
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'ShowResualtOnMap.aspx'; </script>");
    }
}
