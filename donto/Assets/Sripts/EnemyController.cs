using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public SpriteRenderer Enemyskin;
    public GameObject[] Coins;
    public GameObject Hearth;
    public int maxHP = 2;
    public int currentHP;
    public float villog;
    public int spawnedObjects;
    private static System.Random rng;
    void Start()
    {
        currentHP = maxHP;
        rng = new System.Random();
    }
    void Update()
    {
        checkStatus();
    }
    private void checkStatus()
    {
        if(currentHP > maxHP) currentHP = maxHP;
        if(currentHP < 0) currentHP = 0;
        if(currentHP == 0) death();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "sword") receiveDamage();
    }
    public void receiveDamage(){  
        currentHP--;
        DamageFlashing();
    }
    private void DamageFlashing()
    {
        StartCoroutine("Villog");
    }
    public void death(){         
         for (int i = 0; i < spawnedObjects; i++)
         {
            if (rng.Next(0,5) == 1)
            {
                GameObject hearth =  Instantiate(Hearth,new Vector3(transform.position.x,transform.position.y+1.2f,0),Quaternion.identity);
                hearth.GetComponent<Rigidbody2D>().velocity = new Vector2(rng.Next(2, 4),rng.Next(3, 6));
            }
            else 
            {
                int rnd = rng.Next(0,1);
                GameObject coin = Instantiate(Coins[rnd],new Vector3(transform.position.x,transform.position.y+1.2f,0), Quaternion.identity);
                coin.GetComponent<Rigidbody2D>().velocity = new Vector2(rng.Next(2, 4),rng.Next(3, 6));
            }
         }    
         Destroy(this.gameObject); 
    }
    IEnumerator Villog()
    {
        for (int i = 0; i < 5; i++)
        {
            Enemyskin.gameObject.SetActive(false);
            yield return new WaitForSeconds(villog);
            Enemyskin.gameObject.SetActive(true);
            yield return new WaitForSeconds(villog);
        }
    }
}
