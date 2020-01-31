using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSide2side : MonoBehaviour
{
    public Transform Skin;
    public float x1, x2;
    public float speed = 2.0f;
    private Vector3 pos1;
    private Vector3 pos2;
    private bool facingRight = false;
    void Start()
    {
        pos1 = new Vector3(x1,transform.position.y,Skin.position.z);
        pos2 = new Vector3(x2,transform.position.y,Skin.position.z);
    }
    void Update () {
        transform.position = Vector3.Lerp (pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));
        if (transform.position.x <= x1+0.2f && !facingRight)
        {
            facingRight = !facingRight;
            Skin.localScale = new Vector3(Skin.localScale.x*-1,Skin.localScale.y,Skin.localScale.z);
        }
        else if (transform.position.x >= x2-0.2f && facingRight)
        {
            facingRight = !facingRight;
            Skin.localScale = new Vector3(Skin.localScale.x*-1,Skin.localScale.y,Skin.localScale.z);
        }
    }
}
