using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image =UnityEngine.UI.Image;

public class healthbar : MonoBehaviour
{
    //player health bar
    Player_0 playerstat;
    Image bar;
    float nextTimeToSearch = 0f;
    public GameObject player;
    public float normal_health = 100;
    Animator anim;
    Animator flash;
    public GameObject flashy;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        playerstat = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_0>();
        anim = gameObject.GetComponent<Animator>();
        flash = flashy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbarfunc();
    }
    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                playerstat = searchResult.GetComponent<Player_0>();
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
    void healthbarfunc()
    {
        bar.fillAmount = (playerstat.playerStats.Health / 100f);
        if (playerstat == null)
        {
            FindPlayer();
        }
        if (playerstat.playerStats.Health == 100f)
        {
            anim.SetBool("valuefull", true);
            Invoke("stopblink", 1f);
        }


        if (playerstat.playerStats.Health <= 14)
        {
            anim.SetBool("valueless", true);
            flash.SetBool("healthcritical", true);
        }
        else
        {
            anim.SetBool("valueless", false);
            flash.SetBool("healthcritical", false);
        }
    }
    void stopblink()
    {
        anim.SetBool("valuefull", false);
    }
}
