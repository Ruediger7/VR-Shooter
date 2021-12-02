using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]

public class Enemy_AI : MonoBehaviour, IEntity
{
    public float attackDistance = 3f;
    public float movementSpeed = 4f;
    public float npcHP = 100;
    public float npcDamage = 10;
    public float attackRate = 0.5f;
    public Transform firePoint;
    public GameObject npcDeadPrefab;
    private GameObject player;

    [HideInInspector]
    public Transform playerTransform;
    //[HideInInspector]
    //public SC_EnemySpawner es;
    NavMeshAgent agent;
    float nextAttackTime = 0;
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
        //fürs Prefab muss der Player hier gesucht werden
        player = GameObject.Find("CVirtPlayerController");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((agent.remainingDistance - attackDistance) < 0.01f)
        {
            //Debug.Log(agent.remainingDistance - attackDistance);
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + attackRate;

                //Attack
                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);

                        IEntity player = hit.transform.GetComponent<IEntity>();
                        player.ApplyDamage(npcDamage);
                    }
                }
           }
        }

        if (Vector3.Distance(playerTransform.position,transform.position) <= 10) {
            //Move toward player
            agent.destination = playerTransform.position;
            //Always look at player
            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
        }
    }

    public void ApplyDamage(float points)
    {
        npcHP -= points;
        if (npcHP <= 0)
        {
            //Destroy the NPC
            GameObject npcDead = Instantiate(npcDeadPrefab, transform.position, transform.rotation);
            //Slightly bounce the dead model
            npcDead.GetComponent<Rigidbody>().velocity = (-(playerTransform.position - transform.position).normalized * 8) + new Vector3(0, 5, 0);
            Destroy(npcDead, 10);
            Destroy(gameObject);
        }
    }
}