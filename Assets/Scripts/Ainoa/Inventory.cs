using System.Collections.Generic;
using UnityEngine;

namespace Ainoa.Items
{
    [System.Serializable]
    public class Inventory
    {
        [SerializeField] private List<InventorySettings> _inventory;

        [System.Serializable]
        internal class InventorySettings
        {
            internal Item _item;
            internal int _quantity;

            internal InventorySettings(Item i, int quantity)
            {
                _item = i;
                _quantity = quantity;
            }

            internal void AddQuantity(int add = 1)
            {
                add = Mathf.Abs(add);

                _quantity += add;
            }
            internal void RemoveQuantity(int remove = 1)
            {
                remove = Mathf.Abs(remove);
                _quantity -= remove;
            }
        }
        public void AddItem(Item item)
        {
            if (!_inventory.Exists(n => item))
            {
                _inventory.Add(new(item, 1));
            }
        }

        public void RemoveItem(Item item)
        {
            var i = _inventory.Find(n => item);

            if (i != null)
            {
                if (i._quantity > 1) i.RemoveQuantity(1);
                else _inventory.Remove(i);
            }
        }
    }
}