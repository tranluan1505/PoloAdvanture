using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListUser : MonoBehaviour {

    public Text txtName;

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.SetString("User", null);
        //PlayerPrefs.SetInt("highScore", 0);
        txtName.text = ("Name: " + PlayerPrefs.GetString("User"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
