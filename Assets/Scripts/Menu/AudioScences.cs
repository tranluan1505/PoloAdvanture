using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScences : MonoBehaviour {

    private AudioManager audio;
    public float volume;

    public AudioClip clip;

    private void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
    }

    // Use this for initialization
    void Start () {
        if(clip != null)
        {
            audio.audio.volume = volume;       
            audio.ChangeMusic(clip);
        }
           
	}
	
	// Update is called once per frame
	void Update () {
		
	}   
}
