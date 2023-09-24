using System.ComponentModel.DataAnnotations;

namespace API.Models.ValidationAttributes
{
    /// <summary>
    /// Custom validation attribute to ensure that a DateTime value includes a valid time component.
    /// </summary>
    public class ValidDateTimeWithTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTimeValue)
                return dateTimeValue.TimeOfDay != TimeSpan.Zero;

            return false;
        }
    }
}
