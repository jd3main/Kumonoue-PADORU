using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text text;

    void Update()
    {
        text.text = GameManager.Score.ToString();
    }

    public void OnGameOver()
    {
        Destroy(this.gameObject);
    }
}
