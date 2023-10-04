using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionX : MonoBehaviour {

    // Use this for initialization
    private MoveGround move;
	void Start () {
        move = GameObject.FindGameObjectWithTag("MoveCollider").GetComponent<MoveGround>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {

            //Debug.Log("collider");
            move.speed *= move.changeDirection;
        }
    }
}
