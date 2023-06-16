using EntityFrameworkWrittingApp.Data;
using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EntityFrameworkWrittingApp.Repository
{
    public class FollowAndFollowingAsyncRepository : IFollowAndFollowingAsyncRepository
    {
        private readonly ApplicationDbContext dbContext;
        public FollowAndFollowingAsyncRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<long> FollowAndUnfollow(FollowersAndFollowingModel followandfollowing)
        {
            var queryfollower = from f in dbContext.FollowerModel
                                where f.FollowedId == followandfollowing.FollowedId
                                 && f.FollowingId == followandfollowing.FollowingId
                                select f;

            var follower = await queryfollower.SingleOrDefaultAsync();

            if (follower == null)
            {
                followandfollowing.IsFollow = true;
                var queryadd = dbContext.AddAsync(followandfollowing);
                var result = dbContext.SaveChanges();
                return result;
            }
            else
            {
                if (follower.IsFollow == true)
                {
                    follower.IsFollow = false;
                    follower.ModifiedBy = followandfollowing.CreatedBy;
                    follower.ModifiedDate = DateTime.Now;
                    var queryupdate = dbContext.Update(follower);
                    var result = dbContext.SaveChanges();
                    return result;
                }
                else
                {
                    follower.IsFollow = true;
                    follower.ModifiedBy = followandfollowing.CreatedBy;
                    follower.ModifiedDate = DateTime.Now;
                    var queryupdate = dbContext.Update(follower);
                    var result = dbContext.SaveChanges();
                    return result;
                }


            }


        }


        public async Task<List<GetUserFollowModel>> GetFollowerAndFollowingList(long? Id, long flagid)
        {

            if (flagid == 1)
            {

                var query = from u in dbContext.User
                            join f in dbContext.FollowerModel on u.Id equals f.FollowingId
                            where u.IsDeleted == false && f.FollowedId == Id && f.IsFollow==true
                            select new GetUserFollowModel
                            {
                                UserId = u.Id,
                                UserName = u.UserName,
                                Name = u.Name,
                                UserProfile = u.UserProfile,
                                IsFollow = f.IsFollow
                            };



                var userlist = await query.ToListAsync();

                foreach (var item in userlist)
                {
                    var prop = from p in dbContext.UserProfileImages where p.UserId == item.UserId select p;
                    var profile = await prop.SingleOrDefaultAsync();

                    if (profile != null)
                    {
                        item.ImageData = profile.ImageData;
                    }
                }


                return userlist;
            }
            else
            {
                var query = from u in dbContext.User
                            join f in dbContext.FollowerModel on u.Id equals f.FollowedId
                            where u.IsDeleted == false && f.FollowingId == Id && f.IsFollow==true
                            select new GetUserFollowModel
                            {
                                UserId = u.Id,
                                UserName = u.UserName,
                                Name = u.Name,
                                UserProfile = u.UserProfile,
                                IsFollow = f.IsFollow
                            };



                var userlist = await query.ToListAsync();



                foreach (var item in userlist)
                {
                    var prop = from p in dbContext.UserProfileImages where p.UserId == item.UserId select p;
                    var profile = await prop.SingleOrDefaultAsync();

                    if (profile != null)
                    {
                        item.ImageData = profile.ImageData;
                    }
                }
                return userlist;
            }
        }

        public async Task<List<GetUserFollowModel>> GetUserListFollow(long? Id, string? username, string? name)
        {
            /* List<long> ids = new List<long>();

             var queryIds = from u in dbContext.FollowerModel

                            where u.IsDeleted == false && u.FollowingId == Id

                            && u.IsFollow == true

                            select new UserModelIds
                            {
                                UserId = u.FollowedId,
                            };

             long[] id = await queryIds.Select(x => x.UserId).ToArrayAsync();

             var query = from u in dbContext.User
                         where u.IsDeleted == false && u.Id != Id && !id.Contains(u.Id)
                         select new GetUserFollowModel
                         {
                             UserId = u.Id,
                             UserName = u.UserName,
                             Name = u.Name,
                             UserProfile = u.UserProfile
                         };
             */
            var query = from u in dbContext.User
                        where u.IsDeleted == false && u.Id != Id 
                        select new GetUserFollowModel
                        {
                            UserId = u.Id,
                            UserName = u.UserName,
                            Name = u.Name,
                            UserProfile = u.UserProfile
                        };


            var userlist = await query.ToListAsync();
            var getfollowquery = from f in dbContext.FollowerModel where f.FollowingId == Id select f;
            var followedlist = await getfollowquery.ToListAsync();

            if (followedlist.Count > 0)
            {
                foreach (var item in userlist)
                {
                    foreach (var f in followedlist)
                    {
                        if (item.UserId == f.FollowedId)
                        {
                            item.IsFollow = f.IsFollow;
                        }

                    }
                }
            }
            foreach (var item in userlist)
            {
                var prop = from p in dbContext.UserProfileImages where p.UserId == item.UserId select p;
                var profile = await prop.SingleOrDefaultAsync();

                if (profile != null)
                {
                    item.ImageData = profile.ImageData;
                }
            }


            return userlist;


        }
    }
}
