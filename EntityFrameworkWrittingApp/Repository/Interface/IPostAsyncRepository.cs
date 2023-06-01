﻿using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IPostAsyncRepository
    {
        Task<long> PostPosts(PostModel post);
        Task<long> PostLike(LikeModel likemodel);
        Task<List<GetAllPostsModel>> GetAllPosts();
        Task<GetImagePostModel> GetImagesForPost();  
    }
}
