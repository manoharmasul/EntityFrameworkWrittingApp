using EntityFrameworkWrittingApp.Data;
using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.CodeAnalysis;
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
            try
            {
                var query = from u in dbContext.User
                            join p in dbContext.PostModels on u.Id equals p.CreatedBy
                            join i in dbContext.ImageModel on p.ImagesId equals i.Id
                            where u.IsDeleted == false


                            select new GetAllPostsModel
                            {
                                UserId = u.Id,
                                UserName = u.UserName,
                                PostId = p.Id,
                                PostContaint = p.PostContaint,
                                ImageUrl = i.ImageUrl
                            };
                getlist = await query.ToListAsync();
                foreach (var post in getlist)
                {
                    var liquery = from l in dbContext.LikeModel where l.IsDeleted == false && l.PostId == post.PostId select l;

                    var listre = await liquery.ToListAsync();
                    post.likemodel = listre;

                    var commentsnewquery = from u in dbContext.User
                                           join c in dbContext.CommentsModel on u.Id equals c.UserId                                         
                                           select new GetCommentsModel
                                           {
                                               Id = c.Id,
                                               PostId = c.PostId,
                                               UserId = c.UserId,
                                               UserName = u.UserName,
                                               Comments = c.Comments,
                                               UserProfile=u.UserProfile
                                           };



                    var commentlist = await commentsnewquery.ToListAsync();
                    post.commentsmodel = commentlist;

                }
                return getlist;

            }
            catch (Exception ex)
            {
                return getlist;
            }

            
        }

        public async Task<GetImagePostModel> GetImagesForPost()
        {
            GetImagePostModel postModel = new GetImagePostModel();

            var imagesurls = dbContext.ImageModel.ToList();
            postModel.Imagelist = imagesurls;
            return postModel;
        }

        public async Task<long> PostComments(CommentsModel comments)
        {
            try
            {
                var query = dbContext.AddAsync(comments);
                var result = await dbContext.SaveChangesAsync();
                return result;
            }
            catch(Exception ex) 
            {
                return 1;
            }
            
        }

        public async Task<long> PostLike(LikeModel likemodel)
        {

            var result = 0;
            var checklike = from l in dbContext.LikeModel
            where l.UserId == likemodel.UserId && l.IsDeleted == false && l.PostId == likemodel.PostId select l;

            var likess = await checklike.SingleOrDefaultAsync();
            if (likess == null)
            {
                likemodel.LikeOrDislike = true;
                dbContext.AddAsync(likemodel);

                result = await dbContext.SaveChangesAsync();
            }
            else if(likess.LikeOrDislike==true)
            {


                likess.LikeOrDislike = false;

                result=dbContext.SaveChanges();

            }
            else
            {
                likess.LikeOrDislike = true;
                result = dbContext.SaveChanges();
            }
            return result;
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
