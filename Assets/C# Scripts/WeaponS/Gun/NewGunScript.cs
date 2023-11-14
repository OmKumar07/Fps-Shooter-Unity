using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class NewGunScript : MonoBehaviour
{   //where the bullets start


    public Transform firepoint;
    

    //Gun

    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzelFlash;

    //Bullet Effect

    public GameObject impactEffectSnow;
    public GameObject BulletholeSnow;
    public GameObject impactEffectWood;
    public GameObject BulletholeWood;
    public GameObject impactEffectMatle;
    public GameObject BulletholeMatle;
    public GameObject impactEffectConcrete;
    public GameObject BulletholeConcrete;
    public GameObject impactEffectBlood;
    public GameObject BulletholeBlood;
   

    //Reaload

    public int maxAmmo = 30;
    public int currentAmmo = -1;
    private int AmmoToMinus;
    public float ReloadTime;
    private bool isReloding = false;
    public Animator animator;
    public int Ammo = 80;
    
    public GameObject reload;
    public GameObject NoAmmo;
    
    public int reloadDisplay = 10;
    public GameObject Reloading;
    

    //Target

    public float Backforce = 30f;

    //FireRate Of Gun

    public float FireRAte = 80f;
    private float NextTimeToFire = 0.2f;
    public Animator CrossHair;

    public Animator anim;

    //For Bullet Out Effect
    [Header("Bullet out effect")]
    [Space(20)]

    public ParticleSystem CartridgeEjectEffect;

    public bool BulletOutEffect;
    

    public bool ShortGunShake = false;
    public bool GunShake = false;
    public Animator Camera;

    
    public AudioClip shootSound;
    //public AudioClip takeOutSound;
    //public AudioClip holsterSound;
    // public AudioClip reloadSoundOutOfAmmo;
    public AudioClip reloadSoundAmmoLeft;
    public AudioClip matleHit;
    public AudioClip NorMalHit;
    public AudioClip WoodBodyHit;
    public AudioClip GroundHit;
    //private float ShootAudioPitch;
    public float ReloAdPitch = 1.1f;

    [Header("ShellDrop")]
    public AudioSource shelldrop;
    public AudioClip ShellDrop;

    private bool soundHasPlayed = false;

    [Header("Audio Source")]
    //Main audio source
    public AudioSource mainAudioSource;
    //Audio source used for shoot sound
    public AudioSource shootAudioSource;
    public float FirePitch = 1f;
    public float FireVolume = 1f;
    public AudioClip NoAmmoClip;


    [Header("For BulletText")]
    public bool AmmoText = true;
    public Text AllAmmo;
    public Text LiveAmmo;


    [Space]
    public bool CrossHAirColourChange = false;
    public Image Cross1;
    public Image Cross2;
    public Image Cross3;
    public Image Cross4;
    public Color OnTArget;
    public Color NormalColour;


    [Header("For Tdm")]
    private bool friend = true;

    void Start()
    {
       
        currentAmmo = maxAmmo;
        anim.GetComponent<Animator>();
        //Enemy.GetComponent<Animator>();
       
        
        
        
        CrossHair.GetComponent<Animator>();
        //shootAudioSource.clip = shootSound;


        Cross1.color = NormalColour;
        Cross2.color = NormalColour;
        Cross3.color = NormalColour;
        Cross4.color = NormalColour;
    }

    //Fixing A Minor Bug
    void OnEnable()
    {
        isReloding = false;
        animator.SetBool("Realoading", false);
    }
   
    IEnumerator NoAmmoLeft()
    {
        yield return new WaitForSeconds(0f);
        shootAudioSource.Play();

    }
    IEnumerator Crosshair()
    {
        yield return new WaitForSeconds(0.5f);
        CrossHair.SetBool("Recoil", true);
        
    }

    private void FixedUpdate()
    {
        if (Ammo < 0)
        {
            Ammo = 0;
        }
        AmmoToMinus = maxAmmo - currentAmmo;

        if (currentAmmo == maxAmmo)
        {
            reload.SetActive(false);
        }


        if (currentAmmo <= reloadDisplay)
        {
            reload.SetActive(true);
        }
        else if (currentAmmo > reloadDisplay)
        {
            reload.SetActive(false);
        }

        if (Ammo <= 0 && currentAmmo == 0)
        {
            NoAmmo.SetActive(true);
        }
        else if (Ammo > 0)
        {
            NoAmmo.SetActive(false);
        }
        if (currentAmmo == 0)
        {
            Reloading.SetActive(true);
            reload.SetActive(false);
        }
        if (currentAmmo == 0 && Ammo == 0)
        {
            Reloading.SetActive(false);
        }

        //CrossHair Colour Change
        RaycastHit hit2;
        if(Physics.Raycast(firepoint.transform.position, firepoint.transform.forward, out hit2, range))
        {
            if (CrossHAirColourChange == true)
            {
                if (isReloding == true)
                {

                    Cross1.color = Color.red;
                    Cross2.color = Color.red;
                    Cross3.color = Color.red;
                    Cross4.color = Color.red;


                }
                else if (Ammo <= 0 && currentAmmo<=0)
                {
                    
                    Cross1.color = Color.red;
                    Cross2.color = Color.red;
                    Cross3.color = Color.red;
                    Cross4.color = Color.red;


                }

                else if (hit2.transform.tag == "Targets")
                {
                    
                    Cross1.color = OnTArget;
                    Cross2.color = OnTArget;
                    Cross3.color = OnTArget;
                    Cross4.color = OnTArget;


                }
                else if (hit2.transform.tag == "Enemy")
                {
                    Cross1.color = OnTArget;
                    Cross2.color = OnTArget;
                    Cross3.color = OnTArget;
                    Cross4.color = OnTArget;
                }
                else if (hit2.transform.tag == "Turret")
                {
                    Cross1.color = OnTArget;
                    Cross2.color = OnTArget;
                    Cross3.color = OnTArget;
                    Cross4.color = OnTArget;
                }
                else if (hit2.transform.tag == "Dynamite")
                {
                    Cross1.color = OnTArget;
                    Cross2.color = OnTArget;
                    Cross3.color = OnTArget;
                    Cross4.color = OnTArget;
                }
                else if (hit2.transform.tag == "WoodBox")
                {
                    Cross1.color = OnTArget;
                    Cross2.color = OnTArget;
                    Cross3.color = OnTArget;
                    Cross4.color = OnTArget;
                }
                else if (hit2.transform.tag == "TDMfriend")
                {
                    Cross1.color = Color.red;
                    Cross2.color = Color.red;
                    Cross3.color = Color.red;
                    Cross4.color = Color.red;
                    friend = true;
                }
                else if (hit2.transform.tag == "TDMenemy")
                {
                    Cross1.color = OnTArget;
                    Cross2.color = OnTArget;
                    Cross3.color = OnTArget;
                    Cross4.color = OnTArget;
                }
                else
                {
                    friend = false;
                    Cross1.color = NormalColour;
                    Cross2.color = NormalColour;
                    Cross3.color = NormalColour;
                    Cross4.color = NormalColour;

                }
            }
            

        }
        





    }
    void dontShootFriend()
    {
        if (friend == true)
        {

            anim.SetBool("Fire", false);
            if (ShortGunShake == true)
            { Camera.SetBool("Sfire", false); }
            if (GunShake == true)
            { Camera.SetBool("Fire", false); }
            CrossHair.SetBool("Recoil", false);
        }
    }
    void Update()
    {
        dontShootFriend();
        
        if (AmmoText == true)
        {
            AllAmmo.text = Ammo.ToString("0");
        }
        if(AmmoText == true)
        {
            LiveAmmo.text = currentAmmo.ToString("0");
        }
        
       
        if (isReloding)
            return;

        if (currentAmmo <= 0 && AmmoToMinus > 0 && Ammo > 0)
        {
            StartCoroutine(Reload());
            
            return;
        }
        if (Input.GetKey(KeyCode.R) && AmmoToMinus > 0)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetButton("Fire1"))
        {
            
            if (Time.time >= NextTimeToFire)
            {
                if (Ammo <= 0 && currentAmmo <= 0)
                {
                    shootAudioSource.clip = NoAmmoClip;
                    shootAudioSource.pitch = FirePitch;
                    shootAudioSource.volume = FireVolume;
                    StartCoroutine(NoAmmoLeft());
                }

                NextTimeToFire = Time.time + 1f / FireRAte;

                
                if (isReloding == false && currentAmmo > 0)
                {
                    if(friend == false)
                    {
                        Shoot();
                    }
                    
                    
                }

                //muzzelFlash.Play();
            }

        }
        else{anim.SetBool("Fire", false);
            if(ShortGunShake == true)
            {Camera.SetBool("Sfire", false); }
            if (GunShake == true)
            { Camera.SetBool("Fire", false); }
            CrossHair.SetBool("Recoil", false);


        }
            
        
    }

    //IEnumerator HitDisable()
    //{
     //   yield return new WaitForSeconds(0f);
      //  Enemy.SetBool("Hit", false);
   // }
   


    public IEnumerator Reload()
    {
        
        isReloding = true;
        Reloading.SetActive(true);
        animator.SetBool("Realoading", true);


        shootAudioSource.volume = FireVolume;
        shootAudioSource.pitch = ReloAdPitch;
        shootAudioSource.clip = reloadSoundAmmoLeft;
        shootAudioSource.Play();


        animator.SetBool("Fire", false);
        if (ShortGunShake == true)
        {
            Camera.SetBool("Sfire", false);
        }
        if (GunShake == true)
        {
            Camera.SetBool("Fire", false);
        }
        CrossHair.SetBool("Recoil", false);
        


        yield return new WaitForSeconds(ReloadTime - .25f);
        animator.SetBool("Realoading", false);
        
        yield return new WaitForSeconds(-.25f);

        
        if (Ammo < maxAmmo)
        {
            currentAmmo = Ammo;
        }
        else
            currentAmmo = maxAmmo;

        Ammo -= AmmoToMinus;
        Reloading.SetActive(false);

        isReloding = false;
    }
    IEnumerator shellDrop()
    {
        yield return new WaitForSeconds(0.15f);
        shelldrop.volume = 0.5f;
       shelldrop.clip = ShellDrop;
        shelldrop.Play();
    }
   void Shoot()
    {
        

        Reloading.SetActive(false);
        StartCoroutine(Crosshair());
        if (ShortGunShake == true)
        {
            Camera.SetBool("Sfire", true);
        }
        if (GunShake == true)
        {
            Camera.SetBool("Fire", true);
        }
       

        muzzelFlash.Play();
        anim.SetBool("Fire", true);

        if (BulletOutEffect == true)
        CartridgeEjectEffect.Play();
       
        
        

        currentAmmo--;
        shootAudioSource.clip = shootSound;
        shootAudioSource.pitch = FirePitch;
        shootAudioSource.Play();
        StartCoroutine(shellDrop());

        RaycastHit hit;

        Vector3 fwd = firepoint.TransformDirection(Vector3.forward);
        Debug.DrawRay(firepoint.position, fwd * range, Color.green);

            if (Physics.Raycast(firepoint.transform.position, firepoint.transform.forward, out hit, range))
        {
          //  if (hit.collider.CompareTag("Enemy"))
           // {
            //    Enemy.SetBool("Hit", true);
            //    
           // }
           // else
               // Enemy.SetBool("Hit", false);


            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            AttachedBomb bomb = hit.transform.GetComponent<AttachedBomb>();
            if(bomb != null)
            {
                bomb.TakeDamage(damage);
            }
            Destructables Objects = hit.transform.GetComponent<Destructables>();
            if(Objects != null)
            {
                Objects.TakeDamage(damage);
            }

            DanamiteBlast Blast = hit.transform.GetComponent<DanamiteBlast>();
            if (Blast != null)
            {
                Blast.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * Backforce);
            }
            TurretDestroy targetTurret = hit.transform.GetComponent<TurretDestroy>();
            if (targetTurret != null)
            {
                targetTurret.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * Backforce);
            }

            TrainingTarget Trainingtrget = hit.transform.GetComponent<TrainingTarget>();
            if (Trainingtrget != null)
            {
                Trainingtrget.TakeDamage(damage);
            }

            if (hit.transform.tag == "Ground")
            {
                GameObject impactGO = Instantiate(impactEffectSnow, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeSnow, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 2.5f);
                mainAudioSource.pitch = Random.Range(1.5f, 2.5f);
                mainAudioSource.volume = Random.Range(0.4f, 0.5f);
                mainAudioSource.clip = GroundHit;
                mainAudioSource.Play();
            }
            if (hit.transform.tag == "Wood")
            {
                GameObject impactGO = Instantiate(impactEffectWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 2.5f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = WoodBodyHit;
                mainAudioSource.Play();
            }
            if (hit.transform.tag == "WoodBox")
            {
                GameObject impactGO = Instantiate(impactEffectWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 2.5f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = WoodBodyHit;
                mainAudioSource.Play();
            }
            if (hit.transform.tag == "Tank")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 3f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 1f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = matleHit;
                mainAudioSource.Play();

                
            }
            if (hit.transform.tag == "Matle")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 3f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 1f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = matleHit;
                mainAudioSource.Play();


            }
            if (hit.transform.tag == "Targets")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 3f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 1f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = matleHit;
                mainAudioSource.Play();


            }
            if ( hit.transform.tag == "Turret")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 3f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 1f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = matleHit;
                mainAudioSource.Play();


            }
            if (hit.transform.tag == "Concrete")
            {
                GameObject impactGO = Instantiate(impactEffectConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 2.5f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = NorMalHit;
                mainAudioSource.Play();
            }
            if (hit.transform.tag == "Dynamite")
            {
                GameObject impactGO = Instantiate(impactEffectConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 2.5f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.2f, 0.5f);
                mainAudioSource.clip = NorMalHit;
                mainAudioSource.Play();
            }
            if (hit.transform.tag == "Enemy")
            {
                GameObject impactGO = Instantiate(impactEffectBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 3f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.8f, 1f);
                mainAudioSource.clip = WoodBodyHit;
                mainAudioSource.Play();


            }
            if (hit.transform.tag == "TDMfriend")
            {
                GameObject impactGO = Instantiate(impactEffectBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 3f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.5f, 1f);
                mainAudioSource.clip = WoodBodyHit;
                mainAudioSource.Play();


            }
            if (hit.transform.tag == "TDMenemy")
            {
                GameObject impactGO = Instantiate(impactEffectBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 3f);
                mainAudioSource.pitch = Random.Range(1f, 2.5f);
                mainAudioSource.volume = Random.Range(0.5f, 1f);
                mainAudioSource.clip = WoodBodyHit;
                mainAudioSource.Play();


            }


        }


    }
}
