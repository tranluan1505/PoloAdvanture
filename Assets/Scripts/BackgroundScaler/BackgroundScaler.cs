using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour {

    //dùng để làm cho scale giữa MainCamera với background trùng nhau 
	// Use this for initialization
	void Start () {
        //sử dụng Component SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        //kích thước y (chiều cao)
        float height = spriteRenderer.bounds.size.y;
        float width = spriteRenderer.bounds.size.x;

        //
        float maxHeight = Camera.main.orthographicSize * 2f;
        float maxWidth = maxHeight * Screen.width / Screen.height;

        tempScale.x = maxWidth / width;
        tempScale.y = maxHeight / height;

        transform.localScale = tempScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
