using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkWrittingApp.Controllers
{
    public class FollowUnfollowController : Controller
    {
        private readonly IFollowAndFollowingAsyncRepository followAsync;
        public FollowUnfollowController(IFollowAndFollowingAsyncRepository followAsync)
        {
            this.followAsync = followAsync; 
        }
        // GET: FollowUnfollowController
        public async Task<ActionResult> GetUserListFollow(long? userid,string? username,string? name)
        {           
            try
            {
                var uid = HttpContext.Session.GetString("userId");
                var usersid = Int32.Parse(uid);
                var userlist = await followAsync.GetUserListFollow(usersid, username, name);
                return View(userlist);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");

            }

        }

        // GET: FollowUnfollowController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

   
        public async Task<ActionResult> FollowAndUnfollow(long id)
        {
            try
            {
                var uid = HttpContext.Session.GetString("userId");
                var userid = Int32.Parse(uid);
                FollowersAndFollowingModel followed = new FollowersAndFollowingModel();
                followed.FollowedId = id;
                followed.FollowingId= userid;
                followed.CreatedBy = userid;
                followed.CreatedDate = DateTime.Now;

                var result=await followAsync.FollowAndUnfollow(followed);   

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FollowUnfollowController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FollowUnfollowController/Edit/5
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

        // GET: FollowUnfollowController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FollowUnfollowController/Delete/5
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
