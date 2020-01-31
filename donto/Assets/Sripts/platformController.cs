using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.collider.transform.SetParent(transform);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.collider.transform.SetParent(null);
        }
    }
}
