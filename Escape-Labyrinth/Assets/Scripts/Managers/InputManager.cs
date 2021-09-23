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
    public float distanceToSee;
    private RaycastHit _objectThatIHit;

    private Genie genie;
    private PlayerManager playerManager;
    private GameObject pointingFinger;



    // Start is called before the first frame update
    void Start()
    {
        genie = Genie.instance;
        playerManager = PlayerManager.instance;
        pointingFinger = GameObject.Find("PointingFinger");
        pointingFinger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int layerIndex = 1 << LayerMask.NameToLayer("Interactable");
        
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);

        
        if (Physics.Raycast(this.transform.position, this.transform.forward, out _objectThatIHit, distanceToSee, layerIndex))
        {
            if (_objectThatIHit.collider.gameObject.name == "RayCollision")
            {
                genie.ActivateHerbsQuiz();
            }
            else 
            {
                pointingFinger.SetActive(true);
            }

            if (Input.GetKey("space"))
            {
                string tag = _objectThatIHit.collider.gameObject.tag;
                string name = _objectThatIHit.collider.gameObject.name;


                if (Equals(tag, "Lamp") && playerManager.GetState() == -1f)
                {
                    genie.FoundLamp();
                }
                else if (Equals(name, "Flag_Morocco") && playerManager.GetState() >= 1f && playerManager.GetState() < 2f)
                {
                    playerManager.ActivateTeacher();
                }
                else if (Equals(tag, "HerbsQuiz") && playerManager.GetState() >= 2f && playerManager.GetState() < 3f)
                {
                    playerManager.HandleHerbsQuiz(_objectThatIHit.collider.gameObject);
                }
            }
        }
        else
        {
            pointingFinger.SetActive(false);
        }


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
            else if (playerManager.GetState() >= 1f && playerManager.GetState() <= 2f)
            {
                playerManager.HandleGeoQuiz();
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            ShowListOfKeys();
        }
        
    }



    private void ShowListOfKeys()
    {
        Debug.Log("ShowListOfKeys is not yet implemented");
    }

}
