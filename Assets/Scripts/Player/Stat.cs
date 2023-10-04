using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    //[SerializeField]
    //thanh nội dung máu (là giá trị)
    public BarScripts bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;


    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            //gán giá trị máu hiện tại trong khoảng 0 đến giá trị MAX
            //nó sẽ tự động thay đổi khí máu hiện tại giảm hoặc tăng
            this.currentVal = Mathf.Clamp(value, 0, maxVal);
            bar.Values = currentVal;
            //Debug.Log(this.currentVal = Mathf.Clamp(value, 0, maxVal));
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
