using LoginRegister.Models;
using System.Threading.Tasks;

namespace LoginRegister.Services
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ValidateUserAsync(string email, string password);
    }
}
