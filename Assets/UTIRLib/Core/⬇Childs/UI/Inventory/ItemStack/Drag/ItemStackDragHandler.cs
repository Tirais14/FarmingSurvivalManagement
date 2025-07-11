#nullable enable

using UnityEngine;
using UnityEngine.EventSystems;
using UTIRLib.Injector;

namespace UTIRLib.UI
{
    public class ItemStackDragHandler : DragHandler<DragItemStack>
    {
        [SerializeField]
        [GetComponentInParentIfNull]
        protected ItemStackModelBase sourceItemStack = null!;

        protected override void OnAwake()
        {
            base.OnAwake();
            userInterface = GetComponentInParent<UserInterface>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            draggable.gameObject.SetActive(true);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            draggable.gameObject.SetActive(false);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
            if (eventData.selectedObject.TryGetComponent<ItemStackModelBase>(out var droppedItemStack))
            {
                sourceItemStack.Put(droppedItemStack);
            }
        }
    }
}