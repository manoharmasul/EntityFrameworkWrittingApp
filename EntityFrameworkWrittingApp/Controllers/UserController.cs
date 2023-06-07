using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace EntityFrameworkWrittingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAsyncRepository userAsync;
        private readonly IPostAsyncRepository postAsync;
        public UserController(IUserAsyncRepository userAsync,IPostAsyncRepository postAsync)
        {
            this.userAsync = userAsync; 
            this.postAsync = postAsync; 
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
        public async Task<ActionResult> GetCommentsById(long id,string comment)
        {
            try
            {
                CommentsModel comments = new CommentsModel();
                var uid = HttpContext.Session.GetString("userId");
                comments.CreatedBy = Int32.Parse(uid);
                comments.PostId = id;
                comments.Comments = comment;
                comments.UserId = comments.CreatedBy;
                comments.UserId = comments.CreatedBy;
               
                var result11 = await postAsync.PostComments(comments);



                

                var result = await userAsync.GetCommentsById(id);

                return Json(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }
        public async Task<ActionResult> GetUserProfile()
        {
            try
            {
                var uid = HttpContext.Session.GetString("userId");
                var userid = Int32.Parse(uid);
                ViewBag.User = userid;
                var result = await userAsync.GetUserProfile(userid);

                return View(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }
        public async Task<ActionResult> UpdateUserProfile(User user)
        {
            try
            {
                var uid = HttpContext.Session.GetString("userId");
                var userid = Int32.Parse(uid);
              user.Id = userid;             
                var result = await userAsync.UpdateUserProfile(user);

                return RedirectToAction(nameof(GetUserProfile));
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadBackgroundImages(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var filename = Path.GetFileName(imageFile.FileName);

                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();
                    var uid = HttpContext.Session.GetString("userId");
                    var usersid = Int32.Parse(uid);
                    var image = new UserProfileImages
                    {
                        Filename = filename,
                        ImageData = imageData,
                        CreatedBy = usersid,
                        UserId= usersid,
                    };

                    // Save the image to the database using your preferred database access method
                    // Replace 'SaveImageToDatabase' with the appropriate logic to save the image
                    var result = await userAsync.UpdateUserProfile(image);
                }

                return RedirectToAction("GetUserProfile"); // Redirect to a view or action after successful upload
            }

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
