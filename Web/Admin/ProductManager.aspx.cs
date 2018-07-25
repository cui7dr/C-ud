using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "产品管理";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！'); window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            //设置主键字段为SID
            this.GridView.DataKeyNames = new string[] { "PID" };
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
        string sql = "select * from [Product] inner join [User] on Product.UID = [User].UID";
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
        try
        {
            this.GridView.EditIndex = e.NewEditIndex;
            HRDataBind();
            //获取并设置公司名称的方法
            GetEditStateCompanyName(e.NewEditIndex);
        }
        catch(Exception)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script> alert('操作失败！')</script>");
        }
    }

    protected void GridView_RowCancelingEdit(object sender,GridViewCancelEditEventArgs e)
    {
        try
        {
            this.GridView.EditIndex = -1;
            HRDataBind();
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script> alert('操作失败！')</script>");
        }
    }

    protected void GetEditStateCompanyName(int index)
    {
        //绑定代码
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        DropDownList ddlDepItem = this.GridView.Rows[index].FindControl("ddlcompanyName") as DropDownList;
        //sql语句
        StringBuilder sbSql = new StringBuilder();
        sbSql.AppendLine("select");
        sbSql.AppendLine(" *");
        sbSql.AppendLine(" from");
        sbSql.AppendLine(" [User]");
        //声明数据集
        DataSet dataset = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(sbSql.ToString(), strconn);
        //读取数据放入数据集
        adapter.Fill(dataset);
        DataTable dt = dataset.Tables[0];
        ddlDepItem.DataValueField = "UID";
        ddlDepItem.DataTextField = "companyName";
        //下拉列表绑定查询的 10 条数据表
        ddlDepItem.DataSource = dt;
        ddlDepItem.DataBind();
        //通过行索引找到部门的 HiddenField 控件
        HiddenField hfDepItem = this.GridView.Rows[index].FindControl("hfcompanyName") as HiddenField;
        //设置部门下拉框的默认值
        ddlDepItem.SelectedValue = hfDepItem.Value;
    }

    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int PID = Convert.ToInt32(this.GridView.DataKeys[e.RowIndex].Value);
        string version = Convert.ToString(((TextBox)this.GridView.Rows[e.RowIndex].Cells[2].FindControl("txtVersion")).Text);
        string IMEI = Convert.ToString(((TextBox)this.GridView.Rows[e.RowIndex].Cells[3].FindControl("txtIMEI")).Text).ToUpper();
        //下拉列表获取公司名称
        string companyID = (this.GridView.Rows[e.RowIndex].FindControl("ddlcompanyName") as DropDownList).SelectedValue.ToString();
        string sql = "update [Product] set pVersion = @pVersion, pIMEI = @pIMEI, UID = @UID where PID = @PID";
        //Response.Write(sql);
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand(sql.ToString(),cnn);
        cnn.Open();
        cmd.Parameters.Add(new SqlParameter("@pVersion", version));
        cmd.Parameters.Add(new SqlParameter("@pIMEI", IMEI));
        cmd.Parameters.Add(new SqlParameter("@UID", companyID));
        cmd.Parameters.Add(new SqlParameter("@PID", PID));
        int i = (int)cmd.ExecuteNonQuery();
        //Response.Write(sql);
        if (i > 0)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('更新成功！')</script>");
            this.GridView.EditIndex = -1;
            HRDataBind();
        }
        else
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('更新失败！')</script>");
        }
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
            lnkbtnDel.Attributes.Add("onclick", "return confirm('删除该设备将同时删除属于该设备的所有信息，请谨慎操作！！！你确定要继续删除吗？')");
        }
    }

    protected void GridView_RowDeleting(object sender,GridViewDeleteEventArgs e)
    {
        LinkButton lnkbtnDel = GridView.Rows[e.RowIndex].FindControl("linkbtnDel") as LinkButton;
        string PID = lnkbtnDel.CommandArgument.ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        try
        {  
            cmd.Connection = cnn;
            //删除 sql 语句
            string deleteRight = "delete from [Product] where PID = @PID";
            cmd.Parameters.Add(new SqlParameter("@MID", PID));
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = deleteRight;
            cmd.Parameters.Add(new SqlParameter("@PID", PID));
            int o = cmd.ExecuteNonQuery();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('删除成功！')</script>");
            this.GridView.EditIndex = -1;
            HRDataBind();
        }
        catch(Exception)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('删除失败！')</script>");
        }
        finally
        {
            cnn.Close();
        }
    }

    protected void Button3_Click(object sender,EventArgs e)
    {
        string version = this.AddVersion.Text.Trim().ToString();
        string IMEI = this.AddIMEI.Text.Trim().ToString().ToUpper();
        //string companyName = this.AddCompanyName.Text.Trim().ToString();

        //检查输入是否为空，这里只验证登录 ID 和密码，公司名称和公司地址不做要求
        if (version == "" || version == null || IMEI == "" || IMEI == null)
        {
            Response.Write(" <script>alert('请填写完整登录信息！')</script>");
        }
        else
        {
            //检查输入的登录 ID 是否已经存在
            string sql = "select count(*) from [Product] where pIMEI = @pIMEI";
            //Response.Write(sql);
            string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
            //声明数据库连接
            SqlConnection cnn = new SqlConnection(strconn);
            SqlCommand cmd = new SqlCommand(sql.ToString(),cnn);
            cnn.Open();
            cmd.Parameters.Add(new SqlParameter("@pIMEI", IMEI));
            int i = (int)cmd.ExecuteScalar();
            //Response.Write(sql);
            if (i > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('该设备已经存在，请检查后再重新输入！')</script>");
                this.GridView.EditIndex = -1;
                HRDataBind();
            }
            else
            {
                //插入数据
                string sqlAddUser = "insert into [Product] (pVersion, pIMEI) values (@version, @IMEI)";
                SqlCommand cmdAdd = new SqlCommand(sqlAddUser.ToString(), cnn);
                cmdAdd.Parameters.Add(new SqlParameter("@version", version));
                cmdAdd.Parameters.Add(new SqlParameter("@IMEI", IMEI));
                int ia = (int)cmdAdd.ExecuteNonQuery();
                //Response.Write(sql);
                if (ia > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('添加成功！')</script>");
                    this.GridView.EditIndex = -1;
                    HRDataBind();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('添加失败！')</script>");
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

    protected void GridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
