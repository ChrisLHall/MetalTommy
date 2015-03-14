using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemWaypoint : Waypoint {
    public string itemName;

    InventoryItem itemObject;

    bool givenItem;
    
    protected override void Start () {
        base.Start();
        itemObject = null;

        givenItem = false;

        if (Controller.Get.Inventory.HasPersistentItem(itemName)) {
            gameObject.SetActive(false);
        }
    }
    
    public override void OnDeparture () {
    }
    
    public override void OnArrival () {
        if (givenItem) {
            return;
        }
        givenItem = true;
        itemObject = InventoryItem.InstantiateItem(itemName);
        Controller.Get.Inventory.AddPersistentItem(itemObject);

        gameObject.SetActive(false);
    }
}
