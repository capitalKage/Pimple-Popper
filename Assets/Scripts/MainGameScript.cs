using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScript : MonoBehaviour
{
    #region Core Variables
    // Patient pain always starts off as zero
    public float patientPain = 0f;
    // Pressure used to pop pimple is at zero
    public float pressure = 0f;
    // Pressure increase if button is pressed
    private float pressureIncrease = 0.4f;
    // Presssure decrease if button is released
    private float pressureDecrease = 0.35f;
    // Pain increased of outside of the 'Good' area.
    private float painIncrease = 0.5f;
    // Max pain patient can reach.
    private float painMax = 25f;
    // Time that good zone is held.
    private float goodZoneTime = 0f;
    // Good zone held successful time
    private float goodTimeVictory = 6f;
    #endregion
    #region Victory variables
    public bool playerWon = false;
    public bool gameOver = false;
    #endregion
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            pressure += pressureIncrease;
            Debug.Log("Button pressed: " + pressure);
            GoodPressureZone();
        }
        else if (Input.GetButton("Jump"))
        {
            pressure += pressureIncrease * 3f * Time.fixedDeltaTime;
            Debug.Log("Button held: " + pressure);
            GoodPressureZone();
        }
        else if (!Input.GetButton("Jump") && pressure > 0)
        {
            pressure -= pressureDecrease * 2 * Time.fixedDeltaTime;
            Debug.Log("Button released: " + pressure);
            GoodPressureZone();
        }
    }

    private void GoodPressureZone()
    {
        if(pressure > 2 && pressure < 4)
        {
            goodZoneTime += Time.fixedDeltaTime;
            if(goodZoneTime >= goodTimeVictory)
            {
                Debug.Log("POP! Player victory");
                playerWon = true;
            }
        }
        else
        {
            goodZoneTime = 0;
        }

        if (pressure > 4)
        {
            patientPain += painIncrease * pressure * Time.fixedDeltaTime;

            if (patientPain >= painMax)
            {
                Debug.Log("OUCH! Game Over");
                gameOver = true;
            }
        }
    }
}
