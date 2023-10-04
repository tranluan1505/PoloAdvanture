using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    private int dmg = 25;

    [SerializeField]
    private float speed;

    private Rigidbody2D myBody;

    private Vector2 direction;

    private bool lookRight;

    private AwakeMonster Monster;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        //direction = Vector2.right;
        //Boost = GetComponent<DestroyExplosion_Fire>();
        Monster = GetComponent<AwakeMonster>();
    }

    // Use this for initialization
    void Start()
    {
        //lookRight = Monster.lookRight;
    }

    public void Initialized(Vector2 direction)
    {
        this.direction = direction;
    }

    private void FixedUpdate()
    {
        Invoke("DestroyWeapon", 1.5f);
        myBody.velocity = direction * speed;
    }

    void DestroyWeapon()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
            collision.SendMessageUpwards("DamageWeaPon_ORC", dmg);
        }
    }
}
