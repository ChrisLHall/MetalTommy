using UnityEngine;
using System.Collections;

public class DoorFace : MonoBehaviour {
    /** The quickest and dirtiest of quick and dirty solutions to problems. */
    public enum DoorColor { Blue, Yellow, Purple };
    public DoorColor myColor;
    public Sprite normal;
    public Sprite happy;
    public Sprite sad;

    SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Controller.Completion state;
        switch (myColor) {
        case DoorColor.Blue:
            state = Controller.BlueRoomResult;
            break;
        case DoorColor.Yellow:
            state = Controller.YellowRoomResult;
            break;
        case DoorColor.Purple:
            state = Controller.PurpleRoomResult;
            break;
        default:
            state = Controller.Completion.None;
            break;
        }
	    
        switch (state) {
        case Controller.Completion.None:
            renderer.sprite = normal;
            break;
        case Controller.Completion.Help:
            renderer.sprite = happy;
            break;
        case Controller.Completion.Hurt:
            renderer.sprite = sad;
            break;
        default:
            renderer.sprite = normal;
            break;
        }
	}
}
