using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {
    private Character character;

	// Use this for initialization
	protected virtual void Start () {
        character = FindObjectOfType<Character>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

    protected virtual void OnMouseDown () {
        character.TrySetWaypoint(this);
    }

    public virtual void OnArrival () {
    }

    public virtual void OnDeparture () {

    }

    public Vector3 WaypointPosition {
        get {
            return transform.FindChild("Waypoint").position;
        }
    }
}
