using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatBar : MonoBehaviour
{
    #region Singleton

    public static FatBar instance;

    void Awake()
    {
        instance = this;
    }

    #endregion


    private Dictionary<int, float> sizes;

    private int state;

    private float y;

    private GameObject redBorder;
    private GameObject tooFatText;

    // Start is called before the first frame update
    void Start()
    {
        redBorder = GameObject.Find("RedBorder");
        tooFatText = GameObject.Find("TooFat");
        tooFatText.SetActive(false);

        state = 1;

        sizes = new Dictionary<int, float>();
        sizes.Add(1, -124.6f);
        sizes.Add(2, -97.5f);
        sizes.Add(3, -70.5f);
        sizes.Add(4, -43.6f);
        sizes.Add(5, -15.9f);

        y = -27.3992f;
    }

    public void ModifySize(bool increase)
    {
        if (increase && state < 6)
            state += 1;
        else if (!increase && state > 1)
            state -= 1;

        if (state < 6)
        {
            tooFatText.SetActive(false);
            PlayerManager.instance.player.GetComponent<PlayerMovement>().enabled = true;
            redBorder.GetComponent<RectTransform>().anchoredPosition = new Vector2(sizes[state], y);
        }
        else 
        {
            tooFatText.SetActive(true);
            PlayerManager.instance.player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
