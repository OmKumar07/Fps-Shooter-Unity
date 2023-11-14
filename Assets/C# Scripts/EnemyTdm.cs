using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTdm : MonoBehaviour
{
    private float LookRadius = Mathf.Infinity;
    public float ActiveRange = 100f;

    public Transform TARGET;

    public NavMeshAgent agent;

    public Animator Anim;

    public Transform firePoint;

    public GameObject BulletPrefab;
    public ParticleSystem MuzzelFlash;

    public Target enemy;
    public Target enemyHead;
    public int Health = 200;
    


    public int maxAmmo = 30;
    public int currentAmmo = -1;
    public float ReloadTime;
    private bool isReloding = false;
    private float FireRAte = 80f;
    private float NextTimeToFire = 0f;

    public float damage;
    public int MAxDamage = 10;


    [Space(20)]
    public AudioSource Source;
    public AudioClip ShootingClip;
    public float ShootingPitch = 1f;
    public float ShootingVolume = 1f;
    public AudioClip Reloadclip;
    public float reloadPitch = 1f;
    public float ReloadVolume = 1f;

    [Space]
    public bool ReactToPlayer = true;
    public string TAg;

    public Transform[] positions;
    private Transform position;
    // Start is called before the first frame update
    [Space]
    [Header("For Healthbar")]
    public HealthBarScript healthbar;
    public GameObject Healthbar;
    private bool headshot = false;
   

    private void Awake()
    {
        
        StartCoroutine(AtStart());
    }
    void Start()
    {
        healthbar.GetComponent<HealthBarScript>();
        healthbar.SetMaxHealth(enemy.health);
        StartCoroutine(AtStart());
        position = positions[Random.Range(0, positions.Length)];
        damage = Random.Range(5, MAxDamage);
        FireRAte = Random.Range(10, 20);
        currentAmmo = maxAmmo;
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        enemy.GetComponent<Target>();
        Anim.SetBool("Realoading", false);
        Source.GetComponent<AudioSource>();
        
    }
  
    IEnumerator AtStart()
    {
        enemy.health = 10000;
        yield return new WaitForSeconds(3f);
        enemy.health = Health;
        healthbar.SetMaxHealth(Health);
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
       if(enemy.dead2 == true)
        {
            Healthbar.SetActive(false);
        }
        if (enemyHead.dead2 == true)
        {
            Healthbar.SetActive(false);
        }
        if (enemyHead.Headshot1 == true)
        {
            
            healthbar.Sethealth(0);
            headshot = true;
        }
        FindClosestsEnemy();
        StartCoroutine(Increase());
        if(headshot == false)
        {
            healthbar.Sethealth(enemy.health);
        }
        

    }
    IEnumerator Increase()
    {
        yield return new WaitForSeconds(0.1f);
        
    }
    void LateUpdate()
    {

        FindClosestsEnemy();
        if (TARGET != null && ReactToPlayer == true)
        {
            float distance = Vector3.Distance(TARGET.position, transform.position);

            if (isReloding)
                return;
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (distance >= LookRadius)
            {
                Anim.SetBool("Run", false);
            }
            else if (distance <= LookRadius)
            {
                Anim.SetBool("Run", true);
                Anim.SetBool("Active", false);
                Anim.SetBool("Fire", false);
            }
            if (distance <= LookRadius)
            {
                if (enemy.dead == false && enemyHead.dead == false)
                {
                    Anim.SetBool("Run", true);
                    agent.SetDestination(TARGET.position);



                    if (distance <= agent.stoppingDistance)
                    {
                        if (enemy.dead == false && enemyHead.dead == false & Time.time >= NextTimeToFire)
                        {
                            NextTimeToFire = Time.time + 1f / FireRAte;
                            if (isReloding == false)
                            {
                                shoot();
                                MuzzelFlash.Play();

                            }
                        }


                        Anim.SetBool("Fire", true);
                        FaceTarget();
                        Anim.SetBool("Run", false);
                    }


                }

            }
            if (distance <= ActiveRange)
            {
                Anim.SetBool("Active", true);

            }


        }

        if (TARGET == null)
        {

            return;
        }
        

    }
    void FindClosestsEnemy()
    {
        float shortestdistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        
        GameObject[] Allenemies = GameObject.FindGameObjectsWithTag(TAg);

        foreach(GameObject currentEnemy in Allenemies)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (distanceToPlayer < shortestdistance)
            {
                
                
                  shortestdistance = distanceToPlayer;
                   closestEnemy = currentEnemy;
                
                
                
                
            }
        }
        if (closestEnemy != null)
        {
            TARGET = closestEnemy.transform;
        }
        if (closestEnemy == null)
        {
            
            
            agent.SetDestination(position.position);
        }
        
    }
    
    void FaceTarget()
    {



        Vector3 direction = (TARGET.position - transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 5f);



    }

    public IEnumerator Reload()
    {

        isReloding = true;
        Source.clip = Reloadclip;
        Source.pitch = reloadPitch;
        Source.volume = ReloadVolume;
        Source.Play();

        Anim.SetBool("Realoading", true);
        Anim.SetBool("Fire", false);

        yield return new WaitForSeconds(ReloadTime - .25f);
        Anim.SetBool("Realoading", false);
        yield return new WaitForSeconds(-.25f);

        currentAmmo = maxAmmo;


        isReloding = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
    }


    public void shoot()
    {

        Vector3 fwd = firePoint.TransformDirection(Vector3.forward);
        RaycastHit hit;

        currentAmmo--;
        Source.clip = ShootingClip;
        Source.volume = ShootingVolume;
        Source.pitch = ShootingPitch;
        Source.Play();


        Debug.DrawRay(firePoint.position, fwd * agent.stoppingDistance, Color.green);
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, agent.stoppingDistance))
        {
            if (hit.collider.CompareTag(TAg))
            {
                PlayerDeath target = hit.transform.GetComponent<PlayerDeath>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                Target enemy = hit.transform.GetComponent<Target>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                
                GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null & TARGET != null)
                {
                    bullet.Seek(TARGET);

                }
                Destructables Objects = hit.transform.GetComponent<Destructables>();
                if (Objects != null)
                {
                    Objects.TakeDamage(damage);
                }
                

            }
        }
    }
}
