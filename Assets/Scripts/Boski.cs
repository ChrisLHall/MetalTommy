using UnityEngine;
using System.Collections;

public class Boski : NPCWaypoint {
    public Sprite dying;
    public Sprite dead;
    
    static readonly Vector3 POW_OFFSET = new Vector3(0f, 1f, 0f);
    
    InventoryItem boxItem;
    
    int snakesRemaining;
    
    bool setToDead;
    
    // Use this for initialization
    protected override void Start () {
        base.Start();
        snakesRemaining = 4;
        setToDead = false;
        boxItem = null;
        Controller.Get.Inventory.AddItemListener("box", DoBox);
        Controller.Get.Inventory.AddItemListener("snake", DoSnake);
        Controller.PurpleRoomResult = Controller.Completion.None;
        Controller.startDoor = Controller.DoorColor.Purple;
        
        if (Controller.Get.Inventory.HasPersistentItem("snake")) {
            Controller.Get.Inventory.RemoveItem("snake");
        }
    }
    
    protected override void Update () {
        base.Update();
        if (setToDead && !Controller.Get.Dialogue.InConversation) {
            GetComponent<SpriteRenderer>().sprite = dead;
        }
    }
    
    public override void OnArrival () {
        base.OnArrival();
        boxItem = InventoryItem.InstantiateItem("box");
        boxItem.SetClickAction(InventoryItem.CreateClickFunc("box"));
        Controller.Get.Inventory.AddItem(boxItem);
    }
    
    public override void OnDeparture () {
        base.OnDeparture();
        Controller.Get.Inventory.RemoveItem(boxItem);
        boxItem = null;
    }
    
    void DoSnake () {
        // The flashlight only works if I have already talked
        if (Controller.Get.Character.AtWaypoint == this
            && dialogueName == "munkey_intro_old") {
            Controller.Get.Inventory.RemoveItem("snake");
            snakesRemaining--;
            if (snakesRemaining == 0) {
                Controller.PurpleRoomResult = Controller.Completion.Help;
                SetConvoAndStart("munkey_nice");
            } else {
                SetConvoAndStart("munkey_thanks");
                dialogueName = "munkey_intro_old";
            }
        } else {
            Controller.Get.Dialogue.SetConversation("snake");
        }
    }
    
    void DoBox () {
        if (Controller.Get.Character.AtWaypoint == this
            && dialogueName == "munkey_intro_old") {
            setToDead = true;
            SetConvoAndStart("munkey_hurt");
            Controller.PurpleRoomResult = Controller.Completion.Hurt;
            GetComponent<SpriteRenderer>().sprite = dying;
            FindObjectOfType<MakeItemPermanent>().SetPermanence("snake", true);
            dialogueName = "munkey_dead";
        } else if (dialogueName == "munkey_dead") {
            Controller.Get.Dialogue.SetConversation("munkey_dead");
        } else {
            Controller.Get.Dialogue.SetConversation("box");
        }
    }
}

