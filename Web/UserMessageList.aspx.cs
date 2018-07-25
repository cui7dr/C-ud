using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class UserMessageList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "通知列表";
        if (Session["name"] == null)
        {
            Response.Write("<script>alert('您没有登录，请先登录！'); window.location.href = 'Default.aspx';</script>");
        }
        else
        {
            if (!IsPostBack)
            {
                this.btnReading.Enabled = false;
                CurrentPageIndex = 1;
                this.BindList();
                this.SetButtonStates();
            }
        }
    }

    //每页记录数
    private int pageSize = 3;

    //当前页
    private int CurrentPageIndex
    {
        set
        {
            ViewState["CurrentPageIndex"] = value;
        }
        get
        {
            return Convert.ToInt32(ViewState["CurrentPageIndex"]);
        }
    }

    //总页数
    private int PageCount
    {
        set
        {
            ViewState["PageCount"] = value;
        }
        get
        {
            return Convert.ToInt32(ViewState["PageCount"]);
        }
    }

    // 设置按钮的可用性
    public void SetButtonStates()
    {
        //如果当前页是最后一页
        if (CurrentPageIndex >= this.PageCount)
        {
            this.btnNext.Enabled = false;
        }
        else
        {
            this.btnNext.Enabled = true;
        }
        if (CurrentPageIndex <= 1)
        {
            this.btnPre.Enabled = false;
        }
        else
        {
            this.btnPre.Enabled = true;
        }
    }

    //读取 MessState 为 0 的未读消息
    public void BindList()
    {
        string name = Session["name"].ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection conn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        conn.Open();
        cmd.Connection = conn;
        string sqlGetUID = "select UID from [User] where loginID = @loginID";
        cmd.Parameters.Add(new SqlParameter("@loginID", name));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetUID;
        string loginID = cmd.ExecuteScalar().ToString();
        string sqlMess = "select * from [Message] where UID = " + loginID + " and IsRead = 0";
        SqlDataAdapter da = new SqlDataAdapter(sqlMess, conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        PagedDataSource pds = new PagedDataSource();
        //控件是否实现自动分页功能
        pds.AllowPaging = true;
        //使用状态保持当前页数
        pds.CurrentPageIndex = CurrentPageIndex - 1;
        pds.DataSource = ds.Tables[0].DefaultView;
        pds.PageSize = this.pageSize;
        this.PageCount = pds.PageCount;
        this.lblInfo.Text = "第" + CurrentPageIndex + "页，共" + this.PageCount + "页";
        this.dlContent.DataSource = pds;
        conn.Close();
        this.dlContent.DataBind();
    }

    //读取状态为 1 的全部信息
    public void BindListReaded()
    {
        string name = Session["name"].ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection conn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        conn.Open();
        cmd.Connection = conn;
        string sqlGetUID = "select UID from [User] where loginID = @loginID";
        cmd.Parameters.Add(new SqlParameter("@loginID", name));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetUID;
        int loginID = Convert.ToInt32(cmd.ExecuteScalar());
        string sqlMess = "select * from [Message] where UID = " + loginID;
        SqlDataAdapter da = new SqlDataAdapter(sqlMess, conn);
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        da.Fill(ds);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.CurrentPageIndex = CurrentPageIndex - 1;
        pds.DataSource = ds.Tables[0].DefaultView;
        pds.PageSize = this.pageSize;
        this.PageCount = pds.PageCount;
        this.lblInfo.Text = "第" + CurrentPageIndex + "页，共" + this.PageCount + "页";
        this.dlContent.DataSource = pds;
        conn.Close();
        this.dlContent.DataBind();
    }

    //上一页
    protected void btnPre_Click(object sender, EventArgs e)
    {
        //当前页面为未读信息页面，应读取状态为 0 的信息
        if (btnReaded.Enabled == true)
        {
            this.CurrentPageIndex--;
            this.BindList();
            this.SetButtonStates();
        }
        //当前页面为全部信息页面，应读取状态为 1 的信息
        if (btnReading.Enabled == true)
        {
            this.CurrentPageIndex--;
            this.BindListReaded();
            this.SetButtonStates();
            this.dlContent.ItemDataBound += dlContent_ItemDataBound;
        }
    }

    //下一页
    protected void btnNext_Click(object sender, EventArgs e)
    {
        //当前页面为未读信息页面，应读取状态为 0 的信息
        if (btnReaded.Enabled == true)
        {
            this.CurrentPageIndex++;
            this.BindList();
            this.SetButtonStates();
        }
        //当前页面为全部信息页面，应读取状态为 1 的信息
        if (btnReading.Enabled == true)
        {
            this.CurrentPageIndex++;
            this.BindListReaded();
            this.SetButtonStates();
            this.dlContent.ItemDataBound += dlContent_ItemDataBound;
        }
    }

    //全部信息按钮（默认状态为 1 ）
    protected void btnReaded_Click(object  sender,EventArgs e)
    {
        this.btnReaded.Enabled = false;
        this.btnReading.Enabled = true;
        CurrentPageIndex = 1;
        this.BindListReaded();
        this.SetButtonStates();
    }

    //未读信息按钮（状态为 0 ）
    protected void btnReading_Click(object sender,EventArgs e)
    {
        this.btnReaded.Enabled = true;
        this.btnReading.Enabled = false;
        CurrentPageIndex = 1;
        this.BindList();
        this.SetButtonStates();
    }

    protected void btnBack_Click(object sender,EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>window.location='Index.aspx'</script>");
    }

    protected void dlContent_ItemCommand(object sender, DataListCommandEventArgs e)
    {
        if (e.CommandName == "update")
        {
            int messId = Convert.ToInt32(e.CommandArgument);
            string name = Session["name"].ToString();
            string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
            SqlConnection conn = new SqlConnection(strconn);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            string sqlGetUID = "select UID from [User] where loginID = @loginID";
            cmd.Parameters.Add(new SqlParameter("@loginID", name));
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlGetUID;
            int loginID = Convert.ToInt32(cmd.ExecuteScalar());
            string updateState = "update [Message] set Isread = 1 where MID = @MID";
            cmd.Parameters.Add(new SqlParameter("@MID", messId));
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = updateState;
            int countUpdate = cmd.ExecuteNonQuery();
            if (countUpdate > 0)
            {
                if (this.btnReaded.Enabled == true)
                {
                    CurrentPageIndex = 1;
                    this.BindList();
                    this.SetButtonStates();
                }
                else
                {
                    CurrentPageIndex = 1;
                    this.BindListReaded();
                    this.SetButtonStates();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('标记失败！请重试。。。')</script>");
            }
        }
    }

    //如果为未读信息页面，则显示“标记为已读”按钮，反之隐藏
    public void CheckBtnRead(DataListItemEventArgs e)
    {

    }

    protected void dlContent_ItemDataBound(object sender,DataListItemEventArgs e)
    {
        //如果是部分信息页面，显示”标记为已读“按钮
        if (this.btnReading.Enabled == false)//部分信息页面
        {
            e.Item.FindControl("btnRead").Visible = true;
        }
        else if (this.btnReading.Enabled == true)
        {
            e.Item.FindControl("btnRead").Visible = false;
        }
        //如果是全部信息页面，隐藏“标记为已读”按钮
        if (this.btnReaded.Enabled == false)//全部信息页面
        {
            e.Item.FindControl("btnRead").Visible = false;
        }
        else if (this.btnReaded.Enabled == true)
        {
            e.Item.FindControl("btnRead").Visible = true;
        }
    }
}
