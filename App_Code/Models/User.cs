using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// User 的摘要说明
/// </summary>
/// 
namespace Models
{
    public class User
    {
        private int uID;//用户编号
        private string loginID;//用户名
        private string loginPwd;//用户密码
        private string companyName;//用户所属公司名称
        private string companyAddress;//用户公司地址

        public int UID
        {
            get { return uID; }
            set { uID = value; }
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

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public string CompanyAddress
        {
            get { return companyAddress; }
            set { companyAddress = value; }
        }
    }
}
