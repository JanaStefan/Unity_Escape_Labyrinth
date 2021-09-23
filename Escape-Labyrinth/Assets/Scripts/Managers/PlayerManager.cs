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

    // Teacher
    private GameObject teacher;
    private GameObject teacherText;

    // Riddles
    private GameObject flagMorocco;
    private GameObject flagSA;
    private GameObject borderCroatia;
    private GameObject borderIndia;
    private GameObject sofia;
    private GameObject montevideo;
    
    //private GameObject answer;


    


    /*
    States: 
    0: Genie in a lamp
    1.0: Teacher
    1.1: First Border Quiz
    1.2: Second Border Quiz
    1.3: First Flag Quiz
    1.4: Second Flag Quiz 
    2.0: 
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
    }
    
    public float GetState()
    {
        return state;
    }

    public void SetState(float newState)
    {
        state = newState;
    }




    public void HandleTeacher()
    {
        Debug.Log("Handle Teacher");
        teacher.SetActive(true);
        state = 1.1f;
    }

    


    public void HandleGeoQuiz(string inputFieldText = null)
    {
        Debug.Log("Hit geo quiz");

    	// if flag was clicked on, let teacher appear
        if (state == 1.1f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Pass my geography quizzes!";
            state = 1.2f;
            return;
        }
        else if (state == 1.2f)
        {
            teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "First, what country is the flag from?";
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
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no! Should be morocco, you dumb kid";
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
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Again: \n No, no, no! Should be south africa, you dumb kid.";
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
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "No, no, no! Should be croatia, you dumb kid";
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
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Again: \n No, no, no! Should be india, you dumb kid.";
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
                Debug.Log("Answer was: " + inputField.text.ToLower() + ". But should be bulgaria. Should be bulgaria, you dumb kid.");
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
                 Debug.Log("Answer was: " + inputField.text.ToLower() + ". But should be uruguay.");
                teacherText.GetComponent<TMPro.TextMeshProUGUI>().text = "Again: \n No, no, no! Should be uruguay, you dumb kid.";
                geoAnsweredRight = false;
            }
            return;
        }
        else if (state == 1.92f)
        {
            teacher.SetActive(false);
            state = 2f;
            return;
        }
        



        /*
        // handle teacher
        if (state == 1f && Equals(name, "Teacher"))
        {
            Debug.Log("Teacher");
            GameObject.Find(name).SetActive(false);
            state = 1.1f;
        }
        // handle first border quiz
        else if (state == 1.1f && Equals(name, "Border_Quiz_Croatia"))
        {
            Debug.Log("Border");
            GameObject.Find(name).SetActive(false);
            state = 1.3f;
        }
        // handle second border quiz
        else if (state == 1.2f && Equals(name, "Border_Quiz_India"))
        {
            // ACHTUNG: NOCH NICHT IMPLEMENTIERT
            state = 1.2f;
        }
        // handle first flag quiz
        else if (state == 1.3f && Equals(name, "Flag_Morocco"))
        {
            Debug.Log("Morocco");
            GameObject.Find(name).SetActive(false);
            state = 1.4f;
        }
        // handle second flag quiz
        else if (state == 1.4f && Equals(name, "Flag_SouthAfrica"))
        {
            Debug.Log("South Africa");
            GameObject.Find(name).SetActive(false);
            state = 1.5f;
        }
        */

        
    }

    
    
}
