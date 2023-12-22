using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    // message displayed to the player when looking at interactables
    public string promptMessage;

    // add or remove an interactionEvent component to this gameobject
    public bool useEvents;

    // this function will be called form ourm player script
    public void baseInteract()
    {
        // notice that interaction events will run first then interact function will be the second
        if (useEvents)
        {
            GetComponent<interactionEvents>().onInteract.Invoke(); // cause we are using editor script
            // to add the interaction event component then this value would never be null
            // also interaction function will always be called so we can include code inside of our
            // inherited interactable script as well as using events
        }
        interact();
    }
    // Start is called before the first frame update
    protected virtual void interact()
    {
        // nothing will be written here
        // it is just a template function to be overriden by our subclasses
    }
}
