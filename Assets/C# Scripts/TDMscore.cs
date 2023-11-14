using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDMscore : MonoBehaviour
{
    [HideInInspector]
    public float REDScore = 0;
    [HideInInspector]
    public float GreenScore = 0;
    public Text RedPoints;
    public Text Greenpoints;
    public GameObject Win;
    public GameObject Loose;
    public TdmAfterPlayerDeath TDMplayer;
    public GameObject TDMenemy;
    public GameObject TDMfriends;
    public bool hard = false;
    private int winningScore;
    // Start is called before the first frame update
    void Start()
    {
        RedPoints.text = REDScore.ToString("0");
        Greenpoints.text = GreenScore.ToString("0");
        winningScore = hard ? 40 : 50;
    }

    // Update is called once per frame
    void Update()
    {
        RedPoints.text = REDScore.ToString("0");
        Greenpoints.text = GreenScore.ToString("0");
        if(GreenScore >= winningScore)
        {
            
            TDMfriends.SetActive(false);
            TDMenemy.SetActive(false);
            TDMplayer.SpectatorCamera.SetActive(true);
            Win.SetActive(true);
            
        }
        else if(REDScore >= winningScore)
        {
            
            TDMfriends.SetActive(false);
            TDMenemy.SetActive(false);
            TDMplayer.SpectatorCamera.SetActive(true);
            Loose.SetActive(true);
        }
    }
}
