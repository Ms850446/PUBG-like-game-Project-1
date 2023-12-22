// a custom editor allows us to change how script appear in inspector by replacing the layout
// by one we write ourselves like (pro-builder and post-procerssing)
using UnityEditor;
[CustomEditor(typeof(Interactables), true)]
public class interactableEditor : Editor
{
    public override void OnInspectorGUI() // this function is called every time unity updates the editor interface
    {
        Interactables interactable = (Interactables)target;  // target is the current game object that we are inspecting
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            // it doesn' have base.OnInspectorGUI so it would be blank and we need to
            // add it ourselves and create out prompt message field
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            // first parameter is label and the second parameter is the value

            // this is going to be an event only interactables then we don't need to display the use event bool
            EditorGUILayout.HelpBox("EventOnlyInteractable CAN ONLY USE UnityEvents", MessageType.Info);
            if (interactable.GetComponent<interactionEvents>()==null){//  check if the game object doesn't have the component on it yet
            interactable.useEvents=true;
            interactable.gameObject.AddComponent<interactionEvents>();
            }

        }
        else
        {


            base.OnInspectorGUI(); // draw our interactable component, how it appears with no modification
            if (interactable.useEvents)
            {
                // add interaction event component
                if (interactable.GetComponent<interactionEvents>() == null)
                    interactable.gameObject.AddComponent<interactionEvents>();
            }
            else if (interactable.GetComponent<interactionEvents>() != null)
            {
                DestroyImmediate(interactable.GetComponent<interactionEvents>());
            }
        }
    }
}
