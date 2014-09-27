using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo.Messages;

namespace Voodoo.Infrastructure
{
    public class ReflectionTypeLoaderExceptionTranslation :
        ExceptionTranslation
    {
        protected override bool TranslateException(Exception exception, IResponse response)
        {
            var refException = exception as ReflectionTypeLoadException;
            if (refException == null)
                return false;
            response.Message = refException.Message;
            foreach (var item in refException.LoaderExceptions)
            {
                response.Details.Add(new NameValuePair(item.Source, item.Message));
            }
            return true;
        }
    }
}