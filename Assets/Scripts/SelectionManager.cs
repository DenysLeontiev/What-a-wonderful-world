using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance; // for Singleton pattern

    [SerializeField] private GameObject interactionUI;
    private TextMeshProUGUI interactionText;

    [SerializeField] private float maxDistanceRay = 3f;

    private Inventory inventory;
    

    private void Awake()
    {
        if(Instance != null && Instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        interactionText = interactionUI.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // get ray from mouse position(center of the screen, because CursorLockMode.Locked)

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistanceRay))
        {
            var selectionTransform = hit.transform;

            if(selectionTransform.TryGetComponent<ItemWorld>(out ItemWorld itemWorld))
            {
                interactionText.text = itemWorld.Item.itemName;
                interactionUI.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    inventory.AddItem(itemWorld.GetItem());
                    itemWorld.DestroySelf();
                }
            }
            else
            {
                interactionUI.SetActive(false);
            }

        }
        else
        {
            interactionUI.SetActive(false);
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
}
