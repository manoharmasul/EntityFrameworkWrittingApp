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
