namespace SiteYonetimSistemi.API.Models.Apartments.DTOs
{
    public class AllApartmentsUnpaidBillsAndDuesDto
    {
        public List<UnpaidBillsAndDuesForMonthDto> Apartments { get; set; }

        public AllApartmentsUnpaidBillsAndDuesDto()
        {
            Apartments = new List<UnpaidBillsAndDuesForMonthDto>();
        }
    }
}
