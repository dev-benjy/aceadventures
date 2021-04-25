using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;

public class GAME_MANAGER : MonoBehaviour
{
  //singleton creation..
    public static GAME_MANAGER Instance { get; private set; }
    private GameObject Player;
    GameMaster gamemaster;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {      
        gamemaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
     
    void Scene_ended()
    {
        if(gamemaster.levelStats.conquered)
        {
            
            switch (gamemaster.levelStats.scene_index)
            {
                case 0://create save file data with lvl and game stats, unlock level 2
                    break;
                case 1://
                    break;
            }
        }    
    }

    //errorreporting
    //save purchase data oncloud
}
