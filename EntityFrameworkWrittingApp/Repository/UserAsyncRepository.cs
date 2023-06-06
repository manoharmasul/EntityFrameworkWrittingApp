using EntityFrameworkWrittingApp.Data;
using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EntityFrameworkWrittingApp.Repository
{
    public class UserAsyncRepository:IUserAsyncRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserAsyncRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var result =  dbContext.User.ToList();
            return result;
        }
        public async Task<UserRegistrationModel> GetAllUsersForRegistration()
        {
            UserRegistrationModel ur = new UserRegistrationModel();
            var userli = await dbContext.User.ToListAsync();
            ur.userlists = userli;
            return ur;
        }

        public async Task<List<GetCommentsModel>> GetCommentsById(long id)
        {
           
            var joinquery = from u in dbContext.User
                            join c in dbContext.CommentsModel on u.Id equals c.UserId
                            where c.PostId == id
                            select new GetCommentsModel
                            {
                                Id = c.Id,
                                UserId = u.Id,
                                PostId = c.PostId,
                                UserName = u.UserName,
                                Comments = c.Comments,
                                UserProfile = u.UserProfile
                            };
            var result=await joinquery.ToListAsync();
            return result;
            
        }

        public async Task<GetUserProfileModel> GetUserProfile(long userId)
        {
            List<GetAllPostsModel> getlist = new List<GetAllPostsModel>();
            GetUserProfileModel userprofile =new GetUserProfileModel();
            try
            {
                var Userobj = await dbContext.User.FindAsync(userId);
                userprofile.UserId = Userobj.Id;
                userprofile.UserName = Userobj.UserName;
                userprofile.Bio = Userobj.Bio;
                userprofile.Links = Userobj.Links;
                userprofile.UserProfile = Userobj.UserProfile;
                var query = from u in dbContext.User
                            join p in dbContext.PostModels on u.Id equals p.CreatedBy
                            join i in dbContext.ImageModel on p.ImagesId equals i.Id
                            where u.IsDeleted == false && u.Id== userId 


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
                                               UserProfile = u.UserProfile
                                           };



                    var commentlist = await commentsnewquery.ToListAsync();
                    post.commentsmodel = commentlist;

                }

                var followerCountQuery = from f in dbContext.FollowerModel where f.FollowedId == userId && f.IsFollow == true select f;
                var  followerresult=await followerCountQuery.ToListAsync();
                var followerCount= followerresult.Count();

                

                var followingCountQuery =from f in dbContext.FollowerModel where f.FollowingId==userId  select f;    
                var followingresult=await followingCountQuery.ToListAsync();

                var followingCount= followingresult.Count();

                userprofile.NoOfFollower = followerCount;
                userprofile.NoOfFollowing = followingCount;
                userprofile.NoOfPosts= getlist.Count();


                userprofile.GetAllPosts = getlist;
                return userprofile;

            }
            catch (Exception ex)
            {
                return userprofile;
            }
        }

        public async Task<long> UpdateUserProfile(User user)
        {
            var getuser = await dbContext.User.FindAsync(user.Id);
            getuser.ModifiedBy = user.Id;
            getuser.ModifiedDate = DateTime.Now;
            getuser.UserName = user.UserName;
            getuser.Bio=user.Bio;
            getuser.Links= user.Links;


            var updatequery = dbContext.Update(getuser);
            var result = await dbContext.SaveChangesAsync();
            return result;

        }

        public async Task<User> UserLogIn(User user)
        {
            var userlong = from s in dbContext.User

                             where s.UserName == user.UserName && s.Password == user.Password   
                       select s;

            var userlognin = await userlong.SingleOrDefaultAsync();
            return userlognin;
          

        }

        public async Task<long> UserRegistration(UserRegistrationModel user)
        {
            User us = new User();
            us.UserName = user.UserName;
            us.EmailId = user.EmailId;
            us.MobileNo = user.MobileNo;
            us.CreatedDate = DateTime.Now;
            us.IsDeleted = false;
            us.Password = user.Password;
            us.RoleId = 1;

            var query = dbContext.AddAsync(us);
            var result = dbContext.SaveChanges();
            var id = us.Id;
            var getUser = await dbContext.User.FindAsync(id);
            getUser.CreatedBy = (long)id;
            var upquery = dbContext.User.Update(getUser);
            var upresul = dbContext.SaveChanges();
            return upresul;

      
        }
    }
}
