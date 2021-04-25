using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COLLECT : MonoBehaviour
{
    private int spawncoins = 3;
    
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coin)
    {
        if (coin.gameObject.tag == "addlife")
        {
            
            spawncoins = spawncoins + 1;
            Debug.Log("collided with coin send to bank");
            Destroy(coin.gameObject);
        }
    }
   
}
