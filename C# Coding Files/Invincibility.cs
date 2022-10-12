using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    //STILL IN WORKS
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function for when the item collider interacts with the player
    private void OnTriggerEnter(Collider invincibleObject)
    {
        //check to make sure the player collider is what enters
        if (invincibleObject.tag == "Player")
        {
            //set Rhodri to now be invincible
            RhodriObject.instance.isInvincible = true;
            //play the invincibility theme song
            AudioManager.instance.PlayTracks(10);
        }
    }

        
}
