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
                return new ValidationResult(ex.Message, new string[] { context.MemberName});
            }
        }

        protected abstract bool IsValueValid(object value);
        protected virtual ValidationResult IsValueValid(object value, ValidationContext context) {
            try
            {
                var message = base.ErrorMessage ?? "invalid";

                if (IsValueValid(value))
                    return null;
                else
                    return new ValidationResult(message, new string[]{ context.MemberName });
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message, new string[] { context.MemberName });
            }
            return null;

        }
    }
}

#endif