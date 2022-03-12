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
    public static float score
    {
        get => _score;
        set
        {
            if (gameRunning)
                _score = value;
        }
    }

    public StageManager stageManager;
    public GameObject player;

    public UnityEvent OnGameEnd;

    public float timeBeforeStart = 2;
    public float maxGameSpeed = 2;
    public float timeToMaxSpeed = 90;
 
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
            Time.timeScale = TimeScaleFunction(Time.time-gameStartTime);
        }
    }


    public void InitGame()
    {
        score = 0;
    }

    public IEnumerator StartGame()
    {
        for (int i = 1; i <= 10; i++)
        {
            Time.timeScale = i/10;
            yield return new WaitForSecondsRealtime(timeBeforeStart*0.1f);
        }

        Time.timeScale = 1;
        gameRunning = true;
        gameStartTime = Time.fixedTime;
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
}
