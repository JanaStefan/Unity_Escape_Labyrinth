using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*
    Keys:
    - "g": genie appears
    - "enter" : OK (f. ex. for genie to continue speaking) 
    - "space": to interact with interactable objects // outsourced in PlayerRaycast.cs
    */

    private Genie genie;


    // Start is called before the first frame update
    void Start()
    {
        genie = Genie.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("g") && PlayerManager.instance.GetState() == 0.2f)
        {
            ShowMenu();
        }
        else if (Input.GetKey("return"))
        {
            
            if (genie.CheckIfActive())
            {
                genie.PressedReturn();
            }
        }
        
    }


    void ShowMenu()
    {
        Debug.Log("Show Menu is not yet implemented");
    }
}
