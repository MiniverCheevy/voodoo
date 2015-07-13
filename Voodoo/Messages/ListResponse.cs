using System.Collections.Generic;

namespace Voodoo.Messages
{
    public class ListResponse<T> : Response, IResponse where T : new()
    {
        public ListResponse()
        {
            IsOk = true;
            Data = new List<T>();
        }

        public List<T> Data { get; set; }
    }
}