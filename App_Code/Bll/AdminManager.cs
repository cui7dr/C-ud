using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Models;
using Dal;

/// <summary>
/// AdminManager 的摘要说明
/// </summary>
/// 

namespace Bll
{
    public class AdminManager
    {
        public bool AdminLogin(string loginId, string loginPwd, out Admin validAdmin)
        {
            Admin admin = new AdminService().GetAdminByLoginId(loginId);
            if (admin == null)
            {
                validAdmin = null;//用户名不存在
                return false;
            }
            if (admin.LoginPwd == loginPwd)
            {
                validAdmin = admin;
                return true;
            }
            else
            {
                validAdmin = null;//用户名不存在
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
