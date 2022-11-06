using Newtonsoft.Json;
using System.Net;

namespace HotelListing.API.Midelware
{
    public partial class ExceptionMilddleware
    {
        private RequestDelegate _request;
        private readonly ILogger _logger;

        public ExceptionMilddleware(RequestDelegate request, ILogger<ExceptionMilddleware> logger)
        {
            _request = request;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"error happened in {context.Request.Path} - user Registration");
                await HandelExceptionAsync(context, ex);
            }

        }

        private Task HandelExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails
            {
                ErrorType = "Failure",
                ErrorMessage = ex.Message,


            };
        switch (ex) {
                case DirectoryNotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorMessage = "Not Found";
                    break;
                default:

                    break;
            }

            string response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(response);
        }
    }
}
