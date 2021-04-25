using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    Animator anim;
    GameMaster gm;
    AudioSource audi;
    public AudioClip clip;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audi = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && clip != null)
        {

            GameMaster.checkpost(this);
            anim.SetBool("burning", true);
            burn_ol();
          
           
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("burning", false);
            
        }
    }
    public void burn_ol()
    {
        audi.PlayOneShot(clip, 0.90f);
        clip = null;
    }
}
