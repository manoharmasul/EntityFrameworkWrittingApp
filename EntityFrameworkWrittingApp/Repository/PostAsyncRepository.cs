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

        public async Task<List<PostModel>> GetAllPosts()
        {
           var posts=await dbContext.PostModels.ToListAsync();
            return posts;
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
            long result = 0;
            dbContext.AddAsync(post);

            try
            {
                 result = await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
           

            return result;
        }
    }
}
