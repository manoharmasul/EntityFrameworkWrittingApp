using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IUserAsyncRepository
    {
        Task<long> UserRegistration(UserRegistrationModel user); 
        Task<List<User>> GetAllUsers(); 
        Task<UserRegistrationModel> GetAllUsersForRegistration(); 
        Task<User> UserLogIn(User user); 
    }
}
