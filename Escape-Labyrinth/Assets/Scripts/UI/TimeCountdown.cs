using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountdown : MonoBehaviour
{
    private GameObject textDisplay;
    public int secondsLeft = 0;
    public int minutesLeft = 10;
    public bool takingAway = false;

    void Start()
    {
        textDisplay = GameObject.Find("Time");
        textDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = minutesLeft + ":" + secondsLeft;
    }

    void Update()
    {
        if (takingAway == false && (secondsLeft > 0 || minutesLeft > 0))
        {
            StartCoroutine(TimerTake());
        }
    }


    IEnumerator TimerTake()
    {   
        string additionalZeroSeconds = "";  // for beauty's sake
        string additionalZeroMinutes = "0";

        takingAway = true; 
        yield return new WaitForSeconds(1);

        if (secondsLeft > 0)
        {
            secondsLeft -= 1;
            if (secondsLeft < 10)
                additionalZeroSeconds = "0";
        }
        else 
        {
            minutesLeft -= 1;
            secondsLeft = 59;
            if (minutesLeft > 10)
                additionalZeroMinutes = "";
        }
        textDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = additionalZeroMinutes + minutesLeft + ":" + additionalZeroSeconds + secondsLeft;
        takingAway = false;
    }
}
