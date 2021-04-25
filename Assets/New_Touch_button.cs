using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class New_Touch_button : MonoBehaviour
{
    
    RectTransform rect;
    Camera camera;
    Animator anim;
    AudioSource audi;
    HapticTypes hapt;
    public enum Vib_Intensity { light, heavy, heavier}    
    public Vib_Intensity viber;
    public AudioClip Clip;
    public bool Vibrate;
    [HideInInspector]
    public bool rt_id;
    public bool button;
    // Start is called before the first frame update
    void Start()
    {
        viber = Vib_Intensity.light;
        switch (viber)
        {
            case Vib_Intensity.light:
                hapt = HapticTypes.LightImpact;
                break;
            case Vib_Intensity.heavy:
                hapt = HapticTypes.Selection;
                break;
            case Vib_Intensity.heavier:
                hapt = HapticTypes.MediumImpact;
                break;
        }
        camera = Camera.main;
        rect = this.GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        audi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        int touchcount = Input.touchCount;
        
        for (int i = 0; i < touchcount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (RectTransformUtility.RectangleContainsScreenPoint(rect, touch.position, camera))
            {

                if (touch.phase == TouchPhase.Began)
                {                  
                    button = true;
                    if(Vibrate)
                    {
                        MMVibrationManager.Haptic(hapt);
                    }
                    if (Clip != null)
                    {
                        audi.PlayOneShot(Clip);
                    }
                }
                else { button = false; }
                rt_id = true;
            }
            else { rt_id = false; }
          
        }
        
        anim.SetBool("button_anim", button);

    }
    
}
