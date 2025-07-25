using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
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
        if (collision.CompareTag("Player"))
        {
            Debug.Log("HurtPlayer");
            PlayerController f = collision.GetComponent<PlayerController>();
            f.Hurt(3, HurtForce, gameObject.transform.parent.gameObject);
        }
    }
}
