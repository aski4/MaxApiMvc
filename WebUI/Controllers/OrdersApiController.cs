using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Abstract;
using Domain.Enities;
using WebUI.Models;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace WebUI.Controllers
{
    public class OrdersApiController : ApiController
    {
        private IUnitOfWork repository;
        

        public OrdersApiController(IUnitOfWork repo)
        {
            repository = repo;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<Order> List()
        {
            return repository.Order.GetInclude("Lines", "Lines.Product");
        }
        
        public async Task<IHttpActionResult> CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                IDictionary<int, Product> products = repository.Product.Get()
                    .Where(p => order.Lines.Select(ol => ol.ProductId)
                    .Any(id => id == p.ProductId)).ToDictionary(p => p.ProductId);

                order.TotalCost = order.Lines.Sum(ol => ol.Count * products[ol.ProductId].Price);
                await repository.Order.SaveEntityAsync(order, order.Id);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Roles()
        {
            AppUser user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
            return await repository.UserManager.GetRolesAsync(user.Id);
        }
        
        [HttpDelete]
        [Authorize(Roles ="admin")]
        public IHttpActionResult DeleteOrder(int id)
        {
            var result = repository.Order.Remove(id);
            return result == null ? (IHttpActionResult)BadRequest("Product No Found") : Ok(result);
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> Address()
        {
            AppUser user = await repository.UserManager.FindByNameAsync(User.Identity.Name);
            return user == null ? (IHttpActionResult)BadRequest("User not Found") : Ok(user.ClientProfile.Address);
        } 

        [HttpGet]
        public async Task<IEnumerable<UserModelView>> Users()
        {
            List<UserModelView> UserList = new List<UserModelView>();

            var UserR = repository.UserManager.Users
                .Include(x => x.ClientProfile)
                .Where(e => e.Id == e.ClientProfile.Id);
            foreach (var user in UserR) 
            {
                UserList.Add(new UserModelView
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.ClientProfile.Name,
                    Roles = await repository.UserManager.GetRolesAsync(user.Id),
                    Address = user.ClientProfile.Address
                });
                
            }
            
            return UserList;
        }
      
        
    }
}
