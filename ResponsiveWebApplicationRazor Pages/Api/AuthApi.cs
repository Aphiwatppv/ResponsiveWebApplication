using System.Text;
using System.Text.Json;

namespace ResponsiveWebApplicationRazor_Pages.Api
{
    public class AuthApi : IAuthApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SessionModel> LoginAsync(LoginRequestModel loginRequestModel)
        {

          
            var httpClient = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(loginRequestModel), Encoding.UTF8, "application/json");
           
            var response = await httpClient.PostAsync("https://localhost:7279/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var sessionModel = JsonSerializer.Deserialize<SessionModel>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return sessionModel ?? new SessionModel();
            }


            throw new Exception("Login failed");
        }
        public async Task<int> RegisterAsync(RegisterRequestModel registerRequestModel)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(registerRequestModel), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:7279/Register", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var sessionModel = JsonSerializer.Deserialize<int>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return sessionModel;
            }

            // Consider throwing an exception or handling the error appropriately
            throw new Exception("Registration failed");
        }
    }


}

