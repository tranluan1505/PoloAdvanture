using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWind : MonoBehaviour {

    private Vector2 direction;

    // Use this for initialization
    void Start()
    {     
        Invoke("DestroyIt", 0.2f);
    }

    void DestroyIt()
    {
        Destroy(gameObject);
    }

    public void Initialized(Vector2 direction)
    {
        this.direction = direction;    
    }
}
