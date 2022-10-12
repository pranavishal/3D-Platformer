using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    /*
     * this class is for heart pickups that restore the playres health by 3 points or full health
     */
    //create an int for the amount of health being restores
    public int healAmount;
    //boolean to determine whether or not the players health should be fully restored
    public bool fullHealthRestore;
    //create an object for the heart particle effect when the player picks on up
    public GameObject healthEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //enter on trigger function to get the heart
    private void OnTriggerEnter (Collider heartPickUp)
    {
        //make sure it is the player collider that touches the heart
        if (heartPickUp.tag == "Player")
        {
            //destroys whatever game object the script is attatched to
            //in this case is destroys the heart
            Destroy(gameObject);
            //play the sound effect designated for heart pickups from the sudion manager class
            AudioManager.instance.PlaySoundEffects(7);
            //check to see if the heart was a full health restore heart
            if (fullHealthRestore)
            {
                //restore health fully from the HealthRestorer function in the health system
                HealthSystem.instance.HealthRestorer();
                //run the particle effect for the heart pickup at the specified location
                Instantiate(healthEffect, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
            }
            else
            {
                //restore health partially from the AddingHealth function in the health system
                HealthSystem.instance.AddingHealth(healAmount);
                //run the particle effect for the heart pickup at the specified location
                Instantiate(healthEffect, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
            }
        }
    }
}
