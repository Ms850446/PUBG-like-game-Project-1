using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private playerMotor motor;
    private PlayerLook look;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.onFoot;
        motor=GetComponent<playerMotor>();
        onFoot.jump.performed+= ctx => motor.Jump();
        look=GetComponent<PlayerLook>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // tell the playerMotor to move using value from our movement action
        motor.ProcessMove(onFoot.movement.ReadValue<Vector2>());
    }
    private void LateUpdate() {
        look.processLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();

    }
    private void OnDisable() {
    onFoot.Disable();
    }

}
