using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public Waypoint currentWaypoint;
    [HideInInspector]
    public Waypoint nextWaypoint;
    public float speedUnitsPerSec;

    static readonly Vector3 WAYPOINT_OFFSET = new Vector3(0f, -1f, 0f);

	// Use this for initialization
	void Start () {
        transform.position = CurrentWaypointPosition;
        nextWaypoint = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (nextWaypoint != null) {
            float actualSpeed = speedUnitsPerSec / 60f;
            Vector3 diff = GoToPosition - transform.position;
            diff.z = 0f;
            if (diff.magnitude <= actualSpeed) {
                transform.position = GoToPosition;
                currentWaypoint = nextWaypoint;
                nextWaypoint = null;
            } else {
                transform.position += diff.normalized * actualSpeed;
            }
        }
	}

    public void TrySetWaypoint (Waypoint waypoint) {
        if (currentWaypoint != waypoint
            && nextWaypoint != waypoint) {
            nextWaypoint = waypoint;
        }
    }

    public Waypoint AtWaypoint {
        get {
            if (nextWaypoint != null || currentWaypoint == null) {
                return null;
            }
            return currentWaypoint;
        }
    }

    Vector3 GoToPosition {
        get {
            if (nextWaypoint == null) {
                return transform.position;
            } else {
                return nextWaypoint.transform.position + WAYPOINT_OFFSET;
            }
        }
    }

    Vector3 CurrentWaypointPosition {
        get {
            if (currentWaypoint == null) {
                return transform.position;
            } else {
                return currentWaypoint.transform.position + WAYPOINT_OFFSET;
            }
        }
    }
}
