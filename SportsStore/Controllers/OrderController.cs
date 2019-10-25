using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController: Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService){
            repository = repoService;
            cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order){
            if(cart.Lines.Count() == 0){
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }

            if(ModelState.IsValid){
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else{
                return View(order);
            }
        } 

        public ViewResult Completed(){
            cart.Clear();
            return View();
        }

        public ViewResult List() => 
            View(repository.Orders.Where(p => !p.Shipped));

        [HttpPost]
        public IActionResult MarkShipped(int orderID){
            Order order = repository.

        }
    }
}