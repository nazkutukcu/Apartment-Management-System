using SiteYonetimSistemi.API.Models.Apartments;

namespace SiteYonetimSistemi.API.Models.MonthlyDues
{
    public class MonthlyDue
    {
        public int Id { get; set; }
        public int Month { get; set; } 
        public decimal Amount { get; set; }      
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }

        //One-to-many relationship with an apartment
        public int ApartmentId { get; set; }
        public Apartment? Apartment { get; set; } 
    }
}
