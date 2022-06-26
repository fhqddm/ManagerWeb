using Org.BouncyCastle.Asn1.Cmp;
using Solo.Model;
using Solo.Model.ViewModel;
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
                var count = mycontext.UserInfos.Where(x => x.UserName == userInfo.UserName).Count();
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


        public UserLoginView CheckLogin(UserInfo userInfo)
        {
            using (MyContext mycontext = new MyContext())
            {
                try
                {

                    var user =  mycontext.UserInfos.SingleOrDefault(x => x.UserName == userInfo.UserName && x.EncryptedPassword == userInfo.EncryptedPassword);
                    if (user != null)
                    {
                        return new UserLoginView
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            Email = user.Email,
                            Role = user.RoleId

                        };
                    }
                    return null;
                    
                }
                catch (Exception ex)
                {

                    return null;
                }


            }
        }

        public UserLoginView GetUserInfo(int userId)
        {
            using (MyContext mycontext = new MyContext())
            {
                try
                {

                    var user = mycontext.UserInfos.SingleOrDefault(x => x.Id == userId);
                    if (user != null)
                    {
                        return new UserLoginView
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            Email = user.Email,
                            Role = user.RoleId

                        };
                    }
                    return null;

                }
                catch (Exception ex)
                {

                    return null;
                }


            }
        }

        public int UpdateUserInfos(UserInfo userInfo)
        {
            using (MyContext myContext = new MyContext())
            {

                var info = myContext.UserInfos.SingleOrDefault(x => x.Id == userInfo.Id);
                 


                return myContext.SaveChanges();
            }
        }

    }
}
