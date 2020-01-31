using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float rundpeed = 40;
    public bool jump = false;
    private float horMove = 0;
    void Update()
    {
        horMove = CrossPlatformInputManager.GetAxis("Horizontal") * rundpeed; //Mobile Controll
        if (CrossPlatformInputManager.GetButtonDown("Jump")) jump = true; 
        
        //horMove = Input.GetAxisRaw("Horizontal") * rundpeed;                //Pc controll
        //if ((Input.GetButtonDown("Jump")) jump = true;
    }
    private void FixedUpdate()
    {
        controller.Move(horMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
