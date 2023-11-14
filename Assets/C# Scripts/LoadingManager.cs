
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Sprite[] sprite;
    public Image BackgroundImage;
    public GameObject[] tips;
    private GameObject SelectedTip;
    public int loadingtime = 5;
    private bool a = true;
    



    void Start()
    {
        StartCoroutine(A());
    }

    IEnumerator A()
    {
        StartCoroutine(Disable());
        SelectedTip = tips[Random.Range(0, tips.Length)];
        SelectedTip.SetActive(true);
        BackgroundImage.sprite = sprite[Random.Range(0, sprite.Length)];
        yield return new WaitForSeconds(3);
        SelectedTip.SetActive(false);
        if (a == true)
        {
            
            SelectedTip = tips[Random.Range(0, tips.Length)];
            SelectedTip.SetActive(true);
            a = false;
        }
        
        

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }
   
        

    
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(loadingtime);
        gameObject.SetActive(false);
    }
   
        
       
    
    
}
