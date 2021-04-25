using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_music_changer : MonoBehaviour
{
    AudioSource audi;
    GameMaster gm;
    public AudioClip audiochange;
    public AudioClip audioresetto;
    public AudioClip victory;
    public GameObject enemy_null_check;
    public float fadetime;
    bool player_has_entered;
    bool play_once;
    // Start is called before the first frame update
    private void Start()
    {
       audi = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (enemy_null_check == null && victory !=null)
        {
            play_once = true;
            StartCoroutine(fade_in_out_exit());
            playonce();
        }
       
        if (player_has_entered == true && gm.spawncheck)
        {            
          StartCoroutine(fade_in_out_exit());
          Debug.Log("music has reset because player died");
          player_has_entered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "spareshoes" && enemy_null_check != null)
        {
            StartCoroutine(fade_in_out_enter());   
            audi.loop = true;
            player_has_entered = true;
            Debug.Log("player has reached the audio conversion");
        }
    }
    private void OnTriggerExit2D(Collider2D collision )
    {
        if(collision.gameObject.tag == "spareshoes")
        {
           // StartCoroutine(fade_in_out_exit());
            audi.loop = true;
            Debug.Log("player has reached the audio conversion");
            player_has_entered = false;
        }
    }

    public IEnumerator fade_in_out_enter()
    {
        float startvolume = audi.volume;
        yield return new WaitForSeconds(1.5f);
        while (audi.volume>0)
        {
            audi.volume -= startvolume * Time.deltaTime / fadetime;
            yield return null;
        }    
       
        audi.Stop();
        audi.clip = audiochange;
        audi.Play();  
        while (audi.volume < startvolume)
        {
            audi.volume += Time.deltaTime / fadetime;
            yield return null;
        }
        
        yield return new WaitForSeconds(2f);
        StopCoroutine(fade_in_out_enter());
    }
    public IEnumerator fade_in_out_exit()
    {
        float startvolume = audi.volume;
        yield return new WaitForSeconds(1.5f);
        while (audi.volume > 0)
        {
            audi.volume -= startvolume * Time.deltaTime / fadetime;
            yield return null;
        }

        audi.Stop();
        audi.clip = audioresetto;
        audi.Play();
        while (audi.volume < startvolume)
        {
            audi.volume += Time.deltaTime / fadetime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        StopCoroutine(fade_in_out_exit());
        
    }
    void playonce()
    {
        audi.PlayOneShot(victory);
        play_once = false;
        victory = null;
    }

}
