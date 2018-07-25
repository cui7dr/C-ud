using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class DetailEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "信息编辑";
        if (!IsPostBack)
        {
            string key = Session["ReportDataID"].ToString();
            GetInfoByID(key);
        }
        if (Session["name"] != null)
        {
            string username = Session["name"].ToString();
        }
    }

    /*
     * 通过 ID 获取详情列表的相关数据信息
     */
    private void GetInfoByID(string key)
    {
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection connection = new SqlConnection(strconn);
        connection.Open();
        string sql = "select * from [ReportData] where ReportDataID = " + key;
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.CommandType = CommandType.Text;
        SqlDataReader dr = cmd.ExecuteReader();
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
            this.txtLineNO.Text = dr["LineNO"].ToString();
            this.txtManagerNO.Text = dr["ManageNO"].ToString();
            //this.txtManagerNO.ReadOnly = true;
            //设置设备种类
            if (dr["CheckProductType"].ToString() != null && dr["CheckProductType"].ToString() != "")
            {
                this.ddlProductType.SelectedValue = dr["CheckProductType"].ToString();
            }
            //设置设备状态
            if (dr["ProductState"].ToString() != null && dr["ProductState"].ToString() != "")
            {
                this.ddlProductState.SelectedValue = dr["ProductState"].ToString();
            }
            //设置诊断建议
            this.txtSuggestion.Text = dr["Suggestion"].ToString();
            this.txtCheckName.Text = dr["CheckName"].ToString();
            this.datetimepickerEnd.Value = dr["checkTime"].ToString();
            this.datetimepickerEnd.Disabled = true;
            //设置频率
            this.txtFrequency.Text = dr["Frequency"].ToString();
            //this.txtFrequency.ReadOnly = true;
            //设置距离
            this.txtDistance.Text = dr["Distance"].ToString();
            //this.txtDistance.ReadOnly = true;
            this.txtLatitude.Text = dr["Latitude"].ToString();
            //this.txtLatitude.ReadOnly = true;
            this.txtLongitude.Text = dr["Longitude"].ToString();
            //this.txtLongitude.ReadOnly = true;
            this.txtTemperature.Text = dr["Temperature"].ToString();
            //this.txtTemperature.ReadOnly = true;
            this.txtHumidity.Text = dr["Humidity"].ToString();
            //this.txtHumidity.ReadOnly = true;
            this.txtMaxdB.Text = dr["MaxdB"].ToString();
            //this.txtMaxdB.ReadOnly = true;
            this.txtAvgdB.Text = Math.Round((Convert.ToDouble(dr["AVGdB"])), 2).ToString();
            //this.txtAvgdB.ReadOnly = true;
        }
        dr.Close();
    }

    #region    处理图片以及 Bin 文件的上传
    /*
     * 上传电杆编号
     */
    protected void btnNOPic_Click(object sender, EventArgs e)
    {
        string uploadPath = HttpContext.Current.Server.MapPath("upFile/images/") + "\\";
        string fileName = Path.GetFileName(FileNOPic.PostedFile.FileName).Trim();
        string fileExtension = Path.GetExtension(fileName);
        string newName1 = "";
        if (Session["name"] != null)
        {
            newName1 = Session["name"].ToString();
        }
        string newName = DateTime.Now.ToString("yyyyMMdd HHmmss");
        newName = newName.Replace("-", "");
        newName = newName.Replace(":", "");
        newName = newName.Replace(" ", "");
        fileName = newName1.Trim() + "-" + newName + "-" + fileName;
        //缩略图
        string thumbnail = "s_" + fileName;
        //服务器端文件路径
        string webFilePath = Server.MapPath("upFile/images/") + fileName;
        //服务器端缩略图文件路径
        string thumWebFilePath = Server.MapPath("upFile/images/") + thumbnail;
        string hasFile = this.FileNOPic.Value;
        if (hasFile != null && hasFile != "")
        {
            Double size = Math.Round((Double)Convert.ToDecimal(HttpContext.Current.Request.ContentLength / 1024), 2);
            if (size < 10240)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                FileNOPic.PostedFile.SaveAs(Server.MapPath("upFile/images/") + fileName);
                /*
                 * 缩略图处理
                 */
                #region 缩略图处理
                try
                {
                    //原始图
                    Image originalImage = Image.FromFile(webFilePath);
                    int width = 800;
                    int height = 600;
                    int toWidth = width;
                    int toHeight = height;
                    int x = 0;
                    int y = 0;
                    int origWidth = originalImage.Width;
                    int origHeight = originalImage.Height;
                    string mode = "HW";
                    switch (mode)
                    {
                        //指定高宽缩放（可能变形）
                        case "HW":
                            break;
                        //指定宽，高按比例
                        case "W":
                            toHeight = originalImage.Height * width / originalImage.Width;
                            break;
                        //指定高，宽按比例
                        case "H":
                            toWidth = originalImage.Height * height / originalImage.Height;
                            break;
                        //指定比例（不变形）
                        case "Cut":
                            if ((double)originalImage.Width / (double)originalImage.Height > (double)toWidth / (double)toHeight)
                            {
                                origHeight = originalImage.Height;
                                origWidth = originalImage.Height * toWidth / toHeight;
                                y = 0;
                                x = (originalImage.Width - origWidth) / 2;
                            }
                            else
                            {
                                origWidth = originalImage.Width;
                                origHeight = originalImage.Width * height / toWidth;
                                x = 0;
                                y = (originalImage.Height - origHeight) / 2;
                            }
                            break;
                        default:
                            break;
                    }
                    //新建一个 BMP 图片
                    Image bitmap = new Bitmap(toWidth, toHeight);
                    //新建一个画板
                    Graphics gh = Graphics.FromImage(bitmap);
                    //设置高质量插值法
                    gh.InterpolationMode = InterpolationMode.High;
                    //设置高质量低速度呈现平滑程度
                    gh.SmoothingMode = SmoothingMode.HighQuality;
                    //清空画布并以透明背景色填充
                    gh.Clear(Color.Transparent);
                    //在指定位置并且按指定大小绘制原图片的指定部分
                    gh.DrawImage(originalImage, new Rectangle(0, 0, toWidth, toHeight), new Rectangle(x, y, origWidth, origHeight), GraphicsUnit.Pixel);
                    try
                    {
                        //以 jpg 格式保存缩略图
                        bitmap.Save(thumWebFilePath, ImageFormat.Jpeg);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        gh.Dispose();
                        //删除缩略图
                        File.Delete(webFilePath);
                    }
                }
                catch (Exception)
                {
                    this.lblNOPic.Visible = true;
                    this.lblNOPic.Text = "上传失败";
                }
                #endregion

                string PoleNOPicPath = "upFile/images/" + thumbnail;
                int key = Convert.ToInt32(Session["ReportDataID"]);
                string sql = "update [ReportData] set PoleNOPicPath = @PoleNOPicPath where ReportDataID = @ReportDataID";
                string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection connection = new SqlConnection(strconn);
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add(new SqlParameter("@PoleNOPicPath", PoleNOPicPath));
                cmd.Parameters.Add(new SqlParameter("@ReportDataID", key));
                int u = cmd.ExecuteNonQuery();
                if (u > 0)
                {
                    this.lblNOPic.Visible = true;
                    this.lblNOPic.Text = "上传成功";
                }
                else
                {
                    this.lblNOPic.Visible = true;
                    this.lblNOPic.Text = "上传失败";
                }
            }
            else
            {
                this.lblNOPic.Visible = true;
                this.lblNOPic.Text = "文件大小不符合要求！";
            }
        }
        else
        {
            this.lblNOPic.Visible = true;
            this.lblNOPic.Text = "请选择需要上传的照片！";
        }
    }

    /*
     * 上传整体照片
     */
    protected void btnFullPic_Click(object sender, EventArgs e)
    {
        string uploadPath = HttpContext.Current.Server.MapPath("upFile/images/") + "\\";
        string fileName = Path.GetFileName(FileFullPic.PostedFile.FileName).Trim();
        string fileExtension = Path.GetExtension(fileName);
        string newName1 = "";
        if (Session["name"] != null)
        {
            newName1 = Session["name"].ToString();
        }
        string newName = DateTime.Now.ToString("yyyyMMdd HHmmss");
        newName = newName.Replace("-", "");
        newName = newName.Replace(":", "");
        newName = newName.Replace(" ", "");
        fileName = newName1.Trim() + "-" + newName + "-" + fileName;
        string thumbnail = "s_" + fileName;
        string webFilePath = Server.MapPath("upFile/images/") + fileName;
        string thumWebFilePath = Server.MapPath("upFile/images/") + thumbnail;
        string hasFile = this.FileFullPic.Value;
        if (hasFile != null && hasFile != "")
        {
            Double size = Math.Round((Double)Convert.ToDecimal(HttpContext.Current.Request.ContentLength / 1024), 2);
            if (size < 10240)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                FileFullPic.PostedFile.SaveAs(Server.MapPath("upFile/images/") + fileName);
                /*
                 * 缩略图处理
                 */
                #region 缩略图处理
                try
                {
                    Image originalImage = Image.FromFile(webFilePath);
                    int width = 800;
                    int height = 600;
                    int toWidth = width;
                    int toHeight = height;
                    int x = 0;
                    int y = 0;
                    int origWidth = originalImage.Width;
                    int origHeight = originalImage.Height;
                    string mode = "HW";
                    switch (mode)
                    {
                        case "HW":
                            break;
                        case "W":
                            toHeight = originalImage.Height * width / originalImage.Width;
                            break;
                        case "H":
                            toWidth = originalImage.Height * height / originalImage.Height;
                            break;
                        case "Cut":
                            if ((double)originalImage.Width / (double)originalImage.Height > (double)toWidth / (double)toHeight)
                            {
                                origHeight = originalImage.Height;
                                origWidth = originalImage.Height * toWidth / toHeight;
                                y = 0;
                                x = (originalImage.Width - origWidth) / 2;
                            }
                            else
                            {
                                origWidth = originalImage.Width;
                                origHeight = originalImage.Width * height / toWidth;
                                x = 0;
                                y = (originalImage.Height - origHeight) / 2;
                            }
                            break;
                        default:
                            break;
                    }
                    Image bitmap = new Bitmap(toWidth, toHeight);
                    Graphics gh = Graphics.FromImage(bitmap);
                    gh.InterpolationMode = InterpolationMode.High;
                    gh.SmoothingMode = SmoothingMode.HighQuality;
                    gh.Clear(Color.Transparent);
                    gh.DrawImage(originalImage, new Rectangle(0, 0, toWidth, toHeight), new Rectangle(x, y, origWidth, origHeight), GraphicsUnit.Pixel);
                    try
                    {
                        bitmap.Save(thumWebFilePath, ImageFormat.Jpeg);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        gh.Dispose();
                        File.Delete(webFilePath);
                    }
                }
                catch (Exception)
                {
                    this.lblFullPic.Visible = true;
                    this.lblFullPic.Text = "上传失败";
                }
                #endregion

                string WholePicPath = "upFile/images/" + thumbnail;
                int key = Convert.ToInt32(Session["ReportDataID"]);
                string sql = "update [ReportData] set WholePicPath = @WholePicPath where ReportDataID = @ReportDataID";
                string str = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@WholePicPath", WholePicPath));
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
                this.lblFullPic.Text = "文件大小不符合要求！";
            }
        }
        else
        {
            this.lblFullPic.Visible = true;
            this.lblFullPic.Text = "请重新选择需要上传的照片！";
        }
    }

    /*
     * 上传不良设备照片
     */
    protected void btnBadPic_Click(object sender, EventArgs e)
    {
        string uploadPath = HttpContext.Current.Server.MapPath("upFile/images/") + "\\";
        string fileName = Path.GetFileName(FileBadPic.PostedFile.FileName).Trim();
        string fileExtension = Path.GetExtension(fileName);
        string newName1 = "";
        if (Session["name"] != null)
        {
            newName1 = Session["name"].ToString();
        }
        string newName = DateTime.Now.ToString("yyyyMMdd HHmmss");
        newName = newName.Replace("-", "");
        newName = newName.Replace(":", "");
        newName = newName.Replace(" ", "");
        fileName = newName1.Trim() + "-" + newName + "-" + fileName;
        string thumbnail = "s_" + fileName;
        string webFilePath = Server.MapPath("upFile/images/") + fileName;
        string thumWebFilePath = Server.MapPath("upFile/images/") + thumbnail;
        string hasFile = this.FileBadPic.Value;
        if (hasFile != null && hasFile != "")
        {
            Double size = Math.Round((Double)Convert.ToDecimal(HttpContext.Current.Request.ContentLength / 1024), 2);
            if (size < 10240)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                FileBadPic.PostedFile.SaveAs(Server.MapPath("upFile/images/") + fileName);
                /*
                 * 缩略图处理
                 */
                #region 缩略图处理
                try
                {
                    Image originalImage = Image.FromFile(webFilePath);
                    int width = 800;
                    int height = 600;
                    int toWidth = width;
                    int toHeight = height;
                    int x = 0;
                    int y = 0;
                    int origWidth = originalImage.Width;
                    int origHeight = originalImage.Height;
                    string mode = "HW";
                    switch (mode)
                    {
                        case "HW":
                            break;
                        case "W":
                            toHeight = originalImage.Height * width / originalImage.Width;
                            break;
                        case "H":
                            toWidth = originalImage.Height * height / originalImage.Height;
                            break;
                        case "Cut":
                            if ((double)originalImage.Width / (double)originalImage.Height > (double)toWidth / (double)toHeight)
                            {
                                origHeight = originalImage.Height;
                                origWidth = originalImage.Height * toWidth / toHeight;
                                y = 0;
                                x = (originalImage.Width - origWidth) / 2;
                            }
                            else
                            {
                                origWidth = originalImage.Width;
                                origHeight = originalImage.Width * height / toWidth;
                                x = 0;
                                y = (originalImage.Height - origHeight) / 2;
                            }
                            break;
                        default:
                            break;
                    }
                    Image bitmap = new Bitmap(toWidth, toHeight);
                    Graphics gh = Graphics.FromImage(bitmap);
                    gh.InterpolationMode = InterpolationMode.High;
                    gh.SmoothingMode = SmoothingMode.HighQuality;
                    gh.Clear(Color.Transparent);
                    gh.DrawImage(originalImage, new Rectangle(0, 0, toWidth, toHeight), new Rectangle(x, y, origWidth, origHeight), GraphicsUnit.Pixel);
                    try
                    {
                        bitmap.Save(thumWebFilePath, ImageFormat.Jpeg);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        gh.Dispose();
                        File.Delete(webFilePath);
                    }
                }
                catch
                {
                    this.lblBadPic.Visible = true;
                    this.lblBadPic.Text = "上传失败";
                }
                #endregion

                string BadPicPath = "upFile/images/" + thumbnail;
                int key = Convert.ToInt32(Session["ReportDataID"]);
                string sql = "update [ReportData] set BadProductPicPath = @BadPicPath where ReportDataID = @ReportDataID";
                string str = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BadPicPath", BadPicPath));
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
                this.lblBadPic.Text = "文件大小不符合要求！";
            }
        }
        else
        {
            this.lblBadPic.Visible = true;
            this.lblBadPic.Text = "请重新选择需要上传的照片！";
        }
    }
    #endregion

    protected void Button1_Click(object sender,EventArgs e)
    {
        //获取前端输入数据
        string lineName = this.txtLineName.Text;
        DateTime timeStart = Convert.ToDateTime(this.datetimepickerStart.Value);
        string lineNO = this.txtLineNO.Text;
        string managerNO = this.txtManagerNO.Text;
        string checkProductType = this.ddlProductType.SelectedValue;
        string productState = this.ddlProductState.SelectedValue;
        string suggestion = this.txtSuggestion.Text;
        string checkName = this.txtCheckName.Text;
        string frequency = this.txtFrequency.Text;
        DateTime checkTime = Convert.ToDateTime(this.datetimepickerEnd.Value);
        float distance = float.Parse(this.txtDistance.Text);
        float temperature = float.Parse(this.txtTemperature.Text);
        float humidity = float.Parse(this.txtHumidity.Text);
        string longitude = this.txtLongitude.Text;
        string latitude = this.txtLatitude.Text;
        float maxdB = float.Parse(this.txtMaxdB.Text);
        double avgdB = Math.Round((float.Parse(this.txtAvgdB.Text)), 2);

        int key = Convert.ToInt32(Session["ReportDataID"]);
        int PID = Convert.ToInt32(Session["PID"]);
        //数据库写入
        string sql = "update [ReportData] set PID = @PID, LineName = @LineName, [LineNO] = @LineNO, CheckProductType = @CheckProductType, ProductState = @ProductState, ManageNO = @ManageNO, CheckTime = @checkTime, InputTime = @InputTime, suggestion = @suggestion, CheckName = @CheckName, Frequency = @Frequency, distance = @distance, MaxdB = @MaxdB, AVGdB = @AVGdB, DefectLevel = @DefectLevel, Temperature = @Temperature, Humidity = @Humidity, Longitude = @Longitude, Latitude = @Latitude where ReportDataID = @ReportDataID";
        string str = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection conn = new SqlConnection(str);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@PID", PID));
        cmd.Parameters.Add(new SqlParameter("@LineName", lineName));
        cmd.Parameters.Add(new SqlParameter("@LineNO", lineNO));
        cmd.Parameters.Add(new SqlParameter("@CheckProductType", checkProductType));
        cmd.Parameters.Add(new SqlParameter("@ProductState", productState));
        cmd.Parameters.Add(new SqlParameter("@ManageNo", managerNO));
        cmd.Parameters.Add(new SqlParameter("@checkTime", checkTime));
        cmd.Parameters.Add(new SqlParameter("@InputTime", timeStart));
        cmd.Parameters.Add(new SqlParameter("@suggestion", suggestion));
        cmd.Parameters.Add(new SqlParameter("@CheckName", checkName));
        cmd.Parameters.Add(new SqlParameter("@Frequency", frequency));
        cmd.Parameters.Add(new SqlParameter("@distance", distance));
        cmd.Parameters.Add(new SqlParameter("@MaxdB", maxdB));
        cmd.Parameters.Add(new SqlParameter("@AVGdB", avgdB));
        cmd.Parameters.Add(new SqlParameter("@DefectLevel", "20"));//待改
        cmd.Parameters.Add(new SqlParameter("@Temperature", temperature));
        cmd.Parameters.Add(new SqlParameter("@Humidity", humidity));
        cmd.Parameters.Add(new SqlParameter("@Longitude", longitude));
        cmd.Parameters.Add(new SqlParameter("@Latitude", latitude));
        cmd.Parameters.Add(new SqlParameter("@ReportDataID", key));
        int u = cmd.ExecuteNonQuery();
        if (u > 0)
        {
            Response.Write("<script>alert('修改成功!'); window.location.href='Index.aspx';</script>");
        }
        else
        {
            Response.Write("<script>alert('修改失败，请重新修改!');</script>");
        }
        conn.Close();
    }

    protected void Button2_Click(object sender,EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }
}
