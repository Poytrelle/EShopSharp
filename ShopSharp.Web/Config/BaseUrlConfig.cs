namespace ShopSharp.Web.Config;

public class BaseUrlConfig
{
    public const string ConfigName = "baseUrls";

    public required string ApiBase { get; set; }
    public required string WebBase { get; set; }
}
