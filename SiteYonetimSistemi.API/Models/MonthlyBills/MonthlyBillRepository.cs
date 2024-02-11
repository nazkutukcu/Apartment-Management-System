using SiteYonetimSistemi.API.Models.MonthlyDues;

namespace SiteYonetimSistemi.API.Models.BuildingMonthlyBills
{
    public class MonthlyBillRepository(AppDbContext context) : IMonthlyBillRepository
    {
        public List<MonthlyBill> GetAllBills()
        {
            return context.MonthlyBills.ToList();
        }

        public List<MonthlyBill> GetBillsForApartment(int apartmentId)
        {
            return context.MonthlyBills.Where(b => b.ApartmentId == apartmentId).ToList();
        }

        public void AddBill(MonthlyBill bill)
        {
            context.MonthlyBills.Add(bill);
            context.SaveChanges();
        }
        public void Update(MonthlyBill monthlyBill)
        {
            context.Update(monthlyBill);
            context.SaveChanges();
        }
    }
}
