using System;
using Voodoo.Messages;
using Voodoo.Validation.Infrastructure;
using Voodoo.Infrastructure;

namespace Voodoo
{
    public static class ValidationExtensions
    {
        public static void ThrowIfNull(this object @object, string message)
        {
            if (@object == null)
                throw new NotFoundException(message);
        }
       
        public static bool IsValid(this object request)
        {
            if (request == null)
                return true;
            try
            {
                var validator = ValidationManager.GetDefaultValidatitor();
                validator.Validate(request);
                return validator.IsValid;
            }
            catch
            {
                return false;
            }
        }

        public static Response GetValidationResponse(this object request)
        {
            var response = new Response();
            if (request == null)
            {
                response.IsOk = false;
                response.Message = "Request is null";
            }
            try
            {
                var validator = ValidationManager.GetDefaultValidatitor();
                validator.Validate(request);
            }
            catch (Exception ex)
            {
                response.SetExceptions(ex);
            }
            return response;
        }
    }
}