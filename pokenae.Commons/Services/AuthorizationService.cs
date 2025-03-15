using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace pokenae.Commons.Services
{
    public class AuthorizationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthorizationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> HasAccessAsync(string userId, string resource)
        {
            var apiUrl = _configuration["AuthorizationApiUrl"];
            var response = await _httpClient.GetAsync($"{apiUrl}/check?userId={userId}&resource={resource}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return bool.Parse(result);
        }
    }
}
