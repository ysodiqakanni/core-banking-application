using CbaSodiq.CustomAttribute;
using CbaSodiq.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CbaSodiq.Controllers
{
    [RestrictToAdmin]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index(string message)
        {
            ViewBag.Msg = message;
            return View();
        }

        public ActionResult OpenOrCloseBusiness()
        {
            try
            {
                EodLogic logic = new EodLogic();
                if (logic.isBusinessClosed())
                {
                    logic.OpenBusiness();
                }
                else
                {
                    string result = logic.RunEOD();
                    return RedirectToAction("Index", new { message = result });
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Message= " + ex.Message + "\nInner Exception= " + ex.InnerException + "\n");
                return PartialView("Error");
            }
            return RedirectToAction("Index");
        }
	}
}