using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //method for if the player comes into contact with our box collider so that he can respawntwo colliders of any kind come into contact with one another
    //voids called OnTriggeredEnter are for when 
    void OnTriggerEnter(Collider deathCollider)
    {
        //checking if the other box collider hitting the death box is actially the player
        //if for some reason some other material or object with a collider comes into contact with the collider (which it shouldn't), the player shouldn't respawn and make a sound effect
        if (deathCollider.tag == "Player")
        {
            //go to the audio manager file to run through the PlaySoundEffects method with the 8th sound effect in the array
            //knowledge of sound effect order and placement in the array is necessary
            AudioManager.instance.PlaySoundEffects(8);
            //Use a debug to ensure that the program is reaching this stage of the code
            Debug.Log("You have died");
            //go to the game manager and run the respawn function
            GameManager.instance.Respawn();
            

        }
    }
}
