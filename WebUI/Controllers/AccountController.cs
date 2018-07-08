using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using WebUI.Models;
using Domain.Enities;
using System.Web.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUnitOfWork repository;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public AccountController(IUnitOfWork repo)
        {
            repository = repo;
            
        }

        [Authorize]
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Login()
        {
            
            return View();
        }

        public ViewResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(UserModelView model)
        {

            if (ModelState.IsValid)
            {
                AppUser user = await repository.UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    user = new AppUser { Email = model.Email, UserName = model.UserName };
                    var result = await repository.UserManager.CreateAsync(user, model.Password);
                    if (result.Errors.Count() > 0)
                    {
                        foreach(var error in result.Errors.ToList())
                        {
                            ModelState.AddModelError("", error);
                        }
                        return View(model);
                    }
                    await repository.UserManager.AddToRoleAsync(user.Id, "user");
                    ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = model.Address, Name = model.Name };
                    repository.ClientManager.Create(clientProfile); 
                    await repository.Save();
                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким Email уже существует!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Ошибка. Введенные данные не корректны");
                return View(model);
            }
                        
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ClaimsIdentity claim = null;
                UserModelView userM = new UserModelView
                {
                    UserName = model.Name,
                    Password = model.Password

                };
                AppUser user = await repository.UserManager.FindAsync(userM.UserName, userM.Password);
                if (user != null)
                {
                    claim = await repository.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    if (claim == null)
                    {
                        ModelState.AddModelError("", "Ошибка");
                    }
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        return RedirectToAction("Index", "Main");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Main");
        }
        
    }
}