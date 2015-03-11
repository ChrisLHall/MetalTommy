using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItem : MonoBehaviour {
    static readonly float HIGHLIGHTED_SIZE = 1.2f;
    static readonly float SPACING = 10f;
    RectTransform rectTransform;

    public delegate void ClickAction ();
    ClickAction onClick;

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
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left,
                                                    SPACING, height);
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

    public void Highlight () {
        rectTransform.localScale = new Vector3(HIGHLIGHTED_SIZE,
                                               HIGHLIGHTED_SIZE, 1f);
    }

    public void Unhighlight () {
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }


}
