using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeItemPermanent : MonoBehaviour {
    public string[] initialItemNames;
    public bool permanentByDefault;

    Dictionary<string, bool> permanence;

    void Start () {
        permanence = new Dictionary<string, bool>();
        foreach (string item in initialItemNames) {
            permanence[item] = permanentByDefault;
        }
    }

    public void SetPermanence (string itemName, bool isPermanent) {
        permanence[itemName] = isPermanent;
    }

    /** Iterate through the inventory and make stuff permanent that should be. */
    public void ProcessAllItems () {
        foreach (string item in permanence.Keys) {
            InventoryGUI inv = Controller.Get.Inventory;
            if (permanence[item] == true
                    && inv.GetItem(item) != null
                    && !inv.HasPersistentItem(item)) {
                InventoryItem invItem = inv.GetItem(item);
                inv.RemoveItem(invItem);
                inv.AddPersistentItem(invItem);
            }
        }
    }
}
