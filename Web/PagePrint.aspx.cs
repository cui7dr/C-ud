using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class PagePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "报表打印";
        if (Session["name"] == null)
        {
            Response.Write(" <script>alert('您没有登录，请先登录！'); window.location.href='Default.aspx'; </script>");
        }
        else
        {
            if (!IsPostBack)
            {
                int ReportDataID = Convert.ToInt32(Request["ReportDataID"]);
                string str = ConfigurationManager.ConnectionStrings["check"].ConnectionString;
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                string sql = "select * from [ReportData] where ReportDataID = " + ReportDataID;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int PID = Convert.ToInt32(dr["PID"]);
                    Session["PID"] = PID;
                    this.lblCheckTime.Text = dr["checkTime"].ToString();
                    //this.lblInputTime.Text = dr["InputTime"].ToString();
                    this.lblLineName.Text = dr["LineName"].ToString();
                    this.lblLineNO.Text = dr["LineNO"].ToString();
                    //this.lblManageNO.Text = dr["ManageNo"].ToString();
                    this.lblProductType.Text = dr["CheckProductType"].ToString();
                    //this.lblProductState.Text = dr["ProductState"].ToString();
                    this.lblDefectLevel.Text = dr["DefectLevel"].ToString();
                    this.lblSuggestion.Text = dr["suggestion"].ToString();
                    this.lblCheckName.Text = dr["CheckName"].ToString();
                    //this.lblFrequency.Text = dr["Frequency"].ToString();
                    this.lblTemperature.Text = dr["Temperature"].ToString();
                    this.lblHumidity.Text = dr["Humidity"].ToString();
                    this.lblDistance.Text = dr["distance"].ToString();
                    this.lblLongitude.Text = dr["Longitude"].ToString();
                    this.lblLatitude.Text = dr["Latitude"].ToString();
                    this.lblMaxdB.Text = dr["MaxdB"].ToString();
                    this.lblAvgdB.Text = dr["AVGdB"].ToString();
                    this.Session["PoleNOPicPath"].ToString();
                    if (this.lblDefectLevel.Text == "异常")
                    {
                        this.lblDefectLevel.ForeColor = System.Drawing.Color.GreenYellow;
                    }
                    else if(this.lblDefectLevel.Text=="危险")
                    {
                        this.lblDefectLevel.ForeColor = System.Drawing.Color.Red;
                    }
                }
                dr.Close();
            }
        }
    }
}
 