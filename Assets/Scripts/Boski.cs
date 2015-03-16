using UnityEngine;
using System.Collections;

public class Boski : NPCWaypoint {
    public Sprite creepy;
    public Sprite angry;
    public Sprite dead;

    public Sprite fire;
    public Sprite openTrapdoor;

    public string happyEnd;
    public string angryEnd;
    
    public int hits;
    /** HP remaining. This goes wayyy below 0. */
    int hpLeft;

    /** This is true when all doors are happy or sad. */
    bool isAwake;
    /** This is true when all doors are happy. */
    bool isHappy;

    bool dropTommy;
    bool successfulExit;

    int endingCounter = 60;

    static readonly Vector3 POW_OFFSET = new Vector3(0f, 1f, 0f);

    // Use this for initialization
    protected override void Start () {
        base.Start();
        hpLeft = hits;
        Controller.Get.Inventory.AddItemListener("firesnakebat", DoHit);
        Controller.Completion allComplete = Controller.BlueRoomResult;
        dropTommy = false;
        successfulExit = false;
        isAwake = false;
        isHappy = false;
        if (allComplete != Controller.Completion.None) {
            isAwake = (Controller.YellowRoomResult == allComplete
                       && Controller.PurpleRoomResult == allComplete);
            isHappy = (isAwake && allComplete == Controller.Completion.Help);
        }

        if (isAwake) {
            if (isHappy) {
                GetComponent<SpriteRenderer>().sprite = creepy;
                dialogueName = "boski_happy";
            } else {
                GetComponent<SpriteRenderer>().sprite = angry;
                dialogueName = "boski_angry";
                Controller.Get.Inventory.RemoveItem("bat");
                Controller.Get.Inventory.RemoveItem("lighter");
                Controller.Get.Inventory.RemoveItem("snake");
                InventoryItem fsb
                        = InventoryItem.InstantiateItem("firesnakebat");
                fsb.SetClickAction(
                        InventoryItem.CreateClickFunc("firesnakebat"));
                Controller.Get.Inventory.AddItem(fsb);
            }
        }
    }

    protected override void Update () {
        if (isAwake && isHappy && Controller.Get.Dialogue.InConversation) {
            dropTommy = true;
        } else if (dropTommy && !Controller.Get.Dialogue.InConversation) {
            OpenTrapdoor();
            endingCounter--;
        } else if (successfulExit && !Controller.Get.Dialogue.InConversation) {
            endingCounter--;
        }
        if (endingCounter == 0) {
            endingCounter = -1;
            if (dropTommy) {
                Controller.Get.Fade.FadeOut(
                        ()=>Application.LoadLevel(happyEnd));
            } else if (successfulExit) {
                Controller.Get.Fade.FadeOut(
                        ()=>Application.LoadLevel(angryEnd));
            }
        }
    }

    void DoHit () {
        if (Controller.Get.Character.AtWaypoint == this && isAwake
                && !isHappy) {
            hpLeft--;
            if (hpLeft >= 0) {
                MakePow();
            }
            
            if (hpLeft < 0) {
                SetConvoAndStart("boski_dead");
            } else if (hpLeft == 0) {
                GetComponent<SpriteRenderer>().sprite = dead;
                SetConvoAndStart("boski_dead");
                successfulExit = true;
            } else if (hpLeft == hits - 1) {
                SetConvoAndStart("boski_angry");
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

        obj = (GameObject) Instantiate(
            Resources.Load<GameObject>("Prefabs/pow"));
        obj.GetComponent<SpriteRenderer>().sprite = fire;
        obj.transform.position = (transform.position
                                  + Controller.Get.Character.transform.position) / 2f
            + POW_OFFSET * 0.8f;
    }

    void OpenTrapdoor () {
        GameObject trapdoor = GameObject.Find("trapdoor");
        trapdoor.GetComponent<SpriteRenderer>().sprite = openTrapdoor;
        Controller.Get.Character.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}

