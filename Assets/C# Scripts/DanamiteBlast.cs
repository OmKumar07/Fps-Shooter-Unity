using System.Collections;
using UnityEngine;
public class DanamiteBlast : MonoBehaviour
{   public ParticleSystem Blast;
    public float health = 50f;
    public Animator CamAnim;
    [HideInInspector]
    public bool Destroyed = false;


    [Space(20)]
    [Header("Sound")]

    public bool Sound;
    public AudioClip Clip;
    public AudioSource Source;

    
    


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

