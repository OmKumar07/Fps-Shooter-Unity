using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBulbs : MonoBehaviour
{
    public Destructables Object;

    [Space(20)]
    public Light BlinkLight;
    public Light BlinkLight2;
    public Light BlinkLight3;
    public Light BlinkLight4;
    public Light BlinkLight5;
    public Light BlinkLight6;
    public Light BlinkLight7;
    public Light BlinkLight8;
    public Light BlinkLight9;

    [HideInInspector]
    public bool blinking = false;
    void Start()
    {
        Object.GetComponent<Destructables>();
        BlinkLight.GetComponent<Light>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Object.destroyed == true)
        {
            blinking = true;
        }
        if(Object.destroyed == true)
        {
            
            
            
            if(blinking == true)
            {
                StartCoroutine(blink());
                StartCoroutine(blinkdestroy());
            }
            
        }
        IEnumerator blink()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight.enabled = !BlinkLight.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight2.enabled = !BlinkLight2.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight3.enabled = !BlinkLight3.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight4.enabled = !BlinkLight4.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight5.enabled = !BlinkLight5.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight6.enabled = !BlinkLight6.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight7.enabled = !BlinkLight7.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight8.enabled = !BlinkLight8.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.8f));
                BlinkLight9.enabled = !BlinkLight8.enabled;
            }
        }
        IEnumerator blinkdestroy()
        {
            
                yield return new WaitForSeconds(Random.Range(2f, 5f));
                BlinkLight.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            BlinkLight2.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            BlinkLight3.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            BlinkLight4.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            BlinkLight5.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            BlinkLight6.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            BlinkLight7.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(5f, 5f));
            BlinkLight8.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(5f, 5f));
            BlinkLight9.gameObject.SetActive(false);



        }
    }
}
