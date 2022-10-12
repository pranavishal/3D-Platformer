using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamager : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //checking to see if a player is hit
    void OnTriggerEnter(Collider playerDamager)
    {
        //making sure that it is the player collider that is coming into contact so that the player is only taking damage when they come into contact with the damager, and not other colliders making the player take damage
        if (playerDamager.tag == "Player")
        {
            //if the player has the invincible item
            if (RhodriObject.instance.isInvincible)
            {
                //destroy the skeleton
                EnemyHealthManager.instance.TakeDamage();
            }
            else
            {
                //play the damage sound effect from the audio manager
                AudioManager.instance.PlaySoundEffects(8);
                //go to the health system method that controls player damage
                HealthSystem.instance.Damager();
            }

        }

    }
}
