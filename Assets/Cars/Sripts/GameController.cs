using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;
//This Script controls the complete game play.
public class GameController : MonoBehaviour
{
    // Dành cho quảng cáo google 
   
   


    public GameObject BackGround; 

    public GameObject OrangeCar;
    public GameObject BlueCar;
    public GameObject[] BlueGameObjects;
    public GameObject[] OrangeGameObjects;
    GameObject Blue;
    GameObject Orange;
    public float spawnWait;
    public float startWait;
    public static float speed;
    float[] XPosition;
    public static bool BlueAtLeft = false;
    public static bool OrangeAtLeft = false;
    public static bool IsGameOver;
    public static bool IsGamePause;
    public static bool IsSoundOff;
    public static bool IsMusicOff= false;
    public Text GameScoreText;
    public static int score = 0;
    int highscore;
    public GameObject GameOverCanvas;
    public GameObject GameCanvas;
    public GameObject StartCanvas;
    public GameObject WarningPanel;
    public GameObject GuidePanel;
    public Text ScoreText;
    public Text HighScoreText;
    public static int CoinScore = 0;
    public Text CoinScoreText;
    //public Text CoinInGame;
    public Text CoinInGameText;
    static int TotalScore = 0;
    public Text TotalScoreText;

    public static bool Pressrespawn = false;
    private void Awake()
    {
        XPosition = new float[2]
    {
        (float)(Camera.main.orthographicSize * Screen.width / Screen.height / 4),
        (float)(Camera.main.orthographicSize * Screen.width / Screen.height / 4*3)
    };
       
       

    }
    private void Update()
    {
        CoinInGameText.text = CoinScore.ToString(); 
    }
    void Start()
    {
        
       
        CoinInGameText.text = CoinScore.ToString();
        GameScoreText.text = score.ToString();
        GuidePanel.SetActive(false);
        WarningPanel.SetActive(false);
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        TotalScore = PlayerPrefs.GetInt("Total", 0);
        CoinScore = PlayerPrefs.GetInt("Coin", 0);

        //this disables GameOverCanves and GameCanvas, enables startCanvas.
        if (Pressrespawn == false)
        {
            
            GameOverCanvas.SetActive(false);
            GameCanvas.SetActive(false);
            StartCanvas.SetActive(true);
        }
        if(Pressrespawn== true)
        {
            IsGameOver = false;
            GameOverCanvas.SetActive(false);
            GameCanvas.SetActive(true);
            StartCanvas.SetActive(false);
            StartCoroutine(SpawnWaves());
        }
        GameController.speed = 5f;
        Pressrespawn = false; 
      
    }
    //this starts the spawn wave of the cubes and circles.
    IEnumerator SpawnWaves()
    {
        //wait of startWait(int) seconds
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < 100; i++)
            {
               
                //Setting orange gameObject Position randomly between 2 XPosition.
                float OrangeXPos = XPosition[Random.Range(0, XPosition.Length)];
                Vector3 OrangePosition = new Vector3(OrangeXPos, 6.5f, 0);
                //Randomly choose between orange cube & circle.
                Orange = OrangeGameObjects[Random.Range(0, OrangeGameObjects.Length)];
                //create orange gameobject at OrangePosition.
                Instantiate(Orange, OrangePosition, Quaternion.identity);
                //wait between next drop
                yield return new WaitForSeconds(spawnWait);
                //Setting blue gameObject Position randomly between 2 XPosition.
                float BlueXPos = XPosition[Random.Range(0, XPosition.Length)];
                Vector3 BluePosition = new Vector3(-BlueXPos, 6.5f, 0);
                //Randomly choose between blue cube & circle.
                Blue = BlueGameObjects[Random.Range(0, BlueGameObjects.Length)];
                //create blue gameobject at BluePosition.
                Instantiate(Blue, BluePosition, Quaternion.identity);
                //wait between next drop
                yield return new WaitForSeconds(spawnWait);
            }
        }
    }
    // Update is called once per frame
  

    public void StartGame()
    {
        //On Start Game button pressed, disable StartCanvas and GameCanvas & starting the spawn of cubes and circles.
        StartCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        StartCoroutine(SpawnWaves());
        IsGameOver = false;
    }

    public void AddScore()
    {
        //Add score by 1 and showing that score to GameScoreText.
        score += 1;
        GameScoreText.text = score.ToString();
        TotalScore = TotalScore + 1;
        
        //Debug.Log(TotalScore);
        if (TotalScore % 50 == 0 && TotalScore != 0)
        {
            CoinScore += 1;
        }
        CoinInGameText.text = CoinScore.ToString();
        CoinScoreText.text = CoinScore.ToString();
        
        PlayerPrefs.SetInt("Total", TotalScore);
        PlayerPrefs.SetInt("Coin", CoinScore);
    }

    public void LeftButton()
    {
        //on pressing left button, if BlueAtLeft is true set it to false and if it is false set it to true.
        if (IsGamePause==false)
        {
            if (BlueAtLeft)
            {
                BlueAtLeft = false;
            }
            else
            {
                BlueAtLeft = true;
            }
        }
    }

    public void RightButton()
    {
        //on pressing right button, if OrangeAtLeft is true set it to false and if it is false set it to true.
        if (IsGamePause==false)
        {
            if (OrangeAtLeft)
            {
                OrangeAtLeft = false;
            }
            else
            {
                OrangeAtLeft = true;
            }
        }
    }

    public void Restart()
    {
        //Reload the game on restart button
        SceneManager.LoadScene(0);
        score = 0;
    }

    public void Pause()
    {
        //stop the game in scene

        if (IsGamePause)
        {
            Time.timeScale = PlayerMove.t;
            IsGamePause = false;
            IsMusicOff = true;

        }
        else
        {
            Time.timeScale = 0;
            IsGamePause = true;
            IsMusicOff = false;
            
        }
    }

    public void SoundOff()
    {
        if (IsSoundOff)
        {

            IsSoundOff = false;
        }
        else
        {

            IsSoundOff = true;
        }
    }

    public void MusicOff()
    {
        if (IsMusicOff)
        {
            IsMusicOff = false;
            BackGround.GetComponent<BackGroundMove>().MusicOn();
        }
        else
        {
            IsMusicOff = true;
            BackGround.GetComponent<BackGroundMove>().MusicOff();  
        }
    }

    
    public void GameOver()
    {
        //sets is isGameOver to true
        IsGameOver = true;
        //Plays GameOverSound
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        //Pause the time
        Time.timeScale = 0;


        //activate the gameover canvas
        GameOverCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        //show the score value on ScoreText
        
        ScoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        
        CoinScoreText.text = CoinScore.ToString();

        //if score is greater than highscore change the stored value of highscore
        if (score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        //show highscore value on HighScoreText
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = highscore.ToString();
        GameController.score = 0;
    }

    public void GameOver1()
    {
        Time.timeScale = 0;

        int[] b = new int[4] { 0, 1, 2, 3 };
        int a = b[Random.Range(0, 4)];
        if (a % 3 == 0)
        {
            GameObject.FindGameObjectWithTag("Ads").GetComponent<GoogleAds>().ShowinterstitialAds();
        }
        //activate the gameover canvas

        GameOverCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        //show the score value on ScoreText

        ScoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        
        CoinScoreText.text = CoinScore.ToString();

        //if score is greater than highscore change the stored value of highscore
        if (score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        //show highscore value on HighScoreText
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = highscore.ToString();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>().Play(); 
    }

    public void SaveandLoad()
    {
        
            score = PlayerPrefs.GetInt("Score", score);
            SceneManager.LoadScene(0);
            CoinScore = CoinScore - 1;
            PlayerPrefs.SetInt("Coin", CoinScore);
        
        
     
    }

    //public void ShowRewardedAd()
    //{
    //    if (Advertisement.IsReady("rewardedVideo"))
    //    {
    //        var options = new ShowOptions { resultCallback = HandleShowResult };
    //        Advertisement.Show("rewardedVideo", options);
    //    }
    //}



    public void AdReward()
    {
        CoinScore = CoinScore + 1;
        CoinScoreText.text = CoinScore.ToString();
        PlayerPrefs.SetInt("Coin", CoinScore);
    }

    public void Respawn()
    {
        
        Pressrespawn = true;
        if (CoinScore != 0)
        {
            SaveandLoad();

            GameCanvas.SetActive(true);
            StartCoroutine(SpawnWaves());
            
        }
        else
        {
            WarningPanel.SetActive(true);
        }
    }
    public void ShowWarning()
    {
        WarningPanel.SetActive(true);
    }
    public void ShowGuide()
    {
       
        GuidePanel.SetActive(true);
    }
    public void HideGuide()
    {
        GuidePanel.SetActive(false);
    }
}
