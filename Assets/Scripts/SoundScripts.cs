using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScripts : MonoBehaviour {

    public AudioClip swords, coins, monsterDeath;

    public AudioSource source;

	// Use this for initialization
	void Start () {
        swords = Resources.Load<AudioClip>("Sword");
        coins = Resources.Load<AudioClip>("Coins");
        monsterDeath = Resources.Load<AudioClip>("Monster Death");
        source = GetComponent<AudioSource>();
        source.volume = 0.6f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AudioPlay(string tagClip)
    {
        switch (tagClip)
        {
            case "Sword":
                source.clip = swords;
                source.PlayOneShot(swords, 0.5f);
                break;
            case "Coins":
                source.clip = coins;
                source.PlayOneShot(coins, 0.6f);
                break;

            case "Monster Death":
                source.clip = monsterDeath;
                source.PlayOneShot(monsterDeath, 1);
                break;
        }
    }
}
