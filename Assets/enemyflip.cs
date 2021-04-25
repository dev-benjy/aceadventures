using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyflip : MonoBehaviour
{

    SpriteRenderer spriteflip;
    public bool flippedSprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteflip = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && (flippedSprite == false))
        {
            spriteflip.flipX = true;
        } 
        if(collision.gameObject.tag == "Player" && (flippedSprite == true))
        {
            spriteflip.flipX = false;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && (flippedSprite == false))
        {
            spriteflip.flipX = false;
        }
        if (collision.gameObject.tag == "Player" && (flippedSprite == true))
        {
            spriteflip.flipX = true;
        }
    }
    

    
}



