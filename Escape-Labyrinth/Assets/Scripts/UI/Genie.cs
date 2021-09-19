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


    /*
    States: 
    0: Genie activated
    0.1: Genie talks 
    0.2: Genie disappears and Lamp Menu appears
    */


    private GameObject genie;
    private GameObject genieText;
    private GameObject speechText;
    private float state;
    private PlayerManager playerManager;
    private bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        speechText = GameObject.Find("Text_SpeechBubble");  // has to be found before genie (and this as a child component) is deactivated
        genie = this.gameObject;
        genie.SetActive(false);
        playerManager = PlayerManager.instance;
        isActive = false;
    }


    
    public float GetState()
    {
        return state;
    }


    public bool CheckIfActive()
    {
        return isActive;
    }


    public void FoundLamp()
    {
        Debug.Log("Hit lamp");
        GameObject.Find("Lamp_spaceholder").SetActive(false);
        genie.SetActive(true);
        isActive = true;
        playerManager.SetState(0f);
    }


    public void PressedReturn()
    {
        if (playerManager.GetState() == 0f)
        {
            Debug.Log(speechText);
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Hihihihihi";
            playerManager.SetState(0.1f);
        }
        else if (playerManager.GetState() == 0.1f)
        {
            speechText.GetComponent<TMPro.TextMeshProUGUI>().text = "Okay, I will go tidy up my lamp. If you need me, press \"g\"!";
            playerManager.SetState(0.2f);
        }
        else
        {
            genie.SetActive(false);
            isActive = false;
            playerManager.SetState(1f);
        }
    }
}
