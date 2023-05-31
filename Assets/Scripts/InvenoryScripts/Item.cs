using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Stone,
        Wood
    }

    public string itemName;
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() // get item's sprite
    {
        switch (itemType)
        {
            default:
            case ItemType.Stone: return ItemAssets.Instance.stoneSprite;
            case ItemType.Wood: return ItemAssets.Instance.woodSprite;
        }

    }

    public bool IsStackable() // get if our item is stackable
    {
        switch (itemType)
        {
            default:
            case ItemType.Stone:
                return true;
            case ItemType.Wood:
                return false;
            // case ItemType.NotStackable: return false
        }
    }

    public GameObject GetWorldPrefab()
    {
        switch (itemType)
        {
            default:
                case ItemType.Stone: return ItemAssets.Instance.stonePrefab;
                case ItemType.Wood: return ItemAssets.Instance.woodPrefab;
        }
    }
}

