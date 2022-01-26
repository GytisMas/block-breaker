using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private const string scoreText = "Score: ";
    private int totalScore;
    private Text text;

    void Start()
    {
        totalScore = 0;
        text = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        Display();
    }

    void Display()
    {
        text.text = scoreText + totalScore;
    }

    public int GetScore()
    {
        return totalScore;
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        Display();
    }
}
