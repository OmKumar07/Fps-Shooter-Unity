using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public PlayerDeath Player;
    public float damage;
    public ParticleSystem Blast;
   [HideInInspector]
    public bool blasted = false;
    public Animator CamShake;
    public Light RedLight;
    public Collider collider;

    [Space(20)]
    [Header("Sound")]

    public bool Sound;
    public AudioClip Clip;
    public AudioSource Source;
 

   

    private void Start()
    {
        
        Player.GetComponent<PlayerDeath>();
        CamShake.GetComponent<Animator>();
        StartCoroutine(Blink());

    }
    public void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "First Person Player")
        {
            
            CamShake.SetBool("Blast", true);
            blasted = true;
            Player.TakeDamage(damage);
            if (Sound == true)
            {
                Source.clip = Clip;
                Source.Play();
            }
            blasted = true;
            StartCoroutine(b());
            
        }
      

    }
    IEnumerator b()
    {
        collider.isTrigger = false;
        yield return new WaitForSeconds(0.2f);
        CamShake.SetBool("Blast", false);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
        
        Blast.Play();

        

        blasted = true;
    }

    
    IEnumerator Blink()
    {
        while (true)
        {

            yield return new WaitForSeconds(Random.Range(0.02f, 0.1f));
            RedLight.enabled = !RedLight.enabled;
           

        }
    }
}
