using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public enum Completion { None, Help, Hurt };
    public enum DoorColor {Blue, Yellow, Purple, None};
    public static Completion BlueRoomResult = Completion.None;
    public static Completion YellowRoomResult = Completion.None;
    public static Completion PurpleRoomResult = Completion.None;
    public static DoorColor startDoor = DoorColor.Blue;

    void Awake () {
        Application.targetFrameRate = 60;

        Character = FindObjectOfType<Character>();
        Dialogue = FindObjectOfType<DialogueGUI>();
        Fade = FindObjectOfType<FadeGUI>();
        Inventory = FindObjectOfType<InventoryGUI>();
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	    // Sort sprites by depth
        Depth[] depthObjects = FindObjectsOfType<Depth>();
        System.Array.Sort<Depth>(depthObjects, (Depth a, Depth b)
                => b.transform.position.z.CompareTo(a.transform.position.z));

        for (int i = 0; i < depthObjects.Length; i++) {
            depthObjects[i].GetComponent<SpriteRenderer>().sortingOrder = 5 + i;
        }
	}

    /** The level's controller instance. */
    public static Controller Get {
        get {
            return FindObjectOfType<Controller>();
        }
    }

    public Character Character {
        get;
        private set;
    }

    public DialogueGUI Dialogue {
        get;
        private set;
    }

    public FadeGUI Fade {
        get;
        private set;
    }

    public InventoryGUI Inventory {
        get;
        private set;
    }
}
