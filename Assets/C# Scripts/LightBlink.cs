using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    public Light BlinkLight;
    public float MinTime = 0.2f;
    public float MaxTime = 0.5f;
    void Start()
    {
        StartCoroutine(BLink());
    }

    // Update is called once per frame
   IEnumerator BLink()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
            BlinkLight.enabled = !BlinkLight.enabled;
        }
    }
}
