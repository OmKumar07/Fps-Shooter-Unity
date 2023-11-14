using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : MonoBehaviour
{
    public Transform target;

    [Space(5)]
    [Header("Shooting")]

    public float range = 50;
    public float FireRate = 1f;
    private float fireCountdown = 0f;

    private string enemyTag = "First Person Player";

    [Space(20)]
    [Header("Rotating Part Of Turrent")]

    public float turnSpeed = 10f;
    public Transform partToRotate;

    
    public Transform firePoint;

    public ParticleSystem onhit;
    

    public GameObject BulletPrefab;
    public float damage;
    public TurretDestroy turret;
    public Animator Camanim;

    public PlayerDeath player;

    public bool missileLauncher = false;
    public bool ShootSound = false;
    public AudioClip ShootClip;
    public AudioSource Source;
    public float ShootVolume = 1f;
    public float ShootPitch = 1f;



    public bool MuzzelFlash = false;
    public ParticleSystem muzzelFlash;

   

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
        turret.GetComponent<TurretDestroy>();
        turret.GetComponent<TurretDestroy>();
        Camanim.GetComponent<Animator>();
    }




    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestdistance = Mathf.Infinity;
        GameObject nearestplayer = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToPlayer < shortestdistance)
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
    IEnumerator Emp()
    {
        enemyTag = "Empty";
        yield return new WaitForSeconds(3f);
        enemyTag = "First Person Player";
    }

    void Update()
    {
        UpdateTarget();
        if (Input.GetKey(KeyCode.G))
        {
            StartCoroutine(Emp());
            
        }
        if (target == null)
            return;
        if (fireCountdown <= 0f && player.dead == false)
        {
            Shoot();
            firePoint.LookAt(target);
            fireCountdown = 1f / FireRate;
        }
        fireCountdown -= Time.deltaTime;
        if(target != null)
        {
            LockOnTarget();
            
        }
        
        
    }

    void Shoot()
    {
        
        Vector3 fwd = firePoint.TransformDirection(Vector3.forward);
        RaycastHit hit;

       
        
        
            
        
        Debug.DrawRay(firePoint.position, fwd * range, Color.green);
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, range))
        {
           
            if (hit.collider.CompareTag(enemyTag))
            {
                if (MuzzelFlash == true)
                {
                    muzzelFlash.Play();
                }
                if (ShootSound == true)
                {
                    Source.clip = ShootClip;
                    Source.volume = ShootVolume;
                    Source.pitch = ShootPitch;
                    Source.Play();
                }
                GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null)
                {
                   bullet.Seek(target);
                    
                   
                }
                if(missileLauncher == false)
                {
                    PlayerDeath Player = hit.transform.GetComponent<PlayerDeath>();
                    if (Player != null)
                    {
                        Player.TakeDamage(damage);
                    }
                    onhit.Play();
                }
               

            }

            
            
        }
        

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion LookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, LookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
        if (target == null)
        {
            dir.Normalize();
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }




}

