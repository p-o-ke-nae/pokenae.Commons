using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        public const string defaultAccessToken = "guest_access_token";

        public ApiAccessFilter(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var apiBaseUrl = configuration["pUM:BaseUrl"];
            var accessUrl = configuration["pUM:AccessUrl"];
            _apiAccessUrl = $"{apiBaseUrl}{accessUrl}";
        }

        public async Task OnActionExecutionAsync([FromQuery] ActionExecutingContext context, [FromQuery] ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Host", out var apiUrl))
            {
                context.HttpContext.Request.Headers.TryGetValue("Authorization", out var accessToken);
                var uriBuilder = new UriBuilder(_apiAccessUrl);
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                query["accessToken"] = string.IsNullOrWhiteSpace(accessToken) ? accessToken : defaultAccessToken;
                query["apiUrl"] = apiUrl;
                uriBuilder.Query = query.ToString();

                var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
                HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var val = JsonConvert.DeserializeObject<CanAccess>(await httpResponseMessage.Content.ReadAsStringAsync());
                    if (val != null && val.IsAllowed)
                    {
                        await next();
                        return;
                    }
                }
            }

            context.Result = new NotFoundResult();
        }
    }

    /// <summary>
    /// レスポンス用クラス
    /// </summary>
    public class CanAccess
    {
        public bool IsAllowed { get; set; }
    }
}
