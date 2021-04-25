using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class cage : MonoBehaviour
{
    public GameObject lid;
    public GameObject bar;
    public GameObject bar2;

    Image image;
    Image image2;
    GameMaster gm;
    public int how_many_enemies;
    public GameObject damage;
    public GameObject damage2;
    Enemydamage heal;
    Enemydamage heal2;
    public bool boss_fight_bar_disappears;
    [HideInInspector]
    public int kill_enemy;
    bool showhealth;
    int bar_check_before_boss;
    public GameObject boss_completed;
    public Transform message;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        lid.gameObject.SetActive(false);
        kill_enemy = 0;
        heal = damage.GetComponent<Enemydamage>();
        image = bar.GetComponent<Image>();
        if (damage2 != null && bar2 != null)
        {
            heal2 = damage2.GetComponent<Enemydamage>();
            image2 = bar2.GetComponent<Image>();
        }
    }
    private void Update()
    {
        bar_check_before_boss = how_many_enemies - 1;
       if (showhealth == true)
        {
            if (heal != null)
            {
                bar.transform.parent.gameObject.SetActive(true);
                image.fillAmount = (heal.stats.Health / 100f);
            }
            if(damage2 !=null && bar2 !=null)
            {
                bar2.transform.parent.gameObject.SetActive(true);
                image2.fillAmount = (heal2.stats.Health / 100f);
            }

        }
       else if(showhealth == false)
        {
            bar.transform.parent.gameObject.SetActive(false);
            if (bar2 != null)
            {
                bar2.transform.parent.gameObject.SetActive(false);
            }
        }
        if (gm.spawncheck)
        {
            lid.gameObject.SetActive(false);
            showhealth = false;
            if (damage != null)
            {
                heal.stats.Health = 100;
            }
            if(damage2 != null)
            {
                heal2.stats.Health = 100;
            }
            
        
        }
        number_based_destroy();
    }
    void number_based_destroy()
    {
        if(heal.localkill == true)
        {
            kill_enemy += 1;
            heal.localkill = false;
        }
        if(damage2 != null && bar2 != null && heal2.localkill == true)
        {
            kill_enemy += 1;
            heal2.localkill = false;
        }

        if(kill_enemy == how_many_enemies)
        {
            Instantiate(boss_completed,message.position,message.rotation);
            showhealth = false;
            Destroy(gameObject,0.20f);
        }
        if(kill_enemy == bar_check_before_boss && boss_fight_bar_disappears == true)
        {
            showhealth = false;
            

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            lid.gameObject.SetActive(true);
            showhealth = true;
           
        }

    }
    

}
