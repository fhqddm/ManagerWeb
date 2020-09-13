using Solo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solo.BLL
{
    public class UserInfoService : IUserInfoService
    {
        
        public int AddUserInfo(UserInfo userInfo)
        {
            using (MyContext mycontext = new MyContext())
            {
                var count = mycontext.UserInfos.Where(x => x.UserName == 
                    userInfo.UserName && x.EncryptedPassword== userInfo.EncryptedPassword).Count();
                if (count==0)
                {
                    mycontext.UserInfos.Add(userInfo);
                    return mycontext.SaveChanges();
                }
                else
                {
                    return 0;
                }
            }
        }

        public UserInfo VerifyLogin(string UserName,string EncryptedPassword)
        {
            using (MyContext mycontext = new MyContext())
            {
                try
                {
                    return mycontext.UserInfos.Single(x => x.UserName == UserName && x.EncryptedPassword == EncryptedPassword);
                }
                catch (Exception ex)
                {

                    return null;
                }
                
                
            }
        }
    }
}
