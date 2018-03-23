using CbaSodiq.CustomAttribute;
using CbaSodiq.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CbaSodiq.Controllers
{
    [RestrictToLoggedIn]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CustomersCount = new CustomerRepository().GetCount();
            ViewBag.UsersCount = new UserRepository().GetCount();
            ViewBag.CustomerAccountCountsCount = new CustomerAccountRepository().GetCount();
            ViewBag.GlAccountCount = new GlAccountRepository().GetCount();
            return View();
        }

    }
}