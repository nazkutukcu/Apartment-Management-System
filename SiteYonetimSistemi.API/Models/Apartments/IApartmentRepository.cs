using SiteYonetimSistemi.API.Models.MonthlyDues;

namespace SiteYonetimSistemi.API.Models.Apartments
{
    public interface IApartmentRepository
    {
        Apartment Add(Apartment apartment);
        List<Apartment> GetAll();
        void Update(Apartment apartment);
        Apartment GetById(int id);
        Apartment GetApartmentById(int id);
    }
}
