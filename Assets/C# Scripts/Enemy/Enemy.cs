using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float LookRadius = 50f;
    public float ActiveRange = 100f;

    public Transform TARGET;

    public NavMeshAgent agent;

    public Animator Anim;

    public Transform firePoint;

    public ParticleSystem onhit;
    public GameObject BulletPrefab;
    public ParticleSystem MuzzelFlash;

    public Target enemy;
    public Target enemyHead;


    public int maxAmmo = 30;
    public int currentAmmo = -1;
    public float ReloadTime;
    private bool isReloding = false;
    public float FireRAte = 80f;
    private float NextTimeToFire = 0f;

    public float damage;
    public PlayerDeath player;

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

    [Space]
    public bool EnemyBetterAi = false;
    public Count ChildCount;
    public int IncreasedRange = 200;
    private float health;
    private float healthHead;
    public int DamagedRange = 300;
    // Start is called before the first frame update


    void Start()
    {
        
        health = enemy.health;
        healthHead = enemyHead.health;
        currentAmmo = maxAmmo;
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        enemy.GetComponent<Target>();
        Anim.SetBool("Realoading", false);
        Source.GetComponent<AudioSource>();
        if(EnemyBetterAi == true)
        {
            ChildCount.GetComponent<Count>();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        StartCoroutine(Increase());
        if(enemy.health < health)
        {
            LookRadius = DamagedRange;
        }
        if (enemyHead.health < healthHead)
        {
            LookRadius = DamagedRange;
        }
    }
    IEnumerator Increase()
    {
        yield return new WaitForSeconds(0.1f);
        if (EnemyBetterAi == true && ChildCount.Killed >= 2)
        {
            
            LookRadius = IncreasedRange;
        }
    }
    void LateUpdate()
    {
       

       if(TARGET != null && ReactToPlayer == true)
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
       
       if(TARGET == null)
            {
           
                return;
            }
       if(player.dead == true)
        {
            Anim.SetBool("Run", false);
            Anim.SetBool("Fire", false);
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
            if (hit.collider.CompareTag("First Person Player"))
            {
                PlayerDeath target = hit.transform.GetComponent<PlayerDeath>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                onhit.Play();
                GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();
                if (bullet != null & TARGET != null)
                {
                    bullet.Seek(TARGET);

                }
                Destructables Objects = hit.transform.GetComponent<Destructables>();
                if(Objects != null)
                {
                    Objects.TakeDamage(damage);
                }

            }
        }
    }

   
}
