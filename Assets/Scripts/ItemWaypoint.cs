using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemWaypoint : Waypoint {
    public string itemName;
    public bool persistentItem = true;

    InventoryItem itemObject;

    bool givenItem;
    
    protected override void Start () {
        base.Start();
        itemObject = null;

        givenItem = false;

        if (persistentItem
                && Controller.Get.Inventory.HasPersistentItem(itemName)) {
            gameObject.SetActive(false);
        }
    }
    
    public override void OnDeparture () {
    }
    
    public override void OnArrival () {
        if (givenItem || Controller.Get.Inventory.GetItem(itemName) != null) {
            return;
        }
        givenItem = true;
        itemObject = InventoryItem.InstantiateItem(itemName);
        if (persistentItem) {
            Controller.Get.Inventory.AddPersistentItem(itemObject);
        } else {
            Controller.Get.Inventory.AddItem(itemObject);
            itemObject.SetClickAction(InventoryItem.CreateClickFunc(itemName));
        }

        gameObject.SetActive(false);
    }
}
