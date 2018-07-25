using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Media;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "主页";
        //form.Target = "_blank";
        if (Session["name"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='Default.aspx'; </script>");
        }
        else
        {
            string welcomeName = Session["name"].ToString();
            this.lblWelcomeInfo.Text = welcomeName;
            //设置主键字段为 scoreID
            this.GridView.DataKeyNames = new string[] { "ReportDataID" };
            if (!IsPostBack)
            {
                HRDataBind();
                int count = GetMessage();
                if (count > 0)
                {
                    this.div_Message.Style["Display"] = "Block";
                    this.lblCount.Text = count.ToString();
                }
                else
                {
                    this.div_Message.Style["Display"] = "None";
                }
            }
        }
    }

    public int GetMessage()
    {
        string name = Session["name"].ToString();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string sqlGetUID = "select UID from [User] where loginID = @loginID";
        cmd.Parameters.Add(new SqlParameter("@loginID", name));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetUID;
        string loginID = cmd.ExecuteScalar().ToString();
        string sqlGetCount = "select count (MContent) from [Message] where UID = @UID and IsRead = 0";
        cmd.Parameters.Add(new SqlParameter("@UID", loginID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetCount;
        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count;
    }

    public void HRDataBind()
    {
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库链接
        SqlConnection Connection = new SqlConnection(strconn);
        Connection.Open();
        string sql;
        //string str = "select * from [ReportData]";
        string loginID = Session["name"].ToString();
        if (Session["SearchSQL"] == null || (string)Session["SearchSQL"] == "")
        {
            sql = "select * from [ReportData] inner join [Product] on [ReportData].PID = [Product].PID inner join [User] on [Product].UID = [User].UID where [User].loginID = '" + loginID + "'";
        }
        else
        {
            sql = Session["SearchSQL"].ToString();
        }
        DataSet dataset = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(sql, strconn);
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

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView.PageIndex = e.NewPageIndex;
        HRDataBind();
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //添加鼠标移动事件
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "currentcolor = this.style.backgroundColor;this.style.backgroundColor = '#6699ff'");
        //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor = currentcolor");
        //}
        CheckBox cbx = e.Row.FindControl("CheckBox") as CheckBox;
        try
        {
            //绑定选中的 checkBox 客户端 id
            cbx.Attributes.Add("onclick", "ChangeGet(this)");
        }
        catch
        {
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkbtnDel = e.Row.FindControl("lnkbtnDel") as LinkButton;
            lnkbtnDel.Attributes.Add("onclick", "return confirm('删除该条数据将删除与该条数据相关的所有数据，请谨慎操作！你确定要继续么？')");
        }
    }

    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        LinkButton lnkbtnDel = GridView.Rows[e.RowIndex].FindControl("lnkbtnDel") as LinkButton;
        string ReportDataID = lnkbtnDel.CommandArgument.ToString();
        String strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库链接
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string DeleteMessage = "delete from [ReportData] where ReportDataID = @ReportDataID";
        cmd.Parameters.Add(new SqlParameter("@ReportDataID", ReportDataID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = DeleteMessage;
        int d = cmd.ExecuteNonQuery();
        if (d > 0)
        {
            Response.Write(" <script>alert('删除成功!');window.location.href='Index.aspx'; </script>");
        }
        else
        {
            Response.Write(" <script>alert('删除失败!');window.location.href='Index.aspx'; </script>");
        }
    }

    protected void btnReadyUpload_Click(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='Default.aspx'; </script>");
        }
        else
        {
            string loginID = Session["name"].ToString();
            Response.Redirect("UpLoadData.aspx?loginID=" + loginID);
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
            if (timeEnd < timeStart)
            {
                return sql;
            }
            else
            {
                string loginID = Session["name"].ToString();
                sql = "select * from [ReportData] inner join [Product] on [ReportData].PID = [Product].PID inner join [User] on [Product].UID = [User].UID where [User].loginID = '" + loginID + "' and checkTime brtween '" + this.datatimepickerStart.Value + "' and '" + this.datatimepickerEnd.Value + "'";
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
                    sql += "and [CheckProductType] = '" + productType + "'";
                }
                if (productState != "")
                {
                    sql += "and [ProductState] = '" + productState + "'";
                }
                if (badLVStart == "" && badLVEnd != "" || badLVStart != "" && badLVEnd == "")
                {
                    Response.Write("<script>alert('请填写缺陷程度范围！'); </script>");
                }
                if (badLVStart != "" && badLVEnd != "")
                {
                    sql += "and [DefectLevel] between '" + badLVStart + "' and '" + badLVEnd + "'";
                }
            }
        }
        return sql;
    }

    public void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = GetInputDate();
        Session["SearchSQL"] = sql;
        if (this.datatimepickerStart.Value.ToString() == "" || this.datatimepickerEnd.Value.ToString() == "")
        {
            Response.Write("<script>alert('请填写查询日期！'); </script>");
        }
        else
        {
            if (Convert.ToDateTime(this.datatimepickerEnd.Value) < Convert.ToDateTime(this.datatimepickerStart.Value))
            {
                Response.Write("<script>alert('起始日期大于终止日期，请重新输入！'); </script>");
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Session["ReportDataID"] = Convert.ToInt32(GridView.DataKeys[e.NewEditIndex].Value);
        Response.Redirect("DetailEdit.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //跳转到打印预览列表
        for (int i = 0; i < this.GridView.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)this.GridView.Rows[i].Cells[0].FindControl("CheckBox");
            if (cbx.Checked)
            {
                Label lb = this.GridView.Rows[i].Cells[0].FindControl("lblReportDataID") as Label;
                //将 label 的值取出来用"，"分开
                string ReportDataID = lb.Text;
                //Response.Write("<script>alert('选择成功，将打印" + ReportDataID + "'); </script>");
                //获取经纬度信息
                Label Long = this.GridView.Rows[i].Cells[0].FindControl("lblLongitude") as Label;
                string longitude = Long.Text;
                Label Lat = this.GridView.Rows[i].Cells[0].FindControl("lblLatitude") as Label;
                string latitude = Lat.Text;
                Session["LongitudePrint"] = longitude;
                Session["LatitudePrint"] = latitude;
                //获取图片路径
                Label PoleNOPic = this.GridView.Rows[i].Cells[0].FindControl("lblPoloNoPicPath") as Label;
                string PoleNOPicPath = PoleNOPic.Text;

                string wholePicPath = (this.GridView.Rows[i].Cells[0].FindControl("lblWholePicPath") as Label).Text;
                string badProductPicPath = (this.GridView.Rows[i].Cells[0].FindControl("lblBadProductPicPath") as Label).Text;
                Session["PoleNOPicPath"] = PoleNOPicPath;
                Session["WholePicPath"] = wholePicPath;
                Session["BadProductPicPath"] = badProductPicPath;
                Session["RPID"] = ReportDataID;
                Response.Redirect("PagePrint.aspx?ReportDataID=" + ReportDataID);
            }
        }
    }

    protected void btnSearchAll_Click(object sender, EventArgs e)
    {
        string loginID = Session["name"].ToString();
        string sql = "select * from [ReportData] inner join [Product] on [ReportData].PID = [Product].PID inner join [User] on [Product].UID = [User].UID where [User].LoginID= '" + loginID + "'";
        Session["SearchAll"] = sql;
        Response.Redirect("Index.aspx");
    }

    protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SoundPlayer sp = new SoundPlayer();
        GridViewRow row = null;
        //表示触发事件的 IButtonControl ,保持统一性便于后续操作，这里直接转换成控件基层类
        Control cmdControl = e.CommandSource as Control;
        row = cmdControl.NamingContainer as GridViewRow;
        //图片按钮响应事件
        if (e.CommandName == "PlayMusic")
        {
            //初始化图标状态
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                ((ImageButton)(GridView.Rows[i].Cells[1].FindControl("IbtnMusicPlay"))).Visible = true;
                ((ImageButton)(GridView.Rows[i].Cells[1].FindControl("IbtnMusicStop"))).Visible = false;
            }

            //获取音频路径
            Label lblsoundURL = row.FindControl("lblSoundURL") as Label;
            string soundURL = lblsoundURL.Text;
            ImageButton IbtnPlayMusic = row.FindControl("IbtnMusicPlay") as ImageButton;
            ImageButton IbtnStopMusic = row.FindControl("IbtnMusicStop") as ImageButton;
            //开始播放音乐
            IbtnPlayMusic.Visible = false;
            IbtnStopMusic.Visible = true;
            string location = Server.MapPath(soundURL);
            sp.SoundLocation = location;
            sp.Load();
            sp.Play();
        }
        if (e.CommandName == "StopMusic")
        {
            ImageButton IbtnPlayMusic = row.FindControl("IbtnMusicPlay") as ImageButton;
            ImageButton IbtnStopMusic = row.FindControl("IbtnMusicStop") as ImageButton;
            //开始播放音乐
            IbtnPlayMusic.Visible = true;
            IbtnStopMusic.Visible = false;
            //停止播放
            sp.Stop();
        }
    }
}
