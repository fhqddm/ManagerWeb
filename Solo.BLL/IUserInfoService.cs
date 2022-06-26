using Solo.Model;
using Solo.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.BLL
{
    public interface IUserInfoService
    {
        int AddUserInfo(UserInfo userInfo);

        UserInfo VerifyLogin(string UserName, string EncryptedPassword);
        UserLoginView CheckLogin(UserInfo userInfo);
        UserLoginView GetUserInfo(int userId);

    }
}
