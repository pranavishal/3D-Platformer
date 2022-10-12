using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    /*
     * dealing with checkpoint functionality in the game
     */

    //create 2 gameObjects for the checkpoints
    //ON is for an active checpoint
    public GameObject ON;
    //OFF is for a deactive checkpoint
    public GameObject OFF;
    
    

    // Start is called before the first frame update

    void Start()
    {
        //create an array and use a built in function to find all Checkpoint objects within a scene
        CheckPoints[] allcheckPoints = FindObjectsOfType<CheckPoints>();
        
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //function for when the player enters the checkpoint collider
    void OnTriggerEnter (Collider checkpointCollider)
    {
        //play the sound effect of entering  check point
        AudioManager.instance.PlaySoundEffects(4);
        //create an array and use a built in function to find all Checkpoint objects within a scene
        CheckPoints[] allcheckPoints = FindObjectsOfType<CheckPoints>();
       
        //make sure the player is entering the checkpoint
        if (checkpointCollider.tag == "Player")
        {
            //change the new spawn point in the game manager
            GameManager.instance.SetSpawnPoint(transform.position);
            
            //for loop to make all check points off
            for (int i = 0; i < allcheckPoints.Length; i++)
            {
                //take the off version of the checkpoint and activate them
                allcheckPoints[i].OFF.SetActive(true) ;
                //take the ON version of the checkpoint and deactivate them
                allcheckPoints[i].ON.SetActive(false) ;
            }
            //once done, set checkpoint just collided with to have its off version deactivated
            OFF.SetActive(false);
            //have the on version activated
            ON.SetActive(true);
            //restore the player's health when they reach the checkpoint
            HealthSystem.instance.HealthRestorer();


           
           
        }
        
        
    }
}
