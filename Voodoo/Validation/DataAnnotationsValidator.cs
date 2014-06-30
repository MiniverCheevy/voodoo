using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;

namespace Voodoo.Validation
{
    public class DataAnnotationsValidator
    {
        public DataAnnotationsValidator(object @object)
        {
            ICollection<ValidationResult> validation;
            IsValid = tryValidate(@object, out validation);
            ValidationResults = validation.ToArray().ToList();
        }

        public bool IsValid { get; set; }
        public List<ValidationResult> ValidationResults { get; set; }

        public List<KeyValuePair> ValidationResultsAsNameValuePair
        {
            get
            {
                var result = new List<KeyValuePair>();
                if (ValidationResults != null)
                {
                    var items =
                        ValidationResults.Select(
                            c => new KeyValuePair {Key = c.MemberNames.FirstOrDefault(), Value = c.ErrorMessage});
                    result.AddRange(items);
                }
                return result;
            }
        }

        private bool tryValidate(object @object, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(@object, null, null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(@object, context, results, true);
        }
    }
}