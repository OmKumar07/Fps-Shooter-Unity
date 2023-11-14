using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Loading;
    public int waittime;
    public GameObject tdmVarient;
    public Animator TdmVarient;
    public GameObject About;
    public Animator TDMbuttonAnim;
    public Animator TrainingAnim;
    public Animator AboutAnim;
    public Animator StartAnim;
    public GameObject MAPselect;
    public GameObject CommingSoon;
    
    private void Start()
    {
        Loading.SetActive(false);
        StartCoroutine(StartLoad());
    }
    public void TDMLoad()
    {
        TdmVarient.SetBool("Normal", true);
        StartCoroutine(Loadtdmeasy());
        
    }
    public void TDMbutton()
    {
        TDMbuttonAnim.SetBool("Pressed", true);
        StartCoroutine(TDM());
    }
    public void TDMbuttonClose()
    {
        TdmVarient.SetBool("Down", true);
        StartCoroutine(close());
    }
    IEnumerator close()
    {
        yield return new WaitForSeconds(0.5f);
        TdmVarient.SetBool("Down", false);
        tdmVarient.SetActive(false);
    }
    public void TrainingLoad()
    {
        TrainingAnim.SetBool("Pressed", true);
        StartCoroutine(Loadtraining());
    }
    public void loadTdmhard()
    {
        TdmVarient.SetBool("Hardcore", true);
        StartCoroutine(Loadtdmhard());
    }
    public void AboutButton()
    {
        AboutAnim.SetBool("Pressed", true);
        StartCoroutine(about());
        
    }
    public void AboutOkButton()
    {
        About.SetActive(false);
    }
    IEnumerator about()
    {
        yield return new WaitForSeconds(0.22f);
        AboutAnim.SetBool("Pressed", false);
        About.SetActive(true);
    }
    public void newMAp()
    {
        
        
        CommingSoon.SetActive(true);
        StartCoroutine(NewMapClick());
    }
    IEnumerator NewMapClick()
    {
        yield return new WaitForSeconds(1f);
        
        CommingSoon.SetActive(false);
        

    }
        IEnumerator Loadtdmeasy()
    {
        yield return new WaitForSeconds(0.25f);
        TdmVarient.SetBool("Normal", false);
        Loading.SetActive(true);
        yield return new WaitForSeconds(waittime);
        
        SceneManager.LoadScene("TDM");
    }
    public void StartButton()
    {
        StartCoroutine(StartDelay());
    }
    public void StartButtonClose()
    {
        MAPselect.SetActive(false);
    }
    IEnumerator StartDelay()
    {
        
        StartAnim.SetBool("Pressed", true);
        yield return new WaitForSeconds(0.25f);
        StartAnim.SetBool("Pressed", false);
        MAPselect.SetActive(true);


    }
    IEnumerator Loadtdmhard()
    {
        yield return new WaitForSeconds(0.25f);
        TdmVarient.SetBool("Hardcore", false);
        Loading.SetActive(true);
        yield return new WaitForSeconds(waittime);

        SceneManager.LoadScene("TDM(Hard)");
    }
    IEnumerator Loadtraining()
    {
        yield return new WaitForSeconds(0.22f);
        TrainingAnim.SetBool("Pressed", false);
        Loading.SetActive(true);
        yield return new WaitForSeconds(waittime);

        SceneManager.LoadScene("Training");
    }
    IEnumerator StartLoad()
    {
        Loading.SetActive(true);
        yield return new WaitForSeconds(7);
        Loading.SetActive(false);
        
    }
    IEnumerator TDM()
    {
        
        yield return new WaitForSeconds(0.22f);
        tdmVarient.SetActive(true);
        TDMbuttonAnim.SetBool("Pressed", false);


    }
}
