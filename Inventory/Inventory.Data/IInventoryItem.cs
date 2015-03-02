namespace Inventory.Data
{
    public interface IInventoryItem
    {
        string Name { get; }
        string Category { get;}
        int Price { get; set; }
        int Stock { get; set; }
        Supplier PreferredSupplier { get; set; }
        Supplier FailoverSupplier { get; set; }
        int PurchasedPrice { get; set; }
    }
}