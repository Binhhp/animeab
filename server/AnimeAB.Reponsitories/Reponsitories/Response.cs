using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Reponsitories
{
    public abstract class Request
    {
        public Response Success(object data = null, string message = null)
        {
            return new Response { Success = true, Data = data, Message = message };
        }

        public Response Error(string message = "")
        {
            return new Response { Success = false, Data = null, Message = message };
        }
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
