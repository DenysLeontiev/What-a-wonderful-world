using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get;private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite woodSprite; 
    public Sprite stoneSprite;

    public GameObject stonePrefab; // what we instantiate when we remove that from inventory
    public GameObject woodPrefab;
}
