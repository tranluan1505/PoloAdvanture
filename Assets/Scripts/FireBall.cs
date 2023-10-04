using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBall : MonoBehaviour {

    [SerializeField]
    private int dmg = 25;

    [SerializeField]
    private float speed;

    private Rigidbody2D myBody;

    private Vector2 direction;

    [SerializeField]
    private GameObject Boost;

    private bool lookRight;

    private PlayerMoveKeyBoard player;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        //direction = Vector2.right;
        //Boost = GetComponent<DestroyExplosion_Fire>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveKeyBoard>();
    }

    // Use this for initialization
    void Start () {
        lookRight = player.lookRight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialized(Vector2 direction)
    {
        this.direction = direction;
    }

    private void FixedUpdate()
    {
        myBody.velocity = direction * speed;
    }

    //khi fireBall ra khỏi màn hình chơi thì destroy fireBall
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            Destroy(gameObject);
            if (lookRight)
            {
                Boost = Instantiate(Resources.Load("Effect/explosion"),
                     new Vector2(transform.position.x + 0.4f, transform.position.y),
                    Quaternion.Euler(new Vector3(0, 0, 0))) 
                    as GameObject;
            }
            else
            {
                //Debug.Log("Left");
                Boost = Instantiate(Resources.Load("Effect/explosion"),
                     new Vector2(transform.position.x - 0.4f, transform.position.y), 
                    Quaternion.Euler(new Vector3(0, 0, 0))) 
                    as GameObject;
            }
        }

        if(collision.gameObject.tag.Equals("Monster"))
        {
            //Debug.Log("Fire");
            collision.SendMessageUpwards("DamageFire", dmg);
        }
    }


}
