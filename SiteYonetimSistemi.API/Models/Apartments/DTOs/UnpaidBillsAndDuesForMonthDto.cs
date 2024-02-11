using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.MonthlyDues;

namespace SiteYonetimSistemi.API.Models.Apartments.DTOs
{
    public class UnpaidBillsAndDuesForMonthDto
    {
        public List<MonthlyBill> UnpaidBills { get; set; }
        public List<MonthlyDue> UnpaidDues { get; set; }
    }
}
