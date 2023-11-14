using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GunScript : MonoBehaviour

{   //where the bullets start

    public GameObject fpsCam;

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
    public GameObject Bulletholeblood;
    

    //Reaload

    public int maxAmmo = 5;
    public int currentAmmo = -1;
    public float ReloadTime;
    public int Ammo = 15;
    public GameObject reload;
    public GameObject NoAmmo;
    public int reloadDisplay = 2;
    public GameObject Reloading;

    [HideInInspector]
    public bool isReloding = false;

    public Animator animator;

    //Target

    public float Backforce = 30f;

    //FireRate Of Gun

    public float FireRAte = 80f;
    private float NextTimeToFire = 0.2f;

    

    //Recoil Of Gun


   

    [HideInInspector]
    public bool shooting = false;
    public Animator Camshake;



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


    void Start()
    {
        currentAmmo = maxAmmo;
        Camshake.GetComponent<Animator>();


        Cross1.color = NormalColour;
        Cross2.color = NormalColour;
        Cross3.color = NormalColour;
        Cross4.color = NormalColour;
    }

    //Fixing A Minor Bug
    void OnEnable ()
    {
        isReloding = false;
        animator.SetBool("Realoading", false);
    }

    void FixedUpdate()
    {
        RaycastHit hit2;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit2, range))
        {
            if (CrossHAirColourChange == true)
            {
                if (isReloding == true)
                {
                    print("TArget");
                    Cross1.color = Color.red;
                    Cross2.color = Color.red;
                    Cross3.color = Color.red;
                    Cross4.color = Color.red;


                }
                else if (Ammo<=0 && currentAmmo<=0)
                {
                    Cross1.color = Color.red;
                    Cross2.color = Color.red;
                    Cross3.color = Color.red;
                    Cross4.color = Color.red;

                }


            

                else if (hit2.transform.tag == "Targets")
                {
                    print("TArget");
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

                else
                {
                    Cross1.color = NormalColour;
                    Cross2.color = NormalColour;
                    Cross3.color = NormalColour;
                    Cross4.color = NormalColour;

                }
            }


        }
    }
    void Update()
    {
        if (AmmoText == true)
        {
            AllAmmo.text = Ammo.ToString("0");
        }
        if (AmmoText == true)
        {
            LiveAmmo.text = currentAmmo.ToString("0");
        }

        if (currentAmmo <= reloadDisplay)
        {
            reload.SetActive(true);
        }
        else if (currentAmmo > reloadDisplay)
        {
            reload.SetActive(false);
        }
        if (Ammo <= 0)
        {
            NoAmmo.SetActive(true);
            Reloading.SetActive(false);
        }
        else if (Ammo >= 0)
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
        if (isReloding)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1.1f / FireRAte;
            if(isReloding == false && Ammo > 0)
            {
                Shoot();
            }
            
            //muzzelFlash.Play();

            

        }
        else
        {
            animator.SetBool("Fire", false);
            Camshake.SetBool("Sfire", false);
        }
            
    }


    public IEnumerator Reload ()
    {
        isReloding = true;
        Reloading.SetActive(true);

        animator.SetBool("Realoading", true);
        Camshake.SetBool("Sfire", false);

        yield return new  WaitForSeconds(ReloadTime - .25f);
        animator.SetBool("Realoading", false);
        yield return new WaitForSeconds(-.25f);

        currentAmmo = maxAmmo;
        Ammo -= currentAmmo;
        Reloading.SetActive(false);

        isReloding = false;
    }

    void Shoot()
    {
        Reloading.SetActive(false);
        shooting = true;
        muzzelFlash.Play();

        animator.SetBool("Fire", true);
        Camshake.SetBool("Sfire", true);

        currentAmmo--;

        RaycastHit hit;
       if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * Backforce);
            }
            AttachedBomb bomb = hit.transform.GetComponent<AttachedBomb>();
            if (bomb != null)
            {
                bomb.TakeDamage(damage);
            }
            TurretDestroy targetTurret = hit.transform.GetComponent<TurretDestroy>();
            if (targetTurret != null)
            {
                targetTurret.TakeDamage(damage);
            }
            Destructables destroy = hit.transform.GetComponent<Destructables>();
            if (destroy != null)
            {
                destroy.TakeDamage(damage);
            }
            DanamiteBlast dynamite = hit.transform.GetComponent<DanamiteBlast>();
            if (dynamite != null)
            {
                dynamite.TakeDamage(damage);
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
                Destroy(Hole, 5f);
            }
            if (hit.transform.tag == "Wood")
            {
                GameObject impactGO = Instantiate(impactEffectWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }
            if (hit.transform.tag == "WoodBox")
            {
                GameObject impactGO = Instantiate(impactEffectWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeWood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }
            if (hit.transform.tag == "Matle")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }

            if (hit.transform.tag == "Turret")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }
            if ( hit.transform.tag == "Tank")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }
            if (hit.transform.tag == "Targets")
            {
                GameObject impactGO = Instantiate(impactEffectMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeMatle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }

            if (hit.transform.tag == "Concrete")
            {
                GameObject impactGO = Instantiate(impactEffectConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }

            if (hit.transform.tag == "Dynamite")
            {
                GameObject impactGO = Instantiate(impactEffectConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(BulletholeConcrete, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);
            }
            if (hit.transform.tag == "Enemy")
            {
                GameObject impactGO = Instantiate(impactEffectBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2.5f);
                GameObject Hole = Instantiate(Bulletholeblood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(Hole, 5f);

            }

        }


    }
}
