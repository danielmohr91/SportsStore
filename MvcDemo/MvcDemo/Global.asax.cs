using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcDemo.Controllers;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using MvcDemo.Infrastracture.Mvc;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.ServiceLocation;
using MvcDemo.Core;
using MvcDemo.Core.Interfaces;
using MvcDemo.Dal.Interfaces;
using MvcDemo.Dal;
using MvcDemo.Models;
using MvcDemo.Dal.Infrastructure;

namespace MvcDemo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer _container;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" }); 

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Dms", action = "Index", id = "" }  // Parameter defaults    
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            
            /*//Configure container with web.config
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(_container, "containerOne");*/

            _container = new UnityContainer()
                .RegisterType<IDocumentService, DocumentService>(new ContainerControlledLifetimeManager())
                .RegisterType<IFileService, FileService>()
                .RegisterType<IDocumentRepository, DocumentRepository>()
                .RegisterType<IFileRepository, FileRepository>()
                .RegisterType<IRepositoryContext, DmsRepositoryContext>()
                .RegisterType<IFormsAuthenticationService, FormsAuthenticationService>()
                .RegisterType<IMembershipService, AccountMembershipService>();

            //Set for Controller Factory
            IControllerFactory controllerFactory = new UnityControllerFactory(_container);

            ControllerBuilder.Current.SetControllerFactory(controllerFactory);


        }

        protected void Application_Error(object sender, EventArgs e)
        {

            Exception exception = Server.GetLastError();

            // Log the exception.
            //ILogger logger = Container.Resolve<ILogger>();
            //logger.Error(exception);

            Response.Clear();


            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 500:
                        // Server error.
                        routeData.Values.Add("action", "HttpError500");
                        break;
                    // Here you can handle Views to other error codes.
                    // I choose a General error template  
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
            }

            // Pass exception details to the target error View.
            routeData.Values.Add("Shared", exception);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

        }
    }
}