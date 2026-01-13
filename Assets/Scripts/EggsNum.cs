using UnityEngine;
using TMPro;

public class EggsNum : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text scoreText2;

    private int score;

    void OnEnable()
    {
        score = 0;
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text =score.ToString();
        scoreText2.text =score.ToString();
    }

    // ➕ Вызывать при подборе снежинки
    public void Add(int amount = 1)
    {
        score += amount;
        UpdateText();
    }

    // (опционально) получить текущее количество
    public int GetSnowflakes()
    {
        return score;
    }
}
