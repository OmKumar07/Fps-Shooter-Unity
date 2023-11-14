using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirePointTdm : MonoBehaviour
{
    private Transform target;
    public string Tag;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        FindClosestsEnemy();
        
        

    }
    void FindClosestsEnemy()
    {
        float shortestdistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        GameObject[] Allenemies = GameObject.FindGameObjectsWithTag(Tag);

        foreach (GameObject currentEnemy in Allenemies)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (distanceToPlayer < shortestdistance)
            {
                shortestdistance = distanceToPlayer;
                closestEnemy = currentEnemy;
            }
        }
        if(closestEnemy != null)
        {
            target = closestEnemy.transform;
            FaceTarget();

        }
        if(closestEnemy == null)
        {
            
            return;
        }
        
    }
    void FaceTarget()
    {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 5f);

    }
}
