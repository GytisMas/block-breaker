using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject manager;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject paddle;
    [SerializeField] private GameObject blockContainer;
    [SerializeField] private GameObject autoMoveText;
    private Text launchText;
    private GameObject[] blocks;
    private Text textComponent;
    private List<string> textList;
    private int count;

    private string text00 = "Welcome to block breaker!";
    private string text01 = "The goal of this game is to get " +
        "the highest possible score by breaking the blocks on the screen.";
    private string text02 = "Before launching the ball, use your mouse to aim" +
        " in the direction to which you will launch the ball.";
    private string text03 = "Afterwards, use your mouse to control the position " +
        "of the paddle in the bottom.";
    private string text04 = "For testing purposes, you can enable / disable auto " +
        "movement by pressing H.";

    //private string text10 = "Some blocks are stronger than others. You will need to land" +
    //    " multiple hits on them before finally breaking them.";
    //private string text11 = "Blocks with specific colors have a certain amount of hits " +
    //    "that will be neccessary to break them.";
    //private string text12 = "In this level, coloured blocks are protected by the same amount" +
    //    " of cyan blocks that ";

    private string textContinue = "Press G to continue";
    private string textLaunch= "Press G to launch the ball";

    void Start()
    {
        GetBlocks();
        SceneIdleState(true);
        SetTextList();

        foreach (var item in blocks)
        {
            HideObject(item);
        }

        textComponent = GetComponent<Text>();
        textComponent.text = textList[count++];
    }

    void Update()
    {
        Continue();
    }

    void Continue()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (count < textList.Count)
                textComponent.text = textList[count++];
            else
            {
                SceneIdleState(false);
                Destroy(gameObject);
            }
                
        }
    }

    void SceneIdleState(bool v)
    {
        if (v)
        {
            launchText = GameObject.Find("Launch ball text").GetComponent<Text>();
            launchText.text = textContinue;
            ball.GetComponent<Ball>().tutorialBlock = true;
            paddle.GetComponent<Paddle>().SetTutorialBlock(true);
            HideObject(ball);
            HideObject(paddle);
            HideObject(arrow);
        }
        else
        {
            foreach (var item in blocks)
            {
                ShowObject(item);
            }
            ShowObject(paddle);
            ShowObject(ball);
            ShowObject(arrow);
            ball.GetComponent<Ball>().tutorialBlock = false;
            paddle.GetComponent<Paddle>().SetTutorialBlock(false);
            launchText.text = textLaunch;
        }
    }

    void HideObject(GameObject obj)
    {
        obj.GetComponent<Renderer>().enabled = false;
    }

    void ShowObject(GameObject obj)
    {
        obj.GetComponent<Renderer>().enabled = true;
    }

    void GetBlocks()
    {
        var blocksComponents = blockContainer.GetComponentsInChildren<Block>();
        int count = blocksComponents.Length;
        blocks = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            blocks[i] = blocksComponents[i].gameObject;
        }
    }

    void SetTextList()
    {
        count = 0;
        textList = new List<string>();
            textList.Add(text00);
            textList.Add(text01);
            textList.Add(text02);
            textList.Add(text03);
            textList.Add(text04);
    }
}
