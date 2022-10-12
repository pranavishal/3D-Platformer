using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //create an instance of this object for reference in other scrips
    public static GameManager instance;
    //awake function occurs before start
    //create a position in the world for the player to respawn
    private Vector3 respawnPosition;
    //create a slot in the object this script is attatched to, to attatch the particle effect
    public GameObject dyingEffect;
    //an integer for the coint amount that the player will have
    public int coinAmount;
    //create an integer to put in the song name of Scene
    public int victoryTheme;
    //give the slot to put in the name of the scene to leaed
    public string loadLevel;
    //create a string to check the level of the scene
    public string levelName;
    
    //initialize this instance with the Unity Awake function
    //this is not a function in actual C# coding, this is built into unity
    private void Awake()
    {   //set instance to be this object(Systems manager) this script is attached to
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {   //make the cursor not visible to the user
        Cursor.visible = false;
        //sets the state of the cursor to always be in the middle of the screen 
        Cursor.lockState = CursorLockMode.Locked;
        //make the position the player will respawn to the position the player initially starts at
        respawnPosition = RhodriObject.instance.transform.position;
        //take the amount of coins the player collected and convert it to a string
        //from there set it to the cointext slot in the UICanvas 
        UIManager.instance.coinText.text = coinAmount.ToString();
        //get the scene variable initialized with the current scene
        Scene nameOfLevel = SceneManager.GetActiveScene();

        //get the name of the current scene
        levelName = nameOfLevel.name;

    }

    // Update is called once per frame
    void Update()
    {
        //if the player hits escape during gameplay
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //call the change state function which will end up pauding the game
            ChangeState();
        }
        
    }


    public void Respawn()
    {
        //since scene 2 was more of a mobile game, the way the respawn would work was more quick and mainstream friendly
        //thus if level 2 is the current scene and the player dies . .  .
        if (levelName == "Level 2")
        {
            //just reload the level
            DragonController.instance.LoadScene();
        }

        else
        {
            //call the co routine
            StartCoroutine(RespawnCoFunction());
            //
            HealthSystem.instance.InstaDeath();
        }

    }

    public IEnumerator RespawnCoFunction()
    {
        //should make the game object of the player dissapear
        RhodriObject.instance.gameObject.SetActive(false);
        //deactivates the cinemachine
        CameraFunction.instance.theCMBrain.enabled = false;

        //make the game wait for 2 seconds before continuing. This is so the player can process the fact that they died instead of thinking the game randomly teleported them
        UIManager.instance.fadeToBlack = true;
        //this will start the particle effect, with the specific effect variable, the position this effect takes place in, and the rotation is takes place in
        //the position hadded 1 added to the y axis because just stating the players position would put the effect at the players feet which wasn't ideal
        Instantiate(dyingEffect, RhodriObject.instance.transform.position + new Vector3(0f, 1f, 0f), RhodriObject.instance.transform.rotation);
        //this is a custom function built into Unity
        //this function will pause the program for the allocated amount of seconds
        //this function only works in IEnumerator functions, and not regular voids
        yield return new WaitForSeconds(3f);
        //set the fadeFromBack boolean to true
        UIManager.instance.fadeFromBlack = true;
        //call the health restorer function becasue player health should be reset if they die
        HealthSystem.instance.HealthRestorer();
        //sets the respawn position
        RhodriObject.instance.transform.position = respawnPosition;
        //turns on the cinemachine
        CameraFunction.instance.theCMBrain.enabled = true;
        //makes the player active again
        RhodriObject.instance.gameObject.SetActive(true);
    }
    //void to create a new spawn position
    public void SetSpawnPoint(Vector3 newSpawnPosition)
    { 
        //setting the new respawn position
        //primary use of this function is for when a checkpoint is activated
        respawnPosition = newSpawnPosition;
        
    }
    //function to add coins as the players collect more coins
    public void AddCoins(int coinAdd)
    {
        //basic addition to the current amount
    coinAmount += coinAdd;
        //change the amount of coins displayed on the canvas interface
        UIManager.instance.coinText.text = coinAmount.ToString();
    }
    //this is the functions that pauses the game when the pasue menu is activated
    public void ChangeState()
    {   //this function is specific to Unity
        //the heirarchy section displays all of the objects that are present in a scene
        //when the activeInHierarchy function is called, it sees if the object the script is attatched to is actice in heirarchy
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {   //this then deactivates the pause screen so this is for when the user presses escape when the pause screen is actice
            //unpauses the game
            UIManager.instance.pauseScreen.SetActive(false);
            //this is another built in unity function
            //time scale works at mostly 1 or 0, with 1 being that time flows normally, and 0 at time being frozen
            //if at 0, the time scale essentially freezes everything on the screen
            Time.timeScale = 1f;
            //make the cursor not visible to the user
            Cursor.visible = false;
            //sets the state of the cursor to always be in the middle of the screen 
            Cursor.lockState = CursorLockMode.Locked;
        }
        //then the actions that should be called if the pause screen isn't active in the heirarchy, (user wants to pause the screen)
        else
        {   //make the pause screen active in the heirarchy
            UIManager.instance.pauseScreen.SetActive(true);
            //make sure the options is closed
            //this is needed because if the user goes to the options screen and then hits escape to resume gameplay, the next time escape is hit, the screen will revert directly to the options screen instead of the overall pause screen
            UIManager.instance.OptionsClosed();
            //set the time scale to 0 so everything in the game freezes
            //this makes sure no enemies or events can occur while the user is pausing the game
            Time.timeScale = 0f;
            //make the cursor not visible to the user
            Cursor.visible = true;
            //sets the state of the cursor to always be in the middle of the screen 
            Cursor.lockState = CursorLockMode.None;
        }

    }
    //function to open up the options menu
    public void OptionsMenuOpen()
    {
        //SetActive function to activate the screen
        UIManager.instance.optionsScreen.SetActive(true);
    }
    //function to close the options menu
    public void OptionsMenuClosed()
    {
        //SetActive function to deactivate the screen
        UIManager.instance.optionsScreen.SetActive(false);
    }
    //when the level ends, this coroutine runs
    public IEnumerator EndLevelCo()
    {
        //set the haswon boolean to true so that the player can no longer move
        RhodriObject.instance.hasWon = true;
        //let the audiomanager get the integer to play the victory theme
        AudioManager.instance.LevelWon(victoryTheme);
        //pause the game for 10 seconds
        yield return new WaitForSeconds(10f);
        
        //deactivate the player
        RhodriObject.instance.gameObject.SetActive(false);
        //display a message to the console
        Debug.Log("Level Complete!");
        //in the PlayerPrefs, let the name of this scene now be unlocked which allows for the next level to be accessed
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "unlocked", 1);
        //load the next scene (level Select)
        SceneManager.LoadScene(loadLevel);
        
        
        
    }
    

}