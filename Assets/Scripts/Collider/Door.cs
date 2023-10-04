using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    public int level = 4;

    [SerializeField]
    private GameMaster gameMaster;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            anim.SetBool("Open", true);
            gameMaster.textInput.text = ("Press enter E");
            SaveScore();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {              
                SaveScore();
                SceneManager.LoadScene(level);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            anim.SetBool("Open", false);
            gameMaster.textInput.text = ("");
        }
           
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("points", gameMaster.points);
    }
}
