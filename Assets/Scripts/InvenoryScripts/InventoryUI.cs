using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer; // container which holds all itemSlotTemplates
    private Transform itemSlotTemplate; // displays in inventory when we have picked up smth
    private Transform emptySlotContainer;
    private Transform emptySlotTemplate; // just empty slot,to show where we can drag&drop items

    [SerializeField] private int itemSlotCellSize = 105;
    [SerializeField] private int rowLength = 5;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        emptySlotContainer = transform.Find("emptySlotContainer");
        emptySlotTemplate = emptySlotContainer.Find("emptySlotTemplate");
    }

    private void Start()
    {
        DrawInventory();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged; // when we have added smth to inventory(Inventory script),
                                                                    // we notify our listeners(InventoryUI) that there is smth new and we have to update InventoryUI(Visual)

        RefreshInventoryItems();    
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void DrawInventory()
    {
        int x = 0;
        int y = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < rowLength; j++)
            {
                RectTransform emptySlotRectTransform = Instantiate(emptySlotTemplate, emptySlotContainer).GetComponent<RectTransform>();
                emptySlotRectTransform.gameObject.SetActive(true);
                emptySlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);

                x++;
                if (x > rowLength)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }

    public void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer) // here we remove all old templates
        {
            if(child == itemSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        foreach (var item in inventory.GetItemList())
        {
            RectTransform itemSlotTemplateRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotTemplateRectTransform.gameObject.SetActive(true);
            itemSlotTemplateRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotTemplateRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            itemSlotTemplateRectTransform.GetComponent<Button_UI>().ClickFunc = () => // use item
            {
                Debug.Log("Left Click");
            };

            itemSlotTemplateRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => // remove item
            {
                inventory.RemoveItem(item);
                ItemWorld.DropItem(item);
                Debug.Log("Right Click");
            };

            TextMeshProUGUI textAmount = itemSlotTemplateRectTransform.Find("textAmount").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                textAmount.SetText(item.amount.ToString());
            }
            else
            {
                textAmount.SetText("");
            }

            x++;
            if(x > rowLength)
            {
                x = 0;
                y++;
            }

        }
    }
}
