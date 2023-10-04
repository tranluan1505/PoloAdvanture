using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWin : MonoBehaviour {
    public AudioClip clip;
    public AudioSource source;
	// Use this for initialization
	void Start () {
        clip = Resources.Load<AudioClip>("Win");
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void audioWin()
    {
        if (clip != null)
        {
            source.clip = clip;
            source.Play();
        }
    }
}
