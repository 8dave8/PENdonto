using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupController : MonoBehaviour
{
    public GameObject buymenu;
    public Text coinText;
    private bool colliding;
    void Start()
    {
        colliding = false;
        int coins = PlayerPrefs.GetInt("coins");
        /*if (coins <= 9) coinText.text = "00"+coins;
        else if (coins <= 99) coinText.text = "0"+coins;
        else coinText.text = coins+"";*/

        coinText.text = coins.ToString("000");
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Coin") && gameObject.layer == 0 && colliding == false)
        {  
            Destroy(col.transform.parent.gameObject);
            colliding = true;
            StartCoroutine("wait");
            int coins = PlayerPrefs.GetInt("coins") - 1;
            PlayerPrefs.SetInt("coins", coins);
            /*if (coins <= 9) coinText.text = "00"+coins;
            else if (coins <= 99)  coinText.text = "0"+coins;
            else coinText.text = coins+"";*/
            coinText.text = coins.ToString("000");
        }
        else if(col.gameObject.CompareTag("Hearth") && gameObject.layer == 0 && colliding == false)
        {
            Destroy(col.gameObject);
            colliding = true;
            StartCoroutine("wait");
            GetComponent<HealthConroller>().takeDamage();
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "cukrosbacsi")
            {
                buymenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.01f);
        colliding = false;
    }
}