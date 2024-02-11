using SiteYonetimSistemi.API.Models.Apartments.DTOs;
using SiteYonetimSistemi.API.Models.BuildingMonthlyBills;
using SiteYonetimSistemi.API.Models.MonthlyDues;

namespace SiteYonetimSistemi.API.Models.Apartments
{
    public interface IApartmentService
    {
        ResponseDto<int> AddApartment(ApartmentSaveDtoRequest request);
        ResponseDto<List<ApartmentDto>> GetAll();
        ResponseDto<UnpaidBillsAndDuesForMonthDto> GetUnpaidBillsAndDuesForMonth(int apartmentId, int month);
        ResponseDto<BillsAndDuesForMonthDto> GetAllBillsAndDuesForMonth(int apartmentId, int month);
        ResponseDto<AllApartmentsUnpaidBillsAndDuesDto> GetAllApartmentsUnpaidBillsAndDuesForMonth(int month);
    }
}
