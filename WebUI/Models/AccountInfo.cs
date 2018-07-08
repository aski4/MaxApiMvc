using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using Domain.Enities;

namespace WebUI.Models
{
    public class AccountInfo
    {
        public IEnumerable<Account> Accounts { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage ="Введите ник")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Введите пароль")]
        public string Password { get; set; }
    }
}