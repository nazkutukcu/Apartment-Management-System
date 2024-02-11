using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.MonthlyDues;

namespace SiteYonetimSistemi.API.Models.Apartments.DTOs
{
    public class BillsAndDuesForMonthDto
    {
        public List<MonthlyDue> PaidDues { get; set; }
        public List<MonthlyDue> UnpaidDues { get; set; }
        public List<MonthlyBill> PaidBills { get; set; }
        public List<MonthlyBill> UnpaidBills { get; set; }
    }
}
