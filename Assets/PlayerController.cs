using System;
using System.Collections;
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
        StartCoroutine(IncreaseSpeed(5f));
    }

    void Update()
    {
        CheckIfGrounded();

        if (!isGrounded && !Input.GetKey(KeyCode.UpArrow))
        {
            FallFaster();
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }

    private void FallFaster()
    {
        rigid.AddForce(Vector2.down * 15f, ForceMode2D.Force);
    }

    void FixedUpdate()
    {
        MoveRight();
        MoveCamera();
        MoveObstacleCreator();
        MoveGround();
    }

    private IEnumerator IncreaseSpeed(float waitTime)
    {
        while (speed < 100f)
        {
            yield return new WaitForSeconds(waitTime);
            speed += 0.5f;
        }
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
