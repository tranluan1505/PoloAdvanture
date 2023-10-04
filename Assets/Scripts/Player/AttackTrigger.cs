using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int dmg = 20;

    private AttackPlayer player;

    private PlayerMoveKeyBoard isGround;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackPlayer>();
        isGround = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveKeyBoard>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Monster"))
        {
            //Debug.Log("Va Cham");
            collision.SendMessageUpwards("DamageAttack", dmg);
        }

        //if (!collision.isTrigger && collision.CompareTag("Monster")) {
        //    Debug.Log("Not Trigger");
        //}
    }   
}
