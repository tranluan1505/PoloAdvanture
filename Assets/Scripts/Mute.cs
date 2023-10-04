using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour {

    [SerializeField]
    private Image imgChange;

    public Sprite imgTurnOn;

    public Sprite imgTurnOff;

    [SerializeField]
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        imgChange = GetComponent<Image>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (audioManager.audio.mute == true)
        {
            imgChange.sprite = imgTurnOff;
        }
        else
        {
            imgChange.sprite = imgTurnOn;
        }
	}

    public void ChangeOnOff()
    {
        if(imgChange.sprite == imgTurnOn)
        {
            imgChange.sprite = imgTurnOff;
            audioManager.audio.mute = true;
        }
        else
        {
            imgChange.sprite = imgTurnOn;
            audioManager.audio.mute = false;
        }

    }
}
