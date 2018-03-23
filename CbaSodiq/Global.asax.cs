using CbaSodiq.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CbaProcessor;

namespace CbaSodiq
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //create an AccountConfiguration entity
            InitializeAccountConfig();

            //start CBA processor 
          
            Processor processor = new Processor();
            processor.BeginProcess();
        }

        private void InitializeAccountConfig()
        {
            new ConfigurationRepository().Initialize();
        }
    }
}
