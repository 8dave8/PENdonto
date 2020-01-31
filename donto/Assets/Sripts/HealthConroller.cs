using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthConroller : MonoBehaviour
{
    private int savedHP;
    private GameObject last;
    private GameObject spawnPont;
    public Sprite on;
    public GameObject Save;
    public Rigidbody2D rb;
    public Sprite FullHearth;
    public Sprite EmptyHearth;
    //public GameObject hearth1;
    //public GameObject hearth2;
    //public GameObject hearth3;

    public GameObject[] hearts;
    int maxHealth = 5;
    private static int health;
    private System.Random rng = new System.Random();
    private bool takingDamage = false;
    void Start()
    {
        //spawnPont.transform.position = transform.position;
        maxHealth = hearts.Length;
        rb.GetComponent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(11,9);
        health = maxHealth;

        // DEBUG
        for (int i = 0; i < 2; i++) takeDamage();

        /*for (int i = 0; i < 2; i++)
        {
            addHealth();
        }*/
    }
    public void takeDamage()
    {
        health--;
        if(health <= 0)
        {
            Death();
            return;
        }
        hearts[health].GetComponent<Image>().sprite = EmptyHearth;


        /*if (health == 2) hearth3.GetComponent<Image>().sprite = EmptyHearth;
        else if (health == 1) hearth2.GetComponent<Image>().sprite = EmptyHearth;
        else if (health == 0) hearth1.GetComponent<Image>().sprite = EmptyHearth;
        if (health <= 0) Death();*/
    }
    public void addHealth()
    {
        if(health != maxHealth){
            hearts[health].GetComponent<Image>().sprite = FullHearth;
            health++;
        }

        /*if(health <= 0) Death();
        if(health != 3) health++;

        if(health == 3) hearth3.GetComponent<Image>().sprite = FullHearth;
        else if(health == 2) hearth2.GetComponent<Image>().sprite = FullHearth;*/
    }
    void OnTriggerStay2D(Collider2D col)
    {   
        if ((col.gameObject.tag == "Enemy" && gameObject.layer == 0) || col.gameObject.tag == "Spike")
            gotHit();      
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "checkpoint" && spawnPont != col.gameObject)
        {
                last = col.gameObject;
                Save.SetActive(true);
                Time.timeScale = 0;
        }
        if(col.gameObject.tag == "instakill") Death();
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
        if(spawnPont == null) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else{
            transform.position = spawnPont.transform.position;
            for (int i = 0; i < savedHP; i++)  addHealth();
        }
    }
    public void mentes()
    {
        spawnPont = last;
        Save.SetActive(false);
        Time.timeScale = 1;
        if(health > 1) takeDamage();
        else
        {
            Death();
            return;
        }
        spawnPont.GetComponentInChildren<SpriteRenderer>().sprite = on;
        spawnPont.transform.position = transform.position;
        savedHP = health;
    }
    public void Buyhealth()
    {
        if(PlayerPrefs.GetInt("coins") >= 10)
        {   
            addHealth();
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") -10);
            GetComponent<PickupController>().coinText.text = PlayerPrefs.GetInt("coins").ToString("000");
        }
        Time.timeScale = 1;
    }
}