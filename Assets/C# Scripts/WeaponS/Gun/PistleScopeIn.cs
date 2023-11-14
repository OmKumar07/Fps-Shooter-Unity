using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistleScopeIn : MonoBehaviour
{
    public Mouselook senstivity;
    public float slowlook = 100f;
    private float OriginalSensitivity;
    [HideInInspector]
    public bool isScoped = false;
    [HideInInspector]
    public bool Scoped;
    public Animator anim;
    
    public PlayerMovementScript Movement;
    private float OriginalSpeed;
    public float slowSpeed;
    public WeaponSway Sway;
    private float SwayAmount = 0.02f;
    private float ORiginalValue;
    public GameObject CrossHair;
   

    [Header("sound")]
    public AudioSource audioSource;
    public AudioClip ScopIn;

    private void Start()
    {
        Sway.GetComponent<WeaponSway>();
        ORiginalValue = Sway.MaxAmount;
        senstivity.GetComponent<Mouselook>();
        anim.GetComponent<Animator>();
        Movement.GetComponent<PlayerMovementScript>();
        OriginalSensitivity = senstivity.mouseSensitivity;
        OriginalSpeed = Movement.Speed;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            

            if (isScoped)
                StartCoroutine(OnScoped());
            else
                OnUNscoped();
        }

        
    }


    void OnUNscoped()
    {
        CrossHair.SetActive(true);
        audioSource.clip = null;
        Scoped = false;
        Sway.MaxAmount = ORiginalValue;       
        anim.SetBool("Scoped", false);
        senstivity.mouseSensitivity = OriginalSensitivity;
       
        Movement.Speed = OriginalSpeed;
    }

    public IEnumerator OnScoped()
    {
        CrossHair.SetActive(false);
        audioSource.clip = ScopIn;
        Sway.MaxAmount = SwayAmount;
        yield return new WaitForSeconds(0.5f);
        audioSource.Play();
        audioSource.volume = 1f;
        senstivity.mouseSensitivity = slowlook;
        anim.SetBool("Scoped", true);
        yield return new WaitForSeconds(0.35f);
       
        Movement.Speed = slowSpeed;
    }
}
