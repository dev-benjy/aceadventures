using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class AdamControl : MonoBehaviour
{
    public Axis_button_input Joycon;
    public New_Touch_button JUMP;
    public New_Touch_button ATTACK;
   
    //how fast player can move
    public float topspeed = 10.3f;
    float speed;
    //how fast the player falls for shockwave attack
    public float fallspeed = 5f;
    public bool sliding = false;
    float slidetimer;
    public float maxslidetime = 1f;
    public float Dashspeed = 100f;
    bool shocktimer = false;
    bool noshock;

    //get reference to animator
    Animator anim;
    //reference to rigidbody
    Rigidbody2D reggie;
    //when not grounded
    bool grounded = false;
    public float move;
    public bool neutralize;
    //variable to check double jump 
    bool doublejump = false;
    public GameObject dash;
    public Transform dashpoint;
    public GameObject shock;
    public GameObject shock_particle_fx;
    public Transform shockpoint;
    CapsuleCollider2D enable_dash;
    CapsuleCollider2D enable_shock;
    Player_0 playerStat;
    public GameObject charge_fx;
    bool chargefx;
    //dash animation                          

    //transform at character feet to see if he is touching the ground
    public Transform groundCheck;
    //how big the circle is going to be when we check the distance with the ground
    float groundRadius = 0.2f;
    //what layer is actually the ground?
    public LayerMask whatisground;
    public float GroundRadius
    {
        get
        {
            return groundRadius;
        }

        set
        {
            groundRadius = value;
        }
    }

    //force oof the jump
    public float jumpForce = 700f;
    Shake_the_Camera camerashake;
    public float shakeamount;

    AudioSource audi;
    //soundfx
    public AudioClip walk_sound;
    public AudioClip run_sound;
    public AudioClip[] random_jump;
    AudioClip jump_twice;
    public AudioClip land;
    public AudioClip dash_sound;
    public AudioClip shock_sound;
    public AudioClip charging;

    void Start()
    {
        camerashake = GameObject.FindGameObjectWithTag("GM").GetComponent<Shake_the_Camera>();
        enable_dash = GameObject.FindGameObjectWithTag("dash_attack").GetComponent<CapsuleCollider2D>();
        enable_shock = GameObject.FindGameObjectWithTag("shock_attack").GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        reggie = GetComponent<Rigidbody2D>();
        speed = topspeed;
        playerStat = GetComponent<Player_0>();
        audi = GetComponent<AudioSource>();
    }
    //physics is fixed update

    public void FixedUpdate()
    {
        //reset double jump
        {
            if (grounded)
                doublejump = false;
        }
        //move = Joycon.Axis;
        move = Input.GetAxis("Horizontal"); // remove after debug.
        
        if(neutralize)
        {
            move = 0f;
        }
        else if(!neutralize)
        {
            move = Input.GetAxis("Horizontal");
           // move = Joycon.Axis;
        }
 
        //bool statement to find if grounf radius touched what is ground mask
         grounded = Physics2D.OverlapCircle(groundCheck.position, GroundRadius, whatisground);

        //tell animator we are on ground from above statement
        anim.SetBool("Ground", grounded);

        //get how fast we are moving from the rigid body
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


        //add velocity to the rigid body in move direction* our speed
        reggie.velocity = new Vector2(move * topspeed, reggie.velocity.y);


        anim.SetFloat("speed", Mathf.Abs(move));


        //if we are not facing the negative direction and not facing right flip
        //as of 5.3 unity update flip is native in sprite renderer

        if (move > 0)
        { GetComponent<SpriteRenderer>().flipX = false; }
        else if (move < 0)
        { GetComponent<SpriteRenderer>().flipX = true; }
    }
    void Update()
    {

        GetInputMovement();
        sound_randomer();

    }
    public void sound_randomer()
    {
        jump_twice = random_jump[Random.Range(0, random_jump.Length)];       
    }
    public void GetInputMovement()
    {
        bool noidle;
        if ((grounded || !doublejump) && ( /*JUMP.button||*/ Input.GetKeyDown(KeyCode.UpArrow)))
        {
            //nojump
            anim.SetBool("Ground", false);
            noidle = false;
            //add jumpforce to the yaxis of the rigid body
            reggie.AddForce(new Vector2(0, jumpForce));
            neutralize = false;
            audi.PlayOneShot(jump_twice, 0.1f);
            if (!doublejump && !grounded)
            {               
                doublejump = true;                
            }
        }

        if ((/*ATTACK.button ||*/ Input.GetKeyDown(KeyCode.LeftShift)) && doublejump && shocktimer == false && playerStat.playerStats.Energy >= 35)
        {
            //perform shockwave attack
            playerStat.playerStats.Energy -= 40;
            reggie.velocity += Vector2.up * Physics2D.gravity.y * (fallspeed - 1) * Time.deltaTime;
            anim.SetBool("Shockwave", true);
            neutralize = true;
            shocktimer = true;
           
        }
        if (grounded)
        {
            
            if (shocktimer == true)
            {
                enable_shock.enabled = true;
                Instantiate(shock, shockpoint.position, shockpoint.rotation);
                Instantiate(shock_particle_fx, groundCheck.position, groundCheck.rotation);
                shocktimer = false;
                camerashake.Shake(shakeamount, 1f);//camerashake
                audi.PlayOneShot(shock_sound, 0.85f);//explosion audio
                Invoke("disable_shock", 0.7f);
                MMVibrationManager.Haptic(HapticTypes.Failure);
            }
        }
       
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            anim.SetBool("Shockwave", false);
            charge_fx.SetActive(false);
            topspeed = speed;
            noidle = true;
            enable_dash.enabled = false;
        }
        else { noidle = false; }

        if ((/*ATTACK.button ||*/ Input.GetKeyDown(KeyCode.LeftShift)) && grounded && (sliding == true) && (reggie.velocity.y > -1 && reggie.velocity.y < 1) && playerStat.playerStats.Energy >= 12 && noidle == false && (reggie.velocity.x <= -(topspeed-3.3f) || reggie.velocity.x >= (topspeed-3.3f)))
        {
            topspeed = 6f;
            //chargefx = true;
            noshock = true;

            audi.PlayOneShot(charging, 0.20f);
            charge_fx.SetActive(true);
            Invoke("waitdash", 0.22f);
           if(chargefx)
           {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                enable_dash.enabled = true;
                camerashake.Shake(shakeamount, 0.12f);
                if (move > 0)
                {
                    reggie.MovePosition(transform.position + transform.right * Dashspeed);
                    Instantiate(dash, dashpoint.position, dashpoint.rotation);

                }
                else if (move < 0)
                {
                    reggie.MovePosition(transform.position + transform.right * -Dashspeed);
                    Instantiate(dash, dashpoint.position, dashpoint.rotation);
                }
                playerStat.playerStats.Energy -= 12f;
                sliding = false;
                slidetimer = maxslidetime;

                noshock = false;
                chargefx = false;
                charge_fx.SetActive(false);
                anim.SetBool("IsSliding", true);
                audi.PlayOneShot(dash_sound, 0.4f);
                Invoke("disable_dash", 0.7f);
           }
        }
        
       /* if ((/*!ATTACK.button || Input.GetKeyUp(KeyCode.LeftShift)) && grounded && (sliding == true) && (reggie.velocity.y > -1 && reggie.velocity.y < 1) && playerStat.playerStats.Energy >= 12 && noidle == false && chargefx == true && reggie.velocity.x >= (topspeed-4.3f))
        {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                enable_dash.enabled = true;
                camerashake.Shake(shakeamount, 0.12f);
                if (move > 0)
                {
                    reggie.MovePosition(transform.position + transform.right * Dashspeed);
                    Instantiate(dash, dashpoint.position, dashpoint.rotation);

                }
                else if (move < 0)
                {
                    reggie.MovePosition(transform.position + transform.right * -Dashspeed);
                    Instantiate(dash, dashpoint.position, dashpoint.rotation);
                }
                playerStat.playerStats.Energy -= 12f;
                sliding = false;
                slidetimer = maxslidetime;
                
                noshock = false;
                chargefx = false;
                charge_fx.SetActive(false);
                anim.SetBool("IsSliding", true);
                audi.PlayOneShot(dash_sound, 0.4f);
                Invoke("disable_dash", 0.7f);
        }*/
        


        if (sliding == false)
        {
            slidetimer -= Time.deltaTime;
            if (slidetimer <= 0)
            {
                sliding = true;
                anim.SetBool("IsSliding", false);
            }
        }


    }
    void waitdash()
    {
        chargefx = true;
    }
    void disable_dash()
    {
        enable_dash.enabled = false;
    }
    void disable_shock()
    {
        enable_shock.enabled = false;
        neutralize = false;
    }
    public void footstepswalk()
    {
        if (noshock == false)
        {
            audi.PlayOneShot(walk_sound, 1f);
        }
    }
    public void footstepsrun()
    {
        if (noshock == false)
        {
            
            audi.PlayOneShot(run_sound, 1f);
        }
    }
    public void onground()
    {
        audi.PlayOneShot(land, 0.65f);
    }


}
  


    

