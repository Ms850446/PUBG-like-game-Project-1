using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMotor : MonoBehaviour
{
    CharacterController controller;
    Vector3 playerVelocity;
    public float speed=5f;
    bool isGrounded;
    public const float gravity=-9.8f;
    public const int jumpHeight=3;
    // Start is called before the first frame update
    void Start()
    {
        controller =GetComponent<CharacterController>();
        playerVelocity.y=0;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded=controller.isGrounded;
    }
public void ProcessMove(Vector2 input){
    Vector3 moveDirection =Vector3.zero;
    moveDirection.x=input.x;
    moveDirection.z=input.y;
    controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);

    if(!isGrounded)
    {
        playerVelocity.y += gravity*Time.deltaTime;
    }
    else {
        playerVelocity.y=0;
    }
    controller.Move(playerVelocity*Time.deltaTime);
    // Debug.Log(playerVelocity.y);
}

public void Jump(){
    playerVelocity.y=Mathf.Sqrt(-2*gravity*jumpHeight);


}

}
