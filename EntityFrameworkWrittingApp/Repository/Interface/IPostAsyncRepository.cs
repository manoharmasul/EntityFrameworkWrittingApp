using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IPostAsyncRepository
    {
        Task<long> PostPosts(PostModel post);
        Task<long> PostComments(CommentsModel comments);
        Task<long> PostLike(LikeModel likemodel);
        Task<List<GetAllPostsModel>> GetAllPosts();
        Task<List<GetAllPostsModel>> GetAllFollowingPosts(long userid);
        Task<GetImagePostModel> GetImagesForPost();  
    }
}
