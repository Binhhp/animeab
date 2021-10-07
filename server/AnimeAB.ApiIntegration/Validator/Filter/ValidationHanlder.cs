
using AnimeAB.Application.Common.Validator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeAB.ApiIntegration.Validator.Filter
{
    public static class ValidationHanlder
    {
        public static ErrorResponse GetErrors(ModelStateDictionary ModelState)
        {
            var errorsInModelValue = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(key => key.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

            var errorResponse = new ErrorResponse();

            foreach (var error in errorsInModelValue)
            {
                foreach (var subError in error.Value)
                {
                    var errorModel = new ErrorModel
                    {
                        FieldName = error.Key,
                        Message = subError
                    };

                    errorResponse.Errors.Add(errorModel);
                }
            }

            return errorResponse;
        }
    }
}
