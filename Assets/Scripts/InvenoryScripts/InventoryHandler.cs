using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public static InventoryHandler Instance;

    [SerializeField] private GameObject[] inventoryObjectsUI;
    [SerializeField] private KeyCode inventoryButton = KeyCode.Tab;

    private bool isInventoryOpened = false;

    public bool IsInventoryOpened
    {
        get { return isInventoryOpened; }
    }


    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if(Input.GetKey(inventoryButton))
        {
            foreach (var invObj in inventoryObjectsUI)
            {
                invObj.SetActive(true);
            }
            Cursor.lockState = CursorLockMode.None;
            isInventoryOpened = true;
        }
        else 
        {
            foreach (var invObj in inventoryObjectsUI)
            {
                invObj.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.Locked;
            isInventoryOpened = false;
        }
    }
}
