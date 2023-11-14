using System.Collections;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Animator Anim;
    public float DeathTime = 0f;
    [HideInInspector]
    public bool dead = false;
    [HideInInspector]
    public bool dead2 = false;
    public bool pickupObjects = false;
    public GameObject PickupWeapon;
    [HideInInspector]
    public bool Headshot1;
    public bool enemy = true;
    public PlayerDeath player;
    public GameObject PLAYER;
    [Space]
    [Header("For Tdm Score")]
    public bool TDM = false;
    public TDMscore ScoreScript;
    public bool Friend;


    
    public void Start()
    {
        
        Anim.GetComponent<Animator>();
    }
   
    public void TakeDamage (float amount)
    {
        //Anim.SetBool("Hit", true);
        health -= amount;
        if(health <= 0f)
        {
            if (TDM == true)
            {
                StartCoroutine(Score());

            }
            dead2 = true;
            StartCoroutine(Die());
            Anim.SetBool("Death", true);
            StartCoroutine(Dead(0.1f));
            StartCoroutine(Headshot());
            Anim.SetBool("Fire", false);
            if(pickupObjects == true)
            {
                PickupWeapon.SetActive(true);
            }
            
        }
    }
    IEnumerator Score()
    {
        yield return new WaitForSeconds(3);
        if (Friend != true)
        {
            ScoreScript.GreenScore += 1;

        }
        
        if (Friend == true)
        {
            ScoreScript.REDScore += 1;

        }
        
    }
    IEnumerator Dead(float deadtime)
    {
        yield return new WaitForSeconds(deadtime);
        dead = true;
    }
   IEnumerator Die()
    {
            yield return new WaitForSeconds(DeathTime);
            Destroy(PLAYER);
        
    }
    IEnumerator Headshot()
    {
        Headshot1 = true;
        yield return new WaitForSeconds(1f);
        Headshot1 = false;
    }
  
}

