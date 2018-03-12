using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Voodoo.Validation
{
    public class GreaterThanAttribute : SafeValidationAttribute
    {
        public GreaterThanAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }

        public string OtherProperty { get; set; }

        public override bool RequiresValidationContext => true;

        protected override bool IsValueValid(object value)
        {
            return false;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult($"Cannot find property {OtherProperty} from Greater Than comparison");
            }

            var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (otherPropertyValue == null)
                return null;

            var comparableValue = value as IComparable;
            var comparableOtherValue = otherPropertyValue as IComparable;
            if (comparableValue == null || comparableOtherValue == null)
                return new ValidationResult("'{value}' is not greater than '{otherPropertyValue}'");

            var compareResult = comparableValue.CompareTo((IComparable) otherPropertyValue);
            if (compareResult == 1)
                return null;

            var message = ErrorMessage ?? $"must be greater than {OtherProperty.ToFriendlyString()}";
            return new ValidationResult(message);
        }
    }
}