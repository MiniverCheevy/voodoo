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

        public abstract bool IsValueValid(object value);
    }
}

#endif