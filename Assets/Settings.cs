using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class Settings : MonoBehaviour
{
   //general references
    private GameObject GM;
    private GameObject Player;
    AdamControl ada;
    GameMaster gamemaster;
    AudioSource g_music;
    AudioSource player_sound;
    AudioSource[] other_sounds;

    //fixed audio levels
    public float playerdefault = 0.95f;
    public float musicdefault = 0.20f;
    //volume sliders
    Slider playerslider;
    Slider musicslider;
    Slider masterslider;

    //difficulty
    Enemydamage[] all_enem;
    [System.Serializable]
    public class SettingStats
    {
        //toggle vibration on or off
        public bool Vib_Off = true;

        //increase or decrease difficulty by adding or subracting delta values to damgae amount caused by player to enemies
        public class difficulty
        {
            public int shock_damage_delta = 10;
            public int dash_damage_delta = 10;
            public int jump_delta = 2;
        }
        public bool diff;
        public difficulty Difficulty = new difficulty();


        //volume values for all audio sources
        public class fixed_volume
        {
            public float set;
            public bool set_default;
        }
        public fixed_volume fixed_Volume_player = new fixed_volume();
        public fixed_volume fixed_Volume_music = new fixed_volume();
        public fixed_volume fixed_Volume_master = new fixed_volume();
    }
    public SettingStats settingStats = new SettingStats(); // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ada = Player.GetComponent<AdamControl>();
        all_enem = FindObjectsOfType<Enemydamage>();
        Get_Volume_Components();
        Vib_check();
    }

    //All vibration toggle settings
    void Vib_check()
    {
        if (settingStats.Vib_Off)
        {
            ViberManageOff();
        }
        else
        {
            ViberManageOn();
        }

    }
    public void ViberManageOn()
    {
        settingStats.Vib_Off = false;

        if (MMVibrationManager.Android())
        {
            MMVibrationManager.AndroidCancelVibrations();
        }
        else if (MMVibrationManager.iOS())
        {
            MMVibrationManager.iOSReleaseHaptics();
        }
        else
        {
            Debug.LogError("unable to initialize vibration");
            return;
        }

    }
    public void ViberManageOff()
    {

        settingStats.Vib_Off = true;
        if (MMVibrationManager.Android())
        {
            MMVibrationManager.AndroidVibrate(20);
        }
        else if (MMVibrationManager.iOS())
        {
            MMVibrationManager.iOSInitializeHaptics();
        }
        else
        {
            Debug.LogError("unable to stop vibration");
            return;
        }

    }

    //all sound settingStats
    void Get_Volume_Components()
    {
        playerslider = GameObject.Find("playervolslide").GetComponent<Slider>();
        musicslider = GameObject.Find("musicvolslide").GetComponent<Slider>();
        masterslider = GameObject.Find("mastervolslide").GetComponent<Slider>();
        playerslider.value = settingStats.fixed_Volume_player.set;//set slider length on previously set audio levels
        musicslider.value = settingStats.fixed_Volume_music.set;
        masterslider.value = settingStats.fixed_Volume_master.set;
        player_sound = Player.GetComponent<AudioSource>();
        other_sounds = FindObjectsOfType<AudioSource>();
        g_music = GM.GetComponent<AudioSource>();
    }
    public void Sound_Setting()
    {
        // ability to mute or adjust voolume between the player, music, or everything else.
        All_volume();
        BG_volume();
        Player_volume();

    }
    void BG_volume()
    {
        settingStats.fixed_Volume_music.set = musicslider.value; //add fixed ranges by modifying this statement
        g_music.volume = settingStats.fixed_Volume_music.set;
    }
    void Player_volume()
    {
        settingStats.fixed_Volume_player.set = playerslider.value; //add fixed ranges by modifying this statement
        player_sound.volume = settingStats.fixed_Volume_player.set;
    }
    void All_volume()
    {
        settingStats.fixed_Volume_master.set = masterslider.value; //add fixed ranges by modifying this statement
        foreach (AudioSource source in other_sounds)
        {
            source.volume = settingStats.fixed_Volume_master.set;
        }
    }
    public void Default_volume_set()
    {
        playerslider.interactable = musicslider.interactable = false;
        settingStats.fixed_Volume_music.set = 1f;
        settingStats.fixed_Volume_player.set = 1f;
    }

    void Difficulty_Lvl_Toggle()
    {
        foreach (Enemydamage enemy in all_enem)
        {
            if(settingStats.diff == true)
            {
                enemy.dash_kill -= settingStats.Difficulty.dash_damage_delta;
                enemy.shock_kill -= settingStats.Difficulty.shock_damage_delta;
                ada.jumpForce -= settingStats.Difficulty.jump_delta; 
            }
            else if(settingStats.diff == false)
            {
                enemy.dash_kill += settingStats.Difficulty.dash_damage_delta;
                enemy.shock_kill += settingStats.Difficulty.shock_damage_delta;
                ada.jumpForce += settingStats.Difficulty.jump_delta;
            }
        }
    }
    public void Set_Diff_Hard()
    {
        settingStats.diff = true;
        Difficulty_Lvl_Toggle();
    }
    public void Set_Diff_Easy()
    {
        settingStats.diff = false;
        Difficulty_Lvl_Toggle();
    }
    public void Set_Diff_Default()
    {
        Debug.Log("reset player stats and enemies as well(not yet)");



    }

   /* public void Default_Difficulty()
    {
        if (settingStats.diff == true)
        {

        }
    }*/
}
