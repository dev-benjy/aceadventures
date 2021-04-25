using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_control_minion : MonoBehaviour
{ 
 enemycontrol enem;
    boss_control_minion boss;
    Enemydamage damage;
    public Transform stationary_point;
Transform trans;
Transform reset;
shoot_boss shoot;
bool station;
public float boss_shoot_mode;
public float boss_follow_mode;
public bool final_boss_flip = false;
SpriteRenderer sprite;
    cage cage;
// Start is called before the first frame update
void Start()
{
    enem = GetComponent<enemycontrol>();
    shoot = GetComponentInParent<shoot_boss>();
    damage = GetComponentInChildren<Enemydamage>();
    trans = GetComponent<Transform>();
    boss = GetComponent<boss_control_minion>();
    reset = enem.target;
    boss.StartCoroutine("bossfunction");
    sprite = GetComponent<SpriteRenderer>();

}
public IEnumerator bossfunction()
{
    enem.target = stationary_point;
    yield return new WaitForSeconds(4f);
    enem.enabled = false;
    station = true;
    enem.target = reset;
    yield return new WaitForSeconds(boss_shoot_mode);
    enem.enabled = true;
    station = false;
    yield return new WaitForSeconds(boss_follow_mode);
    StartCoroutine("bossfunction");
}

private void Update()
{
    if (station == true)
    {
        trans.position = stationary_point.position;

        shoot.enabled = true;
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
    else if (station == false)
    {
        shoot.enabled = false;
        shoot.projectile.SetActive(false);
    }
        if (damage.localkill == true)
        {
            cage.kill_enemy += 1;
        }

    }
}
   

