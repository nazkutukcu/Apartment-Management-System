using System.ComponentModel.DataAnnotations;

namespace SiteYonetimSistemi.API.Models
{
    public class EnumValueAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumValueAttribute(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type must be an enum.");
            }

            _enumType = enumType;
        }

        public override bool IsValid(object value)
        {
            return Enum.IsDefined(_enumType, value);
        }
    }
}
