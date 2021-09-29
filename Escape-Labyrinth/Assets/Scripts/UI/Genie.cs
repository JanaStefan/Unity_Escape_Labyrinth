using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genie : MonoBehaviour
{
    #region Singleton

    public static Genie instance;

    void Awake()
    {
        instance = this;
    }

    #endregion


    private GameObject genie;
    //private GameObject speechBubble;
    private GameObject speechText;
    private GameObject lampMenu;
    private PlayerManager playerManager;
    private bool isActive;



    // Donut Game
    private GameObject whale;
    private GameObject donut;
    private GameObject fatBar;



    // Start is called before the first frame update
    void Start()
    {
        speechText = GameObject.Find("Text_SpeechBubble");  // has to be found before genie (and this as a child component) is deactivated
        //speechBubble = GameObject.Find("SpeechBubble");
        genie = this.gameObject;
        genie.SetActive(false);
        playerManager = PlayerManager.instance;
        isActive = false;
        lampMenu = GameObject.Find("LampMenu");
        lampMenu.SetActive(false);


        // Donut Game
        whale = GameObject.Find("Whale");
        whale.SetActive(false);
        donut = GameObject.Find("Donut");
        donut.SetActive(false);
        fatBar = GameObject.Find("Fat Bar");
        fatBar.SetActive(false);
    }


    public bool CheckIfActive()
    {
        return isActive;
    }



    public void FoundLamp()
    {
        Debug.Log("Hit lamp");
        GameObject.Find("Lamp3D").SetActive(false);
        genie.SetActive(true);
        isActive = true;
        playerManager.SetState(0f);
    }

/*
    States: 
    0: In your thought palace or should I say labyrinth?
    0.1: <color=red>An apple a day keeps the doctor away!</color>
    0.2: Then <color=#ce490e>RUN</color>!
    0.3: And I will go tidy up my lamp. If you need me, press \"g\"!
    0.4: Genie disappears 
    */
    

    public void PressedReturn()
    {
        if (playerManager.GetState() == 0f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "In your thought palace or should I say labyrinth?";
            playerManager.SetState(0.1f);
            return;
        }
        else if (playerManager.GetState() == 0.1f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>An apple a day keeps the doctor away!</color>";
            playerManager.SetState(0.2f);
            return;
        }
        else if (playerManager.GetState() == 0.2f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Then <color=#ce490e>RUN</color>!";
            playerManager.SetState(0.3f);
            return;
        }

        else if (playerManager.GetState() == 0.3f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "And I will go tidy up my lamp. If you need me, press \"g\"!";
            playerManager.SetState(0.4f);
            return;
        }
        else if (playerManager.GetState() == 0.4f)
        {
            genie.SetActive(false);
            isActive = false;
            playerManager.SetState(1f);
            lampMenu.SetActive(true);
            return;
        }


        // Herbs Quiz
        else if (playerManager.GetState() == 2f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "There is a certain mix of herbs that will help you.";
            playerManager.SetState(2.1f);
            return;
        }
        else if (playerManager.GetState() == 2.1f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Go and pick the right ones.";
            playerManager.SetState(2.2f);
            playerManager.herbsList.SetActive(true);
            return;
        }
        else if (playerManager.GetState() == 2.2f)
        {
            genie.SetActive(false);
            isActive = false;
            playerManager.SetState(2.3f);
            lampMenu.SetActive(true);
            return;
        }


        // Donut Game
        else if (playerManager.GetState() == 5f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "I will grant you the ability to lose weight when you eat fruits.";
            playerManager.SetState(5.1f);
            return;
        }
        else if (playerManager.GetState() == 5.1f)
        {
            genie.SetActive(false);
            isActive = false;
            playerManager.SetState(5.2f);
            fatBar.SetActive(true);
            whale.SetActive(true);
            donut.SetActive(true);
            return;
        }
    }


    public void ActivateHerbsQuiz()
    {
        genie.SetActive(true);
        isActive = true;
        speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oh no, you just got bitten by a piranha!";
        playerManager.SetState(2f);
        lampMenu.SetActive(false);
    }


    public void ActivateDonutGame()
    {
        genie.SetActive(true);
        isActive = true;
        speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hi there again! In this part of the labyrinth you will have to resist your cravings, in your case your donut cravings.";
        playerManager.SetState(5f);
    }


    


    public void ShowMenu()
    {
        if (!isActive)
        {
            //if (playerManager.GetState() <= 1f && playerManager.GetState() >= 2f)
            //{
                genie.SetActive(true);
                isActive = true;
                lampMenu.SetActive(false);
                speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "This is your lifesaving menu! \n For a list of keys, press \"k\".";
            /*}
            else 
            {
                speechBubble.SetActive(true);
                isActive = true;
                speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "I can\'t help you with that, sorry!";
                //speechBubble.RectTransform.SetSizeWithCurrentAnchors(Animations.Axis axis, float size)
            }*/
        }
        else 
        {
            genie.SetActive(false);
            isActive = false;
            lampMenu.SetActive(true);
        }
    }
}
