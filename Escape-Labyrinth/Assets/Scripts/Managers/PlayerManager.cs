using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion



    public GameObject player;
    private GameObject healthBar;

    // CHANGE TO PRIVATE!!!! // and uncomment line in Start()
    public float state;

    private bool geoAnsweredRight;

    [HideInInspector]
    public TMP_InputField inputField;
    [HideInInspector]
    public GameObject speechBubblePlayer;


    // GeoQuiz
    private GameObject teacher;
    private GameObject teacherText;
    private GameObject flagMorocco;
    private GameObject flagSA;
    private GameObject borderCroatia;
    private GameObject borderIndia;
    private GameObject sofia;
    private GameObject montevideo;

    // HerbsQuiz
    public GameObject herbsList;
    private GameObject checkmark1;
    private GameObject checkmark2;
    private GameObject checkmark3;
    private GameObject checkmark4;
    
    // MathQuiz
    private GameObject einstein; 
    private GameObject einsteinText;
    private GameObject einsteinsStein;
    private GameObject mathCode;


    // Cemetry Game 
    private GameObject knife;
    private GameObject skullUI;
    private GameObject skullText;


    


    /*
    States: 
    0: Genie in a lamp
    1.0: Teacher
    1.1: First Border Quiz
    1.2: Second Border Quiz
    1.3: First Flag Quiz
    1.4: Second Flag Quiz 
    1.93: Teacher has disappeared
    2.0: Genie is activated
    2.1: Found First Herb
    2.2: Found Second Herb
    2.3: Found Third Herb
    2.4: Found Fourth Herb
    3.0: HerbsList deactivated & Einstein activated
    3.1: Einsteins says hello
    3.2: Einstein asks for help
    3.3: Einstein explains task, MathQuiz is shown
    3.4: answer is right, Einstein says thank you
    3.5: Einstein has disappeared
    4.0: Cemetry Game
    5.0: Donut Game
 
    */
    
    void Start()
    {
        //state = -1f;
        healthBar = GameObject.Find("Health Bar");

        // Teacher
        teacher = GameObject.Find("Teacher");
        teacherText = GameObject.Find("Text_SpeechBubbleTeacher");
        teacher.SetActive(false);
        geoAnsweredRight = true;

        // Geo Quiz
        flagSA = GameObject.Find("Flag_SouthAfrica");
        flagSA.SetActive(false);
        borderCroatia = GameObject.Find("Border_Croatia");
        borderCroatia.SetActive(false);
        borderIndia = GameObject.Find("Border_India");
        borderIndia.SetActive(false);
        sofia = GameObject.Find("Sofia");
        sofia.SetActive(false);
        montevideo = GameObject.Find("Montevideo");
        montevideo.SetActive(false);

        // Cemetry Game
        knife = GameObject.Find("Knife");
        skullUI = GameObject.Find("SkullUI");
        skullText = GameObject.Find("Text_SpeechBubbleSkull");
        skullUI.SetActive(false);



        // UI
        inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>() as TMP_InputField;
        //answer = GameObject.Find("InputText");
        inputField.DeactivateInputField();
        inputField.gameObject.SetActive(false);
        speechBubblePlayer = GameObject.Find("SpeechBubblePlayer");
        speechBubblePlayer.SetActive(false);
        
        // Herbs & Checkmarks
        herbsList = GameObject.Find("HerbsList");
        checkmark1 = GameObject.Find("Checkmark1");
        checkmark2 = GameObject.Find("Checkmark2");
        checkmark3 = GameObject.Find("Checkmark3");
        checkmark4 = GameObject.Find("Checkmark4");
        herbsList.SetActive(false);
        checkmark1.SetActive(false);
        checkmark2.SetActive(false);
        checkmark3.SetActive(false);
        checkmark4.SetActive(false);
        // Einstein
        einstein = GameObject.Find("Einstein");
        einsteinText = GameObject.Find("Text_SpeechBubbleEinstein");
        einsteinsStein = GameObject.Find("EinsteinsStein");
        mathCode = GameObject.Find("MathCode");
        einstein.SetActive(false);
        mathCode.SetActive(false);


        // Start playing music
        FindObjectOfType<AudioManager>().Play("StartMusic");
    }


    public void HitByEnemy()
    {
        Debug.Log("Hit by enemy!");
        healthBar.GetComponent<HealthBar>().ReduceHealth();
    }

    
    public float GetState()
    {
        return state;
    }

    public void SetState(float newState)
    {
        state = newState;
    }




    public void ActivateTeacher()
    {
        teacher.SetActive(true);
        FindObjectOfType<AudioManager>().Play("State1.0");
        FindObjectOfType<AudioManager>().StopPlaying("StartMusic");
        FindObjectOfType<AudioManager>().Play("GeoMusic");
        state = 1.01f;
    }

    


    public void HandleGeoQuiz(string inputFieldText = null)
    {
    	// if flag was clicked on, let teacher appear
        if (state == 1.01f)
        {
            FindObjectOfType<AudioManager>().Play("State1.01");
            speechBubblePlayer.SetActive(true);
            state = 1.1f;
            return;
        }
        else if (state == 1.1f)
        {
            speechBubblePlayer.SetActive(false);
            FindObjectOfType<AudioManager>().Play("State1.1");
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Pass my geography quiz!";
            state = 1.2f;
            return;
        }
        else if (state == 1.2f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "To which country does this flag belong?";
            FindObjectOfType<AudioManager>().Play("State1.2");
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
            PlayerMovement.instance.SetCanMove(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            state = 1.3f;
            return;
        } 
        else if (state == 1.3f)
        {
            if (inputField.text.ToLower() == "morocco")
            {
                // Maybe: change color of flag
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Okay, and the one next to you?";
                FindObjectOfType<AudioManager>().Play("State1.3");
                inputField.ActivateInputField();
                flagSA.SetActive(true);
                inputField.text = "";
                state = 1.4f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                FindObjectOfType<AudioManager>().Play("T_Nonono");
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.4f)
        {
            if (inputField.text.ToLower() == "south africa")
            {
                // Maybe: change color of flag
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Let\'s try another quiz.";
                FindObjectOfType<AudioManager>().Play("State1.4");
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                PlayerMovement.instance.SetCanMove(true);
                state = 1.5f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                FindObjectOfType<AudioManager>().Play("T_Nonono");
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.5f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Behind you, there is the border of a country. \n Which is it?";
            FindObjectOfType<AudioManager>().Play("State1.5");
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
            borderCroatia.SetActive(true);
            PlayerMovement.instance.SetCanMove(false);
            state = 1.6f;
            return;
        }
        else if (state == 1.6f)
        {
            if (inputField.text.ToLower() == "croatia")
            {
                // Maybe: change color of border picture
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "And this one?";
                FindObjectOfType<AudioManager>().Play("State1.6");
                inputField.text = "";
                inputField.ActivateInputField();
                borderIndia.SetActive(true);
                state = 1.7f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                FindObjectOfType<AudioManager>().Play("T_Nonono");
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.7f)
        {
            if (inputField.text.ToLower() == "india")
            {
                // Maybe: change color of border picture
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "For the last task, I’ll show you postcards.";
                FindObjectOfType<AudioManager>().Play("State1.7");
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                PlayerMovement.instance.SetCanMove(true);
                state = 1.8f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                FindObjectOfType<AudioManager>().Play("T_Nonono");
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.8f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Name the corresponding countries. First, Sofia.";
            FindObjectOfType<AudioManager>().Play("State1.8");
            sofia.SetActive(true);
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
            PlayerMovement.instance.SetCanMove(false);
            state = 1.9f;
            return;
        }
        else if (state == 1.9f)
        {
            if (inputField.text.ToLower() == "bulgaria")
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "And what about Montevideo?";
                FindObjectOfType<AudioManager>().Play("State1.9");
                sofia.SetActive(false);
                montevideo.SetActive(true);
                inputField.text = "";
                inputField.ActivateInputField();
                state = 1.91f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                FindObjectOfType<AudioManager>().Play("T_Nonono");
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.91f)
        {
            if (inputField.text.ToLower() == "uruguay")
            {
                montevideo.SetActive(false);
                // USE GEOANSWEREDRIGHT TO GIVE DIFFERENTIATED ANSWERS
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "My tip: \n Pick the left path!";
                FindObjectOfType<AudioManager>().Play("State1.91");
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                PlayerMovement.instance.SetCanMove(true);
                Cursor.lockState = CursorLockMode.Locked;
                state = 1.92f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                FindObjectOfType<AudioManager>().Play("T_Nonono");
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.92f)
        {
            teacher.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            state = 1.93f;
            return;
        }

    }
        


    public void HandleHerbsQuiz(GameObject hitOBject)
    {
        string name = hitOBject.name;

        Debug.Log("Picked: " + name);

        

        if (name == "Herb1")
        {
            checkmark1.SetActive(true);
            state = (float) System.Math.Round(state + 0.1f, 1);
            Destroy(hitOBject);
            return;
        }
        else if (name == "Herb2")
        {
            checkmark2.SetActive(true);
            state = (float) System.Math.Round(state + 0.1f, 1);
            Destroy(hitOBject);
            return;
        }
        else if (name == "Herb3")
        {
            checkmark3.SetActive(true);
            state = (float) System.Math.Round(state + 0.1f, 1);
            Destroy(hitOBject);
            return;
        }
        else if (name == "Herb4")
        {
            checkmark4.SetActive(true);
            state = (float) System.Math.Round(state + 0.1f, 1);
            Destroy(hitOBject);
            return;
        }
    }


    public void FinishedHerbsGame()
    {
        herbsList.SetActive(false);
        checkmark1.SetActive(false);
        checkmark2.SetActive(false);
        checkmark3.SetActive(false);
        checkmark4.SetActive(false);
        state = 3f;
    }


    public void ActivateEinstein()
    {
        einstein.SetActive(true);
        einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oh, hello there. \n Pardon me, I was preoccupied with my thoughts...";
        FindObjectOfType<AudioManager>().Play("State3.0");
        FindObjectOfType<AudioManager>().StopPlaying("HerbsMusic");
        FindObjectOfType<AudioManager>().Play("EinsteinMusic");
        state = 3.01f;
    }

    

    public void HandleMathQuiz()
    {   if (state == 3.01f)
        {
            FindObjectOfType<AudioManager>().Play("State3.01");
            speechBubblePlayer.SetActive(true);
            state = 3.1f;
            return;
        }
        else if (state == 3.1f)
        {
            speechBubblePlayer.SetActive(false);
            einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Yes, but first, please be so kind and help me with my problem, so I can be released from this rock.";
            FindObjectOfType<AudioManager>().Play("State3.1");
            state = 3.2f;
            return;
        }
        else if (state == 3.2f)
        {
            einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Bring these numbers in the right order from small to large, and type the code they imply.";
            FindObjectOfType<AudioManager>().Play("State3.2");
            mathCode.SetActive(true);
            inputField.gameObject.SetActive(true);
            inputField.ActivateInputField();
            PlayerMovement.instance.SetCanMove(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            state = 3.3f;
            return;
        }
        else if (state == 3.3f)
        {
            if (inputField.text.ToLower() == "govegan!" || inputField.text.ToLower() == "go vegan!")
            {
                einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Education is not the learning of facts, but the training of the mind to think.";
                FindObjectOfType<AudioManager>().Play("State3.3");
                mathCode.SetActive(false);
                inputField.gameObject.SetActive(false);
                PlayerMovement.instance.SetCanMove(true);
                Cursor.lockState = CursorLockMode.Locked;
                einsteinsStein.SetActive(false);
                state = 3.4f;
            return;
            }
            else 
            {
                einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Yeah, I tried that too, but it\'s wrong.";
                FindObjectOfType<AudioManager>().Play("EinsteinWrong");
                inputField.ActivateInputField();
            }
        }
        else if (state == 3.4f)
        {
            einstein.SetActive(false);
            state = 3.5f;
            return;
        }
    }



   
    public void HandleCemetry(GameObject hitOBject)
    { 
        if (hitOBject.name == "SpeakingSkull")
        {
            skullUI.SetActive(true);
            skullText.GetComponent<TMPro.TextMeshProUGUI>().text = "I can’t find peace, unless I have my grandma’s ancient blue bowl back.";
            FindObjectOfType<AudioManager>().Play("State4.0");
            FindObjectOfType<AudioManager>().StopPlaying("EinsteinMusic");
            FindObjectOfType<AudioManager>().Play("CemeteryMusic");
            Debug.Log("Skull should start speaking");
            state = 4.1f;
            return;
        }
        else if (hitOBject.name == "Bowl")
        {
            skullUI.SetActive(true);
            skullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Thank you! Now I can finally go to Nirvana and meet my grandma again!";
            FindObjectOfType<AudioManager>().Play("State4.3");
            state = 4.4f;
            Destroy(GameObject.Find("Tomb"));
            Destroy(GameObject.Find("SpeakingSkull"));
        } 
        Destroy(hitOBject);
    }



    public void SkullTalk()
    {
        if (state == 4.1f)
        {
            skullText.GetComponent<TMPro.TextMeshProUGUI>().text = "Could you please find it for me?";
            FindObjectOfType<AudioManager>().Play("State4.1");
            state = 4.2f;
        }
        else if (state == 4.2f)
        {
            skullUI.SetActive(false);
            state = 4.3f;
        }
        else if (state == 4.4f)
        {
            skullUI.SetActive(false);
            state = 5f;
        }
    }



    public void HitByDonut()
    {
        FatBar.instance.ModifySize(true);
    }


    public void CollectedFruit(GameObject fruit)
    {
        FatBar.instance.ModifySize(false);
        Destroy(fruit);
    }


    public void HitClock()
    {
        FindObjectOfType<AudioManager>().StopPlaying("DonutMusic");
        FindObjectOfType<AudioManager>().Play("Alarm");
        // end game
    }
    
}
