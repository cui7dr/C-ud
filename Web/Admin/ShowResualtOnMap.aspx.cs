using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class ShowResualtOnMap : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "位置信息展示";
        if (Session["AdminName"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！');window.location.href='../Admin.aspx'; </script>");
        }
        else
        {
            if (!IsPostBack)
            {
                #region 默认情况下输出全部数据（由于百度API功能，暂限制为100）
                //声明数据库链接
                string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection Connection = new SqlConnection(strconn);
                Connection.Open();
                string str = "select top 100 Longitude + ',' + Latitude as Location from [ReportData]";
                SqlCommand command = new SqlCommand(str, Connection);
                command.CommandType = CommandType.Text;
                //声明数据集
                DataSet dataset = new DataSet();       
                SqlDataAdapter adapter = new SqlDataAdapter(str, strconn);
                //读取数据放入数据集
                adapter.Fill(dataset);
                DataTable dt = dataset.Tables[0];
                //获取公司名称
                string sqlgetName = "select top 100 ReportDataID, companyName from [ReportData] inner join [Product] on [ReportData].PID=[Product].PID inner join [User] on [Product].UID= [User].UID";
                SqlCommand cmdGetName = new SqlCommand(sqlgetName, Connection);
                cmdGetName.CommandType = CommandType.Text;
                DataSet dsGetname = new DataSet();
                SqlDataAdapter sdaGetName = new SqlDataAdapter(sqlgetName, strconn);
                sdaGetName.Fill(dsGetname);
                DataTable dtGetName = dsGetname.Tables[0];
                //关闭数据库
                Connection.Close();
                //公司名称
                string[] arrayGetName = new string[dtGetName.Rows.Count];
                for (int i = 0; i < dtGetName.Rows.Count; i++)
                {
                    arrayGetName[i] = (dtGetName.Rows[i]["companyName"].ToString());
                }
                string comName = "";
                for (int i = 0; i < dtGetName.Rows.Count; i++)
                {
                    comName = (arrayGetName[i].Split(',')[0]).ToString() + "," + comName;
                }
                comName = comName.Remove(comName.LastIndexOf(","), 1);
                Session["ComPanyName"] = comName;
                //存入缓存
                //将datatable转换为数组 
                string[] Array = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Array[i] = (dt.Rows[i]["Location"].ToString());
                }
                string lon = "";
                string lat = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lon = (Array[i].Split(',')[0]).ToString() + "," + lon;
                    lat = (Array[i].Split(',')[1]).ToString() + "," + lat;
                }
                lon = lon.Remove(lon.LastIndexOf(","), 1);
                lat = lat.Remove(lat.LastIndexOf(","), 1);
                Session["Longitude"] = lon;
                Session["Latitude"] = lat;
                #endregion
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //声明数据库链接
        string strconn = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
        SqlConnection Connection = new SqlConnection(strconn);
        Connection.Open();
        string str = "";
        string selectedItem = this.ddlSelectItem.SelectedItem.ToString();
        if (selectedItem == "所有数据")
        {
            str = "select top 100 Longitude + ',' + Latitude as Location from [ReportData]";
        }
        if (selectedItem == "故障数据")
        {
            str = "select top 100 Longitude + ',' + Latitude as Location from [ReportData] where DefectLevel not in('未分析') and DefectLevel> 25";
        }
        SqlCommand command = new SqlCommand(str, Connection);
        command.CommandType = CommandType.Text;
        //声明数据集 
        DataSet dataset = new DataSet();      
        SqlDataAdapter adapter = new SqlDataAdapter(str, strconn);
        //读取数据放入数据集
        adapter.Fill(dataset);
        //Connection.Close();//关闭数据库
        DataTable dt = dataset.Tables[0];
        //ddl.DataTextField = "Location";
        //ddl.DataSource = dt;//下拉列表绑定查询的10条的数据表
        //ddl.DataBind();
        //获取公司名称
        string sqlgetName = "";
        if (selectedItem == "所有数据")
        {
            sqlgetName = "select top 100 companyName from [ReportData] inner join [Product] on [ReportData].PID=[Product].PID inner join [User] on [Product].UID= [User].UID";
        }
        if (selectedItem == "故障数据")
        {
            sqlgetName = "select top 100 companyName from [ReportData] inner join [Product] on [ReportData].PID=[Product].PID inner join [User] on [Product].UID= [User].UID  where DefectLevel not in('未分析') and DefectLevel > 25";
        }
        SqlCommand cmdGetName = new SqlCommand(sqlgetName, Connection);
        cmdGetName.CommandType = CommandType.Text;
        DataSet dsGetname = new DataSet();
        SqlDataAdapter sdaGetName = new SqlDataAdapter(sqlgetName, strconn);
        sdaGetName.Fill(dsGetname);
        DataTable dtGetName = dsGetname.Tables[0];
        Connection.Close();
        //公司名称
        int nameCount = dtGetName.Rows.Count;
        if (nameCount <= 0)
        {
            //Response.Write(" <script>alert('暂无符合条件的数据!'); </script>");
            Response.Write("<script language='JavaScript'>alert('暂无符合条件的数据!');window.location='UserManager.aspx';</script>");
        }
        else
        {
            string[] arrayGetName = new string[nameCount];
            for (int i = 0; i < nameCount; i++)
            {
                arrayGetName[i] = (dtGetName.Rows[i]["companyName"].ToString());
            }
            string comName = "";
            for (int i = 0; i < nameCount; i++)
            {
                comName = (arrayGetName[i].Split(',')[0]).ToString() + "," + comName;
            }
            comName = comName.Remove(comName.LastIndexOf(","), 1);
            Session["ComPanyName"] = comName;
            //存入缓存
            //DataTable dtMessageInfo = new DataTable();
            //将datatable转换为数组 
            string[] Array = new string[nameCount];
            for (int i = 0; i < nameCount; i++)
            {
                Array[i] = (dt.Rows[i]["Location"].ToString());
            }
            //Double[] lon = new Double[dt.Rows.Count];
            //Double[] lat = new Double[dt.Rows.Count];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    lon[i] = Convert.ToDouble((Array[i].Split(',')[0]));
            //    lat[i] = Convert.ToDouble((Array[i].Split(',')[1]));
            //}
            string lon = "";
            string lat = "";
            for (int i = 0; i < nameCount; i++)
            {
                lon = (Array[i].Split(',')[0]).ToString() + "," + lon;
                lat = (Array[i].Split(',')[1]).ToString() + "," + lat;
            }
            lon = lon.Remove(lon.LastIndexOf(","), 1);
            lat = lat.Remove(lat.LastIndexOf(","), 1);
            Session["Longitude"] = lon;
            Session["Latitude"] = lat;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='JavaScript'>window.location='UserManager.aspx';</script>");
    }
}
