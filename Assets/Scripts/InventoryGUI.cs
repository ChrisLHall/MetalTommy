using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {
    RectTransform inventoryBox;
    Dictionary<string, InventoryItem> inventoryItems;

	// Use this for initialization
	void Start () {
        inventoryBox = transform.FindChild("Box").GetComponent<RectTransform>();
        inventoryItems = new Dictionary<string, InventoryItem>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public InventoryItem GetItem (string name) {
        if (!inventoryItems.ContainsKey(name)) {
            Debug.LogWarning("Inventory has no item called " + name);
            return null;
        }
        return inventoryItems[name];
    }

    public void AddItem (InventoryItem item) {
        if (GetItem(item.name) != null) {
            Debug.LogError("Inventory already contains an object called "
                           + item.name);
            return;
        }
        inventoryItems[item.name] = item;
        item.transform.SetParent(inventoryBox);
    }

    /** Removes an item called NAME from the inventory box and destroys it. */
    public void RemoveItem (string name) {
        if (GetItem(name) == null) {
            Debug.LogWarning("Inventory has no item called " + name);
            return;
        }
        InventoryItem removed = inventoryItems[name];
        removed.transform.SetParent(null);
        removed.gameObject.SetActive(false);
        Destroy(removed);
    }

    /** Removes an ITEM from the inventory box and destroys it. */
    public void RemoveItem (InventoryItem item) {
        RemoveItem(item.name);
    }
}
