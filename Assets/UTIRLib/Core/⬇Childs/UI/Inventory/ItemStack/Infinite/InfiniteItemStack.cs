#nullable enable

using System;
using UTIRLib.Diagnostics;

namespace UTIRLib.UI
{
    public class InfiniteItemStack : ItemStack
    {
        /// <exception cref="ArgumentNullException"></exception>
        public override IItemStack Put(IItem item, int quantity = 1)
        {
            if (item.IsNull())
            {
                throw new ArgumentNullException(nameof(item));
            }

            Clear();
            return base.Put(item, item.MaxQuantity);
        }

        /// <exception cref="ArgumentNullException"></exception>
        public override void Put(IItemStack itemStack)
        {
            if (itemStack.IsNull())
            {
                throw new ArgumentNullException(nameof(itemStack));
            }
            if (itemStack.IsEmpty) return;

            Put(itemStack.Item!, itemStack.Quantity);
        }

        public override IItemStack Take(int quantity = 1)
        {
            if (IsEmpty) return empty;

            return new ItemStack(item, quantity);
        }

        public override IItemStack TakeAll()
        {
            if (IsEmpty) return empty;

            return new ItemStack(item, item!.MaxQuantity);
        }
    }
}