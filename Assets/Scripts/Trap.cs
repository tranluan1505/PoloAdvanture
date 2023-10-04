using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public Canvas trapCanvas;

	// Use this for initialization
	void Start () {
        trapCanvas.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            trapCanvas.enabled = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trapCanvas.enabled = false;
    }
}
