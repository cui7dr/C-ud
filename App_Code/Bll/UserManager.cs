using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Dal;
using Models;

/// <summary>
/// UserManager 的摘要说明
/// </summary>
/// 
namespace Bll
{
    public class UserManager
    {
        public bool UserLogin(string loginId, string loginPwd, out User validUser)
        {
            User user = new UserService().GetUserByLoginId(loginId);
            if (user == null)
            {
                validUser = null;//用户名不存在
                return false;
            }
            if (user.LoginPwd == loginPwd)
            {
                validUser = user;
                return true;
            }
            else
            {
                validUser = null;//密码错误
                return false;
            }
            throw new NotImplementedException();
        }
    }
}
