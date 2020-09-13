using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Solo.Model
{
    public class UserInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "名不能为空!")]
        [MaxLength(20)]
        public string UserName { get; set; }

        public string EncryptedPassword { get; set; }

        public string Email { get; set; }

        public string RoleId { get; set; }
        
    }
}
