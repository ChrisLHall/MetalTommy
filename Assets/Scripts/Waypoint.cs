using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {
    private Character character;

	// Use this for initialization
	void Start () {
        character = FindObjectOfType<Character>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown () {
        if (character.currentWaypoint != gameObject
                && character.nextWaypoint != gameObject) {
            character.nextWaypoint = gameObject;
        }
    }
}
