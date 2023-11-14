using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDestroy : MonoBehaviour
{
    public float health = 50f;
    public ParticleSystem TheEnd;
    public Animator CamAnim;
   [HideInInspector]
    //public bool destroyed = false;
    public ParticleSystem Fire;
    [Header("Sound")]
    public bool Sound = false;
    public AudioSource source;
    public AudioClip clip;

    public GameObject MainTurret;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            TheEnd.Play();
            CamAnim.SetBool("Blast", true);
            Die();
            StartCoroutine(des());
        }


    }
    void Die()
    {
        if(Sound == true)
        {
            Fire.Play();
            source.clip = clip;
            source.Play();
        }
        
        
        //destroyed = true;
    }
    IEnumerator des()
    {
        yield return new WaitForSeconds(1f);
        
        CamAnim.SetBool("Blast", false);
        Destroy(gameObject);

        
        Destroy(MainTurret);
        
    }
}
