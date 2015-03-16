using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueGUI : MonoBehaviour {
    public Sprite tommySprite;
    public Sprite dogSprite;
    public Sprite birdSprite;
    public Sprite monkeySprite;

    Image characterImage;
    Text dialogueText;

    JSONArray currentConvo;

    int currentPart;
    string partText;
    string partCharacter;

	// Use this for initialization
	void Start () {
        characterImage = transform.FindChild("Speaker").GetComponent<Image>();
        dialogueText = transform.FindChild("Box")
                .GetComponentInChildren<Text>();
        currentConvo = null;
        currentPart = 0;
        partCharacter = null;
        partText = null;

        SetCharAndText();
	}
	
	// Update is called once per frame
	void Update () {
        // Hit enter to advance
	    if (Input.GetKeyDown(KeyCode.Return)) {
            if (InConversation) {
                NextPart();
            }
        }
	}

    public bool ConversationExists (string nameOfConvo) {
        TextAsset asset = Resources.Load<TextAsset>("Conversations/"
                                                    + nameOfConvo);
        return asset != null;
    }

    public void SetConversation (string nameOfConvo) {
        if (InConversation) {
            return;
        }

		TextAsset asset = Resources.Load<TextAsset>("Conversations/"
                                                    + nameOfConvo);
        currentConvo = JSON.Parse(asset.text).AsArray;
        currentPart = 0;
        SetCharAndText();
    }

    public void NextPart () {
        if (!InConversation) {
            return;
        }
        currentPart++;
        if (currentPart >= currentConvo.Count) {
            currentPart = 0;
            currentConvo = null;
        }
        SetCharAndText();
    }

    /** Sets the character and text based on the current page of conversation. */
    void SetCharAndText () {
        if (currentConvo == null) {
            characterImage.sprite = null;
            dialogueText.text = "";
            Hide();
            return;
        }
        Show();
        partCharacter = currentConvo[currentPart]["char"];
        partText = currentConvo[currentPart]["text"];

        if (partCharacter == null) {
            characterImage.color = Color.clear;
        } else {
            characterImage.color = Color.white;
            characterImage.sprite
                    = Resources.Load<Sprite>("Conversations/" + partCharacter);
        }
        dialogueText.text = partText;
    }

    void Show () {
        characterImage.gameObject.SetActive(true);
        dialogueText.transform.parent.gameObject.SetActive(true);
        Controller.Get.Inventory.Hide();
    }

    void Hide () {
        characterImage.gameObject.SetActive(false);
        dialogueText.transform.parent.gameObject.SetActive(false);
        Controller.Get.Inventory.Show();
    }

    public bool InConversation {
        get {
            return (currentConvo != null);
        }
    }
}
