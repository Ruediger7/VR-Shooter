using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class Enemy_AI : MonoBehaviour, IEntity
{
    //distance the Enemy can shoot from
    public float attackDistance = 3f;
    //distance in which the enemy recogizes the player and runs toward him
    public float aggroRange = 10;
    public float movementSpeed = 4f;
    public float npcHP = 100;
    public float npcDmg = 10;
    public float attackRate = 0.5f;
    public Transform FirePoint;
    public GameObject npcDeadPrefab;
    public GameObject bulletPrefab;
    //private GameStart gameStart;
    public Animator animator;
    public GameObject Eyes;
    private bool PlayerInVision = false;

    private GameObject player;
    private Transform playerTransform;
    private float nextAttackTime = 0;
    [Range(0, 360f)]
    public float viewAngle = 90f;
    
    NavMeshAgent agent;
    Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.speed = movementSpeed;
        r = GetComponent<Rigidbody>();
        r.useGravity = true;
        r.isKinematic = true;
        //looking for Player here
        player = GameObject.Find("CVirtPlayerController");
        playerTransform = player.transform;
        //the Number given to RandomNavSphere determines the radius in which the random Point will be generated
        agent.destination = RandomNavSphere(this.transform.position, 20);
        Debug.Log(agent.destination);
        //gameStart = FindObjectOfType<GameStart>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if player is generally in vision-cone of enemy
        Vector3 richtungZumZiel = player.transform.position - transform.position;
        float winkelZumZiel = Vector3.Angle(transform.forward, richtungZumZiel);
        if (winkelZumZiel < viewAngle / 2f)
        {
            //check if robot can really see player or if his view is obstructed
            RaycastHit hit;
            if (Physics.Raycast(Eyes.transform.position, new Vector3(player.transform.position.x, Eyes.transform.position.y, player.transform.position.z) - Eyes.transform.position, out hit))
            {
                //if robot sees player, the agent gets his position
                if (hit.collider.CompareTag("Player"))
                {
                    PlayerInVision = true;
                    agent.destination = playerTransform.position;
                    transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
                    Debug.DrawLine(Eyes.transform.position, player.transform.position, Color.red);

                    //if in Range shoot
                    if (Vector3.Distance(player.transform.position, this.transform.position) < attackDistance)
                    {
                        animator.SetBool("walks", false);
                        if (Time.time > nextAttackTime)
                        {
                            Fire();
                            //time till next shot
                            nextAttackTime = Time.time + attackRate;
                        }
                    }
                    //if not in range start walking-animation as robot walks into range
                    else
                    {
                        animator.SetBool("walks", true);
                    }
                }
                //activates the random patrolling if player cant be seen
                else
                {
                    PlayerInVision = false;
                }
            }
        }
        //resets the PlayerInVision if player manages to get out of the cone
        else
        {
            PlayerInVision = false;
        }

        //if the agent reached his current goal, give him a new one
        if(!agent.pathPending && PlayerInVision == false)
        {
            animator.SetBool("walks", true);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if(agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    agent.destination = RandomNavSphere(this.transform.position, 20);
                }
            }
            
        }
    }

    public void ApplyDamage(float points)
    {
        npcHP -= points;
        if (npcHP <= 0)
        {
            //Replace Enemy with DEAD-Prefab and bounce it a little
            GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            npcDead.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
            Destroy(npcDead, 10);
            Destroy(gameObject);
            GameObject.Find("Killcounter_Hud").GetComponent<killcounter>().updateKill();
        }
    }

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        Enemy_Bullet bullet = spawnedBullet.GetComponent<Enemy_Bullet>();
        bullet.SetDamage(npcDmg);
    }

    //generates a random Point around the Enemy and then determines the nearest Point on the Navmesh
    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, 1);
        return navHit.position;
    }
}
