#nullable enable
using UTIRLib.UI;

namespace Core
{
    public interface IPlayer
    {
        IItem? HoldItem { get; set; }
        bool HasHoldItem { get; }
    }
}
