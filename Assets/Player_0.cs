using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class Player_0 : MonoBehaviour
{
    Animator anim;
    Animator shieldan;
    Animator enani;
    public Animator en_is_full;
    bool damaged;
    GameMaster gm;

    public GameObject en_barry;
    Image image;

    public bool shield = false;
    public float MaxshieldTime = 10f;//separate class
    public float ShieldTimer;
    public int MaxHealth;
    public int MaxEnergy;
    public GameObject shield_fx;
    public GameObject injurefx;
    public GameObject explosive;
    bool bullet_hit;
    bool slashed;
    public float recharge_per_second;
    public GameObject heart_fx;

    bool collect;
    public GameObject collect_fx;
    Shake_the_Camera camerashake;

    AudioSource audioz;
    public AudioClip[] injure_sounds;
    AudioClip injury;
    public AudioClip hit_by_insect;
    public AudioClip[] collector_sounds;
    AudioClip collect_sounds;
    public AudioClip energy_sound;
    public AudioClip crunch_sounds;

    [System.Serializable]
    public class PlayerStats
    {
        public int Health = 100;
        public float Energy = 100;

    }
    public PlayerStats playerStats = new PlayerStats();
    public int FallBoundary = -80;
    [HideInInspector]
    public SpriteRenderer sprite;
    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        shieldan = shield_fx.GetComponent<Animator>();
        playerStats.Health = MaxHealth;
        playerStats.Energy = MaxEnergy;
        image = en_barry.GetComponent<Image>();
        camerashake = GameObject.FindGameObjectWithTag("GM").GetComponent<Shake_the_Camera>();
        enani = en_barry.GetComponent<Animator>();
        audioz = GetComponent<AudioSource>();

    }
    public void Update()
    {
        if (image == null)
        {
            image = GameObject.FindGameObjectWithTag("energybar").GetComponent<Image>();
        }

        if (damaged == true)
        {
            StartCoroutine("damageflicker");

            damaged = false;
        }

        //check to prevent character from endless falling
        if (transform.position.y <= FallBoundary)
        { DamagePlayer(100); }

        audioscrambler();
        S_timer();
        Show_energy_Level();
        Recharge_Energy();
        particle_handler();
    }

    void audioscrambler()
    {

        injury = injure_sounds[Random.Range(0, injure_sounds.Length)];
        
    }
    void S_timer()
    {
        if (shield == true)
        {
            ShieldTimer -= Time.deltaTime;
            if (ShieldTimer <= 4f)
            {
                shieldan.SetBool("timeup", true);
            }
            if (ShieldTimer <= 0)
            {
                shieldan.SetBool("timeup", false);
                shield = false;
                shield_fx.SetActive(false);
            }
        }
    }
    public void DamagePlayer(int damage)
    {
        playerStats.Health -= damage;
        MMVibrationManager.Haptic(HapticTypes.Warning);
        if (playerStats.Health <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }
    void Show_energy_Level()
    {
        image.fillAmount = (playerStats.Energy / MaxHealth);
        if (playerStats.Energy <= 30)
        {
            enani.SetBool("valueless", true);
        }
        else if (playerStats.Energy > 20)
        {
            enani.SetBool("valueless", false);
        }

    }
    void Recharge_Energy()
    {
        if (playerStats.Health > MaxHealth)
        {
            playerStats.Health = MaxHealth;
        }

        if (playerStats.Energy < MaxEnergy)
        {
            playerStats.Energy += (recharge_per_second * Time.maximumDeltaTime);
            en_is_full.SetBool("enable", false);
            enani.SetBool("valuefull", false);
        }
        else if (playerStats.Energy >= MaxEnergy)
        {
            playerStats.Energy = MaxEnergy;
            enani.SetBool("valuefull", true);
            en_is_full.SetBool("enable", true);
        }

    }
    void particle_handler()
    {
        if (collect == true)
        {
            collect = false;
            collect_sounds = collector_sounds[Random.Range(0, collector_sounds.Length)];
            GameObject col = Instantiate(collect_fx, transform.position, transform.rotation) as GameObject;
            Debug.Log("added sound effect");
            audioz.PlayOneShot(collect_sounds, 0.7f);
            collect = false;
        }


        if (bullet_hit == true)
        {
            Instantiate(explosive, transform.position, transform.rotation);
            audioz.pitch = Random.Range(1.9f, 2.5f);
            audioz.PlayOneShot(hit_by_insect, 0.8f);
            bullet_hit = false;
        }
        else { audioz.pitch = 1f; }
        if (slashed == true)
        {
            GameObject sl = Instantiate(injurefx, transform.position, transform.rotation) as GameObject;
            Debug.Log("slashed anim is playing");
            audioz.pitch = 3;
            audioz.PlayOneShot(injury, 1f);
            slashed = false;
        }

    }
    IEnumerator damageflicker()
    {
        if (damaged == true)
        {
            int no_of_flickers = 5;
            float wait = 0.10f;
            for(int i = 0; i <= no_of_flickers; i++)
            {
                sprite.color = new Color(1f, 0f, 0f);
                yield return new WaitForSeconds(wait);
                sprite.color = new Color(1f, 1f, 1f);
                yield return new WaitForSeconds(wait);
                i++;
            }

        }
        
    }
    //Collider detection via Trigger to damage player based on three injury levels 
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "shield")
        {
            Destroy(trigger.gameObject);
            shield_fx.SetActive(true);
            ShieldTimer = MaxshieldTime;
            shield = true;
            collect = true;

        }
        if (trigger.gameObject.tag == "addlife")
        {

            Debug.Log("collided with coin send to bank");
            Destroy(trigger.gameObject);
            GameMaster.AddMoney(this);
            collect = true;
        }
        if (trigger.gameObject.tag == "addhealth")
        {

            Debug.Log("collided with coin send to bank");
            Destroy(trigger.gameObject);
            playerStats.Health += 10;
            collect = true;

            audioz.PlayOneShot(crunch_sounds, 0.6f);
            Instantiate(heart_fx, transform.position, transform.rotation);
        }
        if (trigger.gameObject.tag == "addstones")
        {

            Debug.Log("collided with stone send to bank");
            Destroy(trigger.gameObject);
            GameMaster.AddStones(this);
            collect = true;
        }
        if (trigger.gameObject.tag == "addenergy" && playerStats.Energy < MaxEnergy)
        {

            Debug.Log("collided with coin send to bank");
            Destroy(trigger.gameObject);
            playerStats.Energy += 20;

            audioz.PlayOneShot(energy_sound, 0.4f);
            collect = true;
        }

        if ((trigger.gameObject.tag == "enemyTier_1") && (shield == false))
        {

            damaged = true;
            DamagePlayer(2);
        }
        if (trigger.gameObject.tag == "enemyTier_2" && (shield == false))
        {

            damaged = true;
            DamagePlayer(10);
            slashed = true;
        }
        if (trigger.gameObject.tag == "enemyTier_3" && (shield == false))
        {

            damaged = true;
            DamagePlayer(20);
            slashed = true;
        }

    }
    private void OnTriggerExit2D(Collider2D trigger)
    {

        if (trigger.gameObject.tag == "enemyTier_1")
        {

            damaged = false;

        }
        if (trigger.gameObject.tag == "enemyTier_2")
        {

            damaged = false;

        }
        if (trigger.gameObject.tag == "enemyTier_3")
        {

            damaged = false;

        }

    }
    //Collision detection via collision2d to damge player based on 3 injury levels
    private void OnCollisionEnter2D(Collision2D trigger)
    {


        if (trigger.gameObject.tag == "enemybullets" && (shield == false))
        {

            damaged = true;
            DamagePlayer(6);
            bullet_hit = true;
            camerashake.Shake(0.03f, 0.1f);
        }
        if (trigger.gameObject.tag == "enemyrocks" && (shield == false))
        {

            damaged = true;
            DamagePlayer(6);
            bullet_hit = true;
            camerashake.Shake(0.04f, 0.1f);
        }
        else if (trigger.gameObject.tag == "enemyTier_2" && (shield == false))
        {

            damaged = true;
            slashed = true;
            DamagePlayer(15);
        }
        else if (trigger.gameObject.tag == "enemyTier_3" && (shield == false))
        {

            damaged = true;
            slashed = true;
            DamagePlayer(25);
        }

    }
    /* private void OnCollisionExit2D(Collision2D trigger)
     {
         if ((trigger.gameObject.tag == "enemybullets"|| trigger.gameObject.tag == "enemyTier_2"|| trigger.gameObject.tag == "enemyTier_3"))
         {
             damaged = false;
         }
     }*/

}


