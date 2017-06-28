#if(!PCL)
using System;
using System.ComponentModel.DataAnnotations;



namespace Voodoo.Validation
{
    public abstract class SafeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                return IsValueValid(value);
            }
            catch (Exception)
            {
                return false;
            }
        }
       
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            try
            {
                return IsValueValid(value, context);
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
        }

        protected abstract bool IsValueValid(object value);
        protected virtual ValidationResult IsValueValid(object value, ValidationContext context) {
            try
            {
                if (IsValueValid(value))
                    return null;
                else
                    return new ValidationResult("invalid",new string[]{ context.MemberName });
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
            return null;

        }
    }
}

#endif