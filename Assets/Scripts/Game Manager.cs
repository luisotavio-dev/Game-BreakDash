using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
                if (m_Instance == null)
                {
                    GameObject go = new GameObject("Game Manager");
                    m_Instance = go.AddComponent<GameManager>();
                }
            }
            return m_Instance;
        }
    }

    private const int NUM_LEVELS = 2;

    private Ball ball;
    private Paddle paddle;
    private Brick[] bricks;

    private int level = 1;
    private int score = 0;
    private int lives = 3;
    private TMP_Text scoreText;

    public int Level => level;
    public int Score => score;
    public int Lives => lives;

    public Sprite liveSprite;
    public Sprite noLiveSprite;
    public Image[] heartsOnCanvas;

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSceneReferences();
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        m_Instance = this;
        DontDestroyOnLoad(gameObject);
        //FindSceneReferences();
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void FindSceneReferences()
    {
        try
        {
            ball = FindObjectOfType<Ball>();
            paddle = FindObjectOfType<Paddle>();
            bricks = FindObjectsOfType<Brick>();
            scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
            //load live images
            heartsOnCanvas = new Image[lives];
            for (int i = 0; i < lives; i++)
            {
                heartsOnCanvas[i] = GameObject.Find("Life" + (i + 1)).GetComponent<Image>();
            }
        }
        catch
        {
            //
        }
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > NUM_LEVELS)
        {
            // Start over again at level 1 once you have beaten all the levels
            // You can also load a "Win" scene instead
            LoadLevel(1);
            return;
        }

        SceneManager.LoadScene("Level" + level);
    }

    public void OnBallMiss()
    {
        heartsOnCanvas[lives - 1].sprite = noLiveSprite;
        lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    private void GameOver()
    {
        // Start a new game immediately
        // You can also load a "GameOver" scene instead
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        LoadLevel(1);
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points;
        scoreText.text = this.score.ToString();
        ball.IncreaseSpeed(.04f);

        if (Cleared())
        {
            LoadLevel(level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }
}
