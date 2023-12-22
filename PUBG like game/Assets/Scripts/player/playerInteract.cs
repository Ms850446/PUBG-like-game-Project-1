using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// this script to handle all of logic of interactables and handle our player's input
// this script should be added to our player object
public class playerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    [SerializeField]
    private float distance = 3;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private inputManager inputmanager;
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputmanager = GetComponent<inputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(String.Empty);  // to make sure that the text is empty while not looking at it


        // create a ray at the center of the camera , and shooting outwards
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo; // this varaibel is used to store information about the collision
        // of the object and things like that (ray hit is type of rays and used to save information about
        // the collision of the interactable object like distance between it and origin collision ,etc)

        // use ray cast function if we actually hit anything
        if (Physics.Raycast(ray, out hitInfo, distance, mask))  // this function is boolean if it hit something
        // out means that the function will return something
        // here in this case it is hit cast hit so we assigning a value to our hit variable
        {
            // here is check if game object has interactable component on it
            if (hitInfo.collider.GetComponent<Interactables>() != null)
            {
                Interactables interactable = hitInfo.collider.GetComponent<Interactables>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputmanager.onFoot.interact.triggered) // it is like getkeydown function
                {
                    interactable.baseInteract();
                }

            }
        }


    }
}
