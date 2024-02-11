namespace SiteYonetimSistemi.API.Models.MonthlyDues
{
    public interface IMonthlyDueRepository
    {
        List<MonthlyDue> GetAllMonthlyDues();
        List<MonthlyDue> GetMonthlyDuesForApartment(int apartmentId);
        void AddMonthlyDue(MonthlyDue monthlyDue);
        void Update(MonthlyDue monthlyDue);
    }
}
