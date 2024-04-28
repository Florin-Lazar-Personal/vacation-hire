using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRate.Providers.CurrencyLayer.Api
{
    internal class CurrencyLayerAccessKeyHandler : DelegatingHandler
    {
        private readonly string _accessKey;

        public CurrencyLayerAccessKeyHandler(string accessKey)
        {
            _accessKey = accessKey;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string separator = string.IsNullOrEmpty(request.RequestUri.Query) ? "?" : "&";

            UriBuilder uriBuilder = new UriBuilder(request.RequestUri);
            uriBuilder.Query = $"{uriBuilder.Query}{separator}access_key={_accessKey}";

            request.RequestUri = uriBuilder.Uri;
            return base.SendAsync(request, cancellationToken);
        }
    }
}
