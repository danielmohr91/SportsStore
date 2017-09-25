using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers {

    public class CartController : Controller {
        private IProductRepository _repository;
        private IOrderProcessor _orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor processor) {
            _repository = repo;
            _orderProcessor = processor;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel {Cart = GetCart(), ReturnUrl = returnUrl});
        }

        private Cart GetCart()
        {
            var cart = (Cart) Session["cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId,
                string returnUrl) {
            var product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId,
                string returnUrl) {
            var product = _repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart) {
            return PartialView(cart);
        }

        public ViewResult Checkout() {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) {
            if (!cart.Lines.Any()) {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid) {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            } else {
                return View(shippingDetails);
            }
        }

    }
}
