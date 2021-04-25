using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_txt_Client : MonoBehaviour
{
    public GameObject Secondary_icon;
    public GameObject iconchanger;
    public GameObject txt_manager;
    public bool [] ico;
    bool ico_display_match;
    bool ico_run;
    //array value determines which string elemnt to display
    public int element;
    GUI_text_manager texty;
    speak_icon_changer speaky;
    //Start is called before the first frame update
    void Start()
    {
        texty = txt_manager.GetComponent<GUI_text_manager>();
        speaky = iconchanger.GetComponent<speak_icon_changer>();
        if(ico.Length == texty.Text_display[element].each_text.Length)
        {
            ico_display_match = true; 
        }
        else
        {
            ico_display_match = false;
            Debug.LogError ("icons may not match quotes");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ico_display_match && ico_run)
        {
            speaky.Primary.SetActive(!ico[texty.n]);
            if (speaky.Secondary != null)
            {
                speaky.Secondary.SetActive(ico[texty.n]);
            }
                      
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        texty.start_txt = true;
        texty.str_element_to_dsp = element;
        ico_run = true;
        speaky.Secondary = Secondary_icon; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        speaky.Secondary = null;
        ico_run = false;
        Destroy(gameObject);
    }
}
