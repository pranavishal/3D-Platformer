using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import unitys scene management system in order to call and load different scenes into the game
using UnityEngine.SceneManagement;

public class LevelSelectLevelEntry : MonoBehaviour
{
    //create a string in order to load the proper level deginated to the entry object
    public string levelToLoad;
    //create a string in order to tell the player prefs what level has been completed
    public string levelCompletion;
    //a boolean to determine whether or not the player can access the level if they are standing on it
    private bool levelAccessible;
    ////a boolean to determine whether or not the player can access the level if the prior level haas been unlocked
    private bool canUnlock;
    //create an object to store particle effects for when a player stands on a locked entry point
    public GameObject particleEffectLevelLocked;
    //there are 2 level entry objects, one is for when its locked and unlocked
    //on is for when its unlocked
    public GameObject on;
    //off is for when its locked
    public GameObject off;
    //boolean to fade out in and out of black between levels
    private bool fade;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * PlayerPrefs are an in built system into unity, (this isn't a function that works with just C#
         * Straight forward in terms of calling functions
         * can only save integers, strings, and floats
         * The main use of this is to unlock levels as players progress through the game
         * basic functions is PlayerPrefs.(name of function)
         */

        //get an integer from the player prefs function, as well as the string attatched
        //in this case unlocked levels are labeled as unlocked
        //the or portion of the if statement is for when the user first starts the game. They should be able to enter the first level as the levelCompletion string is empty
        if (PlayerPrefs.GetInt(levelCompletion + "unlocked") == 1 || levelCompletion == "")
        {
            //this now means the the entry object should be set to the on version in which players can now enter the level
            on.SetActive(true);
            //the off version should be deactivated as well
            //this is more of a precautionary measure to avoid things potentially going wrong in the future since turning the on version should override the off version
            off.SetActive(false);
            //this now sets the canUnlock variable to true which is one of the conditions that must be met for the player to be able to be able to proceed to the next level
            canUnlock = true;
        }
        //if these conditions in the if statement are not met the user should have the level be inaccessible to them
        else
        {
            //the on version should be set to deactivate
            on.SetActive(false);
            //the off version should be active
            off.SetActive(true);
            //and the canUnlock bool should be false, which means a condition in order to start a level is not there
            canUnlock = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if statement checked every frame
        /*
         * Based on 3 conditions being met
         * the first condition is to see if the user presses shift 
         * the second condition is if the player is actually standing on the level entry object. The player shouldn't be able to access a level if they aren't standing on the levelentry object
         * the final condition is the canUnlock condition which is to make sure the prior levels have been completeted
         */
        if (Input.GetKey(KeyCode.LeftShift) && levelAccessible && canUnlock)
        {
            //sets the fade variable to be true which loads a black screen into the new level
            fade = true;
            //using the scene manager import, unity will load the scene given to it from the levelToLoad string
            SceneManager.LoadScene(levelToLoad);
            //while this is happening, a cotourtine should run
            StartCoroutine(LevelBegin());
        }
        //if the user is not standing on the object and it isn't accessible to them
        if (!levelAccessible && !canUnlock)
        {
            //no particle effect should be false
            particleEffectLevelLocked.SetActive(false);
            //screen shouldn't fade to black
            fade = false;

        }
        
    }
    //void for when the player comes into contact witht he level entry
    private void OnTriggerEnter(Collider levelEntryCollider)
    {
        //if statement for when the 2 colliders meet (Player and entrycollider)
        if (levelEntryCollider.tag == "Player")
        {
            //turn the boolean to true because the player box collider is in contact with the level entry collider 
            levelAccessible = true;
            //turn on the particle effect
            //since the particle effect is only attatched to the off version in unity, it will only work for the off version which is intended
            particleEffectLevelLocked.SetActive(true);
        }
    }
    //create a void for when the player exits the level entry
    //this is necessary because if this void doesn't exist, levelAccessible would remain true if a player walked into an entry and then left it and pressed shift
    //this means a player could enter a level without being located on the entry point
    private void OnTriggerExit(Collider levelEntryCollider)
    {
        //if statement for when the 2 colliders meet (Player and entrycollider)
        if (levelEntryCollider.tag == "Player")
        {
            //set the levelAccessible boolean to false
            levelAccessible = false;
        }
    }
   
    //this is the coroutine that runs when a player begins a level
    public IEnumerator LevelBegin()
    {
        //turn off/deactivate the player in order to give the impression of teleportation
        RhodriObject.instance.gameObject.SetActive(false);  
        //wait 3 seconds
        //this is why a coroutine is needed as this function cannot be run in a regular void
        yield return new WaitForSeconds(3f);
        //load the scene the level entry object is attatched to
        SceneManager.LoadScene(levelToLoad);
    }
    
}
