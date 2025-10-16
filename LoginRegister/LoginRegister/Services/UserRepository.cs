using LoginRegister.Data;
using LoginRegister.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LoginRegister.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Password == password);
        }
    }
}
