namespace SiteYonetimSistemi.API.Models.BuildingMonthlyBills
{
    public interface IMonthlyBillRepository
    {
        List<MonthlyBill> GetAllBills();
        List<MonthlyBill> GetBillsForApartment(int apartmentId);
        void AddBill(MonthlyBill bill);
        void Update(MonthlyBill monthlyBill);
    }
}
