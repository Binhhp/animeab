using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.Application.Common.ExceptionsHanlder
{
    public static class ErrorDetails
    {
        public static ErrorResponse InnerError(int code, string message, string status)
        {
            return new ErrorResponse
            {
                error = new Error
                {
                    code = code,
                    message = message,
                    status = status
                }
            };
        }
    }
}
