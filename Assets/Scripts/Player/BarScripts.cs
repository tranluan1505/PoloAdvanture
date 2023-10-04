using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScripts : MonoBehaviour {

    //[SerializeField]
    private float fillAmount = 1;

    [SerializeField]
    private float speedLerp;

    [SerializeField]
    private Image content;
    public float MaxValue { get; set; }

    //[SerializeField]
    public Text text;   

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    public float Values
    {
        set
        {
            string[] temp = text.text.Split(':');
            text.text = temp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    // Use this for initialization
    void Start () {
        //currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar();
	}

    void HandleBar()
    {
        //if(fillAmount >= 0 || fillAmount <= 1)
        if(fillAmount != content.fillAmount)
        {
            //dùng để tạo thời gian delta để cho nó chạy từ từ mượt hơn
            //gán giá trị fillAmount cho content.fillAmount
            //content.fillAmount là gia trị để chạy trong content(Image)
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * speedLerp);
        }

        //dùng để chuyển màu giữa 2 màu nhất định
        content.color = Color.Lerp(lowColor, fullColor, fillAmount);
           

        //dùng để giới hạn lại cho fillAmount không vượt quá từ 0 tới 1
        //content.fillAmount = Mathf.Clamp(fillAmount, 0, 1);


    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
