using DS.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net;

namespace DS.Web.Framework.Filters
{
    

    public class DSExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            
            Log.Error(context.Exception.Message);

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                
                message = string.IsNullOrEmpty(context.Exception.Message) ? "Unauthorized Access" : context.Exception.Message;
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(DSException))
            {
                status = HttpStatusCode.InternalServerError;
                message = context.Exception.Message;
            }
            else
            {
                status = HttpStatusCode.BadRequest;
                message = context.Exception.ToString();
            }

            var result = new ContentResult() { StatusCode = (int)status, ContentType = "application/json", Content = message};
            context.Result = result;
        }
    }
}
