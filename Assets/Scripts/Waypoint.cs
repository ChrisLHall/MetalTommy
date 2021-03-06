﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Waypoint : MonoBehaviour {
    private Character character;
    public enum FaceDir { None, Left, Right };
    public FaceDir forceFace = FaceDir.None;

	// Use this for initialization
	protected virtual void Start () {
        character = FindObjectOfType<Character>();
        EventTrigger.TriggerEvent clickEvent = new EventTrigger.TriggerEvent();
        clickEvent.AddListener((BaseEventData unused)=>Click());

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = clickEvent;
        
        EventTrigger trigger = GetComponent<EventTrigger>();
        trigger.delegates.Add(entry);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

    public virtual void Click () {
        character.TrySetWaypoint(this);
    }

    public virtual void OnArrival () {
        if (forceFace == FaceDir.Right) {
            character.Face(true);
        } else if (forceFace == FaceDir.Left) {
            character.Face(false);
        }
    }

    public virtual void OnDeparture () {

    }

    public Vector3 WaypointPosition {
        get {
            return transform.FindChild("Waypoint").position;
        }
    }
}
