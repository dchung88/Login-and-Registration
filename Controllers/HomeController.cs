using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using LoginRegistration.Models;

namespace LoginRegistration.Controllers
{
    public class HomeController : Controller
    {
        private LRContext dbContext;
        public HomeController(LRContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public RedirectToActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.users.Any(n => n.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email is already in use! Please enter a new email");
                    return RedirectToAction("Index");
                }
                else
                {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("InSession", newUser.UserId);
                return RedirectToAction("Success");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        [Route("login")]
        public ViewResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("loggedin")]
        public RedirectToActionResult CheckUser(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.users.FirstOrDefault(v => v.Email == userSubmission.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return RedirectToAction("Login");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Password", "Incorrect Password");
                    return RedirectToAction("Login");
                }
                else
                {
                    HttpContext.Session.SetInt32("InSession", userInDb.UserId);
                    return RedirectToAction("Success");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetString("InSession") == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Success");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
