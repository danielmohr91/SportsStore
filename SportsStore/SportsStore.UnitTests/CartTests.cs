﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Order_Screen()
        {
            // Arrange
            var productId = 36;
            var mock = CreateProductRepository(productId);
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product
                {
                    ProductID = productId,
                    Description = "D1",
                    Category = "C1",
                    Price = 123.45m,
                    Name = "N1"
                }
            }.AsQueryable());
            var cart = new Cart();
            var cartController = new CartController(mock.Object, null);

            // Act - Add product to cart
            var result = cartController.AddToCart(cart, productId, "myUrl");

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            var p1 = new Product
            {
                ProductID = 1,
                Name = "P1",
                Price = 100M
            };
            var p2 = new Product
            {
                ProductID = 2,
                Name = "P2",
                Price = 50M
            };

            // Arrange - create a new cart
            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            var result = target.ComputeTotalValue();

            // Assert
            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            var p1 = new Product
            {
                ProductID = 1,
                Name = "P1"
            };
            var p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };

            // Arrange - create a new cart
            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            var results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create some test products
            var p1 = new Product
            {
                ProductID = 1,
                Name = "P1"
            };
            var p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };

            // Arrange - create a new cart
            var target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            var results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange
            //  create cart, product, and create controller
            var productId = 36;
            var mock = CreateProductRepository(productId);
            var cart = new Cart();
            var controller = new CartController(mock.Object, null);

            // Act 
            //  Add a product to the cart
            controller.AddToCart(cart, productId, "TestUrl");

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1); // Ensure there's exactly one item in the cart
            Assert.AreEqual(cart.Lines.First().Product.ProductID, productId); // Ensure product in cart is the one we just created
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - create a mock order processor
            var mock = new Mock<IOrderProcessor>();
            // Arrange - create a cart with an item
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            var target = new CartController(null, mock.Object);

            // Act - try to checkout
            var result = target.Checkout(cart, new ShippingDetails());

            // Assert - check that the order has been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());
            // Assert - check that the method is returning the Completed view
            Assert.AreEqual("Completed", result.ViewName);
            // Assert - check that I am passing a valid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            var p1 = new Product
            {
                ProductID = 1,
                Name = "P1",
                Price = 100M
            };
            var p2 = new Product
            {
                ProductID = 2,
                Name = "P2",
                Price = 50M
            };

            // Arrange - create a new cart
            var target = new Cart();

            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act - reset the cart
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            var p1 = new Product
            {
                ProductID = 1,
                Name = "P1"
            };
            var p2 = new Product
            {
                ProductID = 2,
                Name = "P2"
            };
            var p3 = new Product
            {
                ProductID = 3,
                Name = "P3"
            };

            // Arrange - create a new cart
            var target = new Cart();
            // Arrange - add some products to the cart
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.AreEqual(target.Lines.Count(c => c.Product == p2), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange
            var cart = new Cart();
            var cartController = new CartController(null, null);

            // Act - call the index action method
            var result = (CartIndexViewModel) cartController.Index(cart, "myUrl").ViewData.Model; 
            
            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        //[TestMethod]
        //public void Can_Add_To_Cart() {

        //    // Arrange - create the mock repository
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[] {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples"},
        //    }.AsQueryable());

        //    // Arrange - create a Cart
        //    Cart cart = new Cart();

        //    // Arrange - create the controller
        //    CartController target = new CartController(mock.Object, null);

        //    // Act - add a product to the cart
        //    target.AddToCart(cart, 1, null);

        //    // Assert
        //    Assert.AreEqual(cart.Lines.Count(), 1);
        //    Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        //}

        //[TestMethod]
        //public void Adding_Product_To_Cart_Goes_To_Cart_Screen() {
        //    // Arrange - create the mock repository
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(m => m.Products).Returns(new Product[] {
        //        new Product {ProductID = 1, Name = "P1", Category = "Apples"},
        //    }.AsQueryable());

        //    // Arrange - create a Cart
        //    Cart cart = new Cart();

        //    // Arrange - create the controller
        //    CartController target = new CartController(mock.Object, null);

        //    // Act - add a product to the cart
        //    RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

        //    // Assert
        //    Assert.AreEqual(result.RouteValues["action"], "Index");
        //    Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        //}

        //[TestMethod]
        //public void Can_View_Cart_Contents() {
        //    // Arrange - create a Cart
        //    Cart cart = new Cart();

        //    // Arrange - create the controller
        //    CartController target = new CartController(null, null);

        //    // Act - call the Index action method
        //    CartIndexViewModel result
        //        = (CartIndexViewModel)target.Index("myUrl").ViewData.Model; // todo: add cart to this

        //    // Assert
        //    Assert.AreSame(result.Cart, cart);
        //    Assert.AreEqual(result.ReturnUrl, "myUrl");
        //}

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange - create a mock order processor
            var mock = new Mock<IOrderProcessor>();
            // Arrange - create an empty cart
            var cart = new Cart();
            // Arrange - create shipping details
            var shippingDetails = new ShippingDetails();
            // Arrange - create an instance of the controller
            var target = new CartController(null, mock.Object);

            // Act
            var result = target.Checkout(cart, shippingDetails);

            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());
            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            // Assert - check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange - create a mock order processor
            var mock = new Mock<IOrderProcessor>();
            // Arrange - create a cart with an item
            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            // Arrange - create an instance of the controller
            var target = new CartController(null, mock.Object);
            // Arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            // Act - try to checkout
            var result = target.Checkout(cart, new ShippingDetails());

            // Assert - check that the order hasn't been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());
            // Assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);
            // Assert - check that I am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        private static Mock<IProductRepository> CreateProductRepository(int productId)
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new[]
            {
                new Product
                {
                    Category = "Test Category",
                    Description = "Test Description",
                    Name = "Test Name",
                    Price = 5.99m,
                    ProductID = productId
                }
            }.AsQueryable());
            return mock;
        }
    }
}