using SimpleJSON;
using UnityEngine;
using System.Collections;

public class DialogueGUI : MonoBehaviour {
    string currentConvo;

	// Use this for initialization
	void Start () {
        currentConvo = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetConversation (string nameOfConversation) {
        if (InConversation) {
            return;
        }

        // TODO load text assets as JSON files
    }

    public bool InConversation {
        get {
            return (currentConvo != null);
        }
    }
}
