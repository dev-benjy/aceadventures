using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sbr : MonoBehaviour
{
    public float Destroytime;
    public float maxforce;
    public float minforce;
    public float ProjectileAngle_B;
    public float torqueAngle;
    Rigidbody2D sporerb;
    AudioSource audi;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        audi = GetComponent<AudioSource>();
        audi.pitch = Random.Range(0.90f, 1.3f);
        audi.PlayOneShot(clip);
        sporerb = GetComponent<Rigidbody2D>();
        sporerb.AddForce(new Vector2(ProjectileAngle_B, Random.Range(minforce, maxforce)), ForceMode2D.Impulse);
        sporerb.AddTorque(Random.Range(-torqueAngle, torqueAngle));
        
    }

    private void Awake()
    {
        Destroy(gameObject, Destroytime);
    }
}
