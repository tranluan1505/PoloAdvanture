using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterway : MonoBehaviour {

    //khi không va chạm nền đất nữa thì bOSS sẽ quay mặt
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("Derection");
            character.facingleft = !character.facingleft;      
        }
    }
}
