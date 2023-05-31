using EntityFrameworkWrittingApp.Data;
using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkWrittingApp.Repository
{
    public class PostAsyncRepository: IPostAsyncRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PostAsyncRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<GetImagePostModel> GetImagesForPost()
        {
            GetImagePostModel postModel = new GetImagePostModel();

            var imagesurls = dbContext.ImageModel.ToList();
            postModel.Imagelist = imagesurls;
            return postModel;
        }

        public  async Task<long> PostPosts(PostModel post)
        {
            dbContext.AddAsync(post);

            var result=await dbContext.SaveChangesAsync();

            return result;
        }
    }
}
