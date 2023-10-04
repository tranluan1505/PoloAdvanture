using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFalling : MonoBehaviour {

    private Rigidbody2D myBody;

    
    //thời gian bắt đầu và thời gian kết thúc
    public float timeFallStart = 0.7f;
    public float timeFallEnd = 1.1f;

    //public float forceFalling = 0.01f;

    //private Vector3 temPosition;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        //temPosition = transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
        //StartCoroutine(fall());
	}

    IEnumerator fall()
    {
        yield return new WaitForSeconds(Random.Range(timeFallStart, timeFallEnd));

        //temPosition.y -= forceFalling;

        //transform.position = temPosition;

        //thay đổi kiểu static(type đứng yên) sang dynamic(type có thể chuyển động)
        myBody.bodyType = RigidbodyType2D.Dynamic;

        yield return 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(fall());
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("River"))
        {
            Destroy(gameObject);
        }
    }
}
