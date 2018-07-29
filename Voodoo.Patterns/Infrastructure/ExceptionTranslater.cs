using System;
using System.Collections.Generic;
using Voodoo.Messages;

namespace Voodoo.Infrastructure
{
    public class ExceptionTranslater : Dictionary<Type, List<ExceptionTranslation>>
    {
        public bool Contains<T>()
        {
            return ContainsKey(typeof(T));
        }

        public bool DecorateResponseWithException<T>(Exception ex, IResponse response)
        {
            if (!Contains<T>())
                return false;

            var items = this[typeof(T)];
            var result = false;
            foreach (var translator in items)
            {
                result = translator.DecorateResponse(ex, response);
                if (result)
                    return result;
            }
            return result;
        }

        public bool DecorateResponseWithException(Exception ex, IResponse response)
        {
            if (!ContainsKey(ex.GetType()))
                return false;
          
            var items = this[ex.GetType()];
            var result = false;
            foreach (var translator in items)
            {
                result = translator.DecorateResponse(ex, response);
                if (result)
                    return result;
            }
            return result;
        }
    }
}