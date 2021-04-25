using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_shootfrom : MonoBehaviour
{
    enemycontrol enem;
    Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        enem = GetComponentInParent<enemycontrol>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion target = Quaternion.Euler(0, 180, 0);
        Quaternion reset = Quaternion.Euler(0, 0, 0);
        if (enem.direction<0)
        {
            transform.rotation = target;
        }
        else if(enem.direction >= 0)
        {
            transform.rotation = reset;
        }
    }
}
