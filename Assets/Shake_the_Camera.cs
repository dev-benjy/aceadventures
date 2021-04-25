using UnityEngine;
using System.Collections;

public class Shake_the_Camera : MonoBehaviour
{

    public Camera mainCam;
    float shakeAmount = 0;
    Vector3 camPos;
    float reset;
    float set = 70;
    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
        reset = mainCam.fieldOfView;
    }

    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    void DoShake()
    {
        if (shakeAmount > 0)
        {
            
            camPos = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;
            mainCam.fieldOfView = reset + 10;
            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        //mainCam.fieldOfView = reset - 10;
    }

}
