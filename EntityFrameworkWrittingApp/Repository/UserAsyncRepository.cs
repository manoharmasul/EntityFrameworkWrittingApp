using EntityFrameworkWrittingApp.Data;
using EntityFrameworkWrittingApp.Models;
using EntityFrameworkWrittingApp.Repository.Interface;

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

        public async Task<long> UserRegistration(User user)
        {
            var query = dbContext.AddAsync(user);
            var result = dbContext.SaveChanges();
            return result;
        }
    }
}
