using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterPlayerDeath : MonoBehaviour
{
    public PlayerDeath PlayerDeathScript;
    public GameObject Camera;
    public Camera DeathCam;
    public GameObject minimap;
    public GameObject SoilderBody;
    public GameObject PlayerCam;
    public ScopeIn Sniper;
    public GameObject AfterDeath;
    public GameObject HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        PlayerDeathScript.GetComponent<Target>();
        Sniper.GetComponent<ScopeIn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerDeathScript.dead == true)
        {
            Camera.SetActive(true);
            minimap.SetActive(false);
            SoilderBody.SetActive(true);
            PlayerCam.SetActive(false);
            //HealthBar.SetActive(false);
            Sniper.OnUNscoped();
            StartCoroutine(AfterDeathEffects());
            



        }
    }
  
    IEnumerator AfterDeathEffects()
    {
        yield return new WaitForSeconds(0.1f);
        AfterDeath.SetActive(true);
    }
}
