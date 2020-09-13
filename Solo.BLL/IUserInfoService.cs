using Solo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IUserInfoService
    {
        int AddUserInfo(UserInfo userInfo);

        UserInfo VerifyLogin(string UserName, string EncryptedPassword);

    }
}
