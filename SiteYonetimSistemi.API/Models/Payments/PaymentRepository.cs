using Microsoft.EntityFrameworkCore;
using SiteYonetimSistemi.API.Models.Users;

namespace SiteYonetimSistemi.API.Models.Payments
{
    public class PaymentRepository(AppDbContext context) : IPaymentRepository
    {
        public Payment Add(Payment payment)
        {
            context.Payments.Add(payment);
            context.SaveChanges();
            return payment;
        }

        public List<Payment> GetPaymentsByApartmentId(int apartmentId)
        {
            return context.Payments
                           .Where(p => p.ApartmentId == apartmentId)
                           .ToList();
        }


        public List<Payment> GetAll()
        {
            return context.Payments.ToList(); 
        }

    }
}
