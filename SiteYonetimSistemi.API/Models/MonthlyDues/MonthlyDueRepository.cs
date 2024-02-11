using Microsoft.EntityFrameworkCore;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.MonthlyDues
{
    public class MonthlyDueRepository(AppDbContext context) : IMonthlyDueRepository
    {
        public List<MonthlyDue> GetAllMonthlyDues()
        {
            return context.MonthlyDues.ToList();
        }

        public List<MonthlyDue> GetMonthlyDuesForApartment(int apartmentId)
        {
            return context.MonthlyDues.Where(md => md.ApartmentId == apartmentId).ToList();
        }

        public void AddMonthlyDue(MonthlyDue monthlyDue)
        {
             context.MonthlyDues.Add(monthlyDue);
             context.SaveChanges();
        }

        public void Update(MonthlyDue monthlyDue)
        {
            context.Update(monthlyDue);
            context.SaveChanges();
        }
    }
}
