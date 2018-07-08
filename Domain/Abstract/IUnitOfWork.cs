using Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.Abstract
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> Product { get; }
        IGenericRepository<Account> Account { get; }
        IGenericRepository<Order> Order { get; }
        IClientManager ClientManager { get; }
        
        AppUserManager UserManager { get; }
        AppRoleManager RoleManager { get; }

        Task Save();
    }
}
