using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringySprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector2[] originalSpriteVertices;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        originalSpriteVertices = sprite.vertices;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSprite();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetSprite();
        }
    }

    void ChangeSprite()
    {
        Sprite sprite = spriteRenderer.sprite;
        Vector2[] spriteVertices = sprite.vertices;

        for (int i = 0; i < spriteVertices.Length; i++)
        {
            Debug.Log("before " + spriteVertices[i].x + ", " + spriteVertices[i].y);

            spriteVertices[i].x = Mathf.Clamp
            (
                sprite.vertices[i].x + sprite.bounds.center.x,
                0.0f, 
                sprite.rect.width
            );
            
            spriteVertices[i].y = Mathf.Clamp
            (
                sprite.vertices[i].y + sprite.bounds.center.y,
                0.0f, 
                sprite.rect.height
            );
            
            Debug.Log("after " + spriteVertices[i].x + ", " + spriteVertices[i].y);

        }
        sprite.OverrideGeometry(spriteVertices, sprite.triangles);
    }

    void ResetSprite()
    {
        Sprite sprite = spriteRenderer.sprite;
        sprite.OverrideGeometry(originalSpriteVertices, sprite.triangles);
    }
}
