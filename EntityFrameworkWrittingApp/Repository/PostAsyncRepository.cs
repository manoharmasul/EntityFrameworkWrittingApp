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

        public async Task<List<GetAllPostsModel>> GetAllPosts()
        
        {
            List<GetAllPostsModel> getlist=new List<GetAllPostsModel>();
            /* from o in _myContext.Order
             join ur in _myContext.tblUser on o.CustomerId equals ur.Id
             where o.CustomerId == custId && (o.OrderDate.Date == date)
             orderby o.Id descending

             select new GetOrdersJoin
             {
                 Id = o.Id,
                 UserName = ur.UserName,
                 MobileNo = ur.MobileNo,
                 BillingAddress = o.BillingAddress,

                 ShippingAddress = o.ShippingAddress,
                 OrderStatus = o.OrderStatus,
                 OrderDate = o.OrderDate,

                 TotalAmmount = o.TotalAmmount

             };
            */
            //CreateBy as UserId,UserName,Id as PostId,PostContaint,ImageUser
            var query = from u in dbContext.User
                       join p in dbContext.PostModels on u.Id equals p.CreatedBy
                       join i in dbContext.ImageModel on p.ImagesId equals i.Id
                       where u.IsDeleted ==false
                     

                       select new GetAllPostsModel
                       {
                          UserId=u.Id,
                          UserName=u.UserName,
                          PostId=p.Id,
                          PostContaint=p.PostContaint,
                           ImageUrl = i.ImageUrl
                       };
             getlist = await query.ToListAsync();
            return getlist;
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
            post.IsDeleted = false;
            post.CreatedDate = DateTime.Now;

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
