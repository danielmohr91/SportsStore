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
                new Product {ProductID = 1, Name = "P1", Description = "test", Price = 5, Category = "Cat 1"},
                new Product {ProductID = 2, Name = "P2", Description = "test", Price = 15, Category = "Cat 1"},
                new Product {ProductID = 3, Name = "P3", Description = "test", Price = 5123, Category = "Cat 1"},
                new Product {ProductID = 4, Name = "P4", Description = "test", Price = 35, Category = "Cat 2"},
                new Product {ProductID = 5, Name = "P5", Description = "test", Price = 55, Category = "Cat 2"},
                new Product {ProductID = 6, Name = "P6", Description = "test", Price = 2, Category = "Cat 2"}
            });
            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}