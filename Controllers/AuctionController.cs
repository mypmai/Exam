using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    public class AuctionController : Controller
    {
        private MainContext _context;
 
        public AuctionController(MainContext context)
        {
            _context = context;
        }

         [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard(){
            if(HttpContext.Session.GetString("CurUserEmail")==null){
               return RedirectToAction("Index","Home");
            }
            ViewBag.CurUserName=HttpContext.Session.GetString("CurUserName");

            IEnumerable<Product> AllProduct=_context.Product.Include(p=>p.Bid).OrderBy(d=>d.End_Date).ToList();
            ViewBag.CurUserID=HttpContext.Session.GetInt32("CurUserID");
            
            ViewBag.CurUserWallet=HttpContext.Session.GetInt32("Wallet");

            return View("Dashboard",AllProduct);
        }

        [HttpGet]
        [Route("Product")]
        public IActionResult Product(){
            if(HttpContext.Session.GetString("CurUserEmail")==null){
               return RedirectToAction("Index","Home");
            }
        
            return View("Product");
        }
        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateProduct(ProductViewModel CreateForm){
           
            if(HttpContext.Session.GetString("CurUserEmail")==null){
               return RedirectToAction("Index","Home");
            }

            if(ModelState.IsValid)
            {

                 Product NewProduct = new Product
                {
                    Name = CreateForm.Name,
                    Description = CreateForm.Description,
                    Start_Bid = CreateForm.Start_Bid,
                    End_Date = CreateForm.End_Date,
                    UserId= (int)HttpContext.Session.GetInt32("CurUserID")
                };
                 _context.Product.Add(NewProduct);
                _context.SaveChanges();
                
                return RedirectToAction("Dashboard");
            }

            return View("Product",CreateForm);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id){
            if(HttpContext.Session.GetString("CurUserEmail")==null){
               return RedirectToAction("Index","Home");
            }
            Product ToDelete=_context.Product.Where(p=>p.ProductId==id).SingleOrDefault();
            _context.Product.Remove(ToDelete);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("Show/{id}")]
        public IActionResult Show(int id){
            if(HttpContext.Session.GetString("CurUserEmail")==null){
               return RedirectToAction("Index","Home");
            }
            Product ShowProduct=_context.Product.Include(b => b.Bid).ThenInclude(u=>u.User).Where(w=>w.ProductId==id).SingleOrDefault();
            int Belongto= (int)ShowProduct.UserId;
            User ProductUser=_context.User.Where(u=>u.UserId==Belongto).SingleOrDefault();
            ViewBag.ProductUserFirst=ProductUser.First_name;
            ViewBag.ProductUserLast=ProductUser.Last_name;
            return View("Show", ShowProduct);
        }
        [HttpPost]
        [Route("Bidding")]
        public IActionResult Bidding(float Start_Bid, int id){
            if(HttpContext.Session.GetString("CurUserEmail")==null){
               return RedirectToAction("Index","Home");
            }
            Product Retrieve=_context.Product.Where(p=>p.ProductId==id).SingleOrDefault();
            if(Retrieve.Start_Bid>Start_Bid ){
                
                return RedirectToAction("Dashboard");
            }
            Retrieve.Start_Bid=Start_Bid;
            _context.SaveChanges();
            float wallet=(float)HttpContext.Session.GetInt32("Wallet");
            float newwallet=wallet-Start_Bid;
            HttpContext.Session.SetInt32("Wallet",(int)newwallet);
            return RedirectToAction("Dashboard");
        }



    }
}