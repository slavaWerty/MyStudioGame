using Infentory.ReadOnly;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infentory
{
    public class InfentoryGrid : IReadOnlyInfentoryGrid
    {
        public event Action<Vector2Int> SizeChanged;

        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                {
                    _data.Size = value;
                    SizeChanged?.Invoke(value);
                }
            }
        }

        private readonly InfentoryGridData _data;
        private readonly Dictionary<Vector2Int, InfentorySlot> _slotsMap = new();
        private readonly Dictionary<InfentorySlot, Vector2Int> _coordinateMaps = new();
        private readonly List<ItemConfig> _itemsConfig = new List<ItemConfig>();
        private bool _isCrystalInfentory;

        public List<InfentorySlot> InfentoriesSlot { private set; get; }

        public string OwnerID => _data.OwnerID;

        public bool IsCrystalInfentory => _isCrystalInfentory;

        public InfentoryGrid(InfentoryGridData data, bool isCrystal)
        {
            _data = data;
            InfentoriesSlot = new List<InfentorySlot>();
            _isCrystalInfentory = isCrystal;

            var size = data.Size;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var index = i * size.y + j;
                    var slotData = _data.Slots[index];
                    var slot = new InfentorySlot(slotData);
                    var position = new Vector2Int(i, j);

                    _slotsMap[position] = slot;
                    _coordinateMaps[slot] = position;
                    InfentoriesSlot.Add(slot);
                }
            }
        }

        public void AddItemConfig(ItemConfig itemConfig)
        {
            for (int i = 0; i < _itemsConfig.Count; i++)
                if (itemConfig.ItemID == _itemsConfig[i].ItemID)
                    return;

            if (itemConfig == null)
                throw new Exception("Config Null");

            _itemsConfig.Add(itemConfig);
            Debug.Log("Config Added");
        }

        public void RemoveItemConfig(ItemConfig itemConfig)
        {
            _itemsConfig.Remove(itemConfig);
        }

        public ItemConfig GetItemConfig(string ItemID)
        {
            foreach (var item in _itemsConfig)
                if (item.ItemID == ItemID)
                    return item;

            throw new Exception("not Config");
        }

        public int GetAmount(string itemID)
        {
            var amount = 0;
            var slots = _data.Slots;

            foreach (var slot in slots)
            {
                if (slot.ItemID == itemID)
                {
                    amount += slot.Amount;
                }
            }

            return amount;
        }

        public IReadOnlyInfentorySlot[,] GetSlots()
        {
            var array = new IReadOnlyInfentorySlot[Size.x, Size.y];

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }

            return array;
        }

        public bool Has(string itemID, int amount)
        {
            var amountExist = GetAmount(itemID);
            return amountExist >= amount;
        }

        public AddItemsToInfentoryResult AddItems(string itemID, Sprite sprite, int amount = 1)
        {
            var remainingAmount = amount;
            var itemsAddedToSlotWithSameItemsAmount = AddToSlotWithSameItems(itemID, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
            {
                return new AddItemsToInfentoryResult(amount, itemsAddedToSlotWithSameItemsAmount);
            }

            var itemsAddedToAvaliableSlotsAmount = AddFirstAvaliableSlots(itemID, remainingAmount, out remainingAmount, sprite);
            var totalAddedItemsAmount = itemsAddedToAvaliableSlotsAmount + itemsAddedToSlotWithSameItemsAmount;

            return new AddItemsToInfentoryResult(amount, totalAddedItemsAmount);
        }

        public AddItemsToInfentoryResult AddItems(Vector2Int slotCoordinate, string itemID, Sprite sprite, int amount = 1)
        {
            var slot = _slotsMap[slotCoordinate];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.isEmpty)
                slot.ItemID = itemID;

            var itemSlotCapacity = GetItemSlotCapacity(itemID);

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemID, sprite, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
            }

            return new AddItemsToInfentoryResult(amount, itemsAddedAmount);
        }

        public RemoveToInfentoryResult RemoveItems(string itemID, int amount = 1)
        {
            if (!Has(itemID, amount))
            {
                return new RemoveToInfentoryResult(amount, false);
            }

            var amountToRemove = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var slotCoordinate = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoordinate];

                    if (slot.ItemID != itemID)
                        continue;

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;

                        RemoveItems(slotCoordinate, itemID, slot.Amount);
                    }
                    else
                    {
                        RemoveItems(slotCoordinate, itemID, amountToRemove);

                        return new RemoveToInfentoryResult(amount, true);
                    }


                }
            }

            throw new Exception("Problem in Remove");
        }

        public RemoveToInfentoryResult RemoveItems(Vector2Int slotCoordinate, string itemID, int amount = 1)
        {
            var slot = _slotsMap[slotCoordinate];

            if (slot.isEmpty || slot.ItemID != itemID || slot.Amount < amount)
                return new RemoveToInfentoryResult(amount, false);

            slot.Amount -= amount;

            if (slot.Amount == 0)
            {
                slot.ItemID = null;
                slot.Sprite = null;
            }

            return new RemoveToInfentoryResult(amount, true);
        }

        private int GetItemSlotCapacity(string itemId)
        {
            for (int i = 0; i < _itemsConfig.Count; i++)
                if (_itemsConfig[i].ItemID == itemId)
                    return _itemsConfig[i].AmountCapacity;

            throw new Exception("in Items not itemID");
        }

        private int AddFirstAvaliableSlots(string itemID, int amount, out int remainingAmount, Sprite sprite)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (!slot.isEmpty)
                        continue;

                    slot.ItemID = itemID;
                    slot.Sprite = sprite;
                    var newValue = remainingAmount;
                    var slotItemCapacity = GetItemSlotCapacity(itemID);

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int AddToSlotWithSameItems(string itemID, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.isEmpty)
                        continue;

                    var slotItemCapacity = GetItemSlotCapacity(itemID);

                    if (slot.Amount >= slotItemCapacity)
                        continue;

                    if (slot.ItemID != itemID)
                        continue;

                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                            return itemsAddedAmount;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        public void SwitchSlots(Vector2Int firstCoordinate, Vector2Int secondCoordinate)
        {
            var slotA = _slotsMap[firstCoordinate];
            var slotB = _slotsMap[secondCoordinate];
            var tempSlotItemID = slotA.ItemID;
            var tempSlotItemAmount = slotA.Amount;
            slotA.ItemID = slotB.ItemID;
            slotA.Amount = slotB.Amount;
            slotB.ItemID = tempSlotItemID;
            slotB.Amount = tempSlotItemAmount;
        }



        public Vector2Int GetCoordinate(InfentorySlot slot)
        {
            return _coordinateMaps[slot];
        }
    }

}
