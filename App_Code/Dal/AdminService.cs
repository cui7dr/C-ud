using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Models;

/// <summary>
/// AdminService 的摘要说明
/// </summary>
/// 

namespace Dal
{
    public class AdminService
    {
        string connection = ConfigurationManager.ConnectionStrings["check"].ConnectionString;

        public Admin GetAdminByLoginId(string loginId)
        {
            string sql = "select * from [admin] where loginId = @loginId";
            Admin admin = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@loginId", loginId)))
            {
                if (reader.Read())
                {
                    admin = new Admin();
                    admin.AdminID = (int)reader["AdminID"];
                    admin.LoginID = (string)reader["loginID"];
                    admin.LoginPwd = (string)reader["loginPwd"];
                    reader.Close();
                }
            }
            return admin;
        }
    }
}
