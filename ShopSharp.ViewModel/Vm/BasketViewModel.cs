namespace ShopSharp.ViewModel.Vm;

public class BasketViewModel
{
    public int Id { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = [];
    public string? BuyerId { get; set; }

    public decimal Total()
    {
        return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
    }
}
