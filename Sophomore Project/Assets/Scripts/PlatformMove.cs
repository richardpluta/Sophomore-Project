using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private float dirX;
    private bool moveRight = true;
    private bool moveUp = true;

    public GameObject player;
    public bool MoveHorizontal;
    public bool MoveVertical;
    public float moveSpeed = 3f;
    public float distanceHorizontal;
    public float distanceVertical;


    // Update is called once per frame
    void Update()
    {
        if(MoveHorizontal)
            Horizontal();

        if(MoveVertical)
            Vertical();
    }

    void Horizontal()
    {
        if (transform.position.x > distanceHorizontal)
            moveRight = false;
        if (transform.position.x < -distanceHorizontal)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
    }

    private void Vertical()
    {
        if (transform.position.y > distanceVertical)
            moveUp = false;
        if (transform.position.y < -distanceVertical)
            moveUp = true;

        if (moveUp)
            transform.position = new Vector2(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime));
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}
