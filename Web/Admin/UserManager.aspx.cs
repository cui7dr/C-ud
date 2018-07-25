using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class UserMnager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "用户管理";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！'); window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            //设置主键字段为SID
            this.GridView.DataKeyNames = new string[] { "UID" };
            if (!IsPostBack)
            {
                HRDataBind();
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
        string sql = "select * from [User]";
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
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script> alert('操作失败！')</script>");
        }
    }

    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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

    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int UID = Convert.ToInt32(this.GridView.DataKeys[e.RowIndex].Value);
        //string loginID = Convert.ToString(((TextBox)this.GridView.Rows[e.RowIndex].Cells[1].FindControl("txtLoginID")).Text);
        string loginPwd = Convert.ToString(((TextBox)this.GridView.Rows[e.RowIndex].Cells[2].FindControl("txtPwd")).Text);
        //MD5 加密
        string MD5loginPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(loginPwd, "MD5");
        string companyName = Convert.ToString(((TextBox)this.GridView.Rows[e.RowIndex].Cells[3].FindControl("txtcompanyName")).Text);
        string companyAddress = Convert.ToString(((TextBox)this.GridView.Rows[e.RowIndex].Cells[4].FindControl("txtcompanyAddress")).Text);
        string sql = "update [User] set loginPwd = @loginPwd, companyName = @companyName, companyAddress = @companyAddress where UID = @UID";
        //Response.Write(sql);
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand(sql.ToString(), cnn);
        cnn.Open();
        cmd.Parameters.Add(new SqlParameter("@loginPwd", MD5loginPwd));
        cmd.Parameters.Add(new SqlParameter("@companyName", companyName));
        cmd.Parameters.Add(new SqlParameter("@companyAddress", companyAddress));
        cmd.Parameters.Add(new SqlParameter("@UID", UID));
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
        LinkButton lnkbtnDel = GridView.Rows[e.RowIndex].FindControl("lnkbtnDel") as LinkButton;
        string UID = lnkbtnDel.CommandArgument.ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        try
        {
            cmd.Connection = cnn;
            //删除 sql 语句
            string deleteRight = "delete from [User] where UID = '" + UID + "'";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = deleteRight;
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

    protected void Button3_Click(object sender, EventArgs e)
    {
        string loginID = this.AddLoginID.Text.Trim().ToString();
        string loginPwd = this.AddLoginPwd.Text.Trim().ToString();
        string companyName = this.AddCompanyName.Text.Trim().ToString();
        string companyAddress = this.AddCompanyAddress.Text.Trim().ToString();

        //检查输入是否为空，这里只验证登录 ID 和密码，公司名称和公司地址不做要求
        if (loginID == "" || loginID == null || loginPwd == "" || loginPwd == null)
        {
            Response.Write(" <script>alert('请填写完整登录信息！')</script>");
        }
        else
        {
            //检查输入的登录 ID 是否已经存在
            string sql = "select count(*) from [User] where loginID = @loginID";
            //Response.Write(sql);
            string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
            //声明数据库连接
            SqlConnection cnn = new SqlConnection(strconn);
            SqlCommand cmd = new SqlCommand(sql.ToString(), cnn);
            cnn.Open();
            cmd.Parameters.Add(new SqlParameter("@loginID", loginID));
            int i = (int)cmd.ExecuteScalar();
            //Response.Write(sql);
            if (i > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "info", "<script>alert('该用户名已经存在，请检查后再重新输入！')</script>");
                this.GridView.EditIndex = -1;
                HRDataBind();
            }
            else
            {
                //MD5 加密(32 位)
                string MD5LoginPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(loginPwd, "MD5");
                //插入数据
                string sqlAddUser = "insert into [User] (loginID, loginPwd, companyName, companyAddress) values (@loginID, @loginPwd, @companyName, @companyAddress)";
                SqlCommand cmdAdd = new SqlCommand(sqlAddUser.ToString(), cnn);
                cmdAdd.Parameters.Add(new SqlParameter("@loginID", loginID));
                cmdAdd.Parameters.Add(new SqlParameter("@loginPwd", MD5LoginPwd));
                cmdAdd.Parameters.Add(new SqlParameter("@companyName", companyName));
                cmdAdd.Parameters.Add(new SqlParameter("@companyAddress", companyAddress));
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
}