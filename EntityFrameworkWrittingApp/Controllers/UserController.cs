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
        public ActionResult UserRegistration()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserRegistration(User user)
        {
            try
            {
                var result=await userAsync.UserRegistration(user);
                return RedirectToAction(nameof(Index));
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
        [HttpPost]
        public async Task<ActionResult> Login(UserLogInModel userloginmodel)
        {
            var user = await userasynrepo.UserLogIn(userloginmodel);
            if (user != null)
            {
                if (user.Password == userloginmodel.Password)
                {
                    HttpContext.Session.SetString("userName", user.UserName);
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("userRole", user.Role);
                    HttpContext.Session.SetString("roleId", user.RoleId.ToString());
                    ViewBag.user = user.UserName;

                    if (user.Role == "Admin")
                    {

                        at.EmpId = user.Id;
                        attendanceRepo.CheckInOut(at);

                        return RedirectToAction("OrderContSales", "Product");
                    }
                    else
                    {
                        at.EmpId = user.Id;
                        attendanceRepo.CheckInOut(at);

                        return RedirectToAction("Index", "Product");
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
            at.EmpId = Int32.Parse(uId);
            attendanceRepo.CheckInOut(at);
            HttpContext.Session.Clear();

            return RedirectToAction("LogIn");
        }

    }
}
