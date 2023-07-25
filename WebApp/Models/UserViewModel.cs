using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatorio.")]
        [CustomDate(10, 120, ErrorMessage = "La fecha de nacimiento no es válida.")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        [RegularExpression("^[FMO]$", ErrorMessage = "El género no es valido.")]
        public char Gender { get; set; }
    }

    public class CustomDateAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;

        public CustomDateAttribute(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? dateValue = value as DateTime?;

            // Get current date
            DateTime currentDate = DateTime.Now;

            // Validate that the date of birth is not future.
            if (dateValue > currentDate)
            {
                return new ValidationResult("La fecha de nacimiento no puede ser una fecha futura.");
            }

            // Calculate age
            int age = currentDate.Year - dateValue.Value.Year;

            if (age < _minAge || age > _maxAge)
            {
                return new ValidationResult($"La edad debe estar entre {_minAge} y {_maxAge} años.");
            }

            return ValidationResult.Success;
        }
    }
}
