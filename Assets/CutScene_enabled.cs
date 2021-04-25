using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene_enabled : MonoBehaviour
{
    public GameObject let;
    GUI_text_manager texty;
    //int x;
    public int serial;
    Animator anim;
    bool cut_enable;
    // Start is called before the first frame update
    void Start()
    {
        texty = let.GetComponent<GUI_text_manager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(texty.str_element_to_dsp == serial && texty.start_txt)
        {
            cut_enable = true;
        }
        else if( texty.cut_scene_finished)
        {
            cut_enable = false;
        }
        if(cut_enable)
        {
            anim.SetInteger("n", texty.n);
        }
        anim.SetBool("cut_enable", cut_enable);
    }
}
