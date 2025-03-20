using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace pokenae.Commons.Presentation.Filters
{
    public class LoggingActionFilter : IAsyncActionFilter
    {
        private readonly HttpClient _httpClient;
        private readonly string _logApiUrl;

        public LoggingActionFilter(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var apiBaseUrl = configuration["pUM:BaseUrl"];
            var logUrl = configuration["pUM:LogUrl"];
            _logApiUrl = $"{apiBaseUrl}{logUrl}";
        }

        public async Task OnActionExecutionAsync([FromQuery] ActionExecutingContext context, [FromQuery] ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Host", out var apiUrl))
            {
                context.HttpContext.Request.Headers.TryGetValue("Authorization", out var accessToken);
                var controllerName = context.ActionDescriptor.RouteValues["controller"];
                var actionName = context.ActionDescriptor.RouteValues["action"];
                var parameters = context.HttpContext.Request.QueryString.ToString();

                bool canAccess = false;
                bool isError = false;
                string? errorMessage = null;
                string? response = null;

                try
                {
                    var resultContext = await next();

                    canAccess = resultContext.HttpContext.Response.StatusCode == 200;
                    response = resultContext.HttpContext.Response.StatusCode.ToString();
                }
                catch (Exception ex)
                {
                    isError = true;
                    errorMessage = ex.Message;
                    response = "Error occurred during action execution";
                }
                finally
                {
                    await LogToDatabaseAsync(accessToken, apiUrl, canAccess, isError, errorMessage, response, controllerName, actionName, parameters);
                }
            }
        }

        private async Task LogToDatabaseAsync(string accessToken, string apiUrl, bool canAccess, bool isError, string? errorMessage, string? response, string? controllerName, string? actionName, string? parameters)
        {
            var logEntry = new
            {
                AccessToken = accessToken,
                ApiUrl = apiUrl,
                CanAccess = canAccess,
                IsError = isError,
                ErrorMessage = errorMessage,
                Response = response,
                ControllerName = controllerName,
                ActionName = actionName,
                Parameters = parameters
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(logEntry), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_logApiUrl, content);
        }
    }
}
