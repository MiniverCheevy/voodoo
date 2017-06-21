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

        protected override bool IsValueValid(object value)
        {
            var collection = value as ICollection;
            if (collection == null)
                return false;

            return collection.Count != 0;
        }
    }
}

#endif