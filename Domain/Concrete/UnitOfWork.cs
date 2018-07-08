using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Enities;
using Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Concrete
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {

        private EFDbContext _context;
        private EF.AppContext _appContext;
        private IClientManager clientManager;
        private IGenericRepository<Product> productRepo;
        private IGenericRepository<Account> accountRepo;
        private IGenericRepository<Order> orderRepo;
        private AppUserManager userManager;
        private AppRoleManager roleManager;


        public UnitOfWork(EFDbContext context, EF.AppContext appContext)
        {
            _context = context;
            _appContext = appContext;
            userManager = new AppUserManager(new UserStore<AppUser>(_appContext));
            roleManager = new AppRoleManager(new RoleStore<AppRole>(_appContext));
            clientManager = new ClientManager(_appContext);
        }

        public IGenericRepository<Order> Order
        {
            get
            {
                if (orderRepo == null)
                {
                    orderRepo = new EFGenericRepository<Order>(_context);
                }
                return orderRepo;
            }
        }

        public IGenericRepository<Product> Product
        {
            get
            {
                if (productRepo == null)
                {
                    productRepo = new EFGenericRepository<Product>(_context);
                }
                return productRepo;
            }
        }

        public IGenericRepository<Account> Account
        {
            get
            {
                if( accountRepo == null)
                {
                    accountRepo = new EFGenericRepository<Account>(_context);
                }
                return accountRepo;
            }
           
        }

        public AppUserManager UserManager
        {
            get { return userManager; }
        }

        public AppRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IClientManager ClientManager
        {
           get { return clientManager; }
        }
        
        public async Task Save()
        {
            await _appContext.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _appContext.Dispose();                              // этот момент надо прочекать
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            
        }
    }
}
