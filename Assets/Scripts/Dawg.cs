using UnityEngine;
using System.Collections;

public class Dawg : MonoBehaviour {
    NPCWaypoint npc;
    public Sprite dead;

    static readonly Vector3 POW_OFFSET = new Vector3(0f, 1f, 0f);

    public int hits;
    /** HP remaining. This goes wayyy below 0. */
    int hpLeft;

	// Use this for initialization
	void Start () {
        npc = GetComponent<NPCWaypoint>();
        hpLeft = hits;
        Controller.Get.Inventory.AddItemListener("wallet", DoWallet);
        Controller.Get.Inventory.AddItemListener("bat", DoBat);
        Controller.BlueRoomResult = Controller.Completion.None;
        Controller.startDoor = Controller.DoorColor.Blue;

        // Clear the bat
        if (Controller.Get.Inventory.HasPersistentItem("bat")) {
            Controller.Get.Inventory.RemoveItem("bat");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DoWallet () {
        // The wallet only works if I have already talked to the dog
        if (Controller.Get.Character.AtWaypoint == npc
                && npc.dialogueName == "dawg_intro_old") {
            npc.SetConvoAndStart("dawg_nice");
            Controller.Get.Inventory.RemoveItem("wallet");
            Controller.BlueRoomResult = Controller.Completion.Help;
        } else {
            Controller.Get.Dialogue.SetConversation("wallet");
        }
    }

    void DoBat () {
        if (Controller.Get.Character.AtWaypoint == npc) {
            hpLeft--;
            if (hpLeft >= 0) {
                MakePow();
            }

            if (hpLeft < 0) {
                npc.SetConvoAndStart("dawg_dead");
            } else if (hpLeft == 0) {
                Controller.BlueRoomResult = Controller.Completion.Hurt;
                GetComponent<SpriteRenderer>().sprite = dead;
                FindObjectOfType<MakeItemPermanent>().SetPermanence("bat", true);
                npc.SetConvoAndStart("dawg_die");
                npc.dialogueName = "dawg_dead";
            }else if (hpLeft == Mathf.RoundToInt(hits / 2f)) {
                npc.SetConvoAndStart("dawg_hurt2");
            } else if (hpLeft == hits - 1) {
                npc.SetConvoAndStart("dawg_hurt1");
            }
        } else {
            Controller.Get.Dialogue.SetConversation("bat");
        }
    }

    void MakePow () {
        
        GameObject obj = (GameObject) Instantiate(
            Resources.Load<GameObject>("Prefabs/pow"));
        obj.transform.position = (transform.position
                                  + Controller.Get.Character.transform.position) / 2f
            + POW_OFFSET;
    }
}
