using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    private const string message = "Auto movement (H): ";
    [SerializeField] bool autoMove = false;

    private float minX, maxX;
    private bool started = false;
    public bool tutorialBlock = false;
    private Ball ball;
    private Text autoMoveText;

    public void Stop()
    {
        started = false;
    }

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        autoMoveText = GameObject.FindGameObjectWithTag("AutoMove").GetComponent<Text>();
        autoMoveText.text = AutoMoveText();
        SetUpBoundaries();
    }

    void Update()
    {
        ManageAutoMovementAndText();
        if (started)
        {
            if (autoMove)
                MoveAutomatically();
            else
                Move();
        }
        else if (ball.started)
            started = true;
    }
    
    void SetUpBoundaries()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        Collider2D col = gameObject.GetComponent<Collider2D>();
        float width = col.bounds.extents.x;
        minX = minX + width;
        maxX = maxX - width;
    }

    void Move()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        pos = Mathf.Clamp(pos, minX, maxX);
        var temp = new Vector2(pos, transform.position.y);
        transform.position = temp;
    }

    void MoveAutomatically()
    {
        var pos = ball.transform.position.x;
        pos = Mathf.Clamp(pos, minX, maxX);
        var temp = new Vector2(pos, transform.position.y);
        transform.position = temp;
    }

    public void SetTutorialBlock(bool v)
    {
        tutorialBlock = v;
        autoMoveText.text = v ? "" : AutoMoveText();
    }

    void ManageAutoMovementAndText()
    {
        if (!tutorialBlock && Input.GetKeyDown(KeyCode.H))
        {
            autoMove = !autoMove;
            autoMoveText.text = AutoMoveText();
        }
    }

    string AutoMoveText()
    {
        return message + (autoMove ? "on" : "off");
    }
}
