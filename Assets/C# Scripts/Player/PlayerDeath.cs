using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDeath : MonoBehaviour
{
    public float Health = 500f;
    //[HideInInspector]
    
    [HideInInspector]
    public bool dead = false;
    public float DeathTime;
    public GameObject DeathCam;
    public GameObject SoilderBody;
    public Vector3 offset;
    public GameObject Cam;
    public Animator DeathCamAnim;
    public HealthBarScript healthbar;
    public GameObject Dameged;
    public PostProcessVolume damageeffect;
    [Space()]
    public int HealthKitAmount = 2;
    public GameObject healthbutton;
    public GameObject Healing;
    public float HealTime = 3f;

    [Space(20)]
    [Header("For Tdm Score")]
    public bool TDM = false;
    public TDMscore ScoreScript;


    public void Start()
    {
       
        DeathCam.transform.position = transform.position;
        SoilderBody.transform.position = transform.position;
        DeathCamAnim.GetComponent<Animator>();
        healthbar.GetComponent<HealthBarScript>();
        healthbar.SetMaxHealth(Health);
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            HealthUp();
        }

        if(HealthKitAmount == 0)
        {
            healthbutton.SetActive(false);
        }
        
        if (dead == true)
        {
            DeathCamAnim.SetBool("Dead", true);
        }
        else if(dead == false)
        {
            DeathCamAnim.SetBool("Dead", false);
        }


        if (Health <= 200f)
        {
            damageeffect.weight = 0.2f;
            Dameged.SetActive(true);

        }
        if (Health <= 100f)
        {
            damageeffect.weight = 0.7f;
            Dameged.SetActive(true);
            
            

        }
        if (Health <= 50f)
        {
            damageeffect.weight = 1f;
            
            Dameged.SetActive(true);
            
        }
        else if (Health >= 200f)
        {
            Dameged.SetActive(false);
        }
        
        healthbar.Sethealth(Health);

    }
    
    public void TakeDamage(float amount)
    {
        Health -= amount;
        healthbar.Sethealth(Health);
        if (Health <= 0f)
        {
           
            
            dead = true;
            if(TDM == true)
            {
                StartCoroutine(Score());
                StartCoroutine(Die());

            }
            
            

        }
       
        
    }
    IEnumerator Score()
    {
        yield return new WaitForSeconds(5);
       
            ScoreScript.GreenScore += 1;
            
        
        
    }
   IEnumerator Die()
    {
        yield return new WaitForSeconds(DeathTime);
        Destroy(gameObject);
        yield return new WaitForSeconds(0f);
        Cam.SetActive(false);
    }
   public void HealthUp()
    {
        
        if(HealthKitAmount > 0 && Health < 800)
        {

            Healing.SetActive(true);
            StartCoroutine(Heal());
        }
        
        
    }
    IEnumerator Heal()
    {
        yield return new WaitForSeconds(HealTime);
        Health = 800f;
        HealthKitAmount -= 1;
        Healing.SetActive(false);
    }
}
