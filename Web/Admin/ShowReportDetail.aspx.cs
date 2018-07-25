using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ShowReportDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "详情编辑";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！'); window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            if (Session["ReportDataID"] == null)
            {
                Response.Write(" <script>alert('请求不合法！！'); window.location.href='../Admin.aspx'; </script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    string key = Session["ReportDataID"].ToString();
                    GetInfoById(key);
                }
            }
        }
    }

    //通过 ID 获取详情列表的相关数据信息
    private void GetInfoById(string key)
    {
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection connection = new SqlConnection(strconn);
        connection.Open();
        string sql = "select * from [ReportData] where ReportDataID = " + key;
        SqlCommand command = new SqlCommand(sql, connection);
        command.CommandType = CommandType.Text;
        SqlDataReader dr = command.ExecuteReader();
        if (dr.Read())
        {
            int PID = Convert.ToInt32(dr["PID"]);
            Session["PID"] = PID;
            /*
             * 填充空白页面
             */
            //设置线路名称
            this.txtLineName.Text = dr["LineName"].ToString();
            this.datetimepickerStart.Value = DateTime.Now.ToString();
            //设置输入时间为只读
            this.datetimepickerStart.Disabled = true;
            //设置线路编号
            this.txtLineNo.Text = dr["LineNO"].ToString();
            //设置管理编号
            this.txtManagerNo.Text = dr["ManageNo"].ToString();
            this.txtManagerNo.ReadOnly = true;
            //设置设备种类
            if(dr["CheckProductType"].ToString()!=null&& dr["CheckProductType"].ToString() != "")
            {
                this.ddlProductType.SelectedValue = dr["CheckProductType"].ToString();
            }
            //设置设备状态
            if (dr["ProductState"].ToString() != null && dr["ProductState"].ToString() != "")
            {
                this.ddlProductState.SelectedValue = dr["ProductState"].ToString();
            }
            //设置诊断建议
            this.txtSuggestion.Text = dr["suggestion"].ToString(); 
            this.txtCheckName.Text = dr["CheckName"].ToString();
            this.datetimepickerEnd.Value = dr["CheckTime"].ToString();
            //设置检测时间为只读
            this.datetimepickerEnd.Disabled = true;
            //设置缺陷程度
            //this.txtDefectLevel.Text = dr["DefectLevel"].ToString();
            //this.txtDefectLevel.ReadOnly = true;
            //设置频率
            this.txtFrequency.Text = dr["Frequency"].ToString();
            this.txtFrequency.ReadOnly = true;
            //设置检测距离
            this.txtDistance.Text = dr["distance"].ToString();
            this.txtDistance.ReadOnly = true;
            //设置温度
            this.txtTemperature.Text = dr["Temperature"].ToString();
            this.txtTemperature.ReadOnly = true;
            //设置湿度
            this.txtHumidity.Text = dr["Humidity"].ToString();
            this.txtHumidity.ReadOnly = true;
            //设置经度
            this.txtLongitude.Text = dr["Longitude"].ToString();
            this.txtLongitude.ReadOnly = true;
            //设置纬度
            this.txtLatitude.Text = dr["Latitude"].ToString();
            this.txtLatitude.ReadOnly = true;
            //设置最大 dB 值
            this.txtMaxdB.Text = dr["MaxdB"].ToString();
            this.txtMaxdB.ReadOnly = true;
            //设置平均db值
            this.txtAvgdB.Text = dr["AVGdB"].ToString();
            this.txtAvgdB.ReadOnly = true;
        }
        dr.Close();
    }

    #region    处理图片以及 bin 文件的上传
    //上传电杆编号照片
    protected void btnNoPic_Click(object sender,EventArgs e)
    {
        //HttpPostedFile file = Request.Files["FileNoPic"];
        //string uploadPath1 = HttpContext.Current.Server.MapPath(@Request["folder"]) + "\\";
        string uploadPath = HttpContext.Current.Server.MapPath("upFile/images/") + "\\";
        //string fileName1 = Path.GetFileName(file.FileName);
        string fileName = Path.GetFileName(FileNoPic.PostedFile.FileName).Trim();
        string fileExtension = Path.GetExtension(fileName);
        //用户名称
        string newName1 = "hbck";
        string newName = DateTime.Now.ToString("yyyyMMdd HHmmss");
        newName = newName.Replace("-", "");
        newName = newName.Replace(":", "");
        newName = newName.Replace(" ", "");
        fileName = newName1.Trim() + "-" + newName + "-" + fileName;
        string hasFile = this.FileNoPic.Value;
        if (hasFile != null && hasFile != "")
        {
            Double size = Math.Round((Double)Convert.ToDecimal(HttpContext.Current.Request.ContentLength / 1024), 2);
            if (size < 5120)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                FileNoPic.PostedFile.SaveAs(Server.MapPath("upFile/images/") + fileName);
                string poleNoPicPath = "upFile/images/" + fileName;
                int key = Convert.ToInt32(Session["ReportDataID"]);
                string sql = "update [ReportData] set poleNOPath = @poleNoPicPath where ReportDataID = @ReportDataID";
                string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection cnn = new SqlConnection(strconn);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.Add(new SqlParameter("@poleNoPicPath", poleNoPicPath));
                cmd.Parameters.Add(new SqlParameter("@ReportDataID", key));
                int u = cmd.ExecuteNonQuery();
                if (u > 0)
                {
                    this.lblNoPic.Visible = true;
                    this.lblNoPic.Text = "上传成功";
                }
                else
                {
                    this.lblNoPic.Visible = true;
                    this.lblNoPic.Text = "上传失败";
                }
            }
            else
            {
                this.lblNoPic.Visible = true;
                this.lblNoPic.Text = "文件大小不符合要求，上传失败！";
            }
        }
        else
        {
            this.lblNoPic.Visible = true;
            this.lblNoPic.Text = "请选择需要上传的图片！";
        }
    }

    //上传整体照片
    protected void btnFuLLPic_Click(object sender,EventArgs e)
    {
        //HttpPostedFile file = Request.Files["FileFullPic"];
        string uploadPath = HttpContext.Current.Server.MapPath("upFile/images/") + "\\";
        string fileName = Path.GetFileName(FileFullPic.PostedFile.FileName).Trim();
        string fileExtension = Path.GetExtension(fileName);
        //用户名称
        string newName1 = "hbck";
        string newName = DateTime.Now.ToString("yyyyMMdd HHmmss");
        newName = newName.Replace("-", "");
        newName = newName.Replace(":", "");
        newName = newName.Replace(" ", "");
        fileName = newName1.Trim() + "-" + newName + "-" + fileName;
        string hasFile = this.FileFullPic.Value;
        if (hasFile != null && hasFile != "")
        {
            Double size = Math.Round((Double)Convert.ToDecimal(HttpContext.Current.Request.ContentLength / 1024), 2);
            if (size < 5120)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                FileFullPic.PostedFile.SaveAs(HttpContext.Current.Request.MapPath("upFile/images/") + fileName);
                string wholePicPath = "upFile/images/" + fileName;
                int key = Convert.ToInt32(Session["ReportDataID"]);
                string sql = "update [ReportData] set wholePicPath = @wholePicPath where ReportDataID = @ReportDataID";
                string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection cnn = new SqlConnection(strconn);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.Add(new SqlParameter("@wholePicPath", wholePicPath));
                cmd.Parameters.Add(new SqlParameter("@ReportDataID", key));
                int u = cmd.ExecuteNonQuery();
                if (u > 0)
                {
                    this.lblFullPic.Visible = true;
                    this.lblFullPic.Text = "上传成功";
                }
                else
                {
                    this.lblFullPic.Visible = true;
                    this.lblFullPic.Text = "上传失败";
                }
            }
            else
            {
                this.lblFullPic.Visible = true;
                this.lblFullPic.Text = "文件大小不符合要求，上传失败！";
            }
        }
        else
        {
            this.lblFullPic.Visible = true;
            this.lblFullPic.Text = "请选择需要上传的图片！";
        }
    }

    //上传不良照片
    protected void btnBadPic_Click(object sender,EventArgs e)
    {
        //HttpPostedFile file = Request.Files["FileBadPic"];
        string uploadPath = HttpContext.Current.Server.MapPath("upFile/images/") + "\\";
        string fileName = Path.GetFileName(FileBadPic.PostedFile.FileName).Trim();
        string fileExtension = Path.GetExtension(fileName);
        //用户名称
        string newName1 = "hbck";
        string newName = DateTime.Now.ToString("yyyyMMdd HHmmss");
        newName = newName.Replace("-", "");
        newName = newName.Replace(":", "");
        newName = newName.Replace(" ", "");
        fileName = newName1.Trim() + "-" + newName + "-" + fileName;
        string hasFile = this.FileBadPic.Value;
        if (hasFile != null && hasFile != "")
        {
            Double size = Math.Round((Double)Convert.ToDecimal(HttpContext.Current.Request.ContentLength / 1024), 2);
            if (size < 5120)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                FileBadPic.PostedFile.SaveAs(HttpContext.Current.Request.MapPath("upFile/images/") + fileName);
                string badProductPicPath = "upFile/images/" + fileName;
                int key = Convert.ToInt32(Session["ReportDataID"]);
                string sql = "update [ReportData] set badProductPicPath = @badProductPicPath where ReportDataID = @ReportDataID";
                string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection cnn = new SqlConnection(strconn);
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.Add(new SqlParameter("@badProductPicPath", badProductPicPath));
                cmd.Parameters.Add(new SqlParameter("@ReportDataID", key));
                int u = cmd.ExecuteNonQuery();
                if (u > 0)
                {
                    this.lblBadPic.Visible = true;
                    this.lblBadPic.Text = "上传成功";
                }
                else
                {
                    this.lblBadPic.Visible = true;
                    this.lblBadPic.Text = "上传失败";
                }
            }
            else
            {
                this.lblBadPic.Visible = true;
                this.lblBadPic.Text = "文件大小不符合要求，上传失败！";
            }
        }
        else
        {
            this.lblBadPic.Visible = true;
            this.lblBadPic.Text = "请选择需要上传的图片！";
        }
    }
    #endregion

    protected void Button1_Click(object sender,EventArgs e)
    {
        //获取前端输入数据
        string lineName = this.txtLineName.Text;
        DateTime timeStart = Convert.ToDateTime(this.datetimepickerStart.Value);
        string lineNo = this.txtLineNo.Text;
        string managerNo = this.txtManagerNo.Text;
        string productState = this.ddlProductState.SelectedValue;
        string suggestion = this.txtSuggestion.Text;
        string checkName = this.txtCheckName.Text;
        DateTime checkTime = Convert.ToDateTime(this.datetimepickerEnd.Value);
        //string defectLevel0 = this.txtDefectLevel.Text;
        string frequency = this.txtFrequency.Text;
        float distance = float.Parse(this.txtDistance.Text);
        string distance1 = this.ddlDistance.SelectedValue;
        float temperature = float.Parse(this.txtTemperature.Text);
        float humidity = float.Parse(this.txtHumidity.Text);
        string longitude=this.txtLongitude.Text;
        string latitude = this.txtLatitude.Text;
        float maxdB = float.Parse(this.txtMaxdB.Text);
        float avgdB = float.Parse(this.txtAvgdB.Text);
        //利用公式计算缺陷程度
        Double defectLevel = 40;

        int key = Convert.ToInt32(Session["ReportDataID"]);
        int PID = Convert.ToInt32(Session["PID"]);
        //数据库写入
        string sql = "update [ReportData] set PID = @PID, LineName = @lineName, [LineNO] = @lineNo, ProductState = @productState, ManagerNo = @managerNo, checkTime = @checkTime, InputTime = @InputTime, Suggestion = @suggestion, CheckName = @checkName, Frequency = @frequency, distance = @distance, MaxdB = @maxdB, AVGdB = @avgdB, DefectLevel = @defectLevel, Temperature = @temperature, Humidity = @humidity, Longitude = @longitude, Latitude = @latitude where ReportDataID = @ReportDataID";
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        //声明数据库连接
        SqlConnection connection = new SqlConnection(strconn);
        connection.Open();
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.Add(new SqlParameter("@PID", PID));
        cmd.Parameters.Add(new SqlParameter("@lineName", lineName));
        cmd.Parameters.Add(new SqlParameter("@lineNo", lineNo));
        cmd.Parameters.Add(new SqlParameter("@productState", productState));
        cmd.Parameters.Add(new SqlParameter("@managerNo", managerNo));
        cmd.Parameters.Add(new SqlParameter("@checkTime", checkTime));
        cmd.Parameters.Add(new SqlParameter("@InputTime", timeStart));
        cmd.Parameters.Add(new SqlParameter("@suggestion", suggestion));
        cmd.Parameters.Add(new SqlParameter("@checkName", checkName));
        cmd.Parameters.Add(new SqlParameter("@frequency", frequency));
        cmd.Parameters.Add(new SqlParameter("@distance", distance));
        cmd.Parameters.Add(new SqlParameter("@maxdB", maxdB));
        cmd.Parameters.Add(new SqlParameter("@avgdB", avgdB));
        cmd.Parameters.Add(new SqlParameter("@defectLevel", defectLevel));
        cmd.Parameters.Add(new SqlParameter("@temperature", temperature));
        cmd.Parameters.Add(new SqlParameter("@humidity", humidity));
        cmd.Parameters.Add(new SqlParameter("@longitude", longitude));
        cmd.Parameters.Add(new SqlParameter("@latitude", latitude));
        cmd.Parameters.Add(new SqlParameter("@ReportDataID", key));

        int u = cmd.ExecuteNonQuery();
        if (u > 0)
        {
            Response.Write(" <script>alert('修改成功！');window.location.href='../Index.aspx'; </script>");
        }
        else
        {
            Response.Write(" <script>alert('修改失败，请重新编辑信息！'); </script>");
        }
        connection.Close();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportManager.aspx");
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
}