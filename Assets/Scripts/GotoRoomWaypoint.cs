using UnityEngine;
using System.Collections;

public class GotoRoomWaypoint : Waypoint {
    public string sceneName;

    GameObject itemPrefab;
    InventoryItem itemObject;

    protected override void Start () {
        base.Start();
        itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        itemObject = null;
    }

    public override void OnDeparture () {
        FindObjectOfType<InventoryGUI>().RemoveItem(itemObject);
        itemObject = null;
    }

    public override void OnArrival () {
        itemObject = ((GameObject) Instantiate(itemPrefab))
                .GetComponent<InventoryItem>();
        itemObject.SetClickAction(GotoRoomFunction);
        itemObject.SetSprite(Resources.Load<Sprite>("Icons/door_icon"));
        FindObjectOfType<InventoryGUI>().AddItem(itemObject);
    }

    void GotoRoomFunction () {
        FindObjectOfType<FadeGUI>().FadeOut(
                ()=>Application.LoadLevel(sceneName));
        
    }
}
