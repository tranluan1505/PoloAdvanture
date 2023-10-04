using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeMonster : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform bulletPos;

    [SerializeField]
    private Transform target;

    private MonserLV1 Monster;

    [SerializeField]
    private bool IsRight;

	// Use this for initialization
	void Start () {
        Monster = FindObjectOfType<MonserLV1>();

    }
	
	// Update is called once per frame
	void Update () {
        //RangeCheck();
        //if (target.transform.position.x > transform.position.x)
        //{
        //    lookRight = true;
        //}

        //if (target.transform.position.x < transform.position.x)
        //{
        //    lookRight = false;
        //}
    }

    //void RangeCheck()
    //{
    //    if (distence > wakeRange)
    //    {
    //        awake = true;
    //    }

    //    if (distence < wakeRange)
    //    {
    //        awake = false;
    //    }
    //}

    //public void Attack(bool attackRight)
    //{
    //    bulletTimer += Time.deltaTime;

    //    if (bulletTimer >= shootInterval)
    //    {
    //        Vector2 direction = target.transform.position - transform.position;
    //        direction.Normalize();

    //        if (attackRight)
    //        {
    //            //GameObject bulletClone;
    //            //bulletClone = Instantiate(bullet, 
    //            //    shootPointRight.transform.position, 
    //            //    Quaternion.Euler(new Vector3(0, 0, -90))) 
    //            //    as GameObject;

    //            bulletClone = Instantiate(
    //                Resources.Load("Effect/ORC_1"),
    //                shootPointRight.transform.position,
    //                Quaternion.Euler(new Vector3(0, 0, -90)))
    //                as GameObject;
    //            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    //        }

    //        bulletTimer = 0;
    //    }
    //}

    private void FixedUpdate()
    {
        
    }

    public void HandleWeapon()
    {
        //khi Player nhìn về bên phải thì FireBall sẽ bắn về bên phải
        if (Monster.IsLookRight)
        {
            //tạo một đối tượng tạm thời để khởi tạo
            //cái gì sẽ được bắn ra : fireBall
            //tại vị trí nào:  new Vector2(transform.position.x + 0.7f, transform.position.y) trước mặt player cách khoảng 0.7f
            //Scale: FireBall theo hướng bên phải == 1
            GameObject temp = (GameObject)Instantiate(bullet, bulletPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            temp.GetComponent<Bullet>().Initialized(Vector2.right);

        }
        //khi Player nhìn vè bên trái FireBall sẽ bắn về bên trái
        else
        {
            //tại vị trí nào:  new Vector2(transform.position.x - 0.7f, transform.position.y) trước mặt player cách khoảng -0.7f
            GameObject temp = (GameObject)Instantiate(bullet, bulletPos.position, Quaternion.Euler(new Vector3(0, 0, -180)));
            temp.GetComponent<Bullet>().Initialized(Vector2.left);
        }
    }
}
