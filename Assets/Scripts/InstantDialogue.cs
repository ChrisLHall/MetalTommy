using UnityEngine;
using System.Collections;

public class InstantDialogue : MonoBehaviour {
    public string convoName;
    public string sceneWhenDone;
    public bool quitWhenDone = false;

    bool exited;

	// Use this for initialization
	void Start () {
        Controller.Get.Dialogue.SetConversation(convoName);
        exited = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (!exited && !Controller.Get.Dialogue.InConversation) {
            if (!quitWhenDone) {
                GotoRoomFunction();
            }
            // Do nothing instead of quitting
            exited = true;
        }
        if (exited && Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
	}

    void GotoRoomFunction () {
        Controller.Get.Fade.FadeOut(
            ()=>Application.LoadLevel(sceneWhenDone));
    }
}
