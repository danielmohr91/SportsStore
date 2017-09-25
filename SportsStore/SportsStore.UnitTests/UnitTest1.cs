using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Carrots"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"}
            });

            // Arrange - create the controller
            var target = new NavController(mock.Object);

            // Act = get the set of categories 
            var model = (Product[]) target.Menu().Model; //  TODO: Resume here on this unit test...
            var results = model.OrderBy(x => x.ProductID).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 4);
            Assert.AreEqual(results[0].Category, "Apples");
            Assert.AreEqual(results[1].Category, "Carrots");
            Assert.AreEqual(results[2].Category, "Plums");
            Assert.AreEqual(results[3].Category, "Oranges");
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });

            // Arrange - create a controller and make the page size 3 items
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Action
            var result = ((ProductsListViewModel) controller.List("Cat2", 1).Model)
                .Products.ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }


        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange - define an HTML helper - we need to do this
            // in order to apply the extension method
            HtmlHelper myHelper = null;

            // Arrange - create PagingInfo data
            var pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });

            // create a controller and make the page size 3 items
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result
                = (ProductsListViewModel) controller.List(null, 2).Model;

            // Assert
            var prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });

            // Arrange
            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // Act
            var result
                = (ProductsListViewModel) controller.List(null, 2).Model;


            // Assert
            var pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }


        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
            });

            // Arrange - create a controller and make the page size 3 items
            var target = new ProductController(mock.Object);
            target.PageSize = 3;

            // Action - test the product counts for different categories
            var res1 = ((ProductsListViewModel) target
                .List("Cat1").Model).PagingInfo.TotalItems;
            var res2 = ((ProductsListViewModel) target
                .List("Cat2").Model).PagingInfo.TotalItems;
            var res3 = ((ProductsListViewModel) target
                .List("Cat3").Model).PagingInfo.TotalItems;
            var resAll = ((ProductsListViewModel) target
                .List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange
            // - create the mock repository
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 4, Name = "P2", Category = "Oranges"}
            });

            // Arrange - create the controller 
            var target = new NavController(mock.Object);

            // Arrange - define the category to selected
            var categoryToSelect = "Apples";

            // Action
            var result = "test"; //target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }
    }
}