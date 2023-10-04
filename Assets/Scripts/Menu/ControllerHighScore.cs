using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerHighScore : MonoBehaviour {

    public int level = 0;

    //public int heightScore;

    public void BackMenu()
    {
        SceneManager.LoadScene(level);
    }
}
