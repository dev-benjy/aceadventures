using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Zoom : MonoBehaviour
{
    //boolean function to call Camera Zoom Logic
    private bool zoomMin;
    private bool zoomMed1;
    private bool zoomMed2;
    private bool zoomMax;
    private bool zoomEx;
    private bool boss;
    // Fix Float values of Camera Zoom Levels
    public float smallest = 45;
    public float small = 55;
    public float big = 60;
    public float bigger = 65;
    public float biggest = 70;
    public float smoothzoom = 5;

    float nulldamp = 0.23f;
    float normaldamp; 

    float nullreturnspeed = 3;
    float normalreturnspeed;

    float nulllookahead = 2;
    float normallookahead = 4;
    public GameObject friend;
    public bool dont_change_cam_values;
    enemy_follow follow;
    Camera2DFollow damp;


    //stop camera lag for moving platforms
    bool dampstop;

    // Start is called before the first frame update
    void Start()
    {
        damp = Camera.main.GetComponent<Camera2DFollow>();
        normaldamp = damp.damping;
        normallookahead = damp.lookAheadFactor;
        normalreturnspeed = damp.lookAheadReturnSpeed;
        follow = friend.GetComponent<enemy_follow>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraZoom();
        Nondamper();
        BossFight();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
            dampstop = true;

        }
        if (collision.gameObject.tag == "defaultcam")
        {
            
            boss = false;
            follow.Reset();

        }
        if (collision.gameObject.tag == "MediumZoom")
        {
            zoomMed1 = true;

        }
        if (collision.gameObject.tag == "MinZoom")
        {
            zoomMin = true;
        }
        if (collision.gameObject.tag == "MediumZoom2")
        {
            zoomMed2 = true;
        }
        if (collision.gameObject.tag == "MaxZoom")
        {
            zoomMax = true;
        }
        if (collision.gameObject.tag == "xtremeZoom")
        {
            zoomEx = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            dampstop = false;

        }
        if (collision.gameObject.tag == "MediumZoom")
        {
            zoomMed1 = false;

        }
        if (collision.gameObject.tag == "MinZoom")
        {
            zoomMin = false;
        }
        if (collision.gameObject.tag == "MediumZoom2")
        {
            zoomMed2 = false;
        }
        if (collision.gameObject.tag == "MaxZoom")
        {
            zoomMax = false;
        }
        if (collision.gameObject.tag == "xtremeZoom")
        {
            zoomEx = false;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="bossfight" && !dont_change_cam_values)
        {
            boss = true;
           
        }
        else
        {
            boss = false;
           
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bossfight")
        {
         
            follow.null_follow();

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bossfight")
        {
            follow.Reset();

        }
    }
    void CameraZoom()
    {
        if (zoomMin == true)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, smallest, Time.deltaTime * smoothzoom);
        }

        if (zoomMed1 == true)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, small, Time.deltaTime * smoothzoom);
        }
        if (zoomMed2 == true)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, big, Time.deltaTime * smoothzoom);
        }
        if (zoomMax == true)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, bigger, Time.deltaTime * smoothzoom);
        }
        if (zoomEx == true)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, biggest, Time.deltaTime * smoothzoom);
        }

    }
    void Nondamper()
    {
      
        if (dampstop == true)
        {
            damp.damping = nulldamp;
            damp.lookAheadReturnSpeed = nullreturnspeed;
            damp.lookAheadFactor = nulllookahead;
        }
        else if (dampstop == false)
        {
            damp.damping = normaldamp;
            damp.lookAheadReturnSpeed = normalreturnspeed;
            damp.lookAheadFactor = normallookahead;

        }
    }
    void BossFight()
    {
        if(boss == true)
        {
            damp.lookAheadFactor = 0f;
           
        }
        if(boss == false)
        {
            damp.lookAheadFactor = 4f;
        }
    }
}