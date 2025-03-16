using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokenae.Commons.Filters
{
    /// <summary>
    /// APIアクセス権限をチェックするフィルタの実装
    /// </summary>
    public class ApiAccessFilter : IAsyncActionFilter
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAccessUrl;

        public ApiAccessFilter(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var apiBaseUrl = configuration["pUM:BaseUrl"];
            var accessUrl = configuration["pUM:AccessUrl"];
            _apiAccessUrl = $"{apiBaseUrl}{accessUrl}";
        }

        public async Task OnActionExecutionAsync([FromQuery] ActionExecutingContext context, [FromQuery] ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Query.TryGetValue("accessToken", out var accessToken) &&
                context.HttpContext.Request.Query.TryGetValue("apiUrl", out var apiUrl))
            {
                var response = await _httpClient.GetAsync($"{_apiAccessUrl}?accessToken={accessToken}&apiUrl={apiUrl}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                    if (result.CanAccess)
                    {
                        await next();
                        return;
                    }
                }
            }

            context.Result = new ForbidResult();
        }
    }
}
