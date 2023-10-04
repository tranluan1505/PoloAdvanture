using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour {

    public AudioSource audio;
    public AudioClip clip;

    //public AudioSource audio;

    private static SwitchSound instance = null;
    private static SwitchSound Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        //List<GameObject> list = new List<GameObject>();

        //list.Add(gameObject);
        if (instance != null && instance != this)
        {
            Debug.Log(instance.ToString());
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
    void Start()
    {
        audio = GetComponent<AudioSource>();
        clip = Resources.Load<AudioClip>("click_0");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMusic()
    {
        audio.clip = clip;
        audio.PlayOneShot(clip);
    }
}
