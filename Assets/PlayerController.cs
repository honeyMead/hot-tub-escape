using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public Camera mainCamera;
    public GameObject ground;
    public GameObject obstacleCreator;

    public Rigidbody2D rigid { get; private set; }

    private Collider2D collider2d;
    private bool isGrounded = true;
    private float camDistanceX;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        camDistanceX = mainCamera.transform.position.x - transform.position.x;
    }

    void Update()
    {
        CheckIfGrounded();

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        MoveRight();
        MoveCamera();
        MoveObstacleCreator();
        MoveGround();
    }

    private void CheckIfGrounded()
    {
        var overlapColliders = new List<Collider2D>();
        var collidersCount = collider2d.OverlapCollider(new ContactFilter2D().NoFilter(), overlapColliders);
        isGrounded = collidersCount >= 1;
    }

    private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpHeight);
    }

    private void MoveRight()
    {
        rigid.velocity = new Vector2(speed, rigid.velocity.y);
    }

    private void MoveCamera()
    {
        var cameraPos = mainCamera.transform.position;
        mainCamera.transform.position = new Vector3(transform.position.x + camDistanceX, cameraPos.y, cameraPos.z);
    }

    private void MoveObstacleCreator()
    {
        var cameraPos = mainCamera.transform.position;
        obstacleCreator.transform.position = new Vector2(cameraPos.x, obstacleCreator.transform.position.y);
    }

    private void MoveGround()
    {
        var cameraPos = mainCamera.transform.position;
        ground.transform.position = new Vector2(cameraPos.x, ground.transform.position.y);
    }
}
