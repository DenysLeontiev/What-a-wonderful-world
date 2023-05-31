using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Item;

public class Inventory
{
	public event EventHandler OnItemListChanged; // event which triggers when we add/remove something to inventory

    private List<Item> itemList;

	public Inventory()
	{
		itemList = new List<Item>();
        //AddItem(new Item { amount = 1, itemType = ItemType.Stone, itemName = "Test" });
        //AddItem(new Item { amount = 1, itemType = ItemType.Stone, itemName = "Test" });
        //AddItem(new Item { amount = 1, itemType = ItemType.Wood, itemName = "Test" });
        //AddItem(new Item { amount = 1, itemType = ItemType.Wood, itemName = "Test" });
	}

	public void AddItem(Item item)
	{
		if(item.IsStackable()) // if we alredy have this item in inventory
		{
			bool isAlreadyInInventory = false;
			foreach (var itemInventory in itemList)
			{
				if(itemInventory.itemType == item.itemType)
				{
					isAlreadyInInventory = true;
					itemInventory.amount += item.amount;
				}
			}

			if(!isAlreadyInInventory)
			{
				itemList.Add(item);
			}
		}
        else // if we dont have this item in inventory
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty); // here we call all the listeners to that event
	}

	public void RemoveItem(Item item)
	{
        if (item.IsStackable()) //
        {
            Item itemInInventory = null;
            foreach (var itemInventory in itemList)
            {
                if (itemInventory.itemType == item.itemType)
                {
                    itemInventory.amount -= 1;
                    itemInInventory = itemInventory;
                }
            }

            if (itemInInventory.amount <= 0 && itemInInventory != null)
            {
                itemList.Remove(item);
            }
        }
        else
        {
            itemList.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

	public List<Item> GetItemList()
	{
		return itemList;
	}
}

