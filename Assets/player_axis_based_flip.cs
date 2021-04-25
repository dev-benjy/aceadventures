using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_axis_based_flip : MonoBehaviour
{
    AdamControl adam;
    Color sprite;
    public bool Fade_Out;
    public float fadetime;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().color;
        adam = GameObject.FindGameObjectWithTag("Player").GetComponent<AdamControl>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Fade_Out== true)
        {
            sprite.a = Mathf.Lerp(sprite.a, 0f, fadetime * Time.deltaTime);

        }
    }
}
