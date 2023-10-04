using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    //points tính điểm chơi hiện tại
    public int points = 0;

    //heightScore tính điểm cao nhất
    public int heightScore = 0;

    //public int tempHigh = 0;

    [SerializeField]
    private Text textScore;

    [SerializeField]
    private Text textHeightScore;

    public Text textInput;

    private void Awake()
    {
        //points = 0;
        //PlayerPrefs.SetInt("heightScore", 0);
    }

    // Use this for initialization
    void Start ()
    {
        //PlayerPrefs.SetInt("heightScore", 0);
        //nhập chuỗi "Heigt Score: Điểm sao lưu"
        //PlayerPrefs là dữ liệu sao lưu, thư viện có sẵn
        textHeightScore.text = ("High Score: " + PlayerPrefs.GetInt("highScore"));

        //tempHigh = PlayerPrefs.GetInt("TempHighScore", points);

        //gán điểm height score sao lưu cho height hiện trên màn hình Scenes
        //Và giá trị default của nó là 0
        heightScore = PlayerPrefs.GetInt("highScore", 0);

        //trả về giá trị true khi key points tồn tại
        if (PlayerPrefs.HasKey("points"))
        {

            //Tạo màn hình  Scene và gán màn hình hiện tại cho màn hình Scene
            Scene activityScene = SceneManager.GetActiveScene();

            //nếu màn hình hiện tại là màn hình đầu tiên
            //Có nghĩa là "Màn1" được chơi
            //Tạo Scene trong build setting
            if (activityScene.buildIndex == 3)
            {
                //nếu là Màn1 thì Delete key "points" và gán points = 0
                PlayerPrefs.DeleteKey("points");
                points = 0;
            }
            //ngược lại nếu có Màn 2 trở lên thì gán points = points sao lưu ở màn 1 để được cộng điểm vào tiếp
            else points = PlayerPrefs.GetInt("points");

            //if(player.currentHealth <= 0)
            //{
            //    points = 0;
            //}
        }
    }
	
	// Update is called once per frame
	void Update () {
        //nhập chuỗi "Score: điểm vào"
        textScore.text = ("Score: " + points);
    }
}
