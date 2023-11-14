using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    // Start is called before the first frame update


    [HideInInspector]
    public int ChildCount;
    public Text EnemyCount;
    public Text KillCount;
    [HideInInspector]
    public int OrgChildCount;
    [HideInInspector]
    public int Killed;
    [HideInInspector]
    public bool Win = false;
    void Start()
    {
        Win = false;
        EnemyCount.GetComponent<Text>();
        OrgChildCount = transform.childCount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Killed = OrgChildCount - ChildCount;
        ChildCount = transform.childCount;
        EnemyCount.text = OrgChildCount.ToString("0");
        KillCount.text = Killed.ToString("0");
     
        if(ChildCount == 0)
        {


            Win = true;
            
            
        }
            
        
    }
    
}
