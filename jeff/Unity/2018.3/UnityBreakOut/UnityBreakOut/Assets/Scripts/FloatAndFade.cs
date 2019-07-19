using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAndFade : MonoBehaviour
{
    bool shouldFloat = false;
    float tickCount = 0;
    float fadeAmount = 0.02f;

    private void FixedUpdate()
    {
        if (shouldFloat)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, -1);
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g - fadeAmount, 
                GetComponent<SpriteRenderer>().color.b - fadeAmount, GetComponent<SpriteRenderer>().color.a - fadeAmount);
            tickCount++;

            if (tickCount >= 100)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        shouldFloat = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
