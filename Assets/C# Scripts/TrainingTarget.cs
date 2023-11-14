using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TrainingTarget : MonoBehaviour
{
    [Range(2f, 5f)]
    public float TimeToRaise;
    public Animator animator;
    [Range(2f, 5f)]
    public float Health;
    public AudioSource Source;
    public AudioClip GoUpClip;
    public AudioClip GoDownclip;
    public float Volume;
    public float Pitch;

    public GameObject PlusScore1;
    public GameObject PlusScore2;
    public GameObject PlusScore5;

    public TrainingPoint point;
    public int Score = 1;

    private bool Down = false;
    private bool Up = true;

    void Start()
    {
        
        animator.GetComponent<Animator>();
        
        Source.GetComponent<AudioSource>();


        point.GetComponent<TrainingPoint>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
         
    }

    public void TakeDamage(float amount)
    {
        //Anim.SetBool("Hit", true);
        Health -= amount;
        if (Health <= 0f)
        {

            StartCoroutine(Damaged());
            
            if(Up == true && Down == false)
            {
                point.Score += Score;
                StartCoroutine(PlusOne());
            }
            
            
        }
    }
    IEnumerator PlusOne()
    {
        if(Score == 1)
        {
            PlusScore1.SetActive(true);
            yield return new WaitForSeconds(0.17f);
            PlusScore1.SetActive(false);
        }
        if (Score == 2)
        {
            PlusScore2.SetActive(true);
            yield return new WaitForSeconds(0.17f);
            PlusScore2.SetActive(false);
        }
        if (Score == 5)
        {
            PlusScore5.SetActive(true);
            yield return new WaitForSeconds(0.17f);
            PlusScore5.SetActive(false);
        }

    }
    IEnumerator Damaged()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("Damaged", true);
        animator.SetBool("GetUp", false);
        Source.clip = GoDownclip;
        Source.loop = false;
        Source.volume = Volume;
        Source.pitch = Pitch;
        Source.Play();
        Down = true;
        Up = false;

        yield return new WaitForSeconds(TimeToRaise);
        animator.SetBool("GetUp", true);
        animator.SetBool("Damaged", false);
        if(Source.isPlaying == true)
        {
            Source.Pause();
        }
        Source.clip = GoUpClip;
        Source.Play();
        TimeToRaise = Random.Range(3f, 10f);
        Health = Random.Range(1f, 50f);
        Down = false;
        Up = true;
    }
}

