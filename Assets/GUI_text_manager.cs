using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUI_text_manager : MonoBehaviour
{
   
    public GameObject canvas_pause;
    public GameObject tap;
    bool next_text;   
    string set_string;
    [System.Serializable]
   
    public class All_text_container
    {
        public string[] each_text; 
    }
    public All_text_container[] Text_display;   
    public float txt_speed;
    int main_array_size;
    int sub_array_size;   
    TextMeshProUGUI gui;
    Animator anim;
    Animator plate;
    AdamControl player;
    [HideInInspector]
    public bool cut_scene_finished = false;
    public int n;
    public int str_element_to_dsp;
    public bool start_txt;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AdamControl>();
        gui = GetComponent<TextMeshProUGUI>();
        main_array_size = Text_display.Length;
        anim = GetComponentInChildren<Animator>();
        plate = tap.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(main_array_size);
          canvaschain();
    }
    void canvaschain()
    {
             
        if (start_txt && str_element_to_dsp <= main_array_size)
        {
            player.neutralize = true;
            canvas_pause.SetActive(false);
            plate.SetBool("dropdown", true);
            n = 0;
            StartCoroutine(typefunc());
            sub_array_size = Text_display[str_element_to_dsp].each_text.Length;
            Debug.Log(sub_array_size);
            start_txt = false;
        }
        if(next_text)
        {
            anim.SetBool("flash", true);

             if (Input.touchCount > 0)
             {
                 Touch touch = Input.GetTouch(0);
                 if (touch.phase == TouchPhase.Began)
                 {
                     taptocontinue();
                     next_text = false;
                 }
             }
           /* if(Input.GetKeyDown(KeyCode.Space))
            {
                taptocontinue();
                next_text = false;
            }*/
        }
    }
    IEnumerator typefunc()
    {
        set_string = Text_display[str_element_to_dsp].each_text[n];
        Text_display[str_element_to_dsp].each_text[n] = "";
        yield return new WaitForSeconds(1.10f);
        foreach (char c in set_string)
        {
            gui.text += c;
            yield return new WaitForSeconds(txt_speed);
        }
        yield return new WaitForSeconds(0.01f);
        next_text = true;
        StopCoroutine(typefunc());
    }
    void taptocontinue()
    {
        gui.text = "";
        anim.SetBool("flash", false);
        if ((n+1) < sub_array_size)
        {
            n += 1 ;
            StartCoroutine(typefunc());
            cut_scene_finished = false;
        }
        else if((n+1) >= sub_array_size)
        {
            plate.SetBool("dropdown", false);
            Invoke("activate_controls", 1.03f);
            cut_scene_finished = true;
        }
    }
    void activate_controls()
    {
        canvas_pause.SetActive(true);
        player.neutralize = false;
    }
}//write to print file


