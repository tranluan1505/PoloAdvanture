using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion_Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyExplosion", 0.4f);
	}

    void DestroyExplosion()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
