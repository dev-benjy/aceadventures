using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class final_it : MonoBehaviour
{
    public GameObject flag;
    public GameObject light;

    public GameObject level_completed;
    public Transform level_complete_location;
    private Animator anim;
    AudioSource audi;
    public AudioClip flag_hoist;
    GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        flag.SetActive(false);
        anim = light.GetComponent<Animator>();
        audi = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        flag.SetActive(true);
        anim.SetBool("burning", true);
        Instantiate(level_completed, level_complete_location.position, level_complete_location.rotation);
        GameMaster.levelcomplete(this);    
        if (flag_hoist != null)
        {
            audi.PlayOneShot(flag_hoist, 0.9f);
            flag_hoist = null;
        }
    }
}
