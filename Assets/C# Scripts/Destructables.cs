using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Destructables : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 50f;
    public GameObject DestroyedVersion;
    public bool DestroyEffect = false;
    public ParticleSystem BlastEffect;
    public bool DestroyedversionSpawn = true;
    [HideInInspector]
    public bool destroyed = false;
    [Space(20)]
    public bool CameraShake = false;
   public Animator CamShake;

    [Space(20)]
    [Header("Sound")]
    public bool blastsound = false;
    public AudioClip BlastSound;
    public AudioSource source;
   

    


    public void Start()
    {
        
       
    }
    public void TakeDamage(float amount)
    {
        //Anim.SetBool("Hit", true);
        health -= amount;
        if (health <= 0f)
        {
            if (blastsound == true)
            {
                source.clip = BlastSound;
                source.Play();
            }
            destroyed = true;

            if (CameraShake == true)
            {
               CamShake.SetBool("Blast", true);
           }
           
           
           
            
            if(DestroyedversionSpawn == true)
            {
               
                StartCoroutine(DestroY());
                return;
                
               
            }
           
        }
    }

    
    public IEnumerator DestroY()
    {
        
        if(DestroyedversionSpawn == true)
        {
             GameObject Spawned =  Instantiate(DestroyedVersion, transform.position, transform.rotation);
            
        }
        if (blastsound == true)
        {
            source.clip = BlastSound;
            source.Play();
        }
        if (DestroyEffect == true)
        {
            BlastEffect.Play();
           
        }
        yield return new WaitForSeconds(0.5f);
        if(CameraShake == true)
        {
            CamShake.SetBool("Blast", false);
        }
        Destroy(gameObject);
    }

   
   

}

