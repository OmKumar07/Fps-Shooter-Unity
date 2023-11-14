using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour

{
    public int selectedWeapon = 0;
    public PlayerDeath Player;
    public AudioSource Source;
    public AudioClip Clip;
    public float Volume;
    public float Pitch;


    public GameObject BulletText;    

    // Start is called before the first frame update
    void Start()
        
    {
        Selectweapon();
        Player.GetComponent<PlayerDeath>();
        Source.GetComponent<AudioClip>();
    }

    // Update is called once per frame
    void Update()

    {
        

        if(Player.dead == true)
        {
            gameObject.SetActive(false);
        }

        int Previousselectedweapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            StartCoroutine(WeaponChange());
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        } if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            StartCoroutine(WeaponChange());
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
                    }

        if(Previousselectedweapon != selectedWeapon)
        {
            StartCoroutine(Selectweapon());
            
            
        }
    }

    IEnumerator WeaponChange()
    {
        BulletText.SetActive(false);
        Source.clip = Clip;
        Source.volume = Volume;
        Source.pitch = Pitch;
        Source.Play();
        yield return new WaitForSeconds(0.2f);
        BulletText.SetActive(true);
    }
    IEnumerator Selectweapon()
    {
        yield return new WaitForSeconds(0f);
        int i = 0;
        

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);

            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }


}
