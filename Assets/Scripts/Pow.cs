using UnityEngine;
using System.Collections;

public class Pow : MonoBehaviour {
    public int framesAlive;

	// Use this for initialization
	void Start () {
        transform.Rotate(new Vector3(0f, 0f, Random.value * 360f));
	}
	
	// Update is called once per frame
	void Update () {
        framesAlive--;
        if (framesAlive <= 0) {
            Destroy(gameObject);
        }
	}
}
