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
	
	}

    public void SetConversation (string nameOfConversation) {
        if (InConversation) {
            return;
        }

        // TODO load text assets as JSON files
		TextAsset asset = Resources.Load<TextAsset>("Conversations/"
                + nameOfConversation);
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
        
        // This is bad programming but we don't need much generality so it's okay
        if (partCharacter == "tommy") {
            characterImage.sprite = tommySprite;
        } else if (partCharacter == "dog") {
            characterImage.sprite = dogSprite;
        } else if (partCharacter == "bird") {
            characterImage.sprite = birdSprite;
        } else if (partCharacter == "monkey") {
            characterImage.sprite = monkeySprite;
        }
        dialogueText.text = partText + " (click)";
    }

    void Show () {
        characterImage.gameObject.SetActive(true);
        dialogueText.transform.parent.gameObject.SetActive(true);
    }

    void Hide () {
        characterImage.gameObject.SetActive(false);
        dialogueText.transform.parent.gameObject.SetActive(false);
    }

    public bool InConversation {
        get {
            return (currentConvo != null);
        }
    }
}
