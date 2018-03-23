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
    public class NodeController : Controller
    {
        NodeRepository nodeRepo = new NodeRepository();
        //
        // GET: /Node/
        public ActionResult Index()
        {
            var nodes = nodeRepo.GetAll();
            return View(nodes);
        }

        //Get
        public ActionResult Create(string message)
        {
            ViewBag.Msg = message;
            var model = new Node();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Node model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //check uniqueness of name and code
                    if (!(nodeRepo.isUniqueName(model.Name)))
                    {
                        ViewBag.Msg = "Node's name must be unique";
                        return View();
                    }
                    nodeRepo.Insert(model);
                    return RedirectToAction("Create", new { message = "Successfully added Node!" });
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
            Node node = nodeRepo.GetById((int)id);// = db.Customers.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Node model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var node = nodeRepo.GetById(model.ID);
                    //check uniqueness of name and code
                    if (!(nodeRepo.isUniqueName(node.Name, model.Name)))
                    {
                        ViewBag.Msg = "Node's name must be unique";
                        return View();
                    }
                    nodeRepo.Update(model);
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