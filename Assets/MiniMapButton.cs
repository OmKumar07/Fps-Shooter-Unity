using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapButton : MonoBehaviour
{
    public Animator MiniMapAnime;
    public void MiniMapOpen()
    {
        MiniMapAnime.SetBool("Close", false);
        MiniMapAnime.SetBool("Open", true);
        
    }
    public void MiniMapClose()
    {
        MiniMapAnime.SetBool("Open", false);
        MiniMapAnime.SetBool("Close", true);


    }
}
