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

    private void Start()
    {
        rb.velocity = Vector2.down * dropSpeed;
    }

    private void FixedUpdate()
    {
        if (rb.position.y < minY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.AddScore(1);
        Destroy(this.gameObject);
    }
}
