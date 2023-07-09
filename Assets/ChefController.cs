using UnityEngine;

public class ChefController : MonoBehaviour
{
    public PlayerController player;
    public Collider2D groundCollider;

    private Rigidbody2D rigid;
    private Collider2D collider2d;
    private float speed;
    private float jumpHeight;
    private bool isGrounded = true;
    private Vector2 startPosition;
    private float playerDistanceX;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        startPosition = transform.position;
        playerDistanceX = player.transform.position.x - transform.position.x;
    }

    void Update()
    {
        StayGrounded();
        OnlyComeCloserWhenPlayerIsStopped();
    }

    void FixedUpdate()
    {
        Jump();
        CheckIfGrounded();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var hasHitPlayer = collision.gameObject.CompareTag("Player");

        if (hasHitPlayer)
        {
            // TODO game over
            Debug.Log("Game Over");
        }
    }

    private void Jump()
    {
        var playerHeightToCauseJump = 1f;
        jumpHeight = player.jumpHeight;

        if (isGrounded && player.transform.position.y >= playerHeightToCauseJump)
        {
            rigid.gravityScale = 1;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);
        }
    }

    private void CheckIfGrounded()
    {
        isGrounded = collider2d.IsTouching(groundCollider);
    }

    private void StayGrounded()
    {
        if (transform.position.y < startPosition.y)
        {
            rigid.gravityScale = 0;
            transform.position = new Vector3(transform.position.x, startPosition.y, -1);
        }
    }

    private void OnlyComeCloserWhenPlayerIsStopped()
    {
        if (player.rigid.velocity.x <= player.speed / 5f)
        {
            playerDistanceX = player.transform.position.x - transform.position.x;
            MoveRight();
        }
        var x = player.transform.position.x - playerDistanceX;
        transform.position = new Vector3(x, transform.position.y, -1);
    }

    private void MoveRight()
    {
        speed = player.speed;
        rigid.velocity = new Vector2(speed, rigid.velocity.y);
    }
}
