using UTIRLib.UI;

#nullable enable
namespace Game.InventorySystem
{
    public interface IInventoryUI : IUserInterface
    {
        IStorage Inventory { get; }
    }
}
