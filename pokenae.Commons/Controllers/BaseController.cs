using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace pokenae.UserManager.API.Controllers
{
    /// <summary>
    /// ベースコントローラ
    /// </summary>
    public abstract class BaseController : ControllerBase, IAsyncActionFilter
    {
        private readonly HttpClient _httpClient;
        private readonly string _logApiUrl;

        protected BaseController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var apiBaseUrl = configuration["pUM:BaseUrl"];
            var logUrl = configuration["pUM:LogUrl"];
            _logApiUrl = $"{apiBaseUrl}{logUrl}";
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var accessToken = context.HttpContext.Request.Query["accessToken"].ToString();
            var apiUrl = context.HttpContext.Request.Path;
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
