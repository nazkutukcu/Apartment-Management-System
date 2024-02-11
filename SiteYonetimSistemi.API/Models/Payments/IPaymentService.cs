using SiteYonetimSistemi.API.Models.Apartments.DTOs;
using SiteYonetimSistemi.API.Models.Payments.DTOs;

namespace SiteYonetimSistemi.API.Models.Payments
{
    public interface IPaymentService
    {
        ResponseDto<int> MakePaymentForMonthlyDue(DuePaymentMakeDtoRequest request);
        ResponseDto<int> MakePaymentForMonthlyBill(BillPaymentMakeDtoRequest request);
        ResponseDto<List<PaymentDto>> GetPaymentsForApartment(int apartmentId);
        ResponseDto<List<ApartmentDto>> GetApartmentsWithRegularPayments();

    }
}
