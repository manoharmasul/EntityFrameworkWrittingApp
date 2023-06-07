using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IUserAsyncRepository
    {
        Task<long> UserRegistration(UserRegistrationModel user); 
        Task<long> UpdateUserProfile(User user); 
        Task<List<User>> GetAllUsers();
        Task<List<GetCommentsModel>> GetCommentsById(long id);
        Task<GetUserProfileModel> GetUserProfile(long userId); 
        Task<UserRegistrationModel> GetAllUsersForRegistration();

        Task<long> UpdateUserProfile(UserProfileImages userPorfile);

        Task<User> UserLogIn(User user); 
    }
}
