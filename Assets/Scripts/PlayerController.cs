/* Daniel Eggers
 * 12/13/2020
 * Player logic and HUD
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float forwardSpeed;  //vspeed
    public float slideSpeed;    //hspeed
    private float steer;        //h-axis
    private bool isAlive;
    public float fuelMax;       //starting and maximum amount
    private float fuelTank;     //current amount (dynamic)
    public float fuelEconomy;   //fuel depletion rate
    public float fuelBonus;     //fuel gain from pickup
    public Text fuelText;       //hud
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isAlive = true;
        fuelTank = fuelMax;
        restartButton.gameObject.SetActive(false);  //hide button at start
    }

    private void Update()
    {
        if (isAlive)
        {
            fuelTank -= fuelEconomy * Time.deltaTime;
            if (fuelTank <= 0) 
            { 
                isAlive = false; restartButton.gameObject.SetActive(true); 
            }
            fuelText.text = fuelTank >= 1 ? " Fuel: " + Mathf.Floor(fuelTank) : " Fuel: E ";    //update fuel HUD
            steer = Input.GetAxis("Horizontal");    //get player left right input
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        //if player is still alive, process movement
        rb2d.velocity = isAlive ? new Vector2(steer * slideSpeed * Time.fixedDeltaTime, forwardSpeed * Time.fixedDeltaTime) : new Vector2(0,0); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioSource[] sounds = GetComponents<AudioSource>();    //get sound fx from inspector

        if (other.gameObject.CompareTag("Enemy"))
        {
            //induce game over
            sounds[3].Stop();
            sounds[2].Play();
            restartButton.gameObject.SetActive(true);
            isAlive = false;
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            fuelTank += fuelBonus;
            other.gameObject.SetActive(false);
            sounds[1].Play();
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            sounds[0].volume *= 0.5f;
            sounds[4].Play();   //play fanfare
            isAlive = false;
            fuelText.text = " YOU WIN! ";
        }
    }

    public void OnRestartButtonPress()
    {
        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene("SampleScene"); // restart the game
    }
}
