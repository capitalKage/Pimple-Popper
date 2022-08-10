using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider slider;
    public Image sliderFill;
    public TMP_Text vConditionText;
    MainGameScript main;
    private float value;
    public TMP_Text sliderFillText;
    public TMP_Text painText;

    public TMP_Text playAgain;
    // Start is called before the first frame update
    void Start()
    {
        playAgain.enabled = false;
        main = FindObjectOfType<MainGameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        WinLoseText();

        if (main.pressure < 2)
        {
            sliderFill.color = Color.yellow;
        }
        else if (main.pressure > 2 && main.pressure < 4)
        {
            sliderFill.color = Color.green;
        }
        else if (main.pressure > 4)
        {
            sliderFill.color = Color.red;
        }
        slider.value = main.pressure;

        int convertedValue = (int)main.pressure;
        sliderFillText.text = convertedValue.ToString();

        int convertedPain = (int)main.patientPain;
        painText.text = "Pain: " + convertedPain + "/10";
    }

    private void WinLoseText()
    {
        if (main.gameOver == true)
        {
            vConditionText.text = "Ouch!";
            playAgain.enabled = true;
        }
        else if (main.playerWon == true)
        {
            playAgain.enabled = true;
        }
    }
}
