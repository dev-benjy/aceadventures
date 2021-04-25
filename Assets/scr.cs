using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    public float Destroytime;
    public float maxforce;
    public float minforce;
    public float ProjectileAngle_A;
    public float torqueAngle;
    public bool do_you_want_camerashake;
    public float shakeamount= 0.1f;
    Shake_the_Camera camerashake;
    Rigidbody2D sporerb;
    AudioSource audi;
    public AudioClip clip;
    public float AudioPitch;
    public float AudioPitchDelta;
    // Start is called before the first frame update
    void Start()
    {
        sporerb = GetComponent<Rigidbody2D>();
        audi = GetComponent<AudioSource>();
        audi.pitch = Random.Range(AudioPitch, AudioPitchDelta);
        audi.PlayOneShot(clip, 0.7f);
        sporerb.AddForce(new Vector2(Random.Range(ProjectileAngle_A,-ProjectileAngle_A), Random.Range(minforce, maxforce)), ForceMode2D.Impulse);
        sporerb.AddTorque(Random.Range(-torqueAngle, torqueAngle));
        if (do_you_want_camerashake == true)
        {
            camerashake.Shake(shakeamount, 0.20f);
        }
    }

    private void Awake()
    {
        Destroy(gameObject, Destroytime);
        camerashake = GameObject.FindGameObjectWithTag("GM").GetComponent<Shake_the_Camera>();
    }
}
