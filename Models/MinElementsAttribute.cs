using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Ex05_MVC.Models
{
	public class MinOneElementsAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is ICollection collection && collection.Count >= 1)
			{
				return ValidationResult.Success;
			}

			return new ValidationResult(ErrorMessage);
		}
	}
}
