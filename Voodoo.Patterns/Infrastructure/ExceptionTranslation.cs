using System;
using Voodoo.Messages;

namespace Voodoo.Infrastructure
{
    public abstract class ExceptionTranslation

    {
        public virtual bool DecorateResponse(Exception exception, IResponse response)
        {
            try
            {
                return TranslateException(exception, response);
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected abstract bool TranslateException(Exception exception, IResponse response);
    }
}