using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /*
    Keys:
    - "g": genie appears
    - "return" : OK (f. ex. for genie to continue speaking) 
    - "space": to interact with interactable objects // outsourced in PlayerRaycast.cs
    */

    private Genie genie;
    private PlayerManager playerManager;


    // Start is called before the first frame update
    void Start()
    {
        genie = Genie.instance;
        playerManager = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G) && playerManager.GetState() == 1f)
        {
            genie.ShowMenu();
        }
        else if (Input.GetKeyUp("return"))
        {
            
            if (genie.CheckIfActive())
            {
                genie.PressedReturn();
            } 
            /*else if (playerManager.GetState() >= 2f && playerManager.GetState() <= 3f)
            {
                playerManager.
            }*/
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            ShowListOfKeys();
        }
        
    }


    // aus PlayerManager hier hin geschoben // vielleicht PlayerRaycast und InputManager joinen? 
    /*
    public void HandleInteractables(string tag, string name)
    {
        if (Equals(tag, "Lamp") && state == -1f)
        {
            Genie.instance.FoundLamp();
        }
        else if (Equals(tag, "Geo_Quiz") && state >= 1f && state < 2f)
        {
            HandleGeoQuiz(name);
        }
        else if (Equals(tag, "?Quiz") && state >= 2f && state < 3f)
        {
            
        }
    }
    */


    private void ShowListOfKeys()
    {
        Debug.Log("ShowListOfKeys is not yet implemented");
    }

}
