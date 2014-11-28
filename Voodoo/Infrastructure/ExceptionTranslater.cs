using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;

namespace Voodoo.Infrastructure
{
    public class ExceptionTranslater : Dictionary<Type, ExceptionTranslation>
    {
        public bool Contains<T>()
        {
            return this.ContainsKey(typeof (T));
        }

        public bool DecorateResponseWithException<T>(Exception ex, IResponse response)
        {
            if (! this.Contains<T>())
                return false;

            var translator = this[typeof (T)];
            return translator.DecorateResponse(ex, response);
        }

        public bool DecorateResponseWithException(Exception ex, IResponse response)
        {
            if (!this.ContainsKey(ex.GetType()))
                return false;

            var translator = this[ex.GetType()];
            return translator.DecorateResponse(ex, response);
        }
    }
}