using CbaSodiq.Core.Models;
using CbaSodiq.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CbaSodiq.Controllers
{
    public class AtmTerminalController : Controller
    {
        AtmTerminalRepo atmRepo = new AtmTerminalRepo();
        // GET: AtmTerminal
        public ActionResult Index()
        {
            var terminals = atmRepo.GetAll();
            return View(terminals);
        }

        //Get
        public ActionResult Create(string message)
        {
            ViewBag.Msg = message;
            var model = new AtmTerminal();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AtmTerminal model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //check uniqueness of name and code
                    if (!(atmRepo.isUniqueCode(model.Code) && atmRepo.isUniqueName(model.Name)))
                    {
                        ViewBag.Msg = "Terminal name and code must be unique";
                        return View();
                    }
                    atmRepo.Insert(model);
                    return RedirectToAction("Create", new { message = "Successfully added Terminal!" });
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log("Message= " + ex.Message + "\nInner Exception= " + ex.InnerException + "\n");
                    return PartialView("Error");
                }
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Msg = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtmTerminal terminal = atmRepo.GetById((int)id);// = db.Customers.Find(id);
            if (terminal == null)
            {
                return HttpNotFound();
            }
            return View(terminal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AtmTerminal model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var terminal = atmRepo.GetById(model.ID);
                    //check uniqueness of name and code
                    if (!(atmRepo.isUniqueCode(terminal.Code, model.Code) && atmRepo.isUniqueName(terminal.Name, model.Name)))
                    {
                        ViewBag.Msg = "Terminal name and code must be unique";
                        return View();
                    }
                    atmRepo.Update(model);
                    ViewBag.Msg = "Updated";
                    return View();                    
                }
                ViewBag.Msg = "Please enter correct data";
                return View();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Message= " + ex.Message + "\nInner Exception= " + ex.InnerException + "\n");
                return PartialView("Error");
            }
        }
    }
}