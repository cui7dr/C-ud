using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class ReportManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "报表管理";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！'); window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            //设置主键字段为SID
            this.GridView.DataKeyNames = new string[] { "ReportDataID" };
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
        string sql;
        //string sql = "select * from [ReportData]";
        if (Session["SearchSql"] == null || (string)Session["SearchSQL"] == "")
        {
            sql = "select * from [ReportData]";
        }
        else
        {
            sql = Session["SearchSQL"].ToString();
        }
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
            lnkbtnDel.Attributes.Add("onclick", "return confirm('删除该数据将同时删除与该数据相关的所有信息，请谨慎操作！！！你确定要继续删除吗？')");
        }
    }

    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LinkButton lnkbtnDel = GridView.Rows[e.RowIndex].FindControl("linkbtnDel") as LinkButton;
        string ReportDataID = lnkbtnDel.CommandArgument.ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        //删除 sql 语句
        string deleteMessage = "delete from [ReportData] where ReportDataID = @ReportDataID";
        cmd.Parameters.Add(new SqlParameter("@ReportDataID", ReportDataID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = deleteMessage;
        int o = cmd.ExecuteNonQuery();
        if (o > 0)
        {
            Response.Write("<script>alert('删除成功！'); window.location.href='ReportManager.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('删除失败！'); window.location.href='ReportManager.aspx';</script>");
        }
    }

    public string GetInputDate()
    {
        DateTime timeStart = Convert.ToDateTime(this.datatimepickerStart.Value);
        DateTime timeEnd = Convert.ToDateTime(this.datatimepickerEnd.Value);
        string wayName = this.txtWayName.Text;
        string wayNO = this.txtWayNO.Text;
        string managerNO = this.txtManagerNO.Text;
        string productType = this.ddlProductType.SelectedValue.ToString();
        string productState = this.ddlProductState.SelectedValue.ToString();
        string badLVStart = this.txtBadLVStart.Text;
        string badLVEnd = this.txtBadLVEnd.Text;
        string sql1 = "select * from [ReportData] where checkTime between " + timeStart + " and " + timeEnd;
        string sql = "";
        if (this.datatimepickerStart.Value.ToString() == "" || this.datatimepickerEnd.Value == "")
        {
            return sql;
        }
        else
        {
            if (Convert.ToDateTime(this.datatimepickerEnd.Value) < Convert.ToDateTime(this.datatimepickerStart.Value))
            {
                return sql;
            }
            else
            {
                sql= "select * from [ReportData] where checkTime between '" + this.datatimepickerStart.Value + "' and '" + this.datatimepickerEnd.Value + "'";
                if (wayName != "")
                {
                    sql += "and [LineName] = '" + wayName + "'"; 
                }
                if (wayNO != "")
                {
                    sql += "and [LineNO] = '" + wayNO + "'";
                }
                if (managerNO != "")
                {
                    sql += "and [ManagerNO] = '" + managerNO + "'";
                }
                if (productType != "")
                {
                    sql += "and [ChecksProductType] = '" + productType + "'";
                }
                if (productState != "")
                {
                    sql += "and [ProductState] = '" + productState + "'";
                }
                if (badLVStart == ""&&badLVEnd!=""|| badLVStart != "" && badLVEnd == "")
                {
                    Response.Write("<script>alert('请填写缺陷程度范围！'); </script>");
                }
                if(badLVStart != "" && badLVEnd != "")
                {
                    sql += "and [DefectLevel] between '" + badLVStart + "' and '" + badLVEnd + "'";
                }
            }
        }
        return sql;
    }

    public void btnSearch_Click(object sender,EventArgs e)
    {
        string sql = GetInputDate();
        Session["SearchSQL"] = sql;
        if(this.datatimepickerStart.Value.ToString()==""|| this.datatimepickerEnd.Value.ToString() == "")
        {
            Response.Write("<script>alert('请填写查询日期！'); </script>");
        }
        else
        {
            if(Convert.ToDateTime(this.datatimepickerEnd.Value) < Convert.ToDateTime(this.datatimepickerStart.Value))
            {
                Response.Write("<script>alert('起始日期大于终止日期，请重新输入！'); </script>");
            }
            else
            {
                Response.Redirect("ReportManager.aspx");
            }
        }
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session["ReportDataID"] = Convert.ToInt32(GridView.DataKeys[e.NewEditIndex].Value);
        Response.Redirect("ShowReportDetail.aspx");
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
