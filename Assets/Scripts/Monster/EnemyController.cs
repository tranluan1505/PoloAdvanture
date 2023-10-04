using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject target;

    public int maxRange;
    public int minRange;
    private Vector3 targetTran;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        targetTran = target.transform.position;
    }

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, target.transform.position));

        if ((Vector3.Distance(transform.position, target.transform.position) < maxRange)
            && (Vector3.Distance(transform.position, target.transform.position) > minRange))
        {
            transform.LookAt(targetTran);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
