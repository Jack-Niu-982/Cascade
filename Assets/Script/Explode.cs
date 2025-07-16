using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public static float BurnDamage = 3f;
    public AudioClip ExplodeAudio;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.PlayOneShot(ExplodeAudio);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
