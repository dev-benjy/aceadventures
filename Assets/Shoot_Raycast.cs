using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Raycast : MonoBehaviour
{
    bool Raygo;
    public Transform firepoint;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Raygo == true)
        {
            Raygoshoot();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Raygo = true;
        }
    }
    void Raygoshoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firepoint.position, firepoint.right);
        Debug.Log(hitInfo);
        if (hitInfo  )
        {
            Instantiate(projectile, firepoint.position, firepoint.rotation);

            projectile.transform.Translate(hitInfo.rigidbody.position);

            Debug.DrawLine(firepoint.position, hitInfo.rigidbody.position);
        }
       
    }
}

