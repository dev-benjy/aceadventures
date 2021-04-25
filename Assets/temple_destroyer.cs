using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temple_destroyer : MonoBehaviour
{
    public GameObject finalboss;
    public GameObject explosions;
    public GameObject fireworks;
    public Transform fireworkspoint;
    Enemydamage damge;
    bool parry;
    
    // Start is called before the first frame update
    void Start()
    {
        damge = finalboss.GetComponent<Enemydamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finalboss == null)
        {
            parry = true;
        }
        if(parry == true)
        {
            Instantiate(explosions, transform.position, transform.rotation);
            Instantiate(fireworks, fireworkspoint.position, fireworkspoint.rotation);
            parry = false;
            Destroy(gameObject);
        }
    }
}
