using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    public Text text;

    void Start()
    {
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        text.enabled = false;
        yield return new WaitForSeconds(0.3f);
        text.enabled = true;
        yield return new WaitForSeconds(0.7f);

        StartCoroutine("Blink");
    }
}
