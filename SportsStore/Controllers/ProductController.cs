using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 4;

        private IProductRepository repository;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int productPage = 1) 
            => View(repository.Products.OrderBy(e => e.ProductID)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize));

    }
}