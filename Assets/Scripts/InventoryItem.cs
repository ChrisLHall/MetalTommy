using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItem : MonoBehaviour {
    public Sprite sprite;
    static readonly float HIGHLIGHTED_SIZE = 1.2f;
    static readonly float SPACING = 15f;
    RectTransform rectTransform;

    public delegate void ClickAction ();
    ClickAction onClick;

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
        GetComponent<Image>().sprite = sprite;

        PositionAtXIndex(0);
        SetClickAction(()=>Debug.Log("hej"));
	}
	
	// Update is called once per frame
	void Update () {

	}

    /** Position this inventory item as the INDEXth item (starting from 0). */
    public void PositionAtXIndex (int index) {
        Vector3 offset = rectTransform.offsetMin;
        Vector3 offsetMax = rectTransform.offsetMax;
        float height = rectTransform.rect.height;
        offset.x = SPACING * ((float)index + 1f)
                + height * (float)index;
        offsetMax.x = offset.x + height;
        rectTransform.offsetMin = offset;
        rectTransform.offsetMax = offsetMax;
    }

    /** Sets the function that is called when this item is clicked. */
    public void SetClickAction (ClickAction clickAction) {
        onClick = clickAction;
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
