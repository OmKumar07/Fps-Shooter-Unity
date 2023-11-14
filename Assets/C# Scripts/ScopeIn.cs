using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeIn : MonoBehaviour
{
    public Mouselook senstivity;
    public float slowlook = 100f;

    public Animator animator;

    private bool isScoped = false;

    public GameObject scopeOverlay;

    public GameObject WeaponCamera;

    public Camera mainCamera;
    public float Zoom = 15f;
    private float normalfov;

    public GameObject sniper;

    public WeaponSway Sway;
    private float newSway = 0.02f;
    private float OriginalSway;
    

    


    private void Start()
    {
        senstivity.GetComponent<Mouselook>();
        Sway.GetComponent<WeaponSway>();
        OriginalSway = Sway.MaxAmount;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped =! isScoped;
            animator.SetBool("Scoped", isScoped);

            if (isScoped)
                StartCoroutine(OnScoped());
            else
                OnUNscoped();
            
        }

        
    }


 public   void OnUNscoped()
    {



        Sway.MaxAmount = OriginalSway;
        scopeOverlay.SetActive(false);
        WeaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalfov;
        senstivity.mouseSensitivity += slowlook;

    }

    IEnumerator OnScoped()
    {
        Sway.MaxAmount = newSway;
        yield return new WaitForSeconds(0.8f);
        scopeOverlay.SetActive(true);
        WeaponCamera.SetActive(false);

        normalfov = mainCamera.fieldOfView;
        mainCamera.fieldOfView = ++Zoom;
        senstivity.mouseSensitivity -= slowlook;

    }
}

