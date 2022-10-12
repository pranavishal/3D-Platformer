using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    /*
     * deals with picking up coins in the game
     */
    //an integer to represent how much a coin is worth
    public int coinValue;
    //create a game object to store the coin particle effect
    public GameObject coinEffect;
    //create an int to play the designated sound effect in the audio manager array 
    public int soundToPlay;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider coinPickUp)
    {
        //making sure the other collider is the player's
        if(coinPickUp.tag == "Player")
        {

            //destroys whatever game object this is attatched to
            Destroy(gameObject);
            //add the coin value to the GameManager
            GameManager.instance.AddCoins(coinValue);
            //play the particle effect
            Instantiate(coinEffect, transform.position, transform.rotation);
            //play the sound effect from the audio manager
            AudioManager.instance.PlaySoundEffects(soundToPlay);
            
            
        }
    }
}
