using Pathfinding;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class enemycontrol : MonoBehaviour
{
    public Transform target;
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rb;
    //the calculated path
    public Path path;
    //the pathfinding speed per second
    public float speed = 300f;
    public ForceMode2D fmode;
    [HideInInspector]
    public bool pathisended = false;
    //max distance between waypoint and follower to decide next waypoint 
    public float nextWaypointDistance = 3f;
    //the waypoint we are moving towards
    private int currentWayPoint = 0;
    private bool searchingforplayer = false;
    SpriteRenderer sprite;
    public float direction;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    

        if (target == null)
        {
            if (!searchingforplayer)
            {
                searchingforplayer = true;
                StartCoroutine(Searchforplayer());
            }
            return;
        }
        //start a new path to target position using onpathcomplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }
    IEnumerator Searchforplayer()
    {
        GameObject sresult = GameObject.FindGameObjectWithTag("Player");
        if (sresult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Searchforplayer());
        }
        else
        {
            target = sresult.transform;
            searchingforplayer = false;
            yield return false;
        }
        StartCoroutine (UpdatePath());
        //target = GameObject.FindGameObjectWithTag("Player");
    }
    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingforplayer)
            {
                searchingforplayer = true;
                StartCoroutine(Searchforplayer());
            }
          yield return false;
        }
        //start a new path to target position using onpathcomplete method
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }



    public void OnPathComplete(Path p)
    {
        Debug.Log("we found a path.Did we get an error" + p.error);
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            if (!searchingforplayer)
            {
                searchingforplayer = true;
                StartCoroutine(Searchforplayer());
            }
            return;
        }
        //to do: always look at player
        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
        {
            if (pathisended)
                return;
            Debug.Log("end of path reached");
            pathisended = true;
            return;
        }
        pathisended = false;

        //direction to next waypoint
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        
        //move the ai
        rb.AddForce(dir, fmode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist < nextWaypointDistance)
        {
            currentWayPoint++;
            return;
        }
        direction = dir.x;
        if(dir.x < 0)
        {
            sprite.flipX = true;
        }
        else if (dir.x >= 0)
        {
            sprite.flipX = false;
        }
    }
}

    



    
 
