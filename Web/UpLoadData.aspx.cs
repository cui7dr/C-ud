// UpLoadData
using ASP;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class UpLoadData : Page, IRequiresSessionState
{
    public static double pi = 3.1415926535897931;
    public static double a = 6378245.0;
    public static double ee = 0.0066934216229659433;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "文件上传";
        if (this.Session["name"] == null)
        {
            base.Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='Default.aspx'; </script>");
        }
        this.divStep2.Visible = false;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string statInfo = "";
        bool flag = false;
        string uploadPath = HttpContext.Current.Server.MapPath("upfile/bin/\\");
        string fileName = this.FileUpload.FileName;
        if (fileName != "")
        {
            string fileExtension = Path.GetExtension(this.FileUpload.FileName).ToLower();
            string[] allowedExtension = new string[1] { ".txt" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                if (fileExtension == allowedExtension[i])
                {
                    flag = true;
                }
            }
        }
        if (flag)
        {
            try
            {
                //对上传文件重命名（上传用户 + 时间）
                string rename = Path.GetFileName(this.FileUpload.PostedFile.FileName).Trim();
                string preName = this.Session["name"].ToString();
                string timeNow = DateTime.Now.ToString("yyyyMMdd HHmmss");
                timeNow = timeNow.Replace("-", "");
                timeNow = timeNow.Replace(":", "");
                timeNow = timeNow.Replace(" ", "");
                rename = preName.Trim() + "-" + timeNow + "-" + rename;
                double size = Math.Round((double)Convert.ToDecimal(HttpContext.Current.Request.ContentLength / 1024), 2);
                if (size < 4096.0)
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    //保存文件
                    this.FileUpload.PostedFile.SaveAs(HttpContext.Current.Request.MapPath("upfile/bin/") + rename);
                    //解析文件
                    string mapPath = "upfile/bin/" + rename;
                    string path = base.Server.MapPath(mapPath);
                    string content = "";
                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    BinaryReader binaryReader = new BinaryReader(fileStream);
                    //得到存储全部文件内容的字符串 content
                    for (int i = (int)fileStream.Length; i > 0; i--)
                    {
                        byte tempByte = binaryReader.ReadByte();
                        string tempStr = Convert.ToString(tempByte, 16);
                        if (tempStr.Length == 1)
                        {
                            tempStr = "0" + tempStr;
                        }
                        content += tempStr;
                    }
                    fileStream.Close();
                    binaryReader.Close();


                    //判断该文件的组数
                    int length = content.Length;
                    string mark = content.Substring(0, 12);
                    string[] dataBehind = content.Split(new string[] { mark }, StringSplitOptions.None);

                    for (int i = 1; i < dataBehind.Length; i++)
                    {

                        string data = mark + dataBehind[i];

                        //获取版本号
                        string version = data.Substring(0, 12);
                        string rVersion = "";
                        string realVersion = "";
                        for (int k = 0; k < 12; k += 2)
                        {
                            string rVer = Convert.ToInt32(version.Substring(k, 2), 16).ToString();
                            if (rVer.Length == 1)
                            {
                                rVer = "0" + rVer;
                            }
                            rVersion = rVersion + "." + rVer;
                            realVersion = rVersion.Substring(1, rVersion.Length - 1);
                        }

                        //获取设备串号
                        string Chuanhao = data.Substring(12, 24);
                        string realChuanhao = Chuanhao.ToString().ToUpper();//字母转大写

                        //获取记录号
                        string Jiluhao = content.Substring(36, 8);
                        string realJiluhao = Convert.ToInt32(Jiluhao.Substring(0, 8), 16).ToString();
                        string connectionString = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                        SqlConnection sqlConnection = new SqlConnection(connectionString);

                        //判断是否合法（利用设备串号 + 用户名验证是否匹配）
                        /* string userName = this.Session["name"].ToString();
                         string sqlGetNameAndIMEI = "SELECT pIMEI FROM [Product] inner join [User] on [Product].UID = [User].UID WHERE loginID = @loginID";
                         //string connectionString = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                         //SqlConnection sqlConnection = new SqlConnection(connectionString);
                         SqlCommand sqlCommand = new SqlCommand(sqlGetNameAndIMEI, sqlConnection);
                         sqlCommand.Parameters.Add(new SqlParameter("@loginID", userName));
                         sqlConnection.Open();
                         string checkEqual = sqlCommand.ExecuteScalar().ToString();
                         sqlConnection.Close();
                         if (checkEqual != realChuanhao)
                         {
                             base.Response.Write("<script>alert('上传文件内容不合法，上传数据不属于该用户，请检查！')</script>");
                             this.lblUploadInfo.Visible = true;
                             this.lblUploadInfo.Text = "上传文件内容不合法！";
                         }


                         else
                         {**/
                        
                        //获取时间
                        string getTime = data.Substring(44, 14);
                            string year0 = "";
                            string year1 = Convert.ToInt32(getTime.Substring(0, 2), 16).ToString();
                            int year2 = Convert.ToInt32(getTime.Substring(2, 2), 16);
                            string year3 = "";
                            if (year2 < 10)
                            {
                                year3 = "0" + year2.ToString();
                            }
                            else
                            {
                                year3 = year2.ToString();
                            }
                            if (year1 != "20")
                            {
                                year0 = Convert.ToInt32(getTime.Substring(0, 4), 16).ToString();
                            }
                            else
                            {
                                year0 = year1 + year3;
                            }
                            //string year = Convert.ToInt32(getTime.Substring(0, 4), 16).ToString();
                            string mounth = Convert.ToInt32(getTime.Substring(4, 2), 16).ToString();
                            string day = Convert.ToInt32(getTime.Substring(6, 2), 16).ToString();
                            string hour = Convert.ToInt32(getTime.Substring(8, 2), 16).ToString();
                            string minute = Convert.ToInt32(getTime.Substring(10, 2), 16).ToString();
                            string sesond = Convert.ToInt32(getTime.Substring(12, 2), 16).ToString();
                            string time = year0 + "-" + mounth + "-" + day + " " + hour + ":" + minute + ":" + sesond;


                        //获取温度
                        string getTemp = data.Substring(58, 2);
                            string temp = Convert.ToInt32(getTemp.Substring(0, 2), 16).ToString();
                            string realTemperature = temp + "℃";

                            //获取湿度
                            string getHum = data.Substring(60, 2);
                            string hum = Convert.ToInt32(getHum.Substring(0, 2), 16).ToString();
                            string realHumidity = hum + "%";

                            //获取经纬度信息
                            string getLocation = data.Substring(62, 30);
                            //获取经度
                            string rLongtitude = "";
                            for (int m = 0; m < 16; m += 2)
                            {
                                string pointLong = getLocation.Substring(m, 2).ToString();
                                if (pointLong == "2e")
                                {
                                    pointLong = ".";
                                }
                                else
                                {
                                    pointLong = Convert.ToInt32(pointLong).ToString();
                                }
                                rLongtitude += pointLong;
                            }
                            rLongtitude = Convert.ToDouble(rLongtitude).ToString();
                            //获取纬度
                            string getLatitude = data.Substring(80, 14);//(78,14)
                            string rLatitude = "";
                            for (int n = 0; n < 14; n += 2)
                            {
                                string pointLat = getLatitude.Substring(n, 2).ToString();
                                if (pointLat == "2e")
                                {
                                    pointLat = ".";
                                }
                                else
                                {
                                    pointLat = Convert.ToDouble(pointLat).ToString();
                                }
                                rLatitude += pointLat;
                            }
                            rLatitude = Convert.ToDouble(rLatitude).ToString();

                            //矫正经纬度信息
                            //获取页面输入信心
                            double viewLong = Convert.ToDouble(rLongtitude);
                            double viewLat = Convert.ToDouble(rLatitude);
                            double num10 = viewLong - 105.0;
                            double num11 = viewLat - 35.0;

                            //从地球到火星
                            //经度
                            double earthLong = 300.0 + num10 + 2.0 * num11 + 0.1 * num10 * num10 + 0.1 * num10 * num11 + 0.1 * Math.Sqrt(Math.Abs(num10));
                            earthLong += (20.0 * Math.Sin(6.0 * num10 * UpLoadData.pi) + 20.0 * Math.Sin(2.0 * num10 * UpLoadData.pi)) * 2.0 / 3.0;
                            earthLong += (20.0 * Math.Sin(num10 * UpLoadData.pi) + 40.0 * Math.Sin(num10 / 3.0 * UpLoadData.pi)) * 2.0 / 3.0;
                            earthLong += (150.0 * Math.Sin(num10 / 12.0 * UpLoadData.pi) + 300.0 * Math.Sin(num10 / 30.0 * UpLoadData.pi)) * 2.0 / 3.0;
                            //维度
                            double earthLat = -100.0 + 2.0 * num10 + 3.0 * num11 + 0.2 * num11 * num11 + 0.1 * num10 * num11 + 0.2 * Math.Sqrt(Math.Abs(num10));
                            earthLat += (20.0 * Math.Sin(6.0 * num10 * UpLoadData.pi) + 20.0 * Math.Sin(2.0 * num10 * UpLoadData.pi)) * 2.0 / 3.0;
                            earthLat += (20.0 * Math.Sin(num11 * UpLoadData.pi) + 40.0 * Math.Sin(num11 / 3.0 * UpLoadData.pi)) * 2.0 / 3.0;
                            earthLat += (160.0 * Math.Sin(num11 / 12.0 * UpLoadData.pi) + 320.0 * Math.Sin(num11 * UpLoadData.pi / 30.0)) * 2.0 / 3.0;

                            double rad = viewLat / 180.0 * UpLoadData.pi;
                            double magic = Math.Sin(rad);
                            magic = 1.0 - UpLoadData.ee * magic * magic;
                            double sqrtMagic = Math.Sqrt(magic);
                            earthLat = earthLat * 180.0 / (UpLoadData.a * (1.0 - UpLoadData.ee) / (magic * sqrtMagic) * UpLoadData.pi);
                            earthLong = earthLong * 180.0 / (UpLoadData.a / sqrtMagic * Math.Cos(rad) * UpLoadData.pi);

                            double marsLong = viewLong + earthLong;
                            double marsLat = viewLat + earthLat;

                            //从火星到百度
                            double bdpi = 52.3598775598298873;// π * 3000 / 180
                            double bdLong = Convert.ToDouble(marsLong);
                            double bdLat = Convert.ToDouble(marsLat);
                            double z = Math.Sqrt(bdLong * bdLong + bdLat * bdLat) + 2E-05 * Math.Sin(bdLat * bdpi);
                            double theta = Math.Atan2(bdLat, bdLong) + 3E-06 * Math.Cos(bdLong * bdpi);

                            double rLong = z * Math.Cos(theta) + 0.0065;
                            double rLat = z * Math.Sin(theta) + 0.006;

                            rLongtitude = Math.Round(rLong, 5).ToString();
                            rLatitude = Math.Round(rLat, 5).ToString();

                            //获取扩展信息
                            string getMess = data.Substring(62, 64);

                            //获取录波长度
                            string leng = data.Substring(126, 4);
                            if (leng == "fafa" | leng == "0704")
                            {
                                leng = "01f4";
                            }
                            string realLength = Convert.ToInt32(leng.Substring(0, 4), 16).ToString();

                            //判断该条数据是否存在
                            string dataSerial = "";
                        
                        int len = Convert.ToInt32(realLength);
                        
                        string getWaveData = data.Substring(130, len * 2);
                            for (int l = 0; l < len * 2; l += 2)
                            {
                                string str = getWaveData.Substring(l, 2).ToString();
                                dataSerial = dataSerial+ str;
                            }

                        /*string sqlRepeat = "SELECT count(*) FROM [ReportData] inner join [Product] on [ReportData].PID = [Product].PID where pVersion = @pVersion and pIMEI = @pIMEI = @pIMEI and ManageNO = @ManageNO";


                    SqlCommand cmdRepeat = new SqlCommand(sqlRepeat, sqlConnection);
                        cmdRepeat.Parameters.Add(new SqlParameter("@ManageNO", realJiluhao));
                        cmdRepeat.Parameters.Add(new SqlParameter("@pVersion", realVersion));
                        cmdRepeat.Parameters.Add(new SqlParameter("@pIMEI", realChuanhao));
                    sqlConnection.Open();
                        int report = Convert.ToInt32(cmdRepeat.ExecuteScalar());
                        sqlConnection.Close();

                        int reportCount = 0;
                        if (report > 0)
                        {
                            reportCount++;
                            if (reportCount == dataBehind.Length - 1)
                            {
                                this.lblUploadInfo.Visible = true;
                                this.lblUploadInfo.Text = "上传成功，但未发现新数据，请采集新数据再上传！";
                            }
                        }**/
                        //通过设备串号获取 PID
                        
                        string sqlGetPID = "SELECT PID FROM [Product] WHERE pIMEI = @pIMEI";
                            SqlCommand cmdGetPID = new SqlCommand(sqlGetPID, sqlConnection);
                            cmdGetPID.Parameters.Add(new SqlParameter("@pIMEI", realChuanhao));
                            sqlConnection.Open();
                            int PID = Convert.ToInt32(cmdGetPID.ExecuteScalar());
                            sqlConnection.Close();
                        

                        //插入到 record
                        string sqlInsertRecord = "insert into [dbo].[Record] values(@PID,@RecordNO,@checkTime,@Temperature,@Humidity,@Longitude,@Latitude,@DataSerial);";
                       
                        SqlCommand cmdInsert = new SqlCommand(sqlInsertRecord, sqlConnection);
                        
                        cmdInsert.Parameters.Add(new SqlParameter("@PID", PID));
                            cmdInsert.Parameters.Add(new SqlParameter("@RecordNO", realJiluhao));
                            cmdInsert.Parameters.Add(new SqlParameter("@checkTime", Convert.ToDateTime(time)));
                            cmdInsert.Parameters.Add(new SqlParameter("@Temperature", temp));
                            cmdInsert.Parameters.Add(new SqlParameter("@Humidity", hum));
                            cmdInsert.Parameters.Add(new SqlParameter("@Longitude", rLongtitude));
                            cmdInsert.Parameters.Add(new SqlParameter("@Latitude", rLatitude));
                            cmdInsert.Parameters.Add(new SqlParameter("@DataSerial", dataSerial));
                            sqlConnection.Open();
                        
                        cmdInsert.ExecuteNonQuery();
                        

                        sqlConnection.Close();

                            //同时插入 RecordData
                            string getWave = dataSerial;
                            int[] array2 = new int[len];
                            int j = 0;
                            for (int r = 0; r <= len * 2 - 2; r += 2)
                            {
                                int rLuboData1 = array2[j] = Convert.ToInt32(getWave.Substring(r, 2), 16);
                                array2[j] = rLuboData1;
                                j++;
                            }

                            //最大值
                            int maxDB = array2.Max();
                            //最小值
                            int minDB = array2.Min();
                            //两次循环求方差
                            int sum1 = 0;
                            double ex = 0.0;
                            for (int f = 0; f < len; f++)
                            {
                                sum1 += array2[f];
                            }
                            ex = (double)sum1 * 1.0 / len * 2;
                            double sum2 = 0.0;
                            for (int num29 = 0; num29 < len; num29++)
                            {
                                sum2 += ((double)array2[num29] - ex) * ((double)array2[num29] - ex);
                            }
                            double variance = sum2 / len;
                            //平均值
                            double avgDB = Math.Round(Convert.ToDouble(array2.Average()), 2);

                            //频率
                            int frequency = 0;
                            for (int f = 0; f <= len*2-2; f += 2)
                            {
                                int rLuboData = Convert.ToInt32(getWave.Substring(f, 2), 16);
                                if ((double)rLuboData > avgDB)
                                {
                                    frequency++;
                                }
                            }
                            string sqlInsertRecordData = "insert into [dbo].[ReportData](PID,ManageNO,checkTime,Frequency,MaxdB,AVGdB,DefectLevel,Temperature,Humidity,Longitude,Latitude,DataSerial) values(@PID,@RecordNO,@checkTime,@Frequency,@MaxdB,@AVGdB,@DefectLevel,@Temperature,@Humidity,@Longitude,@Latitude,@DataSerial);";
                        
                            SqlCommand cmdInsertRecordData = new SqlCommand(sqlInsertRecordData, sqlConnection);
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@PID", PID));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@RecordNO", realJiluhao));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@checkTime", Convert.ToDateTime(time)));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@Frequency", 40));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@MaxdB", maxDB));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@AVGdB", avgDB));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@DefectLevel", "未分析"));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@Temperature", temp));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@Humidity", hum));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@Longitude", rLongtitude));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@Latitude", rLatitude));
                            cmdInsertRecordData.Parameters.Add(new SqlParameter("@DataSerial", dataSerial));
                            sqlConnection.Open();
                        //base.Response.Write(" <script>alert('上传1失败！'); </script>");
                        int u = cmdInsertRecordData.ExecuteNonQuery();
                        //base.Response.Write(" <script>alert('上传2失败！'); </script>");
                        sqlConnection.Close();
                            if (u > 0)
                            {
                                this.Session["realVersion"] = realVersion;
                                this.Session["realChuanhao"] = realChuanhao;
                                this.lblUploadInfo.Visible = true;
                                this.lblUploadInfo.Text = "上传成功,请继续上传相关音频文件！";
                                statInfo = "true";
                            }
                            else
                            {
                                this.lblUploadInfo.Visible = true;
                                this.lblUploadInfo.Text = "上传失败";
                                statInfo = "false";
                                base.Response.Write(" <script>alert('上传失败，请检查相关文件！'); </script>");
                            }
                            sqlConnection.Close();
                        }
                    }
               // }


                else
                {
                    this.lblUploadInfo.Visible = true;
                    this.lblUploadInfo.Text = "请选择指定大小文件上传";
                    statInfo = "false";
                    base.Response.Write(" <script>alert('上传失败，请选择指定大小文件上传！'); </script>");
                }
            }
            catch (Exception)
            {
                this.lblUploadInfo.Visible = true;
                this.lblUploadInfo.Text = "上传失败";
                statInfo = "false";
                base.Response.Write(" <script>alert('上传失败！'); </script>");
            }
        }
        else
        {
            this.lblUploadInfo.Visible = true;
            this.lblUploadInfo.Text = "请选择指定文件上传";
            statInfo = "false";
        }
        if (statInfo == "true")
        {
            this.divStep2.Visible = true;
        }
    }
}
