using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public GameObject giftPrefab;

    [SerializeField] Rigidbody2D rb;
    public float jumpVel = 5;
    public bool onGround = false;


    private void Start()
    {
        GameManager.instance.OnGameEnd.AddListener(OnGameEnd);
    }

    private void Reset()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Drop();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }

    public void Jump()
    {
        if (onGround)
        {
            rb.velocity = Vector2.up * jumpVel;
        }
    }
    private void Drop()
    {
        GameObject gift = Instantiate(giftPrefab);
        gift.transform.position = this.transform.position;
    }

    private void OnGameEnd()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        rb.GetAttachedColliders(colliders);
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
        this.enabled = false;
    }
}
