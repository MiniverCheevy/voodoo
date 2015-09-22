using System.Collections;

#if (!PCL)

namespace Voodoo.Validation
{
    public class CollectionMustHaveAtLeastOneItemAttribute : SafeValidationAttribute
    {
        public CollectionMustHaveAtLeastOneItemAttribute()
        {
            ErrorMessage = "is required";
        }

        public override bool IsValueValid(object value)
        {
            if (value == null)
                return false;

            var collection = value as ICollection;
            if (collection == null)
                return false;

            if (collection.Count == 0)
                return false;
            return true;
        }
    }
}

#endif