using CbaSodiq.Core.Models;
using CbaSodiq.Core.ViewModels;
using CbaSodiq.Core.ViewModels.UserViewModels;
using CbaSodiq.CustomAttribute;
using CbaSodiq.Data.Repositories;
using CbaSodiq.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CbaSodiq.Controllers
{
    public class UserManagerController : Controller
    {
        UserRepository userRepo = new UserRepository();
        BranchRepository branchRepo = new BranchRepository();
        RoleRepository roleRepo = new RoleRepository();

        UserLogic userLogic = new UserLogic();
        UtilityLogic utilLogic = new UtilityLogic();
        //
        // GET: /UserManager/
        public ActionResult Index()
        {
            return View(userRepo.GetAll());
        }

        [RestrictToAdmin]
        public ActionResult Create(string message)
        {
            ViewBag.Msg = message;
            ViewBag.Branches = branchRepo.GetAll().AsEnumerable().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });

            ViewBag.Roles = roleRepo.GetAll().AsEnumerable().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });
            var model = new AddNewUserViewModel();
            return View(model);
        }

        [RestrictToAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddNewUserViewModel model)
        {
            ViewBag.Msg = "";
            ViewBag.Branches = branchRepo.GetAll().AsEnumerable().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });
           
            ViewBag.Roles = roleRepo.GetAll().AsEnumerable().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.ID.ToString()
            });

            if (ModelState.IsValid)
            {
                //unique username and email
                if (!userLogic.IsUniqueUsername(model.Username))
                {
                    ViewBag.Msg = "Username must be unique";
                    return View();
                }
                if (!userLogic.IsUniqueEmail(model.Email))
                {
                    ViewBag.Msg = "Email must be unique";
                    return View();
                }

                string autoGenPassword = utilLogic.GetRandomPassword();
                string hashedPassword = UserLogic.HashPassword(autoGenPassword);
                User user = new Core.Models.User { FirstName = model.FirstName, LastName = model.LastName, Username = model.Username, PasswordHash = hashedPassword, Email = model.Email, PhoneNumber = model.PhoneNumber, Role = roleRepo.GetById(model.RoleId), Branch = branchRepo.GetById(model.BranchId) };

                userRepo.Insert(user);

                userLogic.SendPasswordToUser(model.LastName + " " + model.FirstName, model.Email, model.Username, autoGenPassword);
                                
                return RedirectToAction("Create", new { message = "User added" });
            }
            ViewBag.Msg = "Please enter a valid name";
            return View();
        }

        [RestrictToAdmin]
        public ActionResult Edit(int? id)
        {
            ViewBag.ErrorMsg = "";
            var user = userRepo.GetById((int)id);
            var model = new EditUserInformationViewModel();
            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                model.Id = user.ID;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                ViewBag.BranchId = new SelectList(branchRepo.GetAll(), "ID", "Name", user.Branch.ID);
                ViewBag.RoleId = new SelectList(roleRepo.GetAll(), "ID", "Name", user.Role.ID);
                return View(model);
            }               
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserInformationViewModel model)
        {
            ViewBag.BranchId = new SelectList(branchRepo.GetAll(), "ID", "Name", model.BranchId);
            ViewBag.RoleId = new SelectList(roleRepo.GetAll(), "ID", "Name", model.RoleId);
            try
            {
                if (ModelState.IsValid)
                {
                    var user = userRepo.GetById(model.Id);
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Branch = branchRepo.GetById(model.BranchId);
                    user.Role = roleRepo.GetById(model.RoleId);                  
                    userRepo.Update(user);

                    ViewBag.ErrorMsg = "Data modified successfully";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMsg = "Error updating user";
            }
            return View(model);
        }//end edit

        //get
        public ActionResult Login(string returnUrl)
        {
            ViewBag.Msg = "";
            ViewBag.accessMsg = Session["actionRestrictionMsg"];
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userLogic.FindUser(model.Username, model.Password);
                if (user != null)
                {
                    Session["username"] = user.Username;
                    Session["roleId"] = user.Role.ID;
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ViewBag.Msg = "Invalid username or password.";
                    return View();
                }
            }
            return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            Session.Remove("username");
            Session.Remove("roleId");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            ViewBag.ErrorMsg = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangeUserPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = getLoggedInUser();
                    if (user == null)
                    {
                        ViewBag.ErrorMsg = "You are not logged in";
                        return View();
                    }
                    bool isVerified = UserLogic.VerifyHashedPassword(user.PasswordHash, model.OldPass);
                    if (isVerified)
                    {
                        user.PasswordHash = UserLogic.HashPassword(model.NewPass);
                        userRepo.Update(user);
                        ViewBag.ErrorMsg = "Password changed successfully";
                        return View();
                    }
                    ViewBag.ErrorMsg = "Wrong password";
                    return View();
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log("Message= " + ex.Message + "\nInner Exception= " + ex.InnerException + "\n");
                    return PartialView("Error");
                }
            }
            ViewBag.ErrorMsg = "Invalid data";
            return View();
        }

        public User getLoggedInUser()
        {
            if (String.IsNullOrEmpty((String)Session["username"]))
            {
                return null;
            }
            return userRepo.GetByUsername((String)Session["username"]);
        }
           
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
	}
}