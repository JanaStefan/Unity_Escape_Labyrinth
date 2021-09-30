using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;
    private GameObject deadText;

     // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("Health Bar").GetComponent<Slider>();
        deadText = GameObject.Find("DeadText");
        deadText.SetActive(false);
    }


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    
    public void SetHealth(int health)
    {
        slider.value = health;
    }


    public void ReduceHealth()
    {
        slider.value -= 1;
        if (slider.value == 0)
        {
            deadText.SetActive(true);
            PlayerManager.instance.player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
