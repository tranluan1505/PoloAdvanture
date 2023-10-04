using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelPause : MonoBehaviour {

    public GameObject PauseUI;
    public Button btnUI;

    [SerializeField]
    private PlayerMoveKeyBoard health;

    public bool pause = false;

    [SerializeField]
    private GameMaster gameMaster;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {
        //tắt kích hoạt Panel Pause
        PauseUI.SetActive(false);
        gameMaster = FindObjectOfType<GameMaster>().GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {

        //khi nhấn vào button ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        if(pause)
        {
            gameMaster.textInput.enabled = false;
              //Kích hoạt Panel Pause
            PauseUI.SetActive(true);

            //khi kích hoạt Panel Pause.. ta sẽ dừng thời gian hiện tại lại
            // là dừng màn hình lại
            Time.timeScale = 0f;
        }

        if (!pause)
        {
            gameMaster.textInput.enabled = true;
            //ngược lại thì ta sẽ dừng Panel Pause lại

            //Và cho thời gian chạy lại bình thường
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        pause = !pause;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //btnUI.interactable = true;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
