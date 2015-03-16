using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItem : MonoBehaviour {
    static readonly float SPACING = 10f;
    RectTransform rectTransform;

    public delegate void ClickAction ();
    ClickAction onClick;

    public static InventoryItem InstantiateItem (string itemName) {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        InventoryItem itemObject = ((GameObject) Instantiate(itemPrefab))
            .GetComponent<InventoryItem>();
        itemObject.name = itemName;
        itemObject.SetSprite(Resources.Load<Sprite>("Icons/" + itemName + "_icon"));
        return itemObject;
    }

    /** Returns a function that tells the inventory gui that ITEMNAME was
     * clicked. */
    public static ClickAction CreateClickFunc (string itemName) {
        return () => Controller.Get.Inventory.ItemWasClicked(itemName);
    }

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /** Position this inventory item as the INDEXth item (starting from 0). */
    public void PositionAtXIndex (int index) {
        Vector3 offsetMin = rectTransform.offsetMin;
        Vector3 offsetMax = rectTransform.offsetMax;
        offsetMin.y = SPACING;
        offsetMax.y = -SPACING;
        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        
        float height = rectTransform.rect.height;
        rectTransform.SetInsetAndSizeFromParentEdge(
                RectTransform.Edge.Left, height * index + SPACING * (1 + index),
                height);
    }

    /** Sets the function that is called when this item is clicked. */
    public void SetClickAction (ClickAction clickAction) {
        onClick = clickAction;
    }

    public void SetSprite (Sprite spr) {
        GetComponent<Image>().sprite = spr;
    }

    public void OnClickIcon () {
        onClick();
    }
}
