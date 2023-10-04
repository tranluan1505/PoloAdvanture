using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonserLV1 : MonoBehaviour {

    //Máu của Monster
    public int health = 100;

    public float forceX = 2f;

    [SerializeField]
    private PlayerMoveKeyBoard player;

    private bool collision;

    [SerializeField]
    private float damage;

    public SoundScripts source;

    [SerializeField]
    private CircleCollider2D circleCollider;


    [SerializeField]
    private Transform startPos, endPos;

    private Rigidbody2D myBody;
    private Animator anim;

    public bool IsLookRight;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveKeyBoard>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //player = GameObject.FindWithTag("Player");
        source = GameObject.FindGameObjectWithTag("ManagerSound").GetComponent<SoundScripts>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        IsLookRight = true;
        StartCoroutine(autoAttack());
    }

    void Update()
    {
        if (myBody.velocity.x > 0 && transform.localScale.x == 1)
        {
            IsLookRight = true;
        }

        if(myBody.velocity.x < 0 && transform.localScale.x == -1)
        {
            IsLookRight = false;
        }
        if (health <= 0)
        {
            source.AudioPlay("Monster Death");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Move();
        ChangeDirection();
    }

    void ChangeDirection()
    {
        collision = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Grounds"));

        if (!collision)
        {           
            Vector3 temp = transform.localScale;
            if (temp.x == 1f)
            {
                temp.x = -1f;               
            }
            else if (temp.x == -1f)
            {
                temp.x = 1f;
            }

            transform.localScale = temp;

        }
        Debug.DrawLine(startPos.position, endPos.position, Color.cyan);
    }

    IEnumerator autoAttack()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));

        anim.SetTrigger("Attack");

        StartCoroutine(autoAttack());
    }

    void Move()
    {
        myBody.velocity = new Vector2(transform.localScale.x, 0) * forceX;
        anim.SetBool("Walk", true);       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            player.Damage(damage);
            player.KnockBack(1000f);
        }

        //if(collision.gameObject.tag.Equals(""))
    }    

    void DamageAttack(int damage)
    {
        health -= damage;
        Push();
    }

    void DamageFire(int damageFire)
    {
        health -= damageFire;
        Push();
    }

    void Push()
    {
        if(player.lookRight)
        {
            myBody.AddForceAtPosition(new Vector2(1500f, myBody.velocity.y), new Vector2(2000f, myBody.velocity.y) * Time.deltaTime);
        }
        else
        {
            myBody.AddForceAtPosition(new Vector2(-1500f, myBody.velocity.y), new Vector2(-2000f, myBody.velocity.y) * Time.deltaTime);
        }
           
    }
}
