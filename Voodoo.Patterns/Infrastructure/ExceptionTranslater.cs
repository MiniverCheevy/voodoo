using System;
using System.Collections.Generic;
using Voodoo.Messages;

namespace Voodoo.Infrastructure
{
    public class ExceptionTranslater : Dictionary<Type, ExceptionTranslation>
    {
        public bool Contains<T>()
        {
            return ContainsKey(typeof(T));
        }

        public bool DecorateResponseWithException<T>(Exception ex, IResponse response)
        {
            if (!Contains<T>())
                return false;

            var translator = this[typeof(T)];
            return translator.DecorateResponse(ex, response);
        }

        public bool DecorateResponseWithException(Exception ex, IResponse response)
        {
            if (!ContainsKey(ex.GetType()))
                return false;

            var translator = this[ex.GetType()];
            return translator.DecorateResponse(ex, response);
        }
    }
}