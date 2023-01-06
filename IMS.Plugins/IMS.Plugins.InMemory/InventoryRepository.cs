using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.Plugins.InMemory
{
    public class InventoryRepository : IInventoryRepository
    {
        private List<Inventory> _inventories;

        public InventoryRepository()
        {
            _inventories = new List<Inventory>
            {
                new Inventory{ InventoryId = 1,InventoryName = "Bike Seat",Qiantity = 10, Price = 2},
                new Inventory{ InventoryId = 1,InventoryName = "Bike Body",Qiantity = 10, Price = 15},
                new Inventory{ InventoryId = 1,InventoryName = "Bike Wheels",Qiantity = 20, Price = 8},
                new Inventory{ InventoryId = 1,InventoryName = "Bike Pedals",Qiantity = 20, Price = 1}
            };
        }
        public async Task<IEnumerable<Inventory>> GetInventoryByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);

            return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}