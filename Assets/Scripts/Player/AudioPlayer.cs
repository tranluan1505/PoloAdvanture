using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public AudioSource changeAudio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeMusic(AudioClip clip)
    {
        changeAudio.Stop();
        changeAudio.clip = clip;
        changeAudio.Play();
    }
}
