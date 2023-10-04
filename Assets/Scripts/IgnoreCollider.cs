using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour {

    public Collider2D boss;

    public Collider2D monster;

    // Use this for initialization
    void Start () {
        boss = GameObject.Find("Boss").GetComponent<BoxCollider2D>();
        monster = GameObject.Find("MonsterOfBoss(Clone)").GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), boss, true);
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), monster, true);
    }
}
