#nullable enable
using UnityEngine;
using UTIRLib.UI;

namespace Core
{
    public interface IItem : IItemUI
    {
        GameObject WorldModel { get; }
    }
}
