 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    /*
     * this class deals with the functions that occur when a user completes a level
     * once completed, the user should be taken to the level select scene in which they would usually progress to the next level
     */
     //create a boolean to check to see if the level has been completed
    public bool levelCompletion = false;
    //create an animator object for the main character
    //the main character will do a little dance when the level has been completed
    public Animator animation;
    //create a string to store which level has been completed
    public string levelNumber;
    //create a scene for this currentlevel
    public Scene currentLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    //OnTriggerEnter function to deterine if the user has completed the level through seeing if the user's collider has innteracted with the end of level object's colliders
    public void OnTriggerEnter (Collider levelEnded)
    {
        //making sure it is the player collider that has entered this collider
        if (levelEnded.tag == "Player")
        {
            //set the animation trigger to play
            animation.SetTrigger("Enter");
            //begin the coroutine of level end in the game manager file
            StartCoroutine(GameManager.instance.EndLevelCo());
            

             
        }

    }
}
