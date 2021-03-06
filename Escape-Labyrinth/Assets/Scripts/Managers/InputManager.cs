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
    private GameObject knifeUI;
    private GameObject videoImage;
    private bool playingIntro;



    // Start is called before the first frame update
    void Start()
    {
        genie = Genie.instance;
        playerManager = PlayerManager.instance;
        pointingFinger = GameObject.Find("PointingFinger");
        pointingFinger.SetActive(false);
        knifeUI = GameObject.Find("KnifeUI");
        knifeUI.SetActive(false);
        videoImage = GameObject.Find("VideoImage");
        playingIntro = true;
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
                Destroy(_objectThatIHit.collider.gameObject);
            }
            else if (_objectThatIHit.collider.gameObject.name == "RayCollision2")
            {
                genie.ActivateDonutGame();
                Destroy(_objectThatIHit.collider.gameObject);
            }
            else if (_objectThatIHit.collider.gameObject.tag == "Pumpkin")
            {
                knifeUI.SetActive(true);
            }
            else
            {
                pointingFinger.SetActive(true);
            }

            if (Input.GetKey("space"))
            {
                string tag = _objectThatIHit.collider.gameObject.tag;
                string name = _objectThatIHit.collider.gameObject.name;


                if (Equals(name, "Lamp3D") && playerManager.GetState() == -1f)
                {
                    genie.FoundLamp();
                }
                else if (Equals(name, "Flag_Morocco") && playerManager.GetState() == 1f)   // vielleicht lieber ohne State-Abfrage?
                {
                    playerManager.ActivateTeacher();
                }
                else if (Equals(tag, "HerbsQuiz") && playerManager.GetState() >= 2f && playerManager.GetState() < 3f) 
                {
                    playerManager.HandleHerbsQuiz(_objectThatIHit.collider.gameObject);
                }
                else if (Equals(name, "EinsteinsStein") && playerManager.GetState() == 3f)
                {
                    playerManager.ActivateEinstein();
                }
                else if (name == "Knife")
                {
                    genie.FoundKnife(_objectThatIHit.collider.gameObject);
                }
                else if (tag == "Cemetry" || tag == "Pumpkin")
                {
                    playerManager.HandleCemetry(_objectThatIHit.collider.gameObject);
                }
                else if (tag == "Fruit")
                {
                    playerManager.CollectedFruit(_objectThatIHit.collider.gameObject);
                }
                else if (tag == "Donut")   //interim solution for collecting hamburgers at donut game
                {
                    playerManager.HitByDonut();
                    Destroy(_objectThatIHit.collider.gameObject);
                }
                else if (name == "Clock")
                {
                    playerManager.HitClock();
                }
            }
        }
        else
        {
            pointingFinger.SetActive(false);
            knifeUI.SetActive(false);
        }


        if (Input.GetKeyUp(KeyCode.G) && playerManager.GetState() >= 1f && !playerManager.inputField.gameObject.activeSelf)
        {
            genie.ShowMenu();
        }
        else if (Input.GetKeyUp("return"))
        {
            if (playingIntro)
            {
                videoImage.SetActive(false);
                playingIntro = false;
            }
            else if (genie.CheckIfActive())
            {
                genie.PressedReturn();
            } 
            else if (playerManager.GetState() >= 1f && playerManager.GetState() <= 2f)
            {
                playerManager.HandleGeoQuiz();
            }
            else if (playerManager.GetState() == 2.7f)
            {
                playerManager.FinishedHerbsGame();
            }
            else if (playerManager.GetState() >= 3f && playerManager.GetState() <= 4f)
            {
                playerManager.HandleMathQuiz();
            }
            else if (playerManager.GetState() >= 4f && playerManager.GetState() <= 5f)
            {
                playerManager.SkullTalk();
            }

            else 
            {
                Debug.Log("Could not identify state: " + playerManager.GetState());
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
