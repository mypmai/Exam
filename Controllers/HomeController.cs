using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        private MainContext _context;
 
        public HomeController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel UserInput)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                UserInput.Password = Hasher.HashPassword(UserInput, UserInput.Password);
                User NewUser = new User
                {
                    First_name = UserInput.First_name,
                    Last_name = UserInput.Last_name,
                    Email = UserInput.Email,
                    Password = UserInput.Password
                    
                };

                List<User> RegisterUser = _context.User.Where(user => user.Email == UserInput.Email).ToList();
                if(RegisterUser.Count==0){
                    _context.User.Add(NewUser);
                    _context.SaveChanges();
                    List<User> CurUser = _context.User.Where(user => user.Email == UserInput.Email).ToList();
                    HttpContext.Session.SetString("CurUserName",CurUser[0].First_name);
                    HttpContext.Session.SetString("CurUserEmail",CurUser[0].Email);
                    HttpContext.Session.SetInt32("CurUserID",CurUser[0].UserId);
                    // System.Console.WriteLine(HttpContext.Session.GetInt32("CurUserID"));
                    return RedirectToAction("Dashboard","Auction");  
                }
                ViewBag.message="Email Already Exist";
            }
            return View("Index",UserInput);
        }
        

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email1, string Password1)
        {
            List<User> LoginUser=_context.User.Where(user => user.Email == Email1).ToList();
            if(LoginUser.Count==0){
                ViewBag.message="Email Don't Exist";
                return View("Index");
            }
            var Hasher = new PasswordHasher<User>();
            if(0 != Hasher.VerifyHashedPassword(LoginUser[0], LoginUser[0].Password, Password1))
                {
                    HttpContext.Session.SetInt32("Wallet",1000);
                    HttpContext.Session.SetInt32("CurUserID",LoginUser[0].UserId);
                    HttpContext.Session.SetString("CurUserName",LoginUser[0].First_name);
                    HttpContext.Session.SetString("CurUserEmail",LoginUser[0].Email);
                    return RedirectToAction("Dashboard","Auction"); 
                }
            
            ViewBag.message="Password Incorrect";
            return View("Index");
            
        }
       
        
        [HttpGet]
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


    }
}
