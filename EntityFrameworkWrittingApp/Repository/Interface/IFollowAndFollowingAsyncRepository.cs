using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IFollowAndFollowingAsyncRepository
    {
        Task<long> FollowAndUnfollow(FollowersAndFollowingModel followandfollowing);
        Task<List<GetUserFollowModel>> GetUserListFollow(long? Id, string? username, string? name);
        Task<List<GetUserFollowModel>> GetFollowerAndFollowingList(long? Id, long flagid);
    }
}
