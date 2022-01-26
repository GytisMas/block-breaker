using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(450, 800, true);
    }

    void Start()
    {
        PlayerPrefs.SetInt("EndlessMode", 0);
        GameObject scoreText = GameObject.FindGameObjectWithTag("ScoreText");
        if (scoreText != null && PlayerPrefs.HasKey("FinalScore"))
        {
            scoreText.SetActive(true);
            scoreText.GetComponent<Text>().text = "Your score is: " + PlayerPrefs.GetInt("FinalScore");
        }
        else if (scoreText != null)
        {
            scoreText.SetActive(false);
        }
        else if (PlayerPrefs.HasKey("FinalScore"))
        {
            PlayerPrefs.DeleteKey("FinalScore");
        }
    }

    public void MainMenu()
    {
        PlayerPrefs.SetInt("EndlessMode", 0);
        SceneManager.LoadScene("Main Menu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLastLevel()
    {
        if (PlayerPrefs.HasKey("LastScene"))
        {
            if (PlayerPrefs.GetInt("LastScene") == 1)
                PlayerPrefs.SetInt("EndlessMode", 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene"));
        }
        else
            Debug.Log("Player Prefs has no key \"Last Scene\"");
    }

    public void LoadEndlessMode()
    {
        PlayerPrefs.SetInt("EndlessMode", 1);
        SceneManager.LoadScene("Endless");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
