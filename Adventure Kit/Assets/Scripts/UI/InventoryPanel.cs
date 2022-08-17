using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public Inventory inventory;
    public GameObject[] buttons;
    public Image draggingItemPreview;

    // Start is called before the first frame update
    void Start()
    {
        inventory.onChangeEvent.AddListener(Refresh);
        inventory.onDragChangeEvent.AddListener(RefreshDraggingPreview);
        Refresh();
        RefreshDraggingPreview();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(int index)
    {
        var items = inventory.items;
        if (index < items.Count)
        {
            var item = items[index];
            var draggingItem = inventory.DraggingItem;
            if (draggingItem != null)
            {
                inventory.DraggingItem = null;
                if (draggingItem == item)
                {
                    inventory.OnItemInteract(item);
                }
                else
                {
                    inventory.OnCombine(item, draggingItem);
                }
            }
            else
            {
                inventory.DraggingItem = item;
            }
        }
    }

    private void Refresh()
    {
        var items = inventory.items;
        for (int i = 0; i < buttons.Length; i++)
        {
            GameObject button = buttons[i];
            Image image = button.GetComponent<Image>();
            if (i < items.Count)
            {
                InventoryItem item = items[i];
                image.sprite = item.sprite;
            }
            else
            {
                image.sprite = null;
            }
        }
    }

    private void RefreshDraggingPreview()
    {
        if (inventory.DraggingItem != null)
        {
            draggingItemPreview.sprite = inventory.DraggingItem.sprite;
        }
        else
        {
            draggingItemPreview.sprite = null;
        }
    }
}
