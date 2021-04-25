using UnityEngine;
using System.Collections;

public class enemy_follow : MonoBehaviour
{

    public Transform target;
    public Transform null_target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public float yPosRestriction = -1;
    Transform reset;
    float sound_timer;
    float max_timer = 10;
    // public float cameraZoomSpeed = 1f;

    float offsetZ;
    Vector3 lastTargetPosition;
    Vector3 currentVelocity;
    Vector3 lookAheadPos;

    float nextTimeToSearch = 0;
    //private float initialY;
    SpriteRenderer flippy;
    Animator anim;
    AudioSource audi;
    public AudioClip squack;

    // Use this for initialization
    void Start()
    {
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
        flippy = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        reset = target;
        audi = GetComponent<AudioSource>();
        sound_timer = max_timer;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (target == null)
        {
            FindPlayer();
            return;
        }
        Charactermove();

    }
    void Charactermove()
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }
        if (Mathf.Sign(xMoveDelta) < 0)
        {
            anim.SetBool("resting", false);
            flippy.flipX = true;
        }
        else if (Mathf.Sign(xMoveDelta) > 0)
        {
            anim.SetBool("resting", false);
            flippy.flipX = false;
        }
        if (xMoveDelta > -0.05f && xMoveDelta < 0.05f)
        {
            anim.SetBool("resting", true);
            sound_timer -= Time.deltaTime;
            if(sound_timer <= 0)
            {
                audi.PlayOneShot(squack, 0.90f);
                sound_timer = max_timer;
            }
        }

            Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);
       
        newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, yPosRestriction, Mathf.Infinity), newPos.z);
        //newPos.y = initialY;

        transform.position = newPos;

        lastTargetPosition = target.position;
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
                target = searchResult.transform;
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
    public void null_follow()
    {
        flippy.enabled = false;
        target = null_target;
        Debug.Log("target has been set to other");
    }
    public void Reset()
    {
        flippy.enabled = true;
        target = reset;
        Debug.Log("target has been back");
    }
}



