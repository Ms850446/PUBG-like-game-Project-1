using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class keypad : Interactables
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    public ParticleSystem celebration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void interact() // if we want to change color add animation , destroy object or anything
    // for interactable it will be here
    {
        // base.interact();
        // Debug.Log("interacted with"+gameObject.name);
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen",doorOpen);
        if(doorOpen){
            PlayWinningEffect();
        }
    }
    void PlayWinningEffect()
    {
        celebration.Play();
    }
}
