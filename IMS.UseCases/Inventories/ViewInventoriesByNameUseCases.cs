using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Inventories
{
    public class ViewInventoriesByNameUseCases : IViewInventoriesByNameUseCases
    {
        private readonly IInventoryRepository _inventoryRepository;

        public ViewInventoriesByNameUseCases(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
        {
            return await _inventoryRepository.GetInventoryByNameAsync(name);
        }
    }
}
