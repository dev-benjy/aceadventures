using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_boss : MonoBehaviour
{ 
public GameObject projectile;
public Transform shootfrom;
public float shoottime;
float nextshoottime;
public int randomshot;
Animator ani;


// Start is called before the first frame update
void Start()
{
    ani = GetComponentInParent<Animator>();
    nextshoottime = 0f;

}

// Update is called once per frame

private void OnTriggerStay2D(Collider2D other)
{
    if (other.gameObject.tag == "Player" && nextshoottime < Time.time)
    {
        nextshoottime = Time.time + shoottime;
        if (Random.Range(0, 10) >= randomshot)
        {
            Instantiate(projectile, shootfrom.position, Quaternion.identity);
            if (ani != null)
            {
                ani.SetBool("Startattack", true);
            }
        }
    }
}
private void OnTriggerExit2D(Collider2D other)

{
    if (other.gameObject.tag == "Player" && ani != null)
    {
        ani.SetBool("Startattack", false);
    }
}
}

