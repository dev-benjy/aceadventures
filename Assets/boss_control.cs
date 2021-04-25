using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_control : MonoBehaviour
{
    enemycontrol enem;
    cage cage;
    public Transform stationary_point;
    Transform trans;
    Transform reset;
    shoot shoot;
    bool station;
    public float boss_shoot_mode;
    public float boss_follow_mode;
    public bool final_boss_flip = false;
    bool mini_start_coroutine;
    SpriteRenderer sprite;
    AudioSource audi;
    public AudioClip chase;
    // Start is called before the first frame update
    void Start()
    {
        enem = GetComponent<enemycontrol>();
        shoot = GetComponentInParent<shoot>();
        cage = GetComponentInParent<cage>();
        trans = GetComponent<Transform>();
        reset = enem.target;
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine("bossfunction");
        audi = GetComponent<AudioSource>();
    }
   public IEnumerator bossfunction()
    {
        enem.target = stationary_point;
        yield return new WaitForSeconds(4f);
        enem.enabled = false;
        station = true;
        enem.target = reset;
        yield return new WaitForSeconds(boss_shoot_mode);
        audi.PlayOneShot(chase);
        audi.loop = true;
        enem.enabled = true;
        station = false;
        yield return new WaitForSeconds(boss_follow_mode);
        StartCoroutine("bossfunction");
    }

    private void Update()
    {
        if(station == true)
        {
            trans.position = stationary_point.position;
            audi.Stop();
            audi.loop = false;
            shoot.enabled = true;
            if (shoot.projectileB != null)
            {
                shoot.projectileB.SetActive(true);
            }
            shoot.projectile.SetActive(true);
            if (final_boss_flip == false)
            {
                sprite.flipX = false;
            }
            else if(final_boss_flip == true)
            {
                sprite.flipX = true;
            }
        }
         if(station == false)
         {

            shoot.enabled = false;
            shoot.projectile.SetActive(false);
           if(shoot.projectileB != null)
           {
                shoot.projectileB.SetActive(false);
           }
         }

    }
}
   

