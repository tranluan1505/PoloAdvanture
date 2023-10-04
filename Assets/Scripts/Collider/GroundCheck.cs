using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public PlayerMoveKeyBoard player;
    private Animator anim;

    public float fore = 1.3f;

    [SerializeField]
    private MoveGround moveGround;

    [SerializeField]
    private MoveUpGround moveUp;
    [SerializeField]
    private Vector3 move;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveKeyBoard>();
        anim = GetComponent<Animator>();
        moveGround = GameObject.FindGameObjectWithTag("MoveCollider").GetComponent<MoveGround>();
        moveUp = GameObject.FindGameObjectWithTag("MoveUp").GetComponent<MoveUpGround>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MoveCollider"))
        {
            player.isGrounded = true;
            move = player.transform.position;
            move.x += moveGround.speed * fore;
            player.transform.position = move;
          
        }

        if (collision.gameObject.tag.Equals("MoveUp"))
        {
            player.isGrounded = true;
            move = player.transform.position;
            move.y += moveUp.moveUp - 0.08f;
            player.transform.position = move;

        }
        //else player.isGrounded = false;
    }
}
