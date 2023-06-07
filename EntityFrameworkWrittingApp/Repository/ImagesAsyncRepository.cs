using EntityFrameworkWrittingApp.Data;
using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkWrittingApp.Repository
{
    public class ImagesAsyncRepository:IImagesAsyncRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public ImagesAsyncRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<List<ImagesModel>> GetAllBackgroundImages()
        {
            var backgroundimages = await applicationDbContext.ImagesModelss.ToListAsync();
            return backgroundimages;    

        }

        public async Task<List<ImageModels>> GetImages()
        {
            var imges = await applicationDbContext.ImageModels.ToListAsync();
            return imges;
        }

        public async Task<long> UpLoadBackgroundImages(ImagesModel imgs)
        {
            imgs.CreatedDate= DateTime.Now;
            var query = applicationDbContext.AddAsync(imgs);

            long result = await applicationDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<long> UpLoadImages(ImageModels imgs)
        {
            var query = applicationDbContext.AddAsync(imgs);

            long result = await applicationDbContext.SaveChangesAsync();

            return result;
        }
    }
}
