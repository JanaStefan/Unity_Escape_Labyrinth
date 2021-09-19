using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float state;

    


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
        state = -1f;
        
    }
    
    public float GetState()
    {
        return state;
    }

    public void SetState(float newState)
    {
        state = newState;
    }

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

    


    private void HandleGeoQuiz(string name)
    {
        Debug.Log("Hit geo quiz");

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

        
    }

    
    
}
