using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int blockCount;
    private float minX, maxX;

    private bool endless;
    private List<int> selectedPattern;
    private List<int> usedPatterns;
    private int maxPatternCount = 5;
    private float blockRandomSelectAdjust = -3f;
    private float blockRandomSelectIncrease = 0.75f;

    private Score scoreComponent;
    private List<Transform> SpawnLocations;
    private Ball ball;
    private GameObject borderContainer;
    private GameObject blockContainer;
    private LoseTrigger loseTrigger;

    public void UpdateScore(int blockTotalHealth)
    {
        if (!endless)
            return;
        int addScore = (int)((blockTotalHealth + (blockTotalHealth - 1) / 10) * ball.speedRatio * 100);
        scoreComponent.UpdateScore(addScore);
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("EndlessMode"))
            endless = PlayerPrefs.GetInt("EndlessMode") == 1 ? true : false;
        blockContainer = GameObject.FindGameObjectWithTag("Blocks");
        loseTrigger = GameObject.FindGameObjectWithTag("LoseTrigger").GetComponent<LoseTrigger>();
        loseTrigger.SetEndless(endless);
        scoreComponent = GetComponent<Score>();
        usedPatterns = new List<int>();
        GenerateBoundaries();
        if (endless)
        {
            GetBlockSpawns();
            GenerateBlocks();
        }
    }

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        UpdateBlockCount();
    }

    private void Update()
    {
        if (endless)
        {
            ManageReset();
        }
        else
        {
            LevelComplete();
        }
    }

    private void UpdateBlockCount()
    {
        blockCount = 0;
        var blocks = GameObject.FindGameObjectWithTag("Blocks")
            .GetComponentsInChildren<Block>();
        foreach (var block in blocks)
        {
            if (!block.unbreakable)
                blockCount++;
        }
    }

    private void LevelComplete()
    {
        if (blockCount <= 0)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == SceneManager.sceneCountInBuildSettings - 1)
                SceneManager.LoadScene("WinGameOver");
            else
                SceneManager.LoadScene(sceneIndex + 1);

        }
    }

    private void ManageReset()
    {
        if (blockCount <= 0)
        {
            if (blockRandomSelectAdjust < 0)
                blockRandomSelectAdjust += blockRandomSelectIncrease;
            ball.Stop();
            GenerateBlocks();
        }
    }

    private void GetBlockSpawns()
    {
        SpawnLocations = blockContainer.GetComponentsInChildren<Transform>().ToList();
        SpawnLocations.RemoveAt(0);
    }

    private void GenerateBlocks()
    {
        SelectPattern();
        foreach (var index in selectedPattern)
        {
            int random = Mathf.Clamp((int)(UnityEngine.Random.Range(1, 5.99f) + blockRandomSelectAdjust), 1, 5);

            Instantiate(Resources.Load("BlockH" + random), SpawnLocations[index]);
        }
        UpdateBlockCount();
    }

    public void RemoveBlockFromBlockCount()
    {
        blockCount--;
    }
    
    #region Patterns
    void SelectPattern()
    {
        int random;
        do
        {
            random = (int)UnityEngine.Random.Range(0, maxPatternCount - 0.01f);
        } while (usedPatterns.Contains(random));

        switch (random)
        {
            case 0:
                selectedPattern = Pattern0();
                break;
            case 1:
                selectedPattern = Pattern1();
                break;
            case 2:
                selectedPattern = Pattern2();
                break;
            case 3:
                selectedPattern = Pattern3();
                break;
            case 4:
                selectedPattern = Pattern4();
                break;
            default:
                selectedPattern = Pattern0();
                break;
        }

        if (usedPatterns.Count + 1 == maxPatternCount)
            usedPatterns = new List<int>();
        usedPatterns.Add(random);
    }

    List<int> Pattern0()
    {
        var indexes = new List<int>();
        //int currentIndex;
        //for (int i = 0; i <= 8; i++)
        //{
        //    for (int j = 0; j <= 8; j++)
        //    {
        //        if ((i == j || i + j == 8) 
        //            && !indexes.Contains(currentIndex = i * 9 + j))
        //        {
        //            indexes.Add(currentIndex);
        //        }
        //    }
        //}

        // Result of above loop
        indexes.AddRange(new List<int>
        { 0, 8, 10, 16, 20, 24, 30, 32, 40, 48, 50, 56, 60, 64, 70, 72, 80 });
        return indexes;
    }

    List<int> Pattern1()
    {
        var indexes = new List<int>();
        //int currentIndex;
        //for (int i = 0; i <= 8; i++)
        //{
        //    for (int j = 0; j <= 8; j++)
        //    {
        //        if ((i >= 6 || j >= 6) 
        //            && !indexes.Contains(currentIndex = i * 9 + j))
        //        {
        //            indexes.Add(currentIndex);
        //        }

        //    }
        //}

        // Result of above loop
        indexes.AddRange(new List<int>
        { 6, 7, 8, 15, 16, 17, 24, 25, 26, 33, 34, 35, 42, 43, 44,
            51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64,
            65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80 });
        return indexes;
    }

    List<int> Pattern2()
    {
        var indexes = new List<int>();
        //int currentIndex;
        //for (int i = 0; i <= 8; i++)
        //{
        //    for (int j = 0; j <= 8; j++)
        //    {
        //        if ((j-i) % 2 == 0
        //            && !indexes.Contains(currentIndex = i * 9 + j))
        //        {
        //            indexes.Add(currentIndex);
        //        }

        //    }
        //}

        // Result of above loop
        indexes.AddRange(new List<int>
        { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30,
            32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60,
            62, 64, 66, 68, 70, 72, 74, 76, 78, 80 });
        return indexes;
    }

    List<int> Pattern3()
    {
        var indexes = new List<int>();
        //int currentIndex;
        //for (int i = 0; i <= 8; i++)
        //{
        //    for (int j = 0; j <= 8; j++)
        //    {
        //        if (i != j && i != j+1 && i != j-1
        //            && !indexes.Contains(currentIndex = i * 9 + j))
        //        {
        //            indexes.Add(currentIndex);
        //        }

        //    }
        //}

        // Result of above loop
        indexes.AddRange(new List<int>
        { 2, 3, 4, 5, 6, 7, 8, 12, 13, 14, 15, 16, 17, 18, 22, 23, 24,
            25, 26, 27, 28, 32, 33, 34, 35, 36, 37, 38, 42, 43, 44, 45,
            46, 47, 48, 52, 53, 54, 55, 56, 57, 58, 62, 63, 64, 65, 66,
            67, 68, 72, 73, 74, 75, 76, 77, 78 });
        return indexes;
    }

    List<int> Pattern4()
    {
        var indexes = new List<int>();
        int currentIndex;
        int[] arrayEquals = new int[] { 0, -1 };
        int[] arraySum = new int[] { 7, 8 };
        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 8; j++)
            {
                var random = (int)UnityEngine.Random.Range(0, 1.99f);
                if (!arrayEquals.Contains(i - j) && !arraySum.Contains(i + j) && random == 1
                    && !indexes.Contains(currentIndex = i * 9 + j))
                {
                    indexes.Add(currentIndex);
                }
            }
        }
        return indexes;
    }
    #endregion

    #region Border Position
    void GenerateBoundaries()
    {
        Vector2 left = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
        Vector2 top = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 1));
        Vector2 bottom = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0));
        bottom.y -= 1f;

        borderContainer = GameObject.FindGameObjectWithTag("Border");
        Transform[] borders = borderContainer.GetComponentsInChildren<Transform>();
        ChangeBorderPosition(borders[1], left);
        ChangeBorderPosition(borders[2], right);
        ChangeBorderPosition(borders[3], top);
        ChangeBorderPosition(borders[4], bottom);
    }

    void ChangeBorderPosition(Transform obj, Vector2 location)
    {
        obj.position = location;
    }
    #endregion
}
