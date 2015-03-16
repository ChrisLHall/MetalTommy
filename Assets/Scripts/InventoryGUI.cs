using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {
    RectTransform inventoryBox;
    Dictionary<string, InventoryItem> inventoryItems;
    static List<string> persistentItems = new List<string>();

    /** Given item name STRING, if an InventoryItem called STRING is clicked,
     * call the CLICKACTION(s) associated. */
    Dictionary<string, InventoryItem.ClickAction> clickListeners;

	// Use this for initialization
	void Awake () {
        inventoryBox = transform.FindChild("Box").GetComponent<RectTransform>();
        inventoryItems = new Dictionary<string, InventoryItem>();
        clickListeners = new Dictionary<string, InventoryItem.ClickAction>();
	}

    void Start () {
        foreach (string itemName in persistentItems) {
            InventoryItem item = InventoryItem.InstantiateItem(itemName);
            item.SetClickAction(InventoryItem.CreateClickFunc(itemName));
            AddItem(item);
        }
    }
	
	// Update is called once per frame
	void Update () {
        int counter = 0;
        foreach (InventoryItem item in inventoryItems.Values) {
            item.PositionAtXIndex(counter);
            counter++;
        }
	}

    public InventoryItem GetItem (string name) {
        if (!inventoryItems.ContainsKey(name)) {
            return null;
        }
        return inventoryItems[name];
    }

    public bool HasPersistentItem (string itemName) {
        return persistentItems.Contains(itemName);
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

    /** Adds item PERSISTENTLY (so it will be created on level start from this
     * point until it is removed). OVERRIDES ITS CLICK ACTION. */
    public void AddPersistentItem (InventoryItem item) {
        item.SetClickAction(InventoryItem.CreateClickFunc(item.name));
        AddItem(item);
        if (persistentItems.Contains(item.name)) {
            Debug.LogWarning("Already have a persistent item " + item.name);
        }
        persistentItems.Add(item.name);
    }

    /** Removes an item called NAME from the inventory box and destroys it. */
    public void RemoveItem (string name) {
        if (GetItem(name) == null) {
            Debug.LogWarning("Inventory has no item called " + name);
            return;
        }
        InventoryItem removed = inventoryItems[name];
        inventoryItems.Remove(name);
        removed.transform.SetParent(null);
        removed.gameObject.SetActive(false);
        Destroy(removed);

        if (persistentItems.Contains(name)) {
            persistentItems.Remove(name);
        }
    }

    /** Removes an ITEM from the inventory box and destroys it. */
    public void RemoveItem (InventoryItem item) {
        RemoveItem(item.name);
    }

    /** Execute ACTION whenever ITEMNAME is clicked on. */
    public void AddItemListener (string itemName,
                                 InventoryItem.ClickAction action) {
        if (clickListeners.ContainsKey(itemName)) {
            clickListeners[itemName] += action;
        } else {
            clickListeners[itemName] = action;
        }
    }

    /** Stop executing ACTION whenever ITEMNAME is clicked on. */
    public void RemoveItemListener (string itemName,
                                    InventoryItem.ClickAction action) {
        if (clickListeners.ContainsKey(itemName)) {
            clickListeners[itemName] -= action;
        } else {
            Debug.LogWarning("Could not remove item listener for " + itemName);
        }
    }

    /** Call this once if an item gets clicked. */
    public void ItemWasClicked (string itemName) {
        if (clickListeners.ContainsKey(itemName)) {
            clickListeners[itemName]();
        }
    }

    public void Show () {
        inventoryBox.gameObject.SetActive(true);
    }

    public void Hide () {
        inventoryBox.gameObject.SetActive(false);
    }
}
