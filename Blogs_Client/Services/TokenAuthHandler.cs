using Blazored.LocalStorage;
using Blogs_Client.Model;
using System.Net.Http.Headers;

namespace Blogs_Client.Services
{
    public class TokenAuthHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;
        public TokenAuthHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) 
        { 
            var user = await _localStorageService.GetItemAsync<User>("user"); 
            if (!string.IsNullOrEmpty(user?.Token)) 
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token); 
            } 
            return await base.SendAsync(request, cancellationToken); 
        }
    }
}
