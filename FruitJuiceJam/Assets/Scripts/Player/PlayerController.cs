using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public GameObject playerObject;

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Instantiate(playerObject, new Vector2(playerObject.transform.position.x, playerObject.transform.position.y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        if (rb.velocity.x > 0.1f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    void Animate()
    {
        animator.SetFloat("animMoveX", moveDirection.x);
        animator.SetFloat("animMoveY", moveDirection.y);
        animator.SetFloat("animMoveMagnitude", moveDirection.magnitude);
        animator.SetFloat("animLastMoveX", lastMoveDirection.x);
        animator.SetFloat("animLastMoveY", lastMoveDirection.y);
    }
}

