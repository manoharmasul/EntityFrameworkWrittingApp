using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace EntityFrameworkWrittingApp.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImagesAsyncRepository imagesAsync;
        public ImagesController(IImagesAsyncRepository imagesAsync)
        {
            this.imagesAsync = imagesAsync;
        }
        public async Task<IActionResult> DisplayImage()
        {
            var image = await imagesAsync.GetImages();
            if (image == null)
            {
                return NotFound();
            }


            return View(image);
  
        }

        public async Task<IActionResult> GetAllBackgroundImages()
        {
            var image = await imagesAsync.GetAllBackgroundImages();
            if (image == null)
            {
                return NotFound();
            }


            return View(image);

        }


        public ActionResult UploadBackgroundImages()
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
                    var image = new ImagesModel
                    {
                        Filename = filename,
                        ImageData = imageData,
                        CreatedBy = usersid,
                    };

                    // Save the image to the database using your preferred database access method
                    // Replace 'SaveImageToDatabase' with the appropriate logic to save the image
                    var result = await imagesAsync.UpLoadBackgroundImages(image);
                }

                return RedirectToAction("GetAllBackgroundImages"); // Redirect to a view or action after successful upload
            }

            return View();
        }



        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var filename = Path.GetFileName(imageFile.FileName);

                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();

                    var image = new ImageModels
                    {
                        Filename = filename,
                        ImageData = imageData
                    };

                    // Save the image to the database using your preferred database access method
                    // Replace 'SaveImageToDatabase' with the appropriate logic to save the image
                    var result = await imagesAsync.UpLoadImages(image);
                }

                return RedirectToAction("DisplayImage"); // Redirect to a view or action after successful upload
            }

            return View();
        }



        // GET: ImagesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ImagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ImagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImagesController/Edit/5
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

        // GET: ImagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ImagesController/Delete/5
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
