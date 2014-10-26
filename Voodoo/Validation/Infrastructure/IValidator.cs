using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Validation.Infrastructure
{
    public interface IValidator
    {
        void Validate(object request);
    }
}
