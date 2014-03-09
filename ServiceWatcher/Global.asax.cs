using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ServiceWatcher.Facade;
using ServiceWatcher.Facade.Interface;
using StructureMap;

namespace ServiceWatcher
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //this.RegisterTypes();

        }

        //private void RegisterTypes()
        //{
        //    IContainer container=new Container();

        //    container.Configure(p=>p.Scan(
        //        assembly =>
        //        {
        //            assembly.AssemblyContainingType<ServiceCommandHandler>();
        //            assembly.AddAllTypesOf<ICommandHandler<>();
        //            ;
        //        }
        //        ));
        //}
    }
}
