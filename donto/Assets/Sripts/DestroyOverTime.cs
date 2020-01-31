using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float Lifetime;
    private float start = 0;
    void Update()
    {
        start += Time.deltaTime;
        if (start >= Lifetime)  Destroy(gameObject);
    }
}
