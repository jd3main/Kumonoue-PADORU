using UnityEngine;
using TMPro;

public class GameOverScoreUI : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        text.text = GameManager.Score.ToString();
    }
}
