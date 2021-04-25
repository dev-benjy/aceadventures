using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_sound_manager : MonoBehaviour
{
    public AudioClip mainsound;
    public AudioClip secondary_sound;
    AudioSource audi;
    private void Start()
    {
        audi = GetComponent<AudioSource>();
    }
    public void frognoise()
    {
        audi.PlayOneShot(mainsound,1f);
    }
    public void secondarynoise()
    {
        audi.PlayOneShot(secondary_sound, 1f);
    }
}
