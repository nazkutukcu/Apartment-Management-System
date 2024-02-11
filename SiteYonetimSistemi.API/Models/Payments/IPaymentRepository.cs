namespace SiteYonetimSistemi.API.Models.Payments
{
    public interface IPaymentRepository
    {
        Payment Add(Payment payment);
        List<Payment> GetPaymentsByApartmentId(int apartmentId);
        List<Payment> GetAll();
    }
}
