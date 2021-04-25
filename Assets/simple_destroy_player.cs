using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_destroy_player : MonoBehaviour
{
    Player_0 player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_0>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            player.DamagePlayer(13000);
            Debug.Log("player killed via cliff fall");
        }
    }
}
