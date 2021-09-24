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

    // CHANGE TO PRIVATE!!!! // and uncomment line in Start()
    public float state;

    private bool geoAnsweredRight;

    private TMP_InputField inputField;

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
    private GameObject herbsList;
    private GameObject checkmark1;
    private GameObject checkmark2;
    private GameObject checkmark3;
    private GameObject checkmark4;
    
    // MathQuiz
    private GameObject einstein; 
    private GameObject einsteinText;
    private GameObject einsteinsStein;
    private GameObject mathCode;


    


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
 
    */
    
    void Start()
    {
        //state = -1f;

        // Teacher
        teacher = GameObject.Find("Teacher");
        teacherText = GameObject.Find("Text_SpeechBubbleTeacher");
        teacher.SetActive(false);
        geoAnsweredRight = true;

        // Riddles
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


        // UI
        inputField = GameObject.Find("InputField").GetComponent<TMP_InputField>() as TMP_InputField;
        //answer = GameObject.Find("InputText");
        inputField.DeactivateInputField();
        inputField.gameObject.SetActive(false);
        // Herbs & Checkmarks
        herbsList = GameObject.Find("HerbsList");
        checkmark1 = GameObject.Find("Checkmark1");
        checkmark2 = GameObject.Find("Checkmark2");
        checkmark3 = GameObject.Find("Checkmark3");
        checkmark4 = GameObject.Find("Checkmark4");
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
        Debug.Log("Handle Teacher");
        teacher.SetActive(true);
        state = 1.1f;
    }

    


    public void HandleGeoQuiz(string inputFieldText = null)
    {
    	// if flag was clicked on, let teacher appear
        if (state == 1.1f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Pass my geography quiz!";
            state = 1.2f;
            return;
        }
        else if (state == 1.2f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "First, what country does the flag belong to?";
            inputField.ActivateInputField(); // obsolete? // also the other occurences 
            inputField.gameObject.SetActive(true);
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
                flagSA.SetActive(true);
                inputField.text = "";
                state = 1.4f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
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
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                PlayerMovement.instance.SetCanMove(true);
                state = 1.5f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.5f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Behind you, there is the border of a country. \n Which is it?";
            inputField.gameObject.SetActive(true);
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
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "And that one?";
                inputField.text = "";
                borderIndia.SetActive(true);
                state = 1.7f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.7f)
        {
            if (inputField.text.ToLower() == "india")
            {
                // Maybe: change color of border picture
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Aha, now pass the last quiz.";
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                PlayerMovement.instance.SetCanMove(true);
                state = 1.8f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.8f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Tell me the country of these cities. \n First Sofia!";
            sofia.SetActive(true);
            inputField.gameObject.SetActive(true);
            PlayerMovement.instance.SetCanMove(false);
            state = 1.9f;
            return;
        }
        else if (state == 1.9f)
        {
            if (inputField.text.ToLower() == "bulgaria")
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Okay, what\'s with Montevideo?";
                sofia.SetActive(false);
                montevideo.SetActive(true);
                inputField.text = "";
                state = 1.91f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
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
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "I give you this little advice: \n Go left!";
                inputField.text = "";
                inputField.gameObject.SetActive(false);
                PlayerMovement.instance.SetCanMove(true);
                Cursor.lockState = CursorLockMode.Locked;
                state = 1.92f;
            }
            else 
            {
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no!";
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.92f)
        {
            teacher.SetActive(false);
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
        }
        else if (name == "Herb2")
        {
            checkmark2.SetActive(true);
        }
        else if (name == "Herb3")
        {
            Debug.Log("Herb3 branch");
            checkmark3.SetActive(true);
        }
        else if (name == "Herb4")
        {
            checkmark4.SetActive(true);
        }
        else 
        {
            Debug.Log("Else branch");
            return;
        }

        state = (float) state + 0.1f;
        Destroy(hitOBject);

        if (state == 2.4f)
        {
            Debug.Log("State wurde als 2.4f erkannt");
            herbsList.SetActive(false);
            checkmark1.SetActive(false);
            checkmark2.SetActive(false);
            checkmark3.SetActive(false);
            checkmark4.SetActive(false);
            state = 3f;
        } 
        else 
        {
            Debug.Log("State: " + state);
        }
    }


    public void ActivateEinstein()
    {
        einstein.SetActive(true);
        einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Oh, hello there. \n Pardon me, I was preoccupied with my thoughts...";
        state = 3.1f;
    }

    

    public void HandleMathQuiz()
    {
        if (state == 3.1f)
        {
            einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Yes, but first, please be so kind and help me with my problem, so I can be released from this rock.";
            state = 3.2f;
            return;
        }
        else if (state == 3.2f)
        {
            einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Bring these numbers in the right order from small to large, and type the code they imply.";
            mathCode.SetActive(true);
            inputField.gameObject.SetActive(true);
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
                einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Thank you! \n And remember: \n Education is not the learning of facts, but the training of the mind to think.";
                mathCode.SetActive(false);
                inputField.gameObject.SetActive(false);
                inputField.ActivateInputField();
                PlayerMovement.instance.SetCanMove(true);
                Cursor.lockState = CursorLockMode.Locked;
                einsteinsStein.SetActive(false);
                state = 3.4f;
            return;
            }
            else 
            {
                einsteinText.GetComponent<TMPro.TextMeshProUGUI>().text = "Yeah, I tried that too, but it\'s wrong.";
            }
        }
        else if (state == 3.4f)
        {
            einstein.SetActive(false);
            state = 3.5f;
            return;
        }
    }
    
}
