using UnityEngine;
using System.Collections;

public class Byrd : MonoBehaviour {
    NPCWaypoint npc;
    public Sprite dead;
    
    static readonly Vector3 POW_OFFSET = new Vector3(0f, 1f, 0f);
    
    public int flameTime;
    int flameCountdown;
    bool onFire;
    GameObject flame;
    
    // Use this for initialization
    void Start () {
        npc = GetComponent<NPCWaypoint>();
        flameCountdown = flameTime;
        Controller.Get.Inventory.AddItemListener("lighter", DoLighter);
        Controller.Get.Inventory.AddItemListener("flashlight", DoFlashlight);
        Controller.YellowRoomResult = Controller.Completion.None;
        Controller.startDoor = Controller.DoorColor.Yellow;
        
        // Clear the bat
        if (Controller.Get.Inventory.HasPersistentItem("lighter")) {
            Controller.Get.Inventory.RemoveItem("lighter");
        }
    }
    
    // Update is called once per frame
    void Update () {
        if (onFire && !Controller.Get.Dialogue.InConversation) {
            flameCountdown--;
            if (flameCountdown == 0) {
                Destroy(flame);
                flame = null;
            }
        }
    }
    
    void DoFlashlight () {
        // The flashlight only works if I have already talked
        if (Controller.Get.Character.AtWaypoint == npc
                && npc.dialogueName == "byrd_intro_old") {
            npc.SetConvoAndStart("byrd_nice");
            Controller.Get.Inventory.RemoveItem("flashlight");
            Controller.YellowRoomResult = Controller.Completion.Help;
            GameObject.Find("Darkness").GetComponent<SpriteRenderer>().color
                    = Color.clear;
        } else {
            Controller.Get.Dialogue.SetConversation("flashlight");
        }
    }
    
    void DoLighter () {
        if (Controller.Get.Character.AtWaypoint == npc
                && npc.dialogueName == "byrd_intro_old") {
            onFire = true;
            MakeFire();
            npc.SetConvoAndStart("byrd_hurt");
            Controller.YellowRoomResult = Controller.Completion.Hurt;
            GetComponent<SpriteRenderer>().sprite = dead;
            FindObjectOfType<MakeItemPermanent>().SetPermanence("lighter", true);
            npc.dialogueName = "byrd_dead";
        } else {
            Controller.Get.Dialogue.SetConversation("lighter");
        }
    }
    
    void MakeFire () {
        flame = (GameObject) Instantiate(
                Resources.Load<GameObject>("Prefabs/Fire"));
        flame.transform.position
                = (transform.position
                + Controller.Get.Character.transform.position) / 2f
                + POW_OFFSET;
    }
}
