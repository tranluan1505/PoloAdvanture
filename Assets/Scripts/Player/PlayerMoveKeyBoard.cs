using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveKeyBoard : MonoBehaviour {

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    public float LocalScalePlayer;

    public float maxSpeed = 2f;
    public float maxRun = 4f;
    public float jumpHeight = 1000f;
    public float maxJump = 20f;
    public float currentHealth;
    public float currentMana;

    public bool lookRight = true;
    public bool isGrounded;
    
    private bool jumpAttack = false;
    Vector3 impact = Vector3.zero;

    public SoundScripts source;   

    [SerializeField]
    private PanelPause panelPause;

    [SerializeField]
    private GameObject fireBall;

    [SerializeField]
    private GameObject dashWind;

    [SerializeField]
    private character boss;

    //private List<Transform> listTramform = new List<Transform>();

    private Rigidbody2D myBody;
    private Animator anim;
    private Animation animation;
    private GameMaster gameMaster;
    [SerializeField]
    private Transform FirePosition;

    [SerializeField]
    private Transform DashWindPosition;

    private void Awake()
    {
        health.Initialize();
        mana.Initialize();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        source = GameObject.FindGameObjectWithTag("ManagerSound").GetComponent<SoundScripts>();
        boss = GameObject.FindGameObjectWithTag("Monster").GetComponent<character>();
    }

    // Use this for initialization
    void Start () {
    }

    void Update()
    {
        // khi có phím Space và va chạm nền đất thì nó sẽ nhảy
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpAttack = true;
            // khi nó nhảy thì không còn chạm đất nữa => nên gán IsGrround = false
            isGrounded = false;
            // gán lực nhảy cho nó            
            myBody.AddForce(new Vector2(myBody.velocity.x, jumpHeight));
        }        
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (isGrounded)
        {
            jumpAttack = false;
            //jumpAttack = true;
        }
      
        currentHealth = health.CurrentVal;
        currentMana = mana.CurrentVal;

        MoveKeyBoard();

        if (Input.GetKey(KeyCode.K))
        {
            HandleJumpAttack();
        }

        if (myBody.velocity.y > maxJump)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, maxJump);
        }

        if (myBody.velocity.y < -maxJump)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, -4f);
        }

        StartCoroutine(TimeFire());    
        if(boss != null)
        {
            //audioWin = GameObject.Find("audioWin").GetComponent<AudioWin>();
            //Debug.Log("Co du lieu");
            if(boss.healthCurrent <= 0)
            {
                anim.Play("Win");               
                maxSpeed = 0;
                maxRun = 0;
                maxJump = 0;
            }
        } 
    }

    public void TimeWin()
    {

    }

    // dùng để di chuyển đi bộ Walk
    void MoveKeyBoard()
    {
        float hor = Input.GetAxis("Horizontal");

        if(anim.GetFloat("Walk") <= 0.99 && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            anim.SetFloat("Walk", Mathf.Abs(hor));
            myBody.velocity = new Vector2(maxSpeed * hor, myBody.velocity.y);
            //if (isGrounded)//
            //anim.SetTrigger("DashWind");
            //myBody.velocity = impact;
            //animation.CrossFade("DashWind");           
        }
        else if(anim.GetFloat("Walk") > 0.99 && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            anim.SetFloat("Walk", Mathf.Abs(hor));
            myBody.velocity = new Vector2(maxRun * hor, myBody.velocity.y);
            if(Input.GetKeyDown(KeyCode.K))
                anim.SetTrigger("Strike");            
        }
            
        anim.SetBool("IsGrounded", isGrounded);

        if ((hor > 0 && !lookRight) || (hor < 0 && lookRight))
        {
            Flip();
            //effect = Instantiate(Resources.Load("Effect/dashwind"), transform.position, transform.rotation) as GameObject;
            //anim.Play("DashWind");
        }   
    }

    // dùng để biến đổi hiện tại tọa độ scale
    public void Flip()
    {
        lookRight = !lookRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;

        //effect.transform.localScale = myScale;
    }

    // function dùng để kiểm tra sự va chạm với nền đất
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = true;
        }

        if(collision.gameObject.tag.Equals("Monster"))
        {
            isGrounded = true;
        }
           
        
    }

    //function dùng để animation đánh lộn   
    private void HandleJumpAttack()
    {
        if (!isGrounded && jumpAttack)
        {                
            anim.SetTrigger("JumAttack");
            jumpAttack = false;            
        }
    }

    //Khi nhân vật va chạm vào Monster thì Player sẽ bị đẩy ra
    //Đồng thời Player sẽ bị một lượng máu nhất định
    public void Damage(float damage)
    {
        Debug.Log("AttackEnemy");
        health.CurrentVal -= damage;
        animation.CrossFade("Hurt");
        StartCoroutine(TimeWait());
        //animation.Stop();
        if (health.CurrentVal <= 0)
        {
            Dead();
            //if (PlayerPrefs.GetInt("highScore") < gameMaster.points)
            //{
            //    PlayerPrefs.SetInt("highScore", gameMaster.points);
            //}

            PlayerPrefs.SetInt("points", 0);
           // PlayerPrefs.DeleteKey("points");
        }

        if(health.CurrentVal >= health.MaxVal)
        {
            health.CurrentVal = health.MaxVal;
        }
    }

    //tạo khoảng thời gian khi bị Monster tấn công
    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(TimeWait());
    }

    //khi Player chết thì Animator sẽ xuất hiện
    public void Dead()
    {
        anim.Play("Die");
        //animation.CrossFade("Hurt");
        myBody.bodyType = RigidbodyType2D.Static;
        StartCoroutine(Panel());
    }

    //Tạo khoảng thời gian trước khi Panel xuất hiện
    IEnumerator Panel()
    {
        yield return new WaitForSeconds(1.5f);
        panelPause.pause = true;

        yield return 1;
    }

    public void KnockBack(float knock)
    {
        //float tempX = myBody.position.x - knock - 4000f;
        if (myBody.velocity.x > maxSpeed || !isGrounded)
        {
            myBody.AddForce(new Vector2(myBody.velocity.x - knock - 4000f, myBody.velocity.y + knock - 300f));
            isGrounded = false;
        }

        if (myBody.velocity.x < -maxSpeed || !isGrounded)
        {
            myBody.AddForce(new Vector2(myBody.velocity.x + knock + 4000f, myBody.velocity.y + knock - 300f));
            isGrounded = false;
        }       
        //StartCoroutine(TimeWait());
    }

    void HandleFire()
    {      
        //Khi nhấn vào phím L thì sẽ bắn ra FireBall
        //đồng thời xử lí theo ThrowFireBall();
        if (Input.GetKeyDown(KeyCode.L) && mana.CurrentVal >= 10)
        {
            //myBody.velocity = impact;
            anim.SetTrigger("Fire");
            //ThrowFireBall(0);         
        }     
        
        if(mana.CurrentVal >= mana.MaxVal)
        {
            mana.CurrentVal = mana.MaxVal;
        }
    }

    public void decreaseManaPlayer()
    {
        mana.CurrentVal -= 10f;
    }

    //Tạo khoảng thời gian sau khi bắn
    IEnumerator TimeFire()
    {     
        HandleFire();
        yield return new WaitForSeconds(1);
        yield return 0;
    }

    void ThrowFireBall(int value)
    {
        if(!isGrounded && value == 1 || isGrounded && value == 0)
        {
            //khi Player nhìn về bên phải thì FireBall sẽ bắn về bên phải
            if (lookRight)
            {
                //tạo một đối tượng tạm thời để khởi tạo
                //cái gì sẽ được bắn ra : fireBall
                //tại vị trí nào:  new Vector2(transform.position.x + 0.7f, transform.position.y) trước mặt player cách khoảng 0.7f
                //Scale: FireBall theo hướng bên phải == 1
                GameObject temp = (GameObject)Instantiate(fireBall, FirePosition.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                temp.GetComponent<FireBall>().Initialized(Vector2.right);

            }
            //khi Player nhìn vè bên trái FireBall sẽ bắn về bên trái
            else
            {
                //tại vị trí nào:  new Vector2(transform.position.x - 0.7f, transform.position.y) trước mặt player cách khoảng -0.7f
                GameObject temp = (GameObject)Instantiate(fireBall, FirePosition.position, Quaternion.Euler(new Vector3(0, 0, -180)));
                temp.GetComponent<FireBall>().Initialized(Vector2.left);
            }
        }

        //audioPlayer.ChangeMusic(clipPlayer);


    }

    void DashWind(int value)
    {
        if (isGrounded && value == 0)
        {
            //khi Player nhìn về bên phải thì FireBall sẽ bắn về bên phải
            if (lookRight)
            {
                //tạo một đối tượng tạm thời để khởi tạo
                //cái gì sẽ được bắn ra : fireBall
                //tại vị trí nào:  new Vector2(transform.position.x + 0.7f, transform.position.y) trước mặt player cách khoảng 0.7f
                //Scale: FireBall theo hướng bên phải == 1
                GameObject temp = (GameObject)Instantiate(dashWind, DashWindPosition.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                temp.GetComponent<DashWind>().Initialized(Vector2.right);

            }
            //khi Player nhìn vè bên trái FireBall sẽ bắn về bên trái
            else
            {
                //tại vị trí nào:  new Vector2(transform.position.x - 0.7f, transform.position.y) trước mặt player cách khoảng -0.7f
                GameObject temp = (GameObject)Instantiate(dashWind, DashWindPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                temp.GetComponent<DashWind>().Initialized(Vector2.left);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Khi Player va chạm với Coin thì Coin sẽ Destroy();
        //và lúc đó phát ra một âm thanh khi va chạm vào Coin
        //Và point sẽ được cộng thêm một
        if (collision.gameObject.tag.Equals("Coin"))
        {
            source.AudioPlay("Coins");
            Destroy(collision.gameObject);
            gameMaster.points++;
        }

        if (collision.gameObject.tag.Equals("Electric"))
        {
            Destroy(collision.gameObject);
            mana.CurrentVal += 20f;
        }

        if (collision.gameObject.tag.Equals("Health"))
        {
            Destroy(collision.gameObject);
            health.CurrentVal += 30f;
        }
    }

    void DamageWeaPon_ORC(int damage)
    {
        health.CurrentVal -= damage;
    }
}
