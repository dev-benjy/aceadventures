using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemydamage : MonoBehaviour {
    public int shock_kill = 40;
    public int dash_kill = 20;
    public GameObject dash_injure;
    public GameObject injure_particle_fx;
    public float shakeamount = 0.30f;
    bool shockfx;
    bool dashfx;
    public GameObject explode;
    public GameObject explosion_fx;
    [HideInInspector]
    public bool localkill = false;
    Shake_the_Camera camerashake;
    AudioSource audi;
    public AudioClip dash_sound;
    public AudioClip die_sound;

    [System.Serializable]
    public class EnemyStats
    {
        public int Health = 100;
    }

    public EnemyStats stats = new EnemyStats();
    private void Start()
    {
        camerashake = GameObject.FindGameObjectWithTag("GM").GetComponent<Shake_the_Camera>();
        audi = GetComponentInParent<AudioSource>();
    }
    private void Update()
    {
        if(dashfx == true)
        {
            audi.pitch = Random.Range(1f, 1.3f);
            audi.PlayOneShot(dash_sound, 0.80f);
            Instantiate(dash_injure, transform.position, transform.rotation);
            Instantiate(injure_particle_fx, transform.position, transform.rotation);
            dashfx = false;
        }
        if (shockfx == true)
        {
            audi.PlayOneShot(dash_sound, 0.80f);
            Instantiate(injure_particle_fx, transform.position, transform.rotation);
            shockfx = false;
        }
    }
    public void DamageEnemy(int damage)
    {
        stats.Health -= damage;
        if (stats.Health <= 0)
        {
            GameMaster.KillEnemy(this);
            localkill = true;
            camerashake.Shake(shakeamount, 0.50f);
            Debug.Log("enemy killed");
        }
        if(localkill == true)
        {
            if(explode != null)
            {
                Instantiate(explode, transform.position, transform.rotation);
            }            
            Instantiate(explosion_fx,transform.position,transform.rotation);
            audi.PlayOneShot(die_sound);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "shock_attack")
        {
            DamageEnemy(shock_kill);
            shockfx = true;
        
        }
        else if(collision.gameObject.tag == "dash_attack")
        {
            DamageEnemy(dash_kill);
            dashfx = true;


        }
    }
}
