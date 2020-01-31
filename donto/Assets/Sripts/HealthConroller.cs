using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthConroller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Sprite FullHearth;
    public Sprite EmptyHearth;
    public GameObject hearth1;
    public GameObject hearth2;
    public GameObject hearth3;
    private static int health;
    private System.Random rng = new System.Random();
    private bool takingDamage = false;
    void Start()
    {
        Physics.IgnoreLayerCollision(11,9);
        health = 3;
    }
    public void takeDamage()
    {
        health--;
        if (health == 2) hearth3.GetComponent<Image>().sprite = EmptyHearth;
        else if (health == 1) hearth2.GetComponent<Image>().sprite = EmptyHearth;
        else if (health == 0) hearth1.GetComponent<Image>().sprite = EmptyHearth;
        if (health <= 0) Death();
    }
    public void addHealth()
    {
        if(health <= 0) Death();
        if(health != 3) health++;

        if(health == 3) hearth3.GetComponent<Image>().sprite = FullHearth;
        else if(health == 2) hearth2.GetComponent<Image>().sprite = FullHearth;
    }
    void OnTriggerStay2D(Collider2D col)
    {   
        if (col.gameObject.tag == "Enemy" && gameObject.layer == 0 || col.gameObject.tag == "Spike")
            gotHit();      
    }
    IEnumerator wait()
    {
        takingDamage = true;
        takeDamage();
        yield return new WaitForSeconds(2);
        takingDamage = false;
    }
    private void gotHit()
    {
        if(takingDamage) return;
        //knokback
        int i = 0;
        if (rng.Next(0,2) == 0) i = 1;
        else i = -1; 
        rb.AddForce(new Vector2(rng.Next(200,300)*i,rng.Next(300,450)));
        //dmg
        StartCoroutine("wait");
    }
    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}