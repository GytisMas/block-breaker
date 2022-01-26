using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Renderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }

    private void Rotate()
    {
        if (spriteRenderer.enabled)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);
        }
    }
}
