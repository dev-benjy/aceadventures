using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speak_icon_changer : MonoBehaviour
{
    public GameObject manager;
    GUI_text_manager gui_text;
    public GameObject  Secondary;  // enemy or friend he's talking to
    public GameObject Primary;             //default char pic
    // Start is called before the first frame update
    void Start()
    {
        gui_text = manager.GetComponent<GUI_text_manager>();
        Primary.SetActive(false);
        Secondary.SetActive(false);
    }

    void Update()
    {

    }
}
