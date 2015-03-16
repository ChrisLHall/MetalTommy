using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    Waypoint lastWaypoint;
    public Waypoint currentWaypoint;
    Waypoint nextWaypoint;

    public float speedUnitsPerSec;

    Animator animator;

	// Use this for initialization
	void Start () {
        if (Application.loadedLevelName == "mainroom") {
            if (Controller.startDoor == Controller.DoorColor.Blue) {
                currentWaypoint
                        = GameObject.Find("blue_door").GetComponent<Waypoint>();
            } else if (Controller.startDoor == Controller.DoorColor.Yellow) {
                currentWaypoint
                        = GameObject.Find("yellow_door").GetComponent<Waypoint>();
            } else if (Controller.startDoor == Controller.DoorColor.Purple) {
                currentWaypoint
                        = GameObject.Find("purple_door").GetComponent<Waypoint>();
            }
        }
        lastWaypoint = null;
        currentWaypoint.OnArrival();
        transform.position = CurrentWaypointPosition;
        nextWaypoint = null;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (nextWaypoint != null) {
            float actualSpeed = speedUnitsPerSec / 60f;
            Vector3 diff = GoToPosition - transform.position;
            if (diff.x > 0) {
                Face(true);
            } else {
                Face(false);
            }
            diff.z = 0f;
            if (diff.magnitude <= actualSpeed) {
                transform.position = GoToPosition;
                currentWaypoint = nextWaypoint;
                nextWaypoint = null;
                currentWaypoint.OnArrival();
                animator.SetBool("walk", false);
            } else {
                transform.position += diff.normalized * actualSpeed;
            }
        }
	}

    public void TrySetWaypoint (Waypoint waypoint) {
        if (currentWaypoint != waypoint
            && nextWaypoint != waypoint) {
            nextWaypoint = waypoint;
            if (currentWaypoint != null) {
                currentWaypoint.OnDeparture();
                lastWaypoint = currentWaypoint;
            }
            currentWaypoint = null;
            animator.SetBool("walk", true);
        }
    }

    /** Face right if RIGHTORLEFT is true, left otherwise. */
    public void Face (bool rightOrLeft) {
        animator.SetBool("is_right", rightOrLeft);
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
                return nextWaypoint.WaypointPosition;
            }
        }
    }

    Vector3 CurrentWaypointPosition {
        get {
            if (currentWaypoint == null) {
                return transform.position;
            } else {
                return currentWaypoint.WaypointPosition;
            }
        }
    }
}
