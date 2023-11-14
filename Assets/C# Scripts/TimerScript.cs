using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float MaxTime = 5f;
    [HideInInspector]
    public float timeLeft;
    public Text TimerText;
    
    void Start()
    {
        timeLeft = MaxTime;

    }
   

    // Update is called once per frame
    void Update()
    {
        if(timeLeft <= 0.01)
        {
            timeLeft = MaxTime;
        }
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            TimerText.text = timeLeft.ToString("0");
        }
        
    }
}
