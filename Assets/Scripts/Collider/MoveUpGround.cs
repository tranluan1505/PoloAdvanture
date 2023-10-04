using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpGround : MonoBehaviour {

    //tốc đọ di chuyển của ground
    public float moveUp = 0.04f;

    private Rigidbody2D myBody;

   // private PlayerMoveKeyBoard player;

    //Tạo đối tượng pause == null để dừng màn hình
    private PanelPause pauseUI;

    //Gán position hiện tại cho tempPositon
    private Vector3 tempPosition;

    //Kiểm tra va chạm
    private bool isGround = false;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveKeyBoard>();
    }

    // Use this for initialization
    void Start () {
        tempPosition = transform.position;

        //Gán Đối tượng class PanelPause cho pause
        pauseUI = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<PanelPause>();
    }
	
	// Update is called once per frame
	void Update () {
        //khi Player chạm Ground thì Ground Start Move Up
         if (isGround)
         {
            StartCoroutine(MoveUp());
         }
	}

    private void FixedUpdate()
    {
       
    }

    IEnumerator  MoveUp()
    {
        // nếu không == pause(không = dừng màn hình) thì sẽ gán tốc độ cho ground
        if (!pauseUI.pause)
        {
            //đợi 0 giây để di chuyển
            yield return new WaitForSeconds(0f);

            //cộng tốc độ moveUp cho biến temp.y
            //tempPosition.y += moveUp;
            tempPosition.y += Time.deltaTime + 0.02f;
            //Gán temp lại cho transform
            transform.position = tempPosition;
        }

        //nếu == pause thì sẽ gán vị trí hiện tại cho ground
        //là vị trí hiện tại mà ground đang đứng
        if (pauseUI.pause)
        {
            transform.position = tempPosition;
        }
        
        yield return 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //khi Player chạm nền đất
        if (collision.gameObject.tag.Equals("Player"))
        {
            isGround = true;

            Invoke("DestroyMoveUp", 4f);
            //MoveUp();
        }
    }

    void DestroyMoveUp()
    {
        Destroy(gameObject);
        //player.isGrounded = false;
    }
}
