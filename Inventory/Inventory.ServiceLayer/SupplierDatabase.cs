namespace Inventory.ServiceLayer
{
    class SupplierDatabase : BaseDatabase
    {
        public void UpdateStock(string name, string category, int stock)
        {
            InventoryHandler.UpdateItemStock(name, category, stock);
        }
    }
}
