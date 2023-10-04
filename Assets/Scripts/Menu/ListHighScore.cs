using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListHighScore : MonoBehaviour {
    public int hightScore;
    public Text txtScore;

    // Use this for initialization
    void Start () {
		txtScore.text = ("Score: " + PlayerPrefs.GetInt("highScore"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
