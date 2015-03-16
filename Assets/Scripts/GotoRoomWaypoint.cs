using UnityEngine;
using System.Collections;

public class GotoRoomWaypoint : Waypoint {
    public string sceneName;

    InventoryItem itemObject;

    protected override void Start () {
        base.Start();
        itemObject = null;
    }

    public override void OnDeparture () {
        Controller.Get.Inventory.RemoveItem(itemObject);
        itemObject = null;
    }

    public override void OnArrival () {
        itemObject = InventoryItem.InstantiateItem("door");
        itemObject.SetClickAction(GotoRoomFunction);
        Controller.Get.Inventory.AddItem(itemObject);
    }

    void GotoRoomFunction () {
        MakeItemPermanent permanents = GetComponent<MakeItemPermanent>();
        if (permanents != null) {
            permanents.ProcessAllItems();
        }
        Controller.Get.Fade.FadeOut(
                ()=>Application.LoadLevel(sceneName));
        
    }
}
