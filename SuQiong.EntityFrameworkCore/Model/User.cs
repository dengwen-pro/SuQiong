using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuQiong.EntityFrameworkCore.Model
{
    public class User
    {

        //用户Id
        [Key]
        public int UserId { get; set; }
        //用户名
        public string UserName { get; set; }
        //用户密码
        public string Password { get; set; }

    }
}
