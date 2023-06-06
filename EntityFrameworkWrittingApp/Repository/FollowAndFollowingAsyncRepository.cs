﻿using EntityFrameworkWrittingApp.Data;
using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkWrittingApp.Repository
{
    public class FollowAndFollowingAsyncRepository: IFollowAndFollowingAsyncRepository
    {
        private readonly ApplicationDbContext dbContext;
        public FollowAndFollowingAsyncRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<long> FollowAndUnfollow(FollowersAndFollowingModel followandfollowing)
        {
            var queryfollower=from f in dbContext.FollowerModel 
            where f.FollowedId== followandfollowing.FollowedId
             && f.FollowingId==followandfollowing.FollowingId
             select f;

            var follower = await queryfollower.SingleOrDefaultAsync();

            if(follower==null)
            {
                followandfollowing.IsFollow = true;
                var queryadd = dbContext.AddAsync(followandfollowing);
                var result = dbContext.SaveChanges();
                return result;
            }
            else
            {
                if(follower.IsFollow==true)
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

        public async Task<List<GetUserFollowModel>> GetUserListFollow(long? Id, string? username, string? name)
        {
          
            var query = from u in dbContext.User where u.IsDeleted == false select new GetUserFollowModel 
            {
                UserId=u.Id,
                UserName=u.UserName,
                Name=u.Name,
                UserProfile=u.UserProfile,
            };

            var userlist = await query.ToListAsync();
            var getfollowquery = from f in dbContext.FollowerModel where f.FollowingId == Id select f;
            var followedlist=await getfollowquery.ToListAsync();

            if(followedlist.Count > 0 )
            {
                foreach (var item in userlist)
                {
                    foreach (var f in followedlist)
                    {
                        if(item.UserId==f.FollowedId)
                        {
                            item.IsFollow = f.IsFollow;
                        }

                    }
                }
            }

           
            return userlist;
           
           
        }
    }
}