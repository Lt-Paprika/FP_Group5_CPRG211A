namespace Group5
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001");
            };
        }

        public async Task<string> LoginAsync(string employeeID, string password)
        {
            var request = new
            {
                EmployeeID = employeeID,
                password = password
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<JsonElement>(responseBody);
                return data.GetProperty("Role").GetString();
            }

            return null;
        }
    }
}