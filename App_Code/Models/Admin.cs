using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Admin 的摘要说明
/// </summary>
/// 

namespace Models
{
    public class Admin
    {
        private int adminID;//管理员编号
        private string loginID;//管理员名称
        private string loginPwd;//管理员密码

        public int AdminID
        {
            get { return adminID; }
            set { adminID = value; }
        }

        public string LoginID
        {
            get { return loginID; }
            set { loginID = value; }
        }

        public string LoginPwd
        {
            get { return loginPwd; }
            set { loginPwd = value; }
        }
    }
}
