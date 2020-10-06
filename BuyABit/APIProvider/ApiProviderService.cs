using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuyABit
{
    public class ApiProviderService : IApiProviderService
    {
        private HttpClient client;
        private string _countriesBaseApiUrl;
        
        public ApiProviderService(string countriesBaseApiUrl = "https://restcountries.eu/rest/v2/all", string Token = null)
        {
            client = new HttpClient();
            if (Token != null)
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
            _countriesBaseApiUrl = countriesBaseApiUrl;
        }

        private async Task<T> PostAsync<T, U>(U mail, string pathUrl)
        {
            string serializedResponse = null;
            string Content = JsonConvert.SerializeObject(mail); 
            StringContent httpContentWebRequest = new StringContent(Content, Encoding.UTF8, "application/json");
            string request = $"{_countriesBaseApiUrl + pathUrl}";
            Task<HttpResponseMessage> response = client.PostAsync(request, httpContentWebRequest);
            serializedResponse = await response.Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(serializedResponse);
        }

        public async Task<object> GetAllCountriesDataAsync()
        {
            string getExpression = string.Empty; // possibly somewthing like ?searchTerm={searchTerm}
            string pathUrl = string.Empty; // a path specific to this call i.e BaseUrl + "/countries/inSADC/" or whatever
            return await GetAsync<object>(getExpression, pathUrl);
        }
        private async Task<T> GetAsync<T>(string searchTerm, string pathUrl)
        {
            string request = $"{_countriesBaseApiUrl}{pathUrl}{searchTerm}";
            HttpResponseMessage response = await client.GetAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
