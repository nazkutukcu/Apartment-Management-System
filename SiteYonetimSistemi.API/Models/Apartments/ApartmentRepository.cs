using Microsoft.EntityFrameworkCore;
using SiteYonetimSistemi.API.Models.MonthlyDues;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.Apartments
{
    public class ApartmentRepository(AppDbContext context) : IApartmentRepository
    {
        public Apartment Add(Apartment apartment)
        {
            context.Apartments.Add(apartment);
            context.SaveChanges();   
            return apartment;
        }
        public List<Apartment> GetAll()
        {
            return context.Apartments
                   .Include(a => a.MonthlyDues) 
                   .Include(a => a.MonthlyBills) 
                   .ToList();
        }
        public void Update(Apartment apartment)
        {
            context.Update(apartment);
            context.SaveChanges();
        }
        public Apartment GetById(int id)
        {
            return context.Apartments
                  .Include(a => a.MonthlyDues)
                  .Include(a => a.MonthlyBills)
                  .FirstOrDefault(a => a.Id == id);
        }

        public Apartment GetApartmentById(int id)
        {
            return context.Apartments.Find(id);
        }
    }
}
