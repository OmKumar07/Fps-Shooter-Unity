using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public Transform target;

[Space(5)]
[Header("Shooting")]

    public float range = 15f;
    public float FireRate = 1f;
    private float fireCountdown = 0f;

    private string enemyTag = "First Person Player";

[Space(20)]
[Header("Rotating Part Of Turrent")]

    public float turnSpeed = 10f;
    public Transform partToRotate;

    public GameObject BulletPrefab;
    public Transform firePoint;

    public ParticleSystem hit;

    

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

   


    void UpdateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestdistance = Mathf.Infinity;
        GameObject nearestplayer = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToPlayer < shortestdistance)
            {
                shortestdistance = distanceToPlayer;
                nearestplayer = enemy;
            }

        }


        if (nearestplayer != null && shortestdistance <= range)
        {
            target = nearestplayer.transform;
        }
        else
            target = null;
        

        
    }
    void LateUpdate()
    {
        if (target == null)
            return;
        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / FireRate;
        }
        fireCountdown -= Time.deltaTime;
        LockOnTarget();
    }

    void Shoot ()
    {
       GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
            hit.Play();
        }
            
        
    }

    void LockOnTarget ()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, LookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
       
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }




}
