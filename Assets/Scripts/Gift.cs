using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gift : MonoBehaviour
{
    public Rigidbody2D rb;
    public float dropSpeed = 10;
    public float minY = -100;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.position += Vector2.down * dropSpeed * Time.fixedDeltaTime * Time.timeScale;

        if (rb.position.y < minY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.score += 1;
        Debug.Log(collision.gameObject);
        Destroy(this.gameObject);
    }
}