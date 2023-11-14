using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedBomb : MonoBehaviour
{
    public ParticleSystem Blast;
    public float health = 50f;
    public Animator CamAnim;
    [HideInInspector]
    public bool Destroyed = false;


    [Space(20)]
    [Header("Sound")]

    public bool Sound;
    public AudioClip Clip;
    public AudioSource Source;


    [Space(20)]
    [Header("AttachedObject")]
    public bool Destructible = false;
    public Destructables destruct;
    [Space]
    public bool target = false;
    public TurretDestroy turret;




    public void Start()
    {
        CamAnim.GetComponent<Animator>();
    }

    public void Update()
    {

    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            if(target == true)
            {
                turret.TakeDamage(turret.health);
            }
            if (Destructible == true)
            {
                destruct.TakeDamage(destruct.health);
            }

            CamAnim.SetBool("Blast", true);
            StartCoroutine(Destroy());
            if (Sound == true)
            {
                Source.clip = Clip;
                Source.Play();
            }
            StartCoroutine(Destroy());
            Blast.Play();
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        CamAnim.SetBool("Blast", false);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);

    }
}

