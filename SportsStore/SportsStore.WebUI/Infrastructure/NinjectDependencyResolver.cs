using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //kernel.Bind<IProductRepository>().To<EFProductRepository>(); // using mock for now... disabling this

            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                                             .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new List<Product>
            {
                new Product {ProductID = 1, Name = "Soccer Ball", Description = "Black and white soccer ball", Price = 5, Category = "Soccer"},
                new Product {ProductID = 2, Name = "Soccer Flag", Description = "Thrown when players are bad", Price = 15, Category = "Soccer"},
                new Product {ProductID = 3, Name = "Field Cone", Description = "Cone to mark boundary", Price = 5123, Category = "Soccer"},
                new Product {ProductID = 4, Name = "Basketball", Description = "A ball", Price = 35, Category = "Basketball"},
                new Product {ProductID = 5, Name = "Replacement Net", Description = "Medium weight, regulation size replacement net", Price = 55, Category = "Basketball"},
                new Product {ProductID = 6, Name = "Example Product", Description = "description here...", Price = 2, Category = "Test"}
            });
            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}