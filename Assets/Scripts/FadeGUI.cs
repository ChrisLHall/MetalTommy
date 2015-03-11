using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeGUI : MonoBehaviour {
    Image fadeOverlay;
    bool fadeToBlack;
    float alpha;

    static readonly float ALPHA_STEP = 0.05f;

    /** An action to take after fading out. */
    public delegate void FadeOutCallback ();
    FadeOutCallback callback;

	// Use this for initialization
	void Start () {
        fadeOverlay = GetComponentInChildren<Image>();

        alpha = 1f;
        fadeToBlack = false;
        callback = null;

        FadeIn();
	}
	
	// Update is called once per frame
	void Update () {
	    if (fadeToBlack) {
            if (!fadeOverlay.gameObject.activeSelf) {
                alpha = 0f;
                fadeOverlay.gameObject.SetActive(true);
            } else if (alpha < 1f) {
                alpha = Mathf.Clamp01(alpha + ALPHA_STEP);
                Color newColor = fadeOverlay.color;
                newColor.a = alpha;
                fadeOverlay.color = newColor;
                if (alpha == 1f && callback != null) {
                    callback();
                    callback = null;
                }
            }
        } else {
            if (alpha > 0f && fadeOverlay.gameObject.activeSelf) {
                alpha = Mathf.Clamp01(alpha - ALPHA_STEP);
                Color newColor = fadeOverlay.color;
                newColor.a = alpha;
                fadeOverlay.color = newColor;
                if (alpha == 0f) {
                    fadeOverlay.gameObject.SetActive(false);
                }
            }
        }
	}

    public void FadeIn () {
        fadeToBlack = false;
    }

    public void FadeOut (FadeOutCallback fadeCallback = null) {
        fadeToBlack = true;
        if (fadeCallback != null) {
            callback += fadeCallback;
        }
    }
}
