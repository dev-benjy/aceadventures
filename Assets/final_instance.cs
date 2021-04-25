using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class final_instance : MonoBehaviour
{
    cage cagey;
    shoot_boss shoot_Boss;
   public GameObject final_king_wasp;
    public GameObject portal_fx;
    public Transform portal;
    public GameObject bossbar;
    public GameObject damage;
    Enemydamage enemydamage;
    Image image;
    GameMaster gm;
    bool fill_enemy = false;
    bool porty=false;
    public float portal_time;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        cagey = GetComponent<cage>();
        final_king_wasp.SetActive(false);
        shoot_Boss = GetComponent<shoot_boss>();
        shoot_Boss.projectile.SetActive(false);
        image = bossbar.GetComponent<Image>();
        final_king_wasp.transform.position = portal.position;
        enemydamage = damage.GetComponent<Enemydamage>();
        bossbar.transform.parent.gameObject.SetActive(false);
    }
    IEnumerator finalenemy()
    {
        yield return new WaitForSeconds(4f);
        bossbar.transform.parent.gameObject.SetActive(true);
        fill_enemy = true;
        yield return new WaitForSeconds(portal_time);
        porty = true;
        yield return new WaitForSeconds (3f);
        final_king_wasp.SetActive(true);
        shoot_Boss.projectile.SetActive(true);
        yield return new WaitForSeconds(1f);
        StopCoroutine("finalenemy");

    }
    // Update is called once per frame
    void Update()
    {
        if(cagey.kill_enemy == 2 && cagey.lid.activeSelf == true )
        {
            
            
               StartCoroutine("finalenemy");
   

            
        }
        if (fill_enemy == true)
        {
            image.fillAmount = (enemydamage.stats.Health / 100f);
        }
        if (porty == true)
        {
            portal_fx.gameObject.SetActive(true);
            porty = false;
        }
        if( gm.spawncheck == true)
        {
           
            bossbar.transform.parent.gameObject.SetActive(false);
            enemydamage.stats.Health = 100;
        }
        if(enemydamage.localkill == true)
        {
            cagey.kill_enemy += 1;
        }
    }
}
