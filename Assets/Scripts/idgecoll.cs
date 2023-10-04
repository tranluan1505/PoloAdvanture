using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idgecoll : MonoBehaviour {
    
    Collider2D player;
    public Collider2D box;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        box = GameObject.Find("Box").GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        //bỏ qua sự va chạm
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player, true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), box, true);
    }
}
