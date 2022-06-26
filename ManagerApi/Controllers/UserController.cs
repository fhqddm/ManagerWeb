using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ManagerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using Solo.BLL;
using Solo.Model;
using Solo.Model.ViewModel;

namespace ManagerApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly ISuggestService _suggestService;
        private readonly TokenManagement _tokenManagement;

        public UserController(IUserInfoService userInfoService,ISuggestService suggestService, IOptions<TokenManagement> tokenManagement)
        {
            _userInfoService = userInfoService;
            _suggestService = suggestService;
            _tokenManagement = tokenManagement.Value;
        }
        public IActionResult Login([FromForm]UserInfo userInfo)
        {
            return Ok(Authenticate(userInfo));

        }

        public IActionResult Register(UserInfo userInfo)
        {
            if (_userInfoService.AddUserInfo(userInfo) == 1)
            {
                //BaseResult<UserLoginView> result = new BaseResult<UserLoginView>();
                return Ok(Authenticate(userInfo));
                //return Ok(userLoginView);

            }
            else
            {
                return Ok("用户名已被使用，请重试");
            }
            
        }

        public IActionResult Logout(UserInfo userInfo) 
        {
            HttpContext.Session.Clear();
            return Ok("注销");
        
        }

        [Authorize(Policy = "RequireLogin")]
        [HttpPost]
        public IActionResult GetUserInfo()
        {
            int userid = 0;
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userid);
            if (userid>0)
            {
                var userView = _userInfoService.GetUserInfo(userid);
                return Ok(userView);
            }
            else
            {
                return Ok("获取信息失败");
            }
            
        }

        [HttpPost]
        public IActionResult AddSuggest(Suggest suggest)
        {
            return Ok(_suggestService.AddSuggest(suggest));
        }

        private UserLoginView Authenticate(UserInfo userInfo)
        {
            UserLoginView userLoginView = _userInfoService.CheckLogin(userInfo);
            if (userLoginView != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier,userLoginView.UserId.ToString()),
                    new Claim(ClaimTypes.Name,userLoginView.Username),
                    new Claim(ClaimTypes.Role,userLoginView.Role),
                    new Claim(ClaimTypes.Email,userLoginView.Email)

                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims,
                    expires: DateTime.Now.AddHours(_tokenManagement.AccessExpiration),
                    signingCredentials: credentials);
                userLoginView.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                userLoginView.ExpireHours = _tokenManagement.AccessExpiration;
                userLoginView.Succeed = true;
                return userLoginView;
            }
            else
            {
                userLoginView = new UserLoginView();
                userLoginView.Succeed = false;
                userLoginView.Message = "用户名或密码错误";
                return userLoginView;
            }
        }

        
         
    }
}
