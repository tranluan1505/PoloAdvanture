using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    //public int level = 2;

    public void LoadLevelScenes()
    {       
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HighScore()
    {
        SceneManager.LoadScene(1);
    }

    public void Option()
    {
        SceneManager.LoadScene(2);
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
