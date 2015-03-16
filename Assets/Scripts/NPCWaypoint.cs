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
        itemObject.SetClickAction(StartTalking);
        Controller.Get.Inventory.AddItem(itemObject);
    }

    public void SetConvoAndStart (string convo) {
        dialogueName = convo;
        StartTalking();
    }
    
    void StartTalking () {
        Controller.Get.Dialogue.SetConversation(dialogueName);
        // Automatically switch to the "old" version
        if (!dialogueName.EndsWith("_old")) {
            string oldVersion = dialogueName + "_old";
            if (Controller.Get.Dialogue.ConversationExists(oldVersion)) {
                dialogueName = oldVersion;
            }
        }
    }
}
