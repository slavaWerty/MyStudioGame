using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infentory
{
    public class InfentoryService : MonoBehaviour
    {
        private readonly Dictionary<string, InfentoryGrid> _inventoriesMap = new();

        public event Action<AddItemsToInfentoryResult> AddedItemToInfentory;
        public event Action<RemoveToInfentoryResult> RemovedItemToInfentory;

        public InfentoryGrid RegisterInfentory(InfentoryGridData data, bool isCrystal)
        {
            var infentory = new InfentoryGrid(data, isCrystal);
            _inventoriesMap[infentory.OwnerID] = infentory;

            return infentory;
        }

        public AddItemsToInfentoryResult AddItemToInfentory(string ownerId, string itemId, Sprite sprite, int amount = 1)
        {
            var infentory = _inventoriesMap[ownerId];
            var result = infentory.AddItems(itemId, sprite, amount);
            AddedItemToInfentory?.Invoke(result);
            return result;
        }

        public AddItemsToInfentoryResult AddItemToInfentory(string ownerId,
            string itemId, Sprite sprite, Vector2Int coordinate, int amount = 1)
        {
            var infentory = _inventoriesMap[ownerId];
            var result = infentory.AddItems(coordinate, itemId, sprite, amount);
            AddedItemToInfentory?.Invoke(result);
            return result;
        }

        public RemoveToInfentoryResult RemoveItemToInfentory(string ownerId, string itemId, int amount = 1)
        {
            var infetory = _inventoriesMap[ownerId];
            var result = infetory.RemoveItems(itemId, amount);
            RemovedItemToInfentory?.Invoke(result);
            return result;
        }

        public RemoveToInfentoryResult RemoveItemToInfentory(string ownerId, Vector2Int coordinate, string itemId, int amount = 1)
        {
            var infetory = _inventoriesMap[ownerId];
            var result = infetory.RemoveItems(coordinate, itemId, amount);
            RemovedItemToInfentory?.Invoke(result);
            return result;
        }

        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var infentory = _inventoriesMap[ownerId];
            return infentory.Has(itemId, amount);
        }

        public InfentoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }

        public void AddItemConfigToInfentory(string ownerId, ItemConfig config)
        {
            var infentory = _inventoriesMap[ownerId];
            infentory.AddItemConfig(config);
        }
    }
}
