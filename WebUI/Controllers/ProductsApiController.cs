using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain.Abstract;
using Domain.Enities;
using System.Web.Http.Cors;

namespace WebUI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProductsApiController : ApiController
    {
        private IUnitOfWork repository;

        public ProductsApiController(IUnitOfWork repo)
        {
            repository = repo;
        }
        
        public IEnumerable<Product> GetProducts()
        {
            return repository.Product.Get();
        }

        public IHttpActionResult GetProduct(int id)
        {
            Product result = repository.Product.Get().Where(p => p.ProductId == id).FirstOrDefault();
            return result == null ? (IHttpActionResult)BadRequest("Product No Found") : Ok(result);
        }

        [Authorize(Roles = "admin")]
        public IHttpActionResult PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Product.SaveEntity(product, product.ProductId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //[Authorize(Roles = "admin")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var result = repository.Product.Remove(id);
            return result == null ? (IHttpActionResult)BadRequest("Product No Found") : Ok(result);
        }
    }
}
