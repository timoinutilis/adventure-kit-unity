using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public GameObject[] buttons;
    public Image draggingItemPreview;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.Instance.onChangeEvent.AddListener(Refresh);
        Inventory.Instance.onDragChangeEvent.AddListener(RefreshDraggingPreview);
        Refresh();
        RefreshDraggingPreview();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(int index)
    {
        var items = Inventory.Instance.items;
        if (index < items.Count)
        {
            var item = items[index];
            var draggingItem = Inventory.Instance.DraggingItem;
            if (draggingItem != null)
            {
                Inventory.Instance.DraggingItem = null;
                if (draggingItem == item)
                {
                    Inventory.Instance.OnItemInteract(item);
                }
                else
                {
                    Inventory.Instance.OnCombine(item, draggingItem);
                }
            }
            else
            {
                Inventory.Instance.DraggingItem = item;
            }
        }
    }

    private void Refresh()
    {
        var items = Inventory.Instance.items;
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
        if (Inventory.Instance.DraggingItem != null)
        {
            draggingItemPreview.sprite = Inventory.Instance.DraggingItem.sprite;
        }
        else
        {
            draggingItemPreview.sprite = null;
        }
    }
}
