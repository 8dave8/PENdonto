using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinJumper : MonoBehaviour
{
    public PhysicsMaterial2D NotBounce;
    //private float Bouncyness = 1;
    public Rigidbody2D Rb2D;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 13);
        Physics2D.IgnoreLayerCollision(10, 10, true);
        Rb2D.GetComponent<Rigidbody2D>();
    }
    void OnCollisionEnter2D(Collision2D col)
    { 
        //Debug.Log(Bouncyness);
        //if (Bouncyness > 0.2) Bouncyness = Bouncyness - 0.1f;
        //else Rb2D.sharedMaterial = NotBounce;
        //Rb2D.sharedMaterial.bounciness = Bouncyness;
    }
}
