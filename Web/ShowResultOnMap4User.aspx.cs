using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class ShowResultOnMap4User : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                int GetAllCount = GetAll();
                if (GetAllCount == 0)
                {
                    Response.Write("<script language='JavaScript'>alert('暂无数据，请上传数据后再来查看！'); window.location='Index.aspx';</script> ");
                }
                else
                {
                    //填充数据
                    int all = GetAll();
                    this.lblAll.Text = all.ToString();
                    int analysised = GetAnalysised();
                    this.lblAnalysised.Text = analysised.ToString();
                    int bad = GetBad();
                    this.lblBad.Text = bad.ToString();

                    #region 默认情况下输出全部数据（暂定200）
                    string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(strconn);
                    cnn.Open();
                    int PID = GetPID();
                    string sql = "select top 200 Longitude + ',' + Latitude as Location from [ReportData] where [ReportData].PID = " + PID + "";
                    SqlCommand cmd = new SqlCommand(sql,cnn);
                    cmd.CommandType = CommandType.Text;
                    DataSet dataset = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, strconn);
                    adapter.Fill(dataset);
                    DataTable dt = dataset.Tables[0];

                    #region 获取公司名称
                    string sqlGetName = "select top 200 ReportDataID, DefectLevel from [ReportData] inner join [Product] on [ReportData].PID = [Product].PID inner join [User] on [Product].UID = [User].UID where [ReportData].PID = " + PID + "";
                    SqlCommand cmdGetName = new SqlCommand(sqlGetName, cnn);
                    cmdGetName.CommandType = CommandType.Text;
                    DataSet dsGetName = new DataSet();
                    SqlDataAdapter sdaGetName = new SqlDataAdapter(sqlGetName, strconn);
                    sdaGetName.Fill(dsGetName);
                    DataTable dtGetName = dsGetName.Tables[0];
                    //公司名称
                    string[] arrayGetName = new string[dtGetName.Rows.Count];
                    for(int i = 0; i < dtGetName.Rows.Count; i++)
                    {
                        arrayGetName[i] = (dtGetName.Rows[i]["DefectLevel"].ToString());
                    }
                    string comName = "";
                    for(int i = 0; i < dtGetName.Rows.Count; i++)
                    {
                        comName = "缺陷程度：" + (arrayGetName[i].Split(',')[0]).ToString() + "," + comName;
                    }
                    comName = comName.Remove(comName.LastIndexOf(","), 1);
                    Session["ComPanyName"] = comName;
                    #endregion 获取公司名称

                    //获取当前点的时间
                    string sqlGetCheckTime = "select top 200 ReportDataID, checkTime from [ReportData] inner join [Product] on [ReportData].PID = [Product].PID inner join [User] on [Product].UID = [User].UID where [ReportData].PID=" + PID + "";
                    SqlCommand cmdGetCheckTime = new SqlCommand(sqlGetCheckTime, cnn);
                    cmdGetCheckTime.CommandType = CommandType.Text;
                    DataSet dsGetCheckTime = new DataSet();
                    SqlDataAdapter sdaGetCheckTime = new SqlDataAdapter(sqlGetCheckTime, strconn);
                    sdaGetCheckTime.Fill(dsGetCheckTime);
                    DataTable dtGetCheckTime = dsGetCheckTime.Tables[0];
                    string[] arrayGetCheckTime = new string[dtGetCheckTime.Rows.Count];
                    for(int i=0;i< dtGetCheckTime.Rows.Count; i++)
                    {
                        arrayGetCheckTime[i] = (dtGetCheckTime.Rows[i]["checkTime"].ToString());
                    }
                    string comCheckTime = "";
                    for(int i=0;i< dtGetCheckTime.Rows.Count; i++)
                    {
                        comCheckTime = "检测时间：" + (arrayGetCheckTime[i].Split(',')[0]).ToString() + "," + comCheckTime;
                    }
                    comCheckTime = comCheckTime.Remove(comCheckTime.LastIndexOf(","), 1);
                    Session["comCheckTime"] = comCheckTime;
                    /*
                     * 存入缓存
                     */
                    //将 dataTabe 转换为数组
                    string[] array = new string[dt.Rows.Count];
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        array[i] = (dt.Rows[i]["Location"].ToString());
                    }
                    string lon = "";
                    string lat = "";
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        lon = (array[i].Split(',')[0].ToString() + "," + lon);
                        lat = (array[i].Split(',')[1].ToString() + "," + lat);
                    }
                    lon = lon.Remove(lon.LastIndexOf(","), 1);
                    lat = lat.Remove(lat.LastIndexOf(","), 1);
                    Session["Longitude"] = lon;
                    Session["Latitude"] = lat;
                    #endregion
                }
            }
        }
    }

    /*
     * 获取 PID
     */
     protected int GetPID()
    {
        string name = Session["name"].ToString();
        string strconn= ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string sqlGetUID = "select UID from [User] where loginID = @loginID";
        cmd.Parameters.Add(new SqlParameter("@loginID", name));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetUID;
        int loginID = Convert.ToInt32(cmd.ExecuteScalar());
        string sqlGetPID = "select PID from [Product] where UID = @UID";
        cmd.Parameters.Add(new SqlParameter("@UID", loginID));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetPID;
        int PID = Convert.ToInt32(cmd.ExecuteScalar());
        return PID;
    }

    /*
     * 获取总数
     */
    protected int GetAll() {
        int pid=GetPID();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string sqlGetAll = "select count(*) from [ReportData] where PID = @PID";
        cmd.Parameters.Add(new SqlParameter("@PID", pid));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sqlGetAll;
        int all = Convert.ToInt32(cmd.ExecuteScalar());
        return all;
    }

    /*
     * 获取已分析数
     */
     protected int GetAnalysised()
    {
        int pid = GetPID();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string sql = "select count(*) from [ReportData] where PID = @PID and DefectLevel not in ('未分析')";
        cmd.Parameters.Add(new SqlParameter("@PID", pid));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        int analysised = Convert.ToInt32(cmd.ExecuteScalar());
        return analysised;
    }

    /*
     * 获取故障数
     */
     protected int GetBad()
    {
        int pid = GetPID();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection cnn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand();
        cnn.Open();
        cmd.Connection = cnn;
        string sql = "select count(*) from [ReportData] where PID = @PID and DefectLevel not in ('未分析') and DefectLevel > 25";
        cmd.Parameters.Add(new SqlParameter("@PID", pid));
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        int bad = Convert.ToInt32(cmd.ExecuteScalar());
        return bad;
    }

    protected void btnSearch_Click (object sender,EventArgs e)
    {
        int pid = GetPID();
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection cnn = new SqlConnection(strconn);
        cnn.Open();
        string sql = "";
        string selectedItem = this.ddlSelectItem.SelectedItem.ToString();
        if (selectedItem == "所有数据")
        {
            sql = "select top 200 Longitude + ',' + Latitude as Location from [ReportData] where [ReportData].PID = " + pid + "";
        }
        if (selectedItem == "故障数据")
        {
            sql= "select top 200 Longitude + ',' + Latitude as Location from [ReportData] where [ReportData].PID = " + pid + "and DefectLevel not in ('未分析') and DefectLevel > 25";
        }
        SqlCommand cmd = new SqlCommand(sql,cnn);
        cmd.CommandType = CommandType.Text;
        DataSet ds = new DataSet();
        SqlDataAdapter sda= new SqlDataAdapter(sql, strconn);
        sda.Fill(ds);
        DataTable dt = ds.Tables[0];
        string sqlGetName = "";
        if (selectedItem == "所有数据")
        {
            sqlGetName= "select top 200 DefectLevel from [ReportData] inner join [Product] on [ReportData].PID inner join [User] on [Product].UID = [User].UID where [ReportData].PID = " + pid + "";
        }
        if (selectedItem == "故障数据")
        {
            sqlGetName = "select top 200 DefectLevel from [ReportData] inner join [Product] on [ReportData].PID inner join [User] on [Product].UID = [User].UID where [ReportData].PID = " + pid + "and DefectLevel not in ('未分析') and DefectLevel > 25";
        }
        SqlCommand cmdGetName = new SqlCommand(sqlGetName, cnn);
        cmdGetName.CommandType = CommandType.Text;
        DataSet dsGetName = new DataSet();
        SqlDataAdapter sdaGetName = new SqlDataAdapter(sqlGetName, strconn);
        sdaGetName.Fill(dsGetName);
        DataTable dtGetName = dsGetName.Tables[0];
        cnn.Close();
        //公司名称
        int nameCount = dtGetName.Rows.Count;
        if (nameCount < 0)
        {
            Response.Write("<script language='JavaScript'>alert('暂无符合条件的数据！'); window.location.href='Index.aspx'; </script>");
        }
        else
        {
            //开始
            string[] arrayGetName = new string[nameCount];
            for(int i = 0; i < nameCount; i++)
            {
                arrayGetName[i] = (dtGetName.Rows[i]["DefectLevel"].ToString());
            }
            string comName = "";
            for(int i = 0; i < nameCount; i++)
            {
                comName = "缺陷程度：" + (arrayGetName[i].Split(',')[0]).ToString() + "," + comName;
            }
            comName = comName.Remove(comName.LastIndexOf(","), 1);
            Session["comPanyName"] = comName;
            //获取当前时间点
            string sqlGetCheckTime = "";
            if (selectedItem == "所有数据")
            {
                //sqlGetName
                sqlGetCheckTime = "select top 200 checkTime from [ReportData] inner join [Product] on [ReportData].PID = [Product].PID inner join [User] on [Product].UID = [User].UID where [ReportData].PID = " + pid + "";
            }
            if (selectedItem == "故障数据")
            {
                sqlGetCheckTime = "select top 200 checkTime from [ReportData] inner join [Product] on [ReportData].PID = [Product].PID inner join [User] on [Product].UID =  [User].UID  where [ReportData].PID = " + pid + " and DefectLevel not in ('未分析') and DefectLevel > 25";
            }
            SqlCommand cmdGetCheckTime = new SqlCommand(sqlGetCheckTime, cnn);
            cmdGetCheckTime.CommandType = CommandType.Text;
            DataSet dsGetCheckTime = new DataSet();
            SqlDataAdapter sdaGetCheckTime = new SqlDataAdapter(sqlGetCheckTime, strconn);
            sdaGetCheckTime.Fill(dsGetCheckTime);
            DataTable dtGetCheckTime = dsGetCheckTime.Tables[0];
            string[] arrayGetCheckTime = new string[dtGetCheckTime.Rows.Count];
            for(int i=0;i< dtGetCheckTime.Rows.Count; i++)
            {
                arrayGetCheckTime[i] = (dtGetCheckTime.Rows[i]["checkTime"].ToString());
            }
            string comCheckTime = "";
            for(int i = 0; i < dtGetCheckTime.Rows.Count; i++)
            {
                comCheckTime = "检测时间：" + (arrayGetCheckTime[i].Split(',')[0]).ToString() + "," + comCheckTime;
            }
            comCheckTime = comCheckTime.Remove(comCheckTime.LastIndexOf(","), 1);
            Session["comCheckTime"] = comCheckTime;
            string[] array = new string[nameCount];
            for(int i = 0; i < nameCount; i++)
            {
                array[i] = (dt.Rows[i]["Location"].ToString());
            }
            string lon = "";
            string lat = "";
            for(int i = 0; i < nameCount; i++)
            {
                lon = (array[i].Split(',')[0].ToString()) + "," + lon;
                lat = (array[i].Split(',')[1].ToString()) + "," + lat;
            }
            lon = lon.Remove(lon.LastIndexOf(","), 1);
            lat = lat.Remove(lat.LastIndexOf(","), 1);
            Session["Longitude"] = lon;
            Session["Latitude"] = lat;
        }
    }

    protected void btnBackToIndex_Click(object sender ,EventArgs e)
    {
        Response.Write("<script language = 'JavaScript'>window.location = 'Index.aspx';</script>");
    }
}