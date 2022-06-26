using System;
using System.Collections.Generic;
using System.Text;

namespace Solo.Model.ViewModel
{
    public class UserLoginView: BaseViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public int ExpireHours { get; set; }
        
    }
}
