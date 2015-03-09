using UnityEngine;
using System.Collections;

public class Depth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = transform.position;
        newPos.z = newPos.y;
        transform.position = newPos;
	}
}
