using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkWrittingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAsyncRepository userAsync;
        public UserController(IUserAsyncRepository userAsync)
        {
            this.userAsync = userAsync; 
        }
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            try {
                var result = await userAsync.GetAllUsers();

                return View(result);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
            
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public async Task<ActionResult> UserRegistration()
        {
            var user = await userAsync.GetAllUsersForRegistration();
            return View(user);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRegistration(UserRegistrationModel user)
        {
            try
            {
                var result=await userAsync.UserRegistration(user);
                return RedirectToAction(nameof(LogIn));
            }
            catch(Exception ex) 
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(User user)
        {
            var loguser = await userAsync.UserLogIn(user);
            if (user != null)
            {
                if (user.Password == loguser.Password)
                {
         

                    HttpContext.Session.SetString("userName", loguser.UserName);
                    HttpContext.Session.SetString("userId", loguser.Id.ToString()); 
                    HttpContext.Session.SetString("roleId", loguser.RoleId.ToString());
                    ViewBag.user = loguser.UserName;

                    var rid = HttpContext.Session.GetString("userId");
                    var rd = Int32.Parse(rid);

                    long Role = rd;
                    if (loguser.RoleId ==1)
                    {
                        return RedirectToAction("Index", "Post");
                    }
                  
                }
                else
                    ViewBag.num = 1;
                return View();
            }
            else
            {
                ViewBag.num = 1;
                return View();
            }
        }

        public IActionResult Logout()
        {
            var uId = HttpContext.Session.GetString("userId");        
            HttpContext.Session.Clear();

            return RedirectToAction("LogIn");
        }

    }
}
