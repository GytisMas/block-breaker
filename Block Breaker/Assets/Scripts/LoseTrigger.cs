using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour
{
    private Score score;
    private bool endless = false;

    public void SetEndless(bool v)
    {
        endless = v;
    }

    void Start()
    {
        if (endless)
            score = GameObject.FindGameObjectWithTag("Manager").GetComponent<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
        if (endless)
            PlayerPrefs.SetInt("FinalScore", score.GetScore());
        SceneManager.LoadScene("LoseGameOver");
    }
}
