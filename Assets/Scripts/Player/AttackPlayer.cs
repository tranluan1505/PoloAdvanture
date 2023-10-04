using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour {

    public float delayAttack = 0.6f;

    public Collider2D trigger;

    [SerializeField]
    private PlayerMoveKeyBoard player;

    private bool attack;

    private Animator anim;

    public SoundScripts source;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        trigger.enabled = false;
        source = GameObject.FindGameObjectWithTag("ManagerSound").GetComponent<SoundScripts>();
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.K) )
        {
            
            attack = true;           
            delayAttack = 0.6f;
            //trigger.enabled = true;

            //HandleAttack();
        }
        if (attack)
        {

            if (delayAttack > 0.3f)
            {
                
                delayAttack -= Time.deltaTime + 0.1f;
            }
            else
            {
                //Time.timeScale = 0.5f;
                attack = false;

                Time.maximumDeltaTime = 1f;
                //trigger.enabled = false;
            }
            
        }
        //else attack = true;
        //anim.SetTrigger("Attack");
        anim.SetBool("Attack", attack);

        if (!attack)
        {
            trigger.enabled = false;           
        }
        //player.isGrounded = true;
    }

    void OnColliderTrigger(int values)
    {
        if(values == 1)
        {
            trigger.enabled = true;
        }

        if(values == 0)
        {
            trigger.enabled = false;
        }
    }

    void AudioPlay()
    {
        source.AudioPlay("Sword");
    }
}
