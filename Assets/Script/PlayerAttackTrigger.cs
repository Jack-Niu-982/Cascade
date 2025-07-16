using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public float HurtForce = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController f = collision.GetComponent<EnemyController>();
            f.Hurt(3, HurtForce);
            CameraShake.Instance.Shake(0.5f);
        }
        else if (collision.CompareTag("Boss"))
        {
            BossController f = collision.GetComponent<BossController>();
            f.Hurt(3, HurtForce);
            CameraShake.Instance.Shake(0.5f);
        }
    }
}
