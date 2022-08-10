using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float painMax = 10f;
    // Time that good zone is held.
    private float goodZoneTime = 0f;
    // Good zone held successful time
    private float goodTimeVictory = 6f;
    #endregion
    #region Victory variables
    public bool playerWon = false;
    public bool gameOver = false;
    #endregion
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public AudioSource audio;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

    public AudioClip yell;
    public AudioClip pop;

    public GameObject puss;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && gameOver == false && playerWon == false)
        {
            pressure += pressureIncrease;
            Debug.Log("Button pressed: " + pressure);
            GoodPressureZone();
        }
        else if (Input.GetButton("Jump") && gameOver == false && playerWon == false)
        {
            pressure += pressureIncrease * 7 * Time.fixedDeltaTime;
            Debug.Log("Button held: " + pressure);
            GoodPressureZone();
        }
        else if (!Input.GetButton("Jump") && pressure > 0 && gameOver == false && playerWon == false)
        {
            pressure -= pressureDecrease * 7 * Time.fixedDeltaTime;
            Debug.Log("Button released: " + pressure);
            GoodPressureZone();
        }
        else if (Input.GetButtonDown("Jump") && playerWon == true)
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetButtonDown("Jump") && gameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void GoodPressureZone()
    {
        if(pressure > 2 && pressure < 4 && gameOver == false && playerWon == false)
        {
            puss.transform.Translate(Vector3.back * .3f * Time.deltaTime);
            goodZoneTime += Time.fixedDeltaTime;
            if(goodZoneTime >= goodTimeVictory && gameOver == false && playerWon == false)
            {
                puss.transform.Translate(Vector3.back * 2);
                playerWon = true;
                audio.PlayOneShot(pop);
            }
        }
        else
        {
            goodZoneTime = 0;
            if(puss.transform.position.z < -2.42f && gameOver == false && playerWon == false)
            {
                puss.transform.Translate(Vector3.forward * .3f * Time.deltaTime);
            }
        }

        if (pressure > 4 && gameOver == false && playerWon == false)
        {
            patientPain += painIncrease * pressure * Time.fixedDeltaTime;
            if (patientPain >= painMax)
            {
                audio.PlayOneShot(yell);
                gameOver = true;
            }
        }
    }
}
