namespace SiteYonetimSistemi.API.Models.Users
{
    public interface IUserRepository
    {
        User Add(User user);
        void Delete(Guid id);
        List<User> GetAll();
        User GetById(Guid id);
        void Update(User user);
        Task<User> GetByIdAsync(Guid id);
    }
}
