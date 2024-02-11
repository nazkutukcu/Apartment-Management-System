using SiteYonetimSistemi.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteYonetimSistemi.API.Models.Apartments.DTOs
{
    public class ApartmentSaveDtoRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Apartman numarası pozitif bir tamsayı olmalıdır.")]
        public int ApartmentNo { get; set; }  

        [EnumValue(typeof(Block), ErrorMessage = "Geçerli bir blok değeri giriniz.")] 
        public Block Block { get; set; }

        [EnumValue(typeof(ApartmentType), ErrorMessage = "Geçerli bir daire tipi değeri giriniz.")] 
        public ApartmentType Type { get; set; }

        public int Floor { get; set; } 

        [EnumValue(typeof(ResidenceStatus), ErrorMessage = "Geçerli bir konut durumu değeri giriniz.")] 
        public ResidenceStatus ResidenceStatus { get; set; }

    }
}
