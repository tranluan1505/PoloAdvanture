using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public AudioSource audio;

    //public AudioSource audio;

    private static AudioManager instance = null;
    private static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }                  

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {               
        //DontDestroyOnLoad(gameObject);       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeMusic(AudioClip clip)
    {
        audio.Stop();
        audio.clip = clip;
        audio.Play();
    }
}
