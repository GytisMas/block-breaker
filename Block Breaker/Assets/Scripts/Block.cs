using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool unbreakable;

    [SerializeField]
    private Sprite[] hitSprites;
    private int health;
    private SpriteRenderer spRender;
    private GameManager manager;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager")
            .GetComponent<GameManager>();
        spRender = GetComponent<SpriteRenderer>();
        health = hitSprites.Length;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (unbreakable)
            return;
        if (col.gameObject.tag == "Ball")
        {
            health--;
            if (health < 0)
            {
                manager.RemoveBlockFromBlockCount();
                manager.UpdateScore(hitSprites.Length + 1);
                Destroy(gameObject);
            }
            else
                ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        spRender.sprite = hitSprites[health];
    }
}
