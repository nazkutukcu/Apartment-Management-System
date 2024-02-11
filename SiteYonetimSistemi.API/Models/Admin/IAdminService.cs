using SiteYonetimSistemi.API.Models.MonthlyBills.DTOs;
using SiteYonetimSistemi.API.Models.MonthlyDues.DTOs;

namespace SiteYonetimSistemi.API.Models.Admin
{
    public interface IAdminService
    {
        ResponseDto<int> AssignMonthlyDuesToApartment(MontlyDueAddRequestDto request);
        ResponseDto<string> AssignMonthlyDuesToAll(MontlyDuesToAllAddRequestDto request);
        ResponseDto<string> AddMonthlyBillToAllApartments(MonthlyBillToAllAddRequestDto request);
        ResponseDto<decimal> GetMonthlyDebtForApartment(int apartmentId, int month);
        ResponseDto<decimal> GetYearlyDebtForApartment(int apartmentId);
    }
}
