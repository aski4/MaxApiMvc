using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Domain.Enities;
using System.Data.Entity;

namespace Domain.EF
{
    public class AppContext : IdentityDbContext<AppUser>
    {
        public AppContext() : base("UserStore") { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

       
    }
}
