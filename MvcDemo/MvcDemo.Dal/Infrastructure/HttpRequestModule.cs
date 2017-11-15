using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using MvcDemo.Dal.Interfaces;

namespace MvcDemo.Dal.Infrastructure
{
    public class HttpRequestModule : IHttpModule
    {
        public string ModuleName
        {
            get { return "MvcDemo.Dal.EF.HttpRequestModule"; }
        }

        public void Init(HttpApplication application)
        {
            application.EndRequest += new EventHandler(this.Application_EndRequest);
        }

        private void Application_EndRequest(object source, EventArgs e)
        {
            ServiceLocator.Current.GetInstance<IRepositoryContext>().Terminate();
        }

        public void Dispose() { }

    }
}
