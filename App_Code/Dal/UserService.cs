using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Models;

/// <summary>
/// UserService 的摘要说明
/// </summary>
/// 
namespace Dal
{
    public class UserService
    {
        string connection = ConfigurationManager.ConnectionStrings["check"].ConnectionString;

        public User GetUserByLoginId(string loginId)
        {
            string sql = "select * from [user] where loginId = @loginId";
            User user = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@loginId", loginId)))
            {
                if (reader.Read())
                {
                    user = new User();
                    user.UID = (int)reader["UID"];
                    user.LoginID = (string)reader["loginId"];
                    user.LoginPwd = (string)reader["loginPwd"];
                    //user.CompanyName = (string)reader["companyName"];
                    //user.CompanyAddress = (string)reader["companyAddress"];
                    reader.Close();
                }
            }
            return user;
        }
    }
}
