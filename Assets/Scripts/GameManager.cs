using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float score;
    public StageManager stageManager;
    public GameObject player;
    public UnityEvent OnGameEnd;

    [Min(0)]
    public float timeBeforeStart = 2;

    [Min(1)]
    public float timeToHalfSpeedUp = 60;

    [Min(1)] public float maxGameSpeed = 3;

    private float gameStartTime = 0;
    private bool gameRunning = false;


    public static float GameTime => Time.time - Instance.gameStartTime;
    public static float Score { get => Instance.score; }
    public static bool GameRunning { get => Instance.gameRunning; set { Instance.gameRunning = value; } }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (gameRunning)
        {
            Time.timeScale = GetGameSpeed();
            stageManager.difficulty = GetDifficulty();
        }
    }



    public void StartGame()
    {
        StartCoroutine(_StartGame());
    }

    public IEnumerator _StartGame()
    {
        score = 0;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(timeBeforeStart);

        Time.timeScale = 1;
        gameRunning = true;
        gameStartTime = Time.time;
    }


    public static bool AddScore(float value)
    {
        if (Instance.gameRunning)
        {
            Instance.score += value;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        gameRunning = false;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        OnGameEnd.Invoke();
    }

    public float GetGameSpeed()
    {
        float t = GameTime;
        float x = t / timeToHalfSpeedUp;
        return 1 + (maxGameSpeed - 1) * (1 - 1/(Mathf.Pow(x, 2)+1));
    }
    public float GetDifficulty()
    {
        return GameTime;
    }
}
