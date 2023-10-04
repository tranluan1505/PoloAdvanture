using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {

    //tốc độ di chuyển của ground và thay đổi hướng di chuyển cho ground
    public float speed = 0.05f, changeDirection = -1;
    Rigidbody2D myBody;

    //đối tượng dùng để pause
    private PanelPause pauseUI;

    //sử dụng biến tempPosition để gán vị trí tạm thời
    private Vector3 tempPosition;

    private PlayerMoveKeyBoard player;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        pauseUI = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<PanelPause>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveKeyBoard>();
    }

    // Use this for initialization
    void Start () {
        //myBody.isKinematic.GetType();
        tempPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Gọi function cho nó tự di chuyển
        if(transform.position.x != 0 && transform.position.x != 5)
        {
            moveGroundX();
        }

        if(transform.position.y != 0 && transform.position.x == 5)
        {
            moveGroundY();
        }
	}

    //function dùng để viết cách di chuyển cho ground
    private void moveGroundX()
    {
        if (!pauseUI.pause)
        {
            tempPosition.x += speed;
            transform.position = tempPosition;
        }

        if (pauseUI.pause)
        {
            transform.position = tempPosition;
        }
        
    }

    private void moveGroundY()
    {
        if (!pauseUI.pause)
        {
            tempPosition.y += speed;
            transform.position = tempPosition;
        }

        if (pauseUI.pause)
        {
            transform.position = tempPosition;
        }

    }

    //khi va chạm với nền đất bên cạnh thì nó sẽ di chuyển ngược lại
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {

            //Debug.Log("collider");
            speed *= changeDirection;
        }       
    }
}
