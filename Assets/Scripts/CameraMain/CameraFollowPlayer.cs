using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    private Transform player;
    public float minX, maxX;
    public float minY, maxY;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").transform;


    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 temp = transform.position;
            temp.x = player.position.x;


            if (temp.x < minX)
            {

                temp.x = minX;

            }
            else if (temp.x > maxX)
            {
                temp.x = maxX;

            }
            transform.position = temp;
        }

        if (player != null)
        {
            Vector3 temp = transform.position;
            temp.y = player.position.y;


            if (temp.y < minY)
            {

                temp.y = minY;

            }
            else if (temp.y > maxY)
            {
                temp.y = maxY;

            }
            transform.position = temp;
        }
    }
}
