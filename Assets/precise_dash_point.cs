using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class precise_dash_point : MonoBehaviour
{
    Transform tranny;
    AdamControl adam;
    // Start is called before the first frame update
    void Start()
    {
        tranny = GetComponent<Transform>();
        adam = GetComponentInParent<AdamControl>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion target = Quaternion.Euler(0, 180, 0);
        Quaternion reset = Quaternion.Euler(0, 0, 0);
        if(adam.move < 0)
        {
            tranny.rotation = target;
        }
        else if(adam.move >= 0)
        {
            tranny.rotation = reset;
        }
    }
}
