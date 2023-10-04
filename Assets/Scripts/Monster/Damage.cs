using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    [SerializeField]
    private PlayerMoveKeyBoard player;

    public int damage;

    //public float damage = 999;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player.Damage(damage);
            player.KnockBack(1000f);
        }
    }
}
