using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Hover : MonoBehaviour {
    public float hoverSizeMult = 1.2f;

	// Use this for initialization
	void Start () {
        EventTrigger.TriggerEvent hoverEvent = new EventTrigger.TriggerEvent();
        hoverEvent.AddListener((BaseEventData unused)=>StartHover());
        EventTrigger.TriggerEvent unHoverEvent = new EventTrigger.TriggerEvent();
        unHoverEvent.AddListener((BaseEventData unused)=>UnHover());
        
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback = hoverEvent;
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback = unHoverEvent;
        
        EventTrigger trigger = GetComponent<EventTrigger>();
        trigger.delegates.Add(entry);
        trigger.delegates.Add(entry2);
	}

    void StartHover () {
        transform.localScale = new Vector3(hoverSizeMult,
                                               hoverSizeMult, 1f);
    }

    void UnHover () {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
