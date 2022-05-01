using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IInventoriesRepository
    {
        bool CreateInventory(Inventory inventory);
        Inventory GetInventoryByProduct(int id);
        IEnumerable<Inventory> GetInventories();
        UpdateInventoryDTO UpdateInventory(int id, UpdateInventoryDTO inventoryDTO);
    }
}
