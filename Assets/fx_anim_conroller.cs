using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fx_anim_conroller : MonoBehaviour
{
    Animator anim;
    Animator calim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        calim = GetComponent < Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("Shockwave"))
        {
            calim.SetBool("shock", true);
        }
        else if(!anim.GetBool("Shockwave"))
        {
            calim.SetBool("shock", false);
        }
    }
}
