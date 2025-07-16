using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WaterSkill : MonoBehaviour
{
    public float Speed = 13f;
    public float KnockBackForce = 3f;
    public float Duration = 20f;
    public static float WaterDamage = 0f;
    public AudioSource AudioSource;
    public AudioClip WaterAudio;
    private Rigidbody2D rb;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, Duration);
        AudioSource.PlayOneShot(WaterAudio);
        UnityEngine.Debug.Log("Water Audio");
    }
    private void Update()
    {
        rb.velocity = new Vector2(Speed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponent<EnemyController>().KnockBack(KnockBackForce);
        }
    }
}

