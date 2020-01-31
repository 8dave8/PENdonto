using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public Sprite NotPressedBT,PressedBT;
    public void Pressed()
    {
        gameObject.GetComponent<Image>().sprite = PressedBT;
        StartCoroutine("wait");
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.GetComponent<Image>().sprite = NotPressedBT;
    }
}
