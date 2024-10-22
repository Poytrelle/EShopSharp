namespace ShopSharp.ViewModel.Vm;

public class OrderDetailViewModel : OrderViewModel
{
    public List<OrderItemViewModel> OrderItems { get; set; } = new();
}
