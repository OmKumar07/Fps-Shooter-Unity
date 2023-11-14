using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TdmResult : MonoBehaviour
{
   public void Replay()
    {
        SceneManager.LoadScene("TDM");
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
