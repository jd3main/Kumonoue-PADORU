using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject giftPrefab;

    public float jumpVel = 5;
    public int onGround = 0;
    public float maxJumpingTime = 1;
    public float jumpingTime;
    public float dropCD = 1;

    private float lastDropTime = float.NegativeInfinity;


    private void Start()
    {
        GameManager.Instance.OnGameEnd.AddListener(OnGameEnd);
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

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            StopJumping();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround--;
    }

    public void Jump()
    {
        if (onGround > 0)
        {
            rb.velocity = Vector2.up * jumpVel;
        }
    }

    public void StopJumping()
    {
        rb.velocity = Vector2.zero;
    }

    private void Drop()
    {
        if (lastDropTime + dropCD <= Time.time)
        {
            GameObject gift = Instantiate(giftPrefab);
            gift.transform.position = this.transform.position;
            lastDropTime = Time.time;
        }
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
