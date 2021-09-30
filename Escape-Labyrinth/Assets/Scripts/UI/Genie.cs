using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private GameObject apple;



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
        apple = GameObject.Find("Apple");
        apple.SetActive(false);


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
        FindObjectOfType<AudioManager>().Play("State0");
        isActive = true;
        playerManager.SetState(0.01f);
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
        
        if (playerManager.GetState() == 0.01f)
        {
            FindObjectOfType<AudioManager>().Play("State0.01");
            playerManager.speechBubblePlayer.SetActive(true);
            playerManager.SetState(0.02f);
            return;
        }
        else if (playerManager.GetState() == 0.02f)
        {
            playerManager.speechBubblePlayer.SetActive(false);
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "In your thought palace or should I say labyrinth?";
            FindObjectOfType<AudioManager>().Play("State0.02");
            playerManager.SetState(0.03f);
            return;
        }
        else if (playerManager.GetState() == 0.03f)
        {
            playerManager.speechBubblePlayer.SetActive(true);
            FindObjectOfType<AudioManager>().Play("State0.03");
            playerManager.SetState(0.1f);
            return;
        }
        else if (playerManager.GetState() == 0.1f)
        {
            playerManager.speechBubblePlayer.SetActive(false);
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>An apple a day keeps the doctor away!</color>";
            FindObjectOfType<AudioManager>().Play("State0.1");
            playerManager.SetState(0.11f);
            apple.SetActive(true);
            apple.GetComponent<NavMeshAgent>().speed = 1f;
            apple.GetComponent<EnemyController>().lookRadius = 50f;
            return;
        }
        else if (playerManager.GetState() == 0.11f)
        {
            playerManager.speechBubblePlayer.SetActive(true);
            FindObjectOfType<AudioManager>().Play("State0.11");
            playerManager.SetState(0.2f);
            return;
        }
        else if (playerManager.GetState() == 0.2f)
        {
            playerManager.speechBubblePlayer.SetActive(false);
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Then <color=#ce490e>RUN</color>!";
            FindObjectOfType<AudioManager>().Play("State0.2");
            apple.GetComponent<NavMeshAgent>().speed = 3.5f;
            apple.GetComponent<EnemyController>().lookRadius = 30f;
            playerManager.SetState(0.3f);
            return;
        }

        else if (playerManager.GetState() == 0.3f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "And I will go tidy up my lamp. If you need me, press \"g\"!";
            FindObjectOfType<AudioManager>().Play("State0.3");
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
        else if (playerManager.GetState() == 2.01f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "There is a certain mix of herbs that will help you.";
            FindObjectOfType<AudioManager>().Play("State2.01");
            playerManager.SetState(2.1f);
            return;
        }
        else if (playerManager.GetState() == 2.1f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Go and pick the right ones.";
            FindObjectOfType<AudioManager>().Play("State2.1");
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

        // Found knife
        else if (playerManager.GetState() == 3.6f)
        {
            genie.SetActive(false);
            isActive = false;
            lampMenu.SetActive(true);
            playerManager.SetState(4f);
            return;
        }

        // Donut Game
        else if (playerManager.GetState() == 5.01f)
        {
            playerManager.speechBubblePlayer.SetActive(true);
            FindObjectOfType<AudioManager>().Play("State5.01");
            playerManager.SetState(5.02f);
            return;
        }
        else if (playerManager.GetState() == 5.02f)
        {
            playerManager.speechBubblePlayer.SetActive(false);
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Look at your world. It\'s all my fault.";
            FindObjectOfType<AudioManager>().Play("State5.02");
            playerManager.SetState(5.03f);
            return;
        }
        else if (playerManager.GetState() == 5.03f)
        {
            playerManager.speechBubblePlayer.SetActive(true);
            FindObjectOfType<AudioManager>().Play("State5.03");
            playerManager.SetState(5.04f);
            return;
        }
        else if (playerManager.GetState() == 5.04f)
        {
            playerManager.speechBubblePlayer.SetActive(false);
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "I will grant you the ability to lose weight when you eat fruits.";
            FindObjectOfType<AudioManager>().Play("State5.04");
            playerManager.SetState(5.1f);
            return;
        }
        else if (playerManager.GetState() == 5.1f)
        {
            genie.SetActive(false);
            isActive = false;
            lampMenu.SetActive(true);
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
        FindObjectOfType<AudioManager>().Play("State2.0");
        FindObjectOfType<AudioManager>().StopPlaying("GeoMusic");
        FindObjectOfType<AudioManager>().Play("HerbsMusic");
        playerManager.SetState(2.01f);
        lampMenu.SetActive(false);
    }


    public void FoundKnife(GameObject ob)
    {
        Destroy(ob);
        genie.SetActive(true);
        isActive = true;
        speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "You found a knife. This might be very useful!";
        FindObjectOfType<AudioManager>().Play("State3.5");
        playerManager.SetState(3.6f);
        lampMenu.SetActive(false);
    }


    public void ActivateDonutGame()
    {
        genie.SetActive(true);
        isActive = true;
        lampMenu.SetActive(false);
        speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hi there again! In this part of the labyrinth you will have to resist your cravings, in your case your donut cravings.";
        FindObjectOfType<AudioManager>().Play("State5.0");
        FindObjectOfType<AudioManager>().StopPlaying("CemeteryMusic");
        FindObjectOfType<AudioManager>().Play("DonutMusic");
        playerManager.SetState(5.01f);
    }


    


    public void ShowMenu()
    {
        if (!isActive)
        {
            genie.SetActive(true);
            isActive = true;
            lampMenu.SetActive(false);
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Sorry, I\'m busy!";
            FindObjectOfType<AudioManager>().Play("GenieMenu");
        }
        else 
        {
            genie.SetActive(false);
            isActive = false;
            lampMenu.SetActive(true);
        }
    }
}
