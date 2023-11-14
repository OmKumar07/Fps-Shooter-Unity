using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float Speed = 70f;
    public float TurnSpeed;
    public float damage;
    public bool Missile = false;
    public ParticleSystem hitEffect;
    public BoxCollider collider;

    [Header("Blast")]
    public bool BlastSoud = false;
    public AudioClip Clip;
    public AudioSource source;


    private bool LookAt = true;

    


    private void Start()
    {
        if(BlastSoud == true)
        {
            source.Pause();
        }
        
    }
   
    public void Seek(Transform _target)
    {
        target = _target;  
    }

    void FixedUpdate()
    {
        if(Missile == false)
        {
            transform.GetComponent<Rigidbody>().velocity =
            gameObject.transform.forward * Speed;
        }



        if (Missile == true && LookAt == true)
        {
            transform.LookAt(target);
        }
            
       if(target == null)
        {
            if(Missile == true)
            {
                
                StartCoroutine(destroy(1.5f));
            }
            
            if(Missile == false)
            {
                Destroy(gameObject);
            }
            
            return;
        }
        Vector3 dir = target.position - transform.position;
        float DistanceToFrame = Speed * Time.deltaTime;

        if (dir.magnitude <= DistanceToFrame)
        {
            if (Missile == true)
            {
                
                StartCoroutine(destroy(1.5f));
            }
            
            if (Missile == false)
            {
                Destroy(gameObject);
            }

            return;
        }
        
        
        
        transform.Translate(dir.normalized * DistanceToFrame, Space.World);
        

       
    }
    

    IEnumerator destroy(float destroyTime)
    {
        collider.isTrigger = false;
        
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(BlastSoud == true)
        {
            LookAt = false;
            source.Play();
            if(other.gameObject.tag != "First Person Player")
            {
                StartCoroutine(destroy(0.5f));
                hitEffect.Play();
            }
            if (other.gameObject.tag == "Enemy")
            {
                Target Enemy = other.GetComponent<Target>();
                Enemy.TakeDamage(1000);
                StartCoroutine(destroy(0.5f));
                hitEffect.Play();
            }
            if (other.gameObject.tag == "Turret")
            {
                TurretDestroy Turret = other.GetComponent<TurretDestroy>();
                Turret.TakeDamage(1000);
                StartCoroutine(destroy(0.5f));
                hitEffect.Play();
            }
            if (other.gameObject.tag == "Tank")
            {
                Destructables Tank = other.GetComponent<Destructables>();
                Tank.TakeDamage(10000);
                StartCoroutine(destroy(0.5f));
                hitEffect.Play();
            }
            if (other.gameObject.tag == "WoodBox")
            {
                Destructables box = other.GetComponent<Destructables>();
                box.TakeDamage(10000);
                StartCoroutine(destroy(0.5f));
                hitEffect.Play();
            }

        }
        if (Missile == true)
        {
            if (other.gameObject.tag == "First Person Player")
            {
                hitEffect.Play();
                PlayerDeath Player = other.GetComponent<PlayerDeath>();
                Player.TakeDamage(damage);
            }
           
            else
                return;
            
            
        }
        
        
       
        
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Matle" & other.gameObject.tag == "Wood" & other.gameObject.tag == "Concrete" & other.gameObject.tag == "Ground")
        {

            print("collider");
        }
    }
}
