using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
public void UpdateText(String promptMessage){
    promptText.text=promptMessage;

}
}
