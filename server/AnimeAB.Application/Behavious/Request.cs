using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeAB.Application.Behavious
{
    public abstract class Request
    {
        public Response Success(object? data = null, string? message = null)
        {
            return new Response { Success = true, Data = data, Message = message };
        }

        public Response Error(string message = "")
        {
            return new Response { Success = false, Data = null, Message = message };
        }
    }
}
