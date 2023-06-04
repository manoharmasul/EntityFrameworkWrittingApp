using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IUserAsyncRepository
    {
        Task<long> UserRegistration(UserRegistrationModel user); 
        Task<long> UpdateUserProfile(User user); 
        Task<List<User>> GetAllUsers();
        Task<GetUserProfileModel> GetUserProfile(long userId); 
        Task<UserRegistrationModel> GetAllUsersForRegistration(); 
        Task<User> UserLogIn(User user); 
    }
}
