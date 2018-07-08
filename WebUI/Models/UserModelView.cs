using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
    

namespace WebUI.Models
{
    public class UserModelView
    {
        public string Id { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("(\\w*\\d*)*[-.]?(\\w*\\d*)?[@]\\w{1,10}[.]\\w{1,6}", ErrorMessage = "Некорректный Email адрес")]
        [Required(ErrorMessage = "Введите email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nick ")]
        [Required]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Адресс")]
        public string Address { get; set; }
        public IList<string> Roles { get; set; }

    }
}