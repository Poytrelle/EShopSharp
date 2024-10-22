using ShopSharp.Infrastructure.Entity;

namespace ShopSharp.ViewModel.Vm;

public class OrderViewModel
{
    private const string DefaultStatus = "Pending";

    public int OrderNumber { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
    public string Status => DefaultStatus;
    public Address? ShippingAddress { get; set; }
}
