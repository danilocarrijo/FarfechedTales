using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FieldOfView))]
public class Enemy_Behavor : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private bool walking;

    public float lookRadius;
    public float attackRadius;
    public float attackRate = 1f;
    [SerializeField] float restTime;
    [SerializeField] float moveTime;
    [SerializeField] float lookTime;
    [SerializeField] Vector2 movement;
    [SerializeField] float rotationAngle;
    [SerializeField] float normalSpeed;
    private bool isBusy = false;
    private List<Transform> iAWaypoints;
    private Transform currDestination;
    public float walkingRadius;
    Quaternion qTo ;
    float speed = 1.25f;
    float rotateSpeed = 3.0f;
    float timer = 0.0f;

    int currenPatrolChoice = 0; 

    private float nextAttack;

    private bool isLooking;

    Transform targetPlayer;
    FieldOfView fov;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        targetPlayer = GameCenter.Instance.player.transform;
        iAWaypoints = GameObject.FindGameObjectsWithTag("IAWaypoint").Select(x => x.transform).ToList();
    }

    // Update is called once per frame
    void Update()
    {
       

        if (isLooking)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            { // timer resets at 2, allowing .5 s to do the rotating
                qTo = Quaternion.Euler(new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f));
                timer = 0.0f;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotateSpeed);
        }

        if (anim != null)
        {
            anim.SetBool("isWalking", walking);
            anim.SetBool("isIdling", !walking);
        }

        if(targetPlayer == null)
        {
            targetPlayer = GameCenter.Instance.player.transform;
        }

        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        if(fov.canSeePlayer)
        {
            isBusy = false;
            SeekPlayer();
        }
        else
        {
            StartCoroutine(Patrol());
            //walking = false;
        }
    }

    private IEnumerator Patrol()
    {
        if (!isBusy && !isLooking)
        {
            var randomAction = Random.Range(1, 3);
            Debug.Log(randomAction);
            switch (randomAction)
            {
                case 1:
                    Move();
                    break;
                case 2:
                    isLooking = true;
                    StartCoroutine(LookAround());
                    break;
                case 3:
                    isBusy = true;
                    StartCoroutine(Rest());
                    break;
                default:
                    break;
            }
        }else if(currDestination != null)
        {
            PatrolMove();
        }
        yield return true;
    }

    void Move()
    {
        //rb.velocity = movement;
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = normalSpeed;
        var waypoint = iAWaypoints[Random.Range(0, iAWaypoints.Count())];
        currDestination = waypoint;
        isBusy = true;
    }
    //check if this is the "looking behaviour" that you wanted

    private void PatrolMove()
    {
        navMeshAgent.destination = currDestination.position;
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 2)
        {
            currDestination = null;
            isBusy = false;
        }
    }

    private IEnumerator Rest()
    {
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(restTime);
        isBusy = false;
    }
    private IEnumerator LookAround()
    {
        navMeshAgent.isStopped = true;
        
        yield return new WaitForSeconds(lookTime);
        isLooking = false;
        //rb.MoveRotation(rotationAngle);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    void SeekPlayer()
    {
        navMeshAgent.destination = targetPlayer.position;
        navMeshAgent.speed = normalSpeed * 1.5f;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance > attackRadius)
        {
            walking = true;
            navMeshAgent.isStopped = false;
        }
        else
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attackRadius)
        {
            if (anim != null)
            {
                anim.SetBool("isFighting", false);
            }

           // transform.LookAt(targetPlayer);
            Vector3 dirToAttack = targetPlayer.position - transform.position;
            Vector3 rot = Quaternion.LookRotation(dirToAttack).eulerAngles;
            rot.x = rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);

            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;

                if (anim != null)
                {
                    anim.SetBool("isFighting", true);
                }
            }
            walking = false;
            navMeshAgent.isStopped = true;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;

        Handles.DrawWireArc(transform.position + new Vector3(0,0.2f,0),transform.up,transform.right,360,lookRadius);

        Handles.color = Color.red;

        Handles.DrawWireArc(transform.position + new Vector3(0, 0.2f, 0), transform.up, transform.right, 360, attackRadius);
    }
}
