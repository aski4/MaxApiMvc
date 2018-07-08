using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enities;

namespace Domain.Identity
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store) 
            : base(store)
        {
            UserValidator = new UserValidator<AppUser>(this) { AllowOnlyAlphanumericUserNames = false };
            
        }

        
    }
}
