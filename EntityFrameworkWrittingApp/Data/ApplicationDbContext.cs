using EntityFrameworkWrittingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EntityFrameworkWrittingApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<ImageModel> ImageModel { get; set; }
        public DbSet<PostModel> PostModels { get; set; }
        public DbSet<LikeModel> LikeModel { get; set; }
        public DbSet<CommentsModel> CommentsModel { get; set; }
    }
}
