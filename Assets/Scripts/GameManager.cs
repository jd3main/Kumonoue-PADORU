using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool gameRunning = false;

    private static float _score;
    public static float score { get=>_score; private set { _score = value; } }


    public StageManager stageManager;
    public GameObject player;

    public UnityEvent OnGameEnd;

    public float timeBeforeStart = 2;
    public float maxGameSpeed = 2;
    public float timeToMaxSpeed = 90;
 
    public float gameTime => Time.time - gameStartTime;
    private float gameStartTime = 0;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (gameRunning)
        {
            Time.timeScale = TimeScaleFunction(gameTime);
            stageManager.difficulty = DifficultyFunction(gameTime);
        }
    }



    public IEnumerator StartGame()
    {
        InitGame();
        float n = 5;
        for (int i = 1; i <= n; i++)
        {
            Time.timeScale = i/n;
            yield return new WaitForSecondsRealtime(timeBeforeStart/n);
        }

        Time.timeScale = 1;
        gameRunning = true;
        gameStartTime = Time.fixedTime;
    }

    public void InitGame()
    {
        ResetScore();
    }

    public static bool AddScore(float value)
    {
        if (gameRunning)
        {
            score += value;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ResetScore()
    {
        score = 0;
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        gameRunning = false;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        OnGameEnd.Invoke();
    }

    public float TimeScaleFunction(float t)
    {
        return 1 + t/timeToMaxSpeed * (maxGameSpeed-1);
    }
    public float DifficultyFunction(float t)
    {
        return t;
    }
}
