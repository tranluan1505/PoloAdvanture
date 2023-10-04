using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

    [SerializeField]
    private Text content;

    [SerializeField]
    private InputField inputName;

    public float timeWait;
    public float timeWaitSecond;
    public float timeWaitThird;

    private string user;

    public bool inputEnable;

	// Use this for initialization
	void Start () {
        timeWait = Random.Range(1f, 2f);
        //inputName = GetComponent<InputField>();
        inputName.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(inputName.enabled);

        if (timeWait > 0)
        {
            timeWait -= Time.deltaTime;
            if(timeWait < 0)
            {
                content.text = "Victory\n\nYour score: " + PlayerPrefs.GetInt("highScore");
                inputName.enabled = true;
                //inputName.OnPointerEnter = new System.Action<UnityEngine.EventSystems.PointerEventData>
                inputName.onEndEdit.AddListener(OnPointerEnter);
                timeWaitSecond = 2f;              
            }
        }

        if (PlayerPrefs.GetString("User").Equals(user))
        {
            inputName.enabled = false;
            inputName.transform.position = new Vector3(inputName.transform.position.x, -278, 0);
            timeWaitSecond += Time.deltaTime;
            if (timeWaitSecond > 4)
            {
                content.transform.position = new Vector3(0, 0, 0);
				content.text = "Nhà sản xuất\n\nTrần Chí Luân";
                if (timeWaitSecond > 8)
                {
                    content.fontSize = 150;
                    content.text = "Thank You";
                    if (timeWaitSecond > 12)
                    {
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }

        //if(timeWaitThird > 0)
        //{
        //    timeWaitThird -= Time.deltaTime;
        //    if (timeWaitThird < 0)
        //    {
        //        content.text = "Thank you";
        //        timeWaitThird = 2f;
        //        timeWaitThird -= Time.deltaTime;
        //        if (timeWaitThird < 0)
        //        {
        //            SceneManager.LoadScene(0);
        //        }
        //    }
        //}       
    }

    private void OnPointerEnter(string evd)
    {
        Debug.Log(evd);
        PlayerPrefs.SetString("User", evd);
        user = evd;
    }
}
