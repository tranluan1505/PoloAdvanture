using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class character : MonoBehaviour {

    public float healthCurrent;

    //Thanh máu của BOSS
    [SerializeField]
    private Stat HealthBoss;

    //Trọng lực
    public static Rigidbody2D rbEnemy;

    //Hình động
    public static Animator arEnemy;

    //thời giản đứng yên
    public float idle;

    //thời gian đi bộ
    public float walk;
    public bool run;

    //tính khoảng cách giữa nhân vật và Boss
    public float distance;   
    
    //Tọa độ khoảng cách giữa nhân vật và boss
    [SerializeField]
    private Vector3 distanceBetweenPlayerAndEnemy;

    //Tọa độ vị trí cuả nhân vật
    public Transform tmplayer;

    //tốc độ của BOSS
    public static float speed = 3;

    //mặc định BOSS sẽ quay mặc bên trái
    public static bool facingleft = true;
    public bool lookLeft;
    public float speedBoss;

    //Phát hiện nhân vật trên tọa độ X sẽ bằng = true
    public bool followPlayer;

    //hiện thị thời gian following theo nhân vật
    public float timeFollowingPlayer;

    //Hiện thị TextBox
    public Canvas canvasText;

    //Sự dụng các thuộc tính thuộc public của nhân vật ở file Script PlayerMoveKeyBoard
    [SerializeField]
    private PlayerMoveKeyBoard player;

    //Hiện thị dòng chữ được ghi trong code và gán cứng cho nó
    [SerializeField]
    private Text textCommentBoss;

    //thời gian đợi để chuyển Màn hình cuối cùng
    public float timeWait;

    //hiện thị quái vật khi máu Boss xuống 50%
    [SerializeField]
    private GameObject monster;

    //vị trí xuất hiện của quái vật
    [SerializeField]
    private Transform monsterPos;

    [SerializeField]
    private GameObject monster2;

    [SerializeField]
    private Transform monsterPos2;

    [SerializeField]
    private GameObject monster3;

    [SerializeField]
    private Transform monsterPos3;

    //tạo biến đếm số lượng quái vật
    int dem = 0;
    int dem2 = 0;


    //sử dụng thuộc tính của gameMaster (điểm số)
    [SerializeField]
    private GameMaster gameMaster;

    //sử dụng thuộc tính của màu
    private SpriteRenderer color;

    //sử dụng thuộc tính của damage
    private Damage damage;

    // Use this for initialization
    void Start () {
        //khởi tạo máu ban đầu cho BOSS
        HealthBoss.Initialize();

        //tắt TextBox hiển thị giá trị máu
        HealthBoss.bar.text.enabled = false;

        //tắt Canvas UI(là phần sử lí các phần từ trong nó)
        canvasText.enabled = false;

        //khởi tạo run = false (không được chạy)
        run = false;

        //sử dụng Component của Rigidbody
        rbEnemy = GetComponent<Rigidbody2D>();

        //sử dụng Component của Animator
        arEnemy = GetComponent<Animator>();

        //Random thời gian đứng yên cho Boss
        idle = Random.Range(3, 8);

        //lấy vị trí của nhân vật
        tmplayer = GameObject.Find("Player").GetComponent<Transform>();

        //lấy các thuộc tính của nhân vật 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveKeyBoard>();

        //Random thời gian chuyển màn hình
        timeWait = Random.Range(4f, 6f);

        //lấy các thuộc tính của GameMaster(láy điểm)
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

        //lấy màu của SpriteRenderer (là Component của Boss)
        color = GetComponent<SpriteRenderer>();

        //sử dụng thuộc tính của Damage
        damage = GameObject.Find("TriggerAttack").GetComponent<Damage>();
    }
	
	// Update is called once per frame
	void Update () {
        //dùng để hiển thị thông tin của speedBoss, lookLeft ra
        speedBoss = speed;
        lookLeft = facingleft;

        //nếu facing = -1 thì vận tốc sẽ âm và nó sẽ đi vè bên trái
        rbEnemy.velocity = new Vector2(facing() * speed,rbEnemy.velocity.y);

        //chạy animator của Speed
        arEnemy.SetFloat("Speed", speed);

        //nếu Boss quay mặc bên trái thì facing = -1
        //nếu Boss quay mặc bên phải thì facing = 1
        transform.localScale = new Vector3(facing(), transform.localScale.y, transform.localScale.z);
        distanceFollow();
        //Attack();
        Transform();

        if (player.currentHealth > 0 && followPlayer)
        {
            //Debug.Log("Following");
            Attack();
            timeFollowingPlayer = 0;
            canvasText.enabled = false;
        }
        else
        {
            timeFollowingPlayer += Time.deltaTime;
            if(timeFollowingPlayer >= 7)
            {                                                  
                canvasText.enabled = true;
                textCommentBoss.text = "Chú trốn \n\nđi đâu rồi ^_^";
                if(timeFollowingPlayer >= 15)
                {
                    textCommentBoss.text = "Anh \n\nbực rồi đó nha";
                }

                if (rbEnemy.velocity.x > 0 && !facingleft || rbEnemy.velocity.x == 0 && !facingleft)
                {
                    LookRightScaleCanves();
                }
                else
                {
                    LookLeftScaleCanves();
                }
                
            }           
            arEnemy.SetBool("Attack", false);
            //Transform();
        }
       
        if (HealthBoss.CurrentVal <= 0)
        {
            arEnemy.Play("Dead");
            canvasText.enabled = false;
            speed = 0;
            timeWait -= Time.deltaTime;
            if(timeWait < 0)
            {
                SceneManager.LoadScene(6);
            }    
            
            PlayerPrefs.SetInt("highScore", gameMaster.points);
        }

        if (HealthBoss.CurrentVal <= (HealthBoss.MaxVal / 2))
        {
            Debug.Log(HealthBoss.CurrentVal);
            //HealthBoss.CurrentVal -= 10;
            if (dem == 0)
            {
                monster = Instantiate(Resources.Load("Effect/MonsterOfBoss"),
                monsterPos.transform.position,
                monsterPos.transform.rotation) as GameObject;

                monster2 = Instantiate(Resources.Load("Effect/MonsterOfBoss"),
                    monsterPos2.transform.position,
                    monsterPos2.transform.rotation) as GameObject;

                monster3 = Instantiate(Resources.Load("Effect/MonsterOfBoss"),
                    monsterPos3.transform.position,
                    monsterPos3.transform.rotation) as GameObject;

                dem += 3;
            }           
        }

        if (HealthBoss.CurrentVal <= (HealthBoss.MaxVal / 4))
        {
            Debug.Log(HealthBoss.CurrentVal / 4);
            //HealthBoss.CurrentVal -= 10;

            //tempColor = new Color(
            //    Random.Range(1, 255),
            //    Random.Range(1, 255),
            //    Random.Range(1, 255),
            //    Random.Range(1,1)
            //    );

            //if(timeColor > 0)
            //{
            //    timeColor -= Time.deltaTime;

            //    if(timeColor <= 0)
            //    {
            //        color.color = tempColor;
            //        timeColor = 1;
            //    }
            //}
            if (dem2 == 0)
            {
                color.color = Color.blue;
                damage.damage = 50;
                dem2 += 1;
            }
        }
    }

    private void FixedUpdate()
    {
        healthCurrent = HealthBoss.CurrentVal;
    }

    //khi Boss quay mặt bên trái thì Scale TextBox = -0.00474f
    void LookLeftScaleCanves()
    {
        Vector3 myScale = canvasText.transform.localScale;
        myScale.x = -0.00474f;
        canvasText.transform.localScale = myScale;
    }

    void LookRightScaleCanves()
    {
        Vector3 myScale = canvasText.transform.localScale;
        myScale.x = 0.00474f;
        canvasText.transform.localScale = myScale;
    }

    void distanceFollow()
    {
        distanceBetweenPlayerAndEnemy.x = tmplayer.position.x - transform.position.x + distance;
        distanceBetweenPlayerAndEnemy.y = tmplayer.position.y - transform.position.y + distance;

        if (distanceBetweenPlayerAndEnemy.y == Mathf.Clamp(distanceBetweenPlayerAndEnemy.y, -2.5f, 2.5f))
        {
            followPlayer = true;
        }
        else
        {
            followPlayer = false;
        }
    }
    float facing()
    {
        return facingleft ? -1 : 1;
    }
    void Attack()
    {        

        if(distanceBetweenPlayerAndEnemy.x > 1 && facingleft)
        {
            facingleft = false;
        }
        else if (distanceBetweenPlayerAndEnemy.x < -1 && !facingleft)
        {
            facingleft = true;
        }

        if (distanceBetweenPlayerAndEnemy.x == Mathf.Clamp(distanceBetweenPlayerAndEnemy.x, -2, 2) && followPlayer)
        {
            arEnemy.SetBool("Attack", true);
            rbEnemy.velocity = new Vector2(0, rbEnemy.velocity.y);
        }
        //else
        //{
        //    arEnemy.SetBool("Attack", false);
        //    speed = 4f;
        //    //Transform();
        //}
    }  

    void Transform()
    {
        if(idle > 0 )
        {            
            idle -= Time.deltaTime;
            if(idle < 0)
            {
                walk = Random.Range(10, 15);
                speed = Random.Range(3, 8);
                
            }
        }
        

        if(walk > 0)
        {
            walk -= Time.deltaTime;
            if (walk < 0)
            {
                idle = Random.Range(3, 8);
                speed = 0;
            }
        }

        if (speed >= 6)
        {
            run = true;
            arEnemy.SetBool("Run", run);
        }
        else
        {
            run = false;
            arEnemy.SetBool("Run", run);
        }

        if (arEnemy.GetBool("Attack") == true)
        {
            speed = 0;
            idle = 1f;
            walk = 0;
            arEnemy.SetBool("Attack", false);
        }
    }

    void DamageAttack(int damage)
    {
        HealthBoss.CurrentVal -= damage;
        //arEnemy.SetTrigger("Damage");
        Debug.Log("AttackPlayer" + HealthBoss.CurrentVal);
    }

    void DamageFire(int damage)
    {
        HealthBoss.CurrentVal -= damage;
        //arEnemy.SetTrigger("Damage");
        Debug.Log("FireBall");
    }
}
