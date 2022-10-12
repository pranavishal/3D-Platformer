 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    /*
     * this is the basic health system of the game
     * this controls how the players health is displayed on the UICanvas, restored, etc
     */
    //create an instance of this class
    public static HealthSystem instance;
    //int for the health left
    public int healthLeft;
    //int for the max amount of health you can have
    public int maxHealth;
    //create the base for the invulnerable amount which will have a counter which will be reset to this base
    public float invulnerableAmount = 2f;
    //counter to invulnerableAmount
    private float invulnerableCounter;
    //create an array of Sprites (which for all intensive purposes are just images)
    public Sprite[] healthCircleImages;
    //initalize the instance of this class
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //set the health left to the max health
        HealthRestorer();
        
    }

    // Update is called once per frame
    void Update()
    {   
        //if statement to check to see if the player should still be invulnerable
        if(invulnerableCounter > 0)
        {   //start depleting the timer through the passage of time
            invulnerableCounter -= Time.deltaTime;
            //if statement for when the counter is below 1
            //this gives visual display to the invincibility frames
            if(invulnerableCounter < 1)
            {
                //for loop to turn the player on and off (flashing to show that damage has been taken)
                for (int i = 0; i < RhodriObject.instance.partsOfPlayer.Length; i++)
                {   //if the rounded amount down multiplied by 5s even
                    if (Mathf.Floor(invulnerableCounter * 5f) % 2 == 0)
                    {
                        //set the player to active
                        RhodriObject.instance.partsOfPlayer[i].SetActive(true);
                    }
                    else
                    {
                        //if not set the player to unactive
                        RhodriObject.instance.partsOfPlayer[i].SetActive(false);
                    }
                    //once the invulnerable counter has finally reached 0 or below
                    if(invulnerableCounter <= 0)
                    {
                        //set the player to active permanently to let user know there are know more invincibility frames
                        RhodriObject.instance.partsOfPlayer[i].SetActive(true);
                    }

                    
                }
            }
        }
        
    }
    //void to damage the player
    public void Damager()
    {
        //once the invulnerableCOunter has depleted
        if (invulnerableCounter <= 0)
        {
            
            //take away a health point when damage is done;
            healthLeft--;
           //conditional to check if the player has no more health
            if (healthLeft < 1)
            {
                //run the respawn funciton
                GameManager.instance.Respawn();
            }
            
            else
            {
                
                //else run the hit function in RHodriObject
                RhodriObject.instance.Hit();
                //reset the counter to the base
                invulnerableCounter = invulnerableAmount;
                
            }
            //run the update systems function
            UpdateSystems();
        }
    }
    //function to restore health
    public void HealthRestorer()
    {
        //reset the health to the max
        healthLeft = maxHealth;
        //run the update systems function
        UpdateSystems();
    }
    //function to add some health
    public void AddingHealth(int amountRestored)
    {
        //add the amountRestored to the current health
        healthLeft += amountRestored;
        //if this results in the health left being greater than max health set health left to be equal to max health
        //this is important because this way the user can't go over the max health limit
        if(healthLeft > maxHealth)
        {
            healthLeft = maxHealth;
        }
        //run the update systems function
        UpdateSystems();
    }
    //this updates the UI canvas to the player so they know their health
    public void UpdateSystems()
    {
        //sets the health amount on the UICanvas to be what amount of health the player currently has
        UIManager.instance.healthDisplay.text =healthLeft.ToString();
        /*
         * this is another switch case
         * depending on how much health the user has left, the image that pops up should be different
         */
        switch (healthLeft)
        {
            case 5:
                //make sure the image is enabled incase a user is respawning
                UIManager.instance.healthCircle.enabled = true;
                //set the image to be the full health bar image
                UIManager.instance.healthCircle.sprite = healthCircleImages[4];
                //stopping the case with break statement
                break;
            case 4:
                //set the image to have 4 health points left
                UIManager.instance.healthCircle.sprite = healthCircleImages[3];
                break;
            case 3:
                //set the image to have 3 health points left
                UIManager.instance.healthCircle.sprite = healthCircleImages[2];
                break;
            case 2:
                //set the image to have 2 health points left
                UIManager.instance.healthCircle.sprite = healthCircleImages[1];
                break;
            case 1:
                //set the image to have 1 health points left
                UIManager.instance.healthCircle.sprite = healthCircleImages[0];
                break;
            case 0:
                //command to disable the image essentially making it dissapear for the player.
                //equalizing it to null would make a white screen and thats not essential
                UIManager.instance.healthCircle.enabled = false;
                break;

        }

    }
    //function to kill player immediately
    public void InstaDeath()
    {
        //set the current health to be 0
        healthLeft = 0;
        //run the update systems function
        UpdateSystems();
    }
}
