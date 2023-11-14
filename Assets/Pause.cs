using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public Button PauseButton;
   public void pause()
    {
        PauseMenu.SetActive(true);
        PauseButton.interactable = false;
        StartCoroutine(PauseDelay());
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
        
    }
    IEnumerator PauseDelay()
    {
        yield return new WaitForSeconds(0.28f);
        Time.timeScale = 0.02f;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseButton.interactable = true;
        PauseMenu.SetActive(false);
    }
}
