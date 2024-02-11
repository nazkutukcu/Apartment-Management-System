using Microsoft.EntityFrameworkCore;

namespace SiteYonetimSistemi.API.Models.Users
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public User Add(User user)
        {     
            _context.Users.Add(user);
            _context.SaveChanges(); 
            return user;
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.Find(id);           
            _context.Remove(user);        
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
