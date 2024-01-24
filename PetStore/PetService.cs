using Newtonsoft.Json;

namespace PetStoreApp
{
    public class PetService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;

        public PetService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper ?? throw new ArgumentNullException(nameof(httpClientWrapper));
        }

        public async Task<List<Pet>> DisplayAvailablePets()
        {
            string petsUrl = "https://petstore.swagger.io/v2/pet/findByStatus";

            // Fetch available pets
            var queryParams = new Dictionary<string, string> { { "status", "available" } };
            HttpResponseMessage petsResponse = await _httpClientWrapper.GetAsync(petsUrl + ToQueryString(queryParams));
            petsResponse.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<List<Pet>>(petsResponse.Content.ReadAsStringAsync().Result);
        }

        private string ToQueryString(Dictionary<string, string> queryParams)
        {
            if (queryParams.Count == 0)
                return "";

            var queryString = new System.Text.StringBuilder("?");
            foreach (var param in queryParams)
            {
                queryString.Append($"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(param.Value)}&");
            }
            queryString.Length--; // Remove trailing "&"
            return queryString.ToString();
        }

        public interface IHttpClientWrapper
        {
            Task<HttpResponseMessage> GetAsync(string requestUri);
        }

        public class HttpClientWrapper : IHttpClientWrapper
        {
            private readonly HttpClient _httpClient;

            public HttpClientWrapper(HttpClient httpClient)
            {
                _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            }

            public Task<HttpResponseMessage> GetAsync(string requestUri)
            {
                return _httpClient.GetAsync(requestUri);
            }
        }
    }
}
