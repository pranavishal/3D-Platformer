using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    /*
     * manages the Enemy's health and functions
     */
    //create an instance of this class
    public static EnemyHealthManager instance;
     //set the max health of an enemy to 3
     //this can be changed back to 1 in unity
    public int maximumHealth = 3;
    //current health integer
    //not needed as of now since enemies only have 1 health point
    //may add more beefy enemy types later on however
    private int currentHealth;
    //create an integer which will direct to the array in the audio manager to play the corresponding enemy damaged sounde effect
    public int deathSound;
    //create a death particle effect for the skeleton
    public GameObject deathEffectSkeleton;
    //create an object for the bronze coin the skeleton will drop
    public GameObject droppedItem;
    //initialize the instance
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //set the current health to be the maximum health
        currentHealth = maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void to deal with enemy taking damage
    public void TakeDamage()
    {
        
        //lower the current health by 1
        currentHealth--;
        //start the coroutine
        StartCoroutine(WaitForAnimation());
        //play the sound effect for the enemy death sound in the audio manager
        AudioManager.instance.PlaySoundEffects(deathSound);
        //if statement for if the current health is 0
        if (currentHealth <= 0)
        {
            
            //start the same coroutine
            StartCoroutine(WaitForAnimation());
            //play the death of enemy particle effect at specified location
            Instantiate(deathEffectSkeleton, transform.position + new Vector3(0f, 1f, 0f),transform.rotation);
            //play the dropped coin of the enemy particle effect at the specified location
            Instantiate(droppedItem, transform.position + new Vector3(0f, 0.2f, 1f), transform.rotation);
            //destroy the skeleton gameObject (since this is what this script is attatched to
            Destroy(gameObject);
        }
    }
    /*
     * this coroutine is needed because the enemy would die before the player swinging animation would go through fully
     * waiting for .2 seconds make the process look much more smooth for the player
     */
    public IEnumerator WaitForAnimation()
    {
        //wait for .2 seconds
        //this command only works in an IEnumerator and not a void which is why a coroutine was necessary
        yield return new WaitForSeconds(0.2f);
    }
    
}

