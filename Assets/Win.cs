using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public Count CountScript;
    public Count CountScript2;
    public Count CountScript3;
    public bool UseWin = false;
    public GameObject WinCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    IEnumerator Winner()
    {
        yield return new WaitForSeconds(0.4f);
        WinCanvas.SetActive(true);

    }
    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void FixedUpdate()
    {
        if(UseWin == true)
        {
            if (CountScript != null && CountScript2 == null && CountScript3 == null)
            {
                if (CountScript.Win == true)
                {
                    
                    StartCoroutine(Winner());
                    //Time.timeScale = 0.2f;
                    
                }
            }

            else if (CountScript3 == null && CountScript2 != null && CountScript != null)
            {
                if (CountScript.Win == true && CountScript2.Win == true)
                {
                    print("FinalWin2");
                    StartCoroutine(Winner());
                    //Time.timeScale = 0.2f;
                }
            }

            else if (CountScript3 != null && CountScript2 != null && CountScript != null)
            {
                if (CountScript.Win == true && CountScript2.Win == true && CountScript3.Win == true)
                {
                    print("FinalWin3");
                    StartCoroutine(Winner());
                    //Time.timeScale = 0.2f;
                }
            }
        }
        
        
    }
}
