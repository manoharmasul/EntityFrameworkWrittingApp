using EntityFrameworkWrittingApp.Models;

namespace EntityFrameworkWrittingApp.Repository.Interface
{
    public interface IUserAsyncRepository
    {
        Task<long> UserRegistration(User user); 
        Task<List<User>> GetAllUsers(); 
    }
}
