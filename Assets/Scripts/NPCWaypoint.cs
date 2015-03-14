using UnityEngine;
using System.Collections;

public class NPCWaypoint : Waypoint {
    public string dialogueName;
    
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
        itemObject = InventoryItem.InstantiateItem("talk");
        itemObject.SetClickAction(TalkFunction);
        Controller.Get.Inventory.AddItem(itemObject);
    }
    
    void TalkFunction () {
        Controller.Get.Dialogue.SetConversation(dialogueName);
    }
}
