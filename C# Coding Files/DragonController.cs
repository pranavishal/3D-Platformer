using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonController : MonoBehaviour
{
    //create an instance for this class
    public static DragonController instance;
    //create a boolean for restarting the level
    public bool beenHit = false;
    //create a string that represents the scene to reload
    public string restartLevel;
    //initialize the instance
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if beenHit is true
        if (beenHit)
        {
            //run the LoadScene method
            LoadScene();
        }
        //if the player wins by reaching the level end collider
        if (RhodriObject.instance.hasWon)
        {
            //destroy the dragon
            Destroy(gameObject);
        }
    }
    //void for when the box collider of the dragon meets the colider of the player
    public void OnTriggerEnter (Collider other)
    {
        
        //make sure the player box collider is whats interacting
        if (other.tag == "Player")
            
        {
            //set beenHit to true to restart the level
            beenHit = true;

        }
    }
    //simple method to restart the level
    public void LoadScene()
    {
        //restart level
        SceneManager.LoadScene(restartLevel);
    }
}
