using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverCnvas;
    public PlayerDeath Player;
    public float WaitTime;
    // Start is called before the first frame update
    void Start()
    {
        Player.GetComponent<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.dead == true)
        {
            StartCoroutine(AfterDeath());
        }
    }
    public IEnumerator AfterDeath()
    {
        yield return new WaitForSeconds(WaitTime);
    }
}
