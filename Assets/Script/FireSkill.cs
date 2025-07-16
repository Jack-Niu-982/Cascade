using System.Collections;
using UnityEngine;

public class FireSkill : MonoBehaviour
{
    public static float FireExpoldeDamage = 20f;
    public GameObject ExplodePrefab;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float velo = 7.5f;

    private float direction = 1f;

    public void SetDirection(float dir)
    {
        direction = dir;
        if (direction < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        rb.velocity = new Vector2(velo * direction, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            velo = 0f;
            Instantiate(ExplodePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Boss"))
        {
            velo = 0f;
            Instantiate(ExplodePrefab, transform.position, Quaternion.identity);
            BossController f = collision.GetComponent<BossController>();
            f.Hurt(10, 0.7f);
            CameraShake.Instance.Shake(1f);
            Destroy(gameObject);
        }
    }
}
