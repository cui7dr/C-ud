using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class SuggestionManager : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "建议管理";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            //设置主键字段为SID
            this.GridView.DataKeyNames = new string[] { "SID" };
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
        //声明数据库链接
        SqlConnection Connection = new SqlConnection(strconn);
        Connection.Open();
        //string str = "SELECT SID,loginID,companyName,STime,SContent,IsRead FROM  [Suggestion] inner join [User] on [Suggestion].UID=[User].UID";
        string str;
        //string str = "SELECT * FROM  [ReportData]";
        if (Session["SearchSQL"] == null)
        {
            str = "SELECT SID,loginID,companyName,STime,SContent,IsRead FROM  [Suggestion] inner join [User] on [Suggestion].UID=[User].UID";
        }
        else
        {
            str = Session["SearchSQL"].ToString();
        }

        //声明数据集
        DataSet dataset = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(str, strconn);
        //读取数据放入数据集
        adapter.Fill(dataset);
        //Connection.Close();//关闭数据库
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
            Connection.Close();
        }
        else
        {
            GridView.DataSource = dt;
            GridView.DataBind();
            //关闭数据库
            Connection.Close();
        }
    }

    public void GetDropDownList()
    {
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库链接
        SqlConnection Connection = new SqlConnection(strconn);
        string str = "SELECT distinct loginID FROM [Suggestion] INNER JOIN [User] ON [Suggestion].UID = [User].UID";
        //声明数据集  
        DataSet dataset = new DataSet();     
        SqlDataAdapter adapter = new SqlDataAdapter(str, strconn);
        //读取数据放入数据集
        adapter.Fill(dataset);
        //Connection.Close();//关闭数据库
        DataTable dt = dataset.Tables[0];
        this.ddlName.DataTextField = "loginID";
        //下拉列表绑定查询的10条的数据表
        this.ddlName.DataSource = dt;
        this.ddlName.DataBind();
        //添加默认
        this.ddlName.Items.Insert(0, new ListItem("全部", "0"));

    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView.PageIndex = e.NewPageIndex;
        HRDataBind();
        GetDropDownList();
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#6699ff'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkbtnDel = e.Row.FindControl("lnkbtnDel") as LinkButton;
            lnkbtnDel.Attributes.Add("onclick", "return confirm('删除该条数据将删除与该条数据相关的一系列数据，请谨慎操作！！！你确定要继续删除吗？')");
        }
    }


    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LinkButton lnkbtnDel = GridView.Rows[e.RowIndex].FindControl("lnkbtnDel") as LinkButton;
        string SID = lnkbtnDel.CommandArgument.ToString();
        String strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库链接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string DeleteMessage = "delete from [Suggestion] where SID=@SID";
        cmd.Parameters.Add(new SqlParameter("@SID", SID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = DeleteMessage;
        int d = cmd.ExecuteNonQuery();
        if (d > 0)
        {
            Response.Write(" <script>alert('删除成功!');window.location.href='SuggestionManager.aspx'; </script>");
        }
        else
        {
            Response.Write(" <script>alert('删除失败!');window.location.href='SuggestionManager.aspx'; </script>");
        }
    }
    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //标记为已读
        int SID = Convert.ToInt32(GridView.DataKeys[e.NewEditIndex].Value);
        String strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库链接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string ChangeState = "update [Suggestion] set IsRead=@IsRead where SID=@SID";
        cmd.Parameters.Add(new SqlParameter("@IsRead", 1));
        cmd.Parameters.Add(new SqlParameter("@SID", SID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = ChangeState;
        cmd.ExecuteNonQuery();
        Response.Redirect("SuggestionManager.aspx");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Write(" <script> window.location.href='UserManager.aspx'; </script>");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Write(" <script> window.location.href='ProductManager.aspx'; </script>");
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Write(" <script> window.location.href='RecordManager.aspx'; </script>");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Write(" <script> window.location.href='ReportManage.aspx'; </script>");
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        Response.Write(" <script> window.location.href='SuggestionManager.aspx'; </script>");
    }
    protected void btnPostSug_Click(object sender, EventArgs e)
    {
        string loginID = this.ddlName.SelectedValue.ToString();
        if (loginID == "0")
        {
            Session["SearchSQL"] = "SELECT SID,loginID,companyName,STime,SContent,IsRead FROM  [Suggestion] inner join [User] on [Suggestion].UID=[User].UID";
        }
        else
        {
            Session["SearchSQL"] = "SELECT SID,loginID,companyName,STime,SContent,IsRead FROM  [Suggestion] inner join [User] on [Suggestion].UID=[User].UID where  loginID='" + loginID + "'";
        }
        //Response.Write(" <script>alert('SQL语句是：" + sql + "');window.location.href='zzzzzefault2.aspx'; </script>");
        Response.Redirect("SuggestionManager.aspx");
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        Response.Write(" <script> window.location.href='MessageManager.aspx'; </script>");
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        Response.Write(" <script> window.location.href='ShowResualtOnMap.aspx'; </script>");
    }

}