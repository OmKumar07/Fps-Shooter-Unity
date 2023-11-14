using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyFollow : MonoBehaviour
{

    public NavMeshAgent Enemy;

    public Transform enemy;

    public Transform player;

    public float distanceFromPlayer = 4.0f;

    public float patrolingZone;
    

    public int turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Enemy.GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < patrolingZone)
        {

            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;
            

            Enemy.SetDestination(newPos);

            
            

        }
        





        


    }
}
