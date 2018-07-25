using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class RecordManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "记录管理";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！'); window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            //设置主键字段为SID
            this.GridView.DataKeyNames = new string[] { "RecordID" };
            if (!IsPostBack)
            {
                HRDataBind();
            }
        }
    }

    protected void HRDataBind()
    {
        //绑定代码
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection connection = new SqlConnection(strconn);
        connection.Open();
        //sql 语句
        string sql = "select [User].loginID, [User].companyName, [Product].pIMEI, [Record].RecordID, [Record].RecordNO, [Record].checkTime, [Record].Temperature, [Record].Humidity, [Record].Longitude, [Record].Latitude from [Record] inner join [Product] on [Record].PID = [Product].PID inner join [User] on [Product].UID = [User].UID";
        //声明数据集
        DataSet dataset = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(sql, strconn);
        //读取数据放入数据集
        adapter.Fill(dataset);
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

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView.PageIndex = e.NewPageIndex;
        HRDataBind();
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int recordID = Convert.ToInt32(GridView.DataKeys[e.NewEditIndex].Value);
        Session["RecordID"] = recordID;
        //传递经纬度值
        string sqlLongitude = "select Longitude from [Record] where RecordID = @RecordID";
        string strconn= ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection connection = new SqlConnection(strconn);
        SqlCommand getLongitude = new SqlCommand(sqlLongitude.ToString(), connection);
        connection.Open();
        getLongitude.Parameters.Add(new SqlParameter("@RecordID", recordID));
        string longitude = getLongitude.ExecuteScalar().ToString();

        //纬度
        string sqlLatitude = "select Latitude from [Record] where RecordID = @RecordID";
        SqlCommand getLatitude = new SqlCommand(sqlLatitude.ToString(), connection);
        getLatitude.Parameters.Add(new SqlParameter("@RecordID", recordID));
        string latitude = getLatitude.ExecuteScalar().ToString();

        Session["Longitude"] = longitude;
        Session["Latitude"] = latitude;
        Response.Redirect("ShowLocation.aspx?Longitude = " + longitude + " &&Latitude = " + latitude);
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor = this.style.backgroundColor; this.style.backgroundColor = '#6699FF'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = currentcolor");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkbtnDel = e.Row.FindControl("lnkbtnDel") as LinkButton;
            lnkbtnDel.Attributes.Add("onclick", "return confirm('删除该注册会员将同时删除属于该会员的所有信息，请谨慎操作！！！你确定要继续删除吗？')");
        }
    }

    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LinkButton lnkbtnDel = GridView.Rows[e.RowIndex].FindControl("linkbtnDel") as LinkButton;
        string RecordID = lnkbtnDel.CommandArgument.ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        try
        {
            cmd.Connection = cnn;
            //删除 sql 语句
            string deleteReportData = "delete from [Record] where RecordID = @RecordID";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = deleteReportData;
            cmd.Parameters.Add(new SqlParameter("@RecordID", RecordID));
            int o = cmd.ExecuteNonQuery();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('删除成功！')</script>");
            this.GridView.EditIndex = -1;
            HRDataBind();
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('删除失败！')</script>");
        }
        finally
        {
            cnn.Close();
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

    protected void Button7_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'ReportManager.aspx'; </script>");
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        Response.Write(" <script>window.location.href = 'SuggestionManager.aspx'; </script>");
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