using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    TextMeshProUGUI text;
    TextMeshProUGUI text2;
    public GameObject lives_counter;
    public GameObject lives_counter_fx;
    public GameObject lives_counter_add_fx;
    public GameObject white_explode;
    bool lives_counter_add_check = false;   
    public GameObject gems_counter;
    public GameObject gems_counter_fx;
    bool gem_counter_add_check;
    Player_0 play;
    AudioSource audi;
    public AudioClip die;
    public AudioClip reborn;
    public Transform [] checkpoint_array;
    int latest_checkpoint;
   

    [System.Serializable]
    public class GameStats
    {
       public int Lives  = 3;
       public int Stones = 0;

      
    }
    public class LevelStats
    {
        public int scene_index;
        public bool conquered = false;
    }
    public GameStats gameStats = new GameStats();
    public LevelStats levelStats = new LevelStats();
    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();           
        }
        levelStats.scene_index = SceneManager.GetActiveScene().buildIndex; 
        text = lives_counter.GetComponent<TextMeshProUGUI>();
        text2 = gems_counter.GetComponent<TextMeshProUGUI>();
        text.text = gameStats.Lives.ToString();
        text2.text = gameStats.Stones.ToString();
        sprit = PlayerPrefab.GetComponent<SpriteRenderer>();
        play = PlayerPrefab.GetComponent<Player_0>();
        audi = GetComponent<AudioSource>();
        latest_checkpoint = checkpoint_array.Length - 1;
    }
    private void Update()
    {
        lives_gems_particle_controller();
    }

    void lives_gems_particle_controller()
    {
        text.text = gameStats.Lives.ToString();
        if (gameStats.Lives <= 0)
        {
            Debug.Log("reload scene!");
        }
        text2.text = gameStats.Stones.ToString();
        if (lives_counter_add_check == true)
        {
            lives_counter_add_fx.SetActive(true);
            StartCoroutine(waitforturnparticle());
            lives_counter_add_check = false;
        }
        if (gem_counter_add_check == true)
        {
            gems_counter_fx.SetActive(true);
            StartCoroutine(waitforgemparticle());
            gem_counter_add_check = false;
        }
    }
    IEnumerator waitforturnparticle()
    {
        yield return new WaitForSeconds(3.50f);
         lives_counter_add_fx.SetActive(false);
        StopCoroutine(waitforturnparticle()); 
    }
    IEnumerator waitforgemparticle()
    {
        yield return new WaitForSeconds(1.30f);
        gems_counter_fx.SetActive(false);
        StopCoroutine(waitforgemparticle());
    }

    public Transform ReSpawn;
    public GameObject PlayerPrefab;
    public Transform spawnprefab;
    public float spawndelay = 0f;
    [HideInInspector]
    public bool spawncheck;
    public bool allow_checkpost;
    SpriteRenderer sprit;

    public IEnumerator Respawnplayer()
    {
        if (spawncheck == true)
        {
            audi.PlayOneShot(die, 0.80f);
            yield return new WaitForSeconds(0.3f);
            PlayerPrefab.gameObject.SetActive(false);
            spawncheck = false;
            PlayerPrefab.transform.position = ReSpawn.position;
            gameStats.Lives = gameStats.Lives - 1;
            lives_counter_fx.SetActive(true);
            text.text = gameStats.Lives.ToString();
            yield return new WaitForSeconds(spawndelay);
            Transform clone = Instantiate(spawnprefab, ReSpawn.position, ReSpawn.rotation) as Transform;
            PlayerPrefab.SetActive(true);
            sprit.color = new Color(1f, 1f, 1f);
            audi.PlayOneShot(reborn, 1f);           
            play.playerStats.Health = play.MaxHealth;
            Destroy(clone.gameObject, 8f);
            
        }
       
    }  
    public static void KillPlayer(Player_0 player)
    {
        Instantiate(gm.white_explode, player.transform.position, player.transform.rotation);
        gm.spawncheck = true;
       // Instantiate(gm.white_explode, player.transform.position, player.transform.rotation);
        gm.StartCoroutine (gm.Respawnplayer());
        gm.lives_counter_fx.SetActive(false);

    }
    public static void KillEnemy (Enemydamage enemy)
    {
        
        Destroy(enemy.transform.parent.gameObject,0.30f);
    }
    public static void AddMoney (Player_0 player)
    {
        gm.gameStats.Lives = gm.gameStats.Lives + 1;
        gm.lives_counter_add_check = true;
    }
    public static void checkpost (checkpoint checkpoint)
    {
      
        gm.ReSpawn.position = checkpoint.transform.position;
    }
    public static void AddStones (Player_0 player)
    {
        gm.gameStats.Stones += 1;
        gm.gem_counter_add_check = true;
    }
    public static void levelcomplete (final_it finalbit)
    {

        gm.levelStats.conquered = true;
        gm.StartCoroutine(gm.Level_count_and_disp());
        //TRIGGER END OF LEVEL COUNTER.. FOLLOWED WITH BUTTON TO MENU SCENE
    }
    IEnumerator Level_count_and_disp()
    {

        Debug.Log("initiating count animation sequence");
        yield return null;
    }
   
}
