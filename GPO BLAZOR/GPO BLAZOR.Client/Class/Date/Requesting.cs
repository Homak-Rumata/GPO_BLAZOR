using GPO_BLAZOR.Client.Class.JSRunTimeAccess;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GPO_BLAZOR.Client.Class.Date
{
    public static class Requesting
    {
        public async static Task<T> AutorizationRequest<T> (Uri uri, IJSRuntime jsr)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var cookieStorage = new CookieStorageAccessor(jsr);

                var jwt = await cookieStorage.ReadCookieAsync<string>("Autorization");

                httpClient.BaseAddress = uri;
                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress);
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                var tempresponce = await httpClient.SendAsync(requestMessage);

                return await tempresponce.Content.ReadFromJsonAsync<T>();
            }
        }

        public async static Task<T> AutorizationRequest<T, C>(Uri uri, IJSRuntime jsr, C Date)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var cookieStorage = new CookieStorageAccessor(jsr);

                var jwt = await cookieStorage.ReadCookieAsync<string>("Autorization");

                httpClient.BaseAddress = uri;
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, httpClient.BaseAddress);
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                requestMessage.Content = JsonContent.Create(Date);

                var tempresponce = await httpClient.SendAsync(requestMessage);
                return await tempresponce.Content.ReadFromJsonAsync<T>();
            }
        }
    }
}

