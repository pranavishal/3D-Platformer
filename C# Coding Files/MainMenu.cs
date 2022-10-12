using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*
     * the main menu scene is what will first boot when the player starts up the game
     * the main purpose of this scene is to be able to start a new game, or continue your game
     * you should also be able to quit
     */
    //create a string which will represent a new game (for when a new scene is loaded)
    public string newGame;
    //create a string which will represent loading into the level select and continue off 
    public string continueLevel;
    //make the button in order to continue the game
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        //using the PlayerPrefs system, check to see if the user has acually began the game prior
        //this is needed because when the user loads up the game for the first time, they shouldn't be able to continue because it is their first time playing it
        if (PlayerPrefs.HasKey("Began"))
        {
            //but if the player has began the game prior, the continue button should be active
            continueButton.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //if the user selects the new game button
    public void NewGame()
    {
        //load the new game 
        SceneManager.LoadScene(newGame);
        //Set the PlayerPrefs to now contain "Began" as to make sure the continue button will pop up when the game is now booted again
        PlayerPrefs.SetInt("Began", 0);
    }
    //the method to load the level select screen
    public void ContinueLevel()
    {
        //set the time scale to now be normal as the player can move in the level select scene
        Time.timeScale = 1f;
        //load the level select scene
        SceneManager.LoadScene(continueLevel);
    }
    //the method to quit the game should the user click the Quit button
    public void QuitGame()
    {
        //close the application with this in nuilt unity function
        Application.Quit();
        //this cannot be tested within unity, thus this debug is present to make sure the method is running as expected
        Debug.Log("GAme Quitted");
    }
}
