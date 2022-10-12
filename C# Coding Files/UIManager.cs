using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import unitys GUI system
using UnityEngine.UI;
//import Unitys scene management system
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /*
     * The UI Manager script is to control the basic functions of the pause system the player will have avalible to them
     * player will be able to go back to the main menu
     * player will be able to resume (obviously)
     * player will be able to go to main menu
     */
    //create a reference to this object
    public static UIManager instance;

    //create an image variable
    //can only create this if the UI system has been imported
    public Image blackScreen;
    //create a float for the steady decline in imagery from black screen to transparency
    public float fadeSpeed;
    //boolean to fade to black screen from transparency
    public bool fadeToBlack;
    //boolean to fade to transparency from black screen
    public bool fadeFromBlack;
    //create a text to display the amount of health the player currently has
    public Text healthDisplay;
    //a corresponding health circle to visually show the amount of health a player has
    public Image healthCircle;
    //a text to display the amount of coins a player currently posseses
    public Text coinText;
    //create a gameObject to have the pause screen
    public GameObject pauseScreen;
    //create a gameobject for the options screen UI
    public GameObject optionsScreen;
    //make a Slider for the user to change the music level
    public Slider musicSliderVol;
    //make a Slider for the user to change the sound effects
    public Slider soundEffectsSliderVol;
    //make a string to represent the scene to load in the pause screen
    public string levelSelect;
    //create a string to represent the scene to load in the pause screen
    public string mainMenu;

    //runs before start
    private void Awake()
    {
        //set the instance to be equal to this object
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //run changeFade method
        changeFade();

    }
    //make thegame unpause
    public void Resume()
    {
        //run the change state method in the game manager
        GameManager.instance.ChangeState();
    }
    // open up the options menu
    public void OptionsOpen()
    {
        //run the method for the options in the game manager file
        GameManager.instance.OptionsMenuOpen();
    }
    //close the options menu 
    public void OptionsClosed()
    {
        GameManager.instance.OptionsMenuClosed();
    }
    //level select method to load the level select 
    public void LevelSelect()
    {
        //load the scene with the string input name
        SceneManager.LoadScene(levelSelect);
        //change the state of the game to allow it to unpause
        GameManager.instance.ChangeState();
    }
    //load the main menu
    public void MainMenu()
    {
        //load the new scene
        SceneManager.LoadScene(mainMenu);
        
        
    }
    //change the music level
    public void MusicLevel()
    {
        //go to the audio manager to change the music level
        AudioManager.instance.SetMusicLevel();
    }
    //change the sound effects 
    public void SoundEffectsLevel()
    {
        //go to the audio manager script to change the level of the sound effects
        AudioManager.instance.SetSoundEffectsLevel();
    }
    /*
     * changeFade method is to turn the screen black when a character dies/respawns
     * how it works is having a black screen competely overlaying the game
     * when a player dies or the black screen needs to be called for whatever reason, the opacity of the screen, (on a scale of 0 to 1) will slowly be increades which gives the impression of fading to black
     * 
     */
    //make a black screen to fade in and out of when the player dies, etc
    public void changeFade()
    {
        
        //boolean turns true when the user dies
        if (fadeToBlack)
        {
            

            //make a new color with the same specifications except now the opacity is turing to full
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1.0f, fadeSpeed * Time.deltaTime));
            //once the opacity is full, change the state of the boolean
            if (blackScreen.color.a == 1.0f)
            {    
                //now that the screen is black, turn the boolean to be false so the screen isn't trying to turn black
                fadeToBlack = false;
            }
        }
        //once respawned take off the opacity
        if (fadeFromBlack)
        {
            //if fade from black is true, the user should then movetowards making  transparent screen
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0.0f, fadeSpeed * Time.deltaTime));
            //turn the boolean to false once the screen is transparent
            if (blackScreen.color.a == 0.0f)
            {
                //this way the program won't keep trying to turn the screen to be more and mroe transparent
                fadeFromBlack = false;
            }
        }
    }
}
   


