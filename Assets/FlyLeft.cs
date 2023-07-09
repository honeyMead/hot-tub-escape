using UnityEngine;

public class FlyLeft : MonoBehaviour
{
    public float speed;
    private Transform endScreen;

    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        endScreen = GameObject.Find("Chef").GetComponent<ChefController>().endScreen;
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        rigid.rotation += 10f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var hasHitPlayer = collision.gameObject.CompareTag("Player");

        if (hasHitPlayer)
        {
            endScreen.gameObject.SetActive(true);
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}
