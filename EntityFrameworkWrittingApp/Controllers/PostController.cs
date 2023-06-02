using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkWrittingApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostAsyncRepository postAsync;
        public PostController(IPostAsyncRepository postAsync)
        {
            this.postAsync = postAsync;
        }

            // GET: PostController
        public async Task<ActionResult> Index()
        {
           
            try
            {
                try
                {
                    var uid = HttpContext.Session.GetString("userId");
                    ViewBag.User = Int32.Parse(uid);
                    var result = await postAsync.GetAllPosts();
                    return View(result);
                }
                catch(Exception ex) 
                { 
                
                }
               
                return View();
            }
            catch(Exception ex)
            {
               return View();
            }
           
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        public async Task<ActionResult> PostPosts()
        {
            try {

                var uid = HttpContext.Session.GetString("userId");
                ViewBag.User = Int32.Parse(uid);              
                var result = await postAsync.GetImagesForPost();
                return View(result);
            }
            catch(Exception ex)
            {
                return View();
            }
           
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PostPosts(PostModel post)
        {
            try
            {
               var uid= HttpContext.Session.GetString("userId");
                post.CreatedBy =Int32.Parse(uid);
               var result=await postAsync.PostPosts(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
      

     
        public async Task<ActionResult> LikePost(long id)
        {
            try
            {
                LikeModel likes = new LikeModel();
                var uid = HttpContext.Session.GetString("userId");
                likes.CreatedBy = Int32.Parse(uid);
                likes.PostId = id;
                likes.UserId = likes.CreatedBy;
                likes.CreatedDate = DateTime.Now;
                var result = await postAsync.PostLike(likes);
                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> PostComments(string Comments,long PostId)
        {
            try
            {
                CommentsModel comment = new CommentsModel();
                var uid = HttpContext.Session.GetString("userId");
                comment.CreatedBy = Int32.Parse(uid);
                comment.PostId = PostId;
                comment.UserId = comment.CreatedBy;
                comment.UserId = comment.CreatedBy;
                comment.Comments = Comments;
                var result = await postAsync.PostComments(comment);
                return View();
            }
            catch
            {
                return View();
            }
        }


        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
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

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
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
