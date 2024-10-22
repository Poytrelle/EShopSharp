using ShopSharp.Infrastructure.Services;

namespace ShopSharp.Domain.Implementation.Services;

public class UriComposer(CatalogSettings _catalogSettings) : IUriComposer
{
    public string ComposePicUri(string uriTemplate)
    {
        return uriTemplate.Replace("http://<BASE_URL>", _catalogSettings.CatalogBaseUrl);
    }
}
