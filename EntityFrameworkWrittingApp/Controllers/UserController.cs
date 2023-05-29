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
    }
}
