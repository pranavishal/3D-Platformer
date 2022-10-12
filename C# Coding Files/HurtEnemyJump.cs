using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemyJump : MonoBehaviour
{
    /* 
     * Originally was going to kill enemies by jumping on hence the name of the class
     * that was deemed unoriginal and decided to try something much more challenging with adding a sword and an animation to sword swing
     *Had to Use blender to add sword in players hand and bring in animation
     *Blender is the most widely used 3d model creator and animator
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //function to deal damage to the enemy
    private void OnTriggerStay(Collider hurtEnemy)
    {
        //making sure the collider that the player is intact with is that of an enemy
        if (hurtEnemy.tag == "Enemy")
        {
            
            //write a Debug to prove this as some issues and inconsitencies were popping up
            Debug.Log("Successful 1");
            if (RhodriObject.instance.isInvincible)
            {
                //go to the enemy health manager and use the take damage function
                //this is a different way in Unity and C# to call a function in a different class
                hurtEnemy.GetComponent<EnemyHealthManager>().TakeDamage();
            }
            //if the player presses shift here along with being in the collider
            if (Input.GetButtonDown("Fire3"))
            {
                //go to the enemy health manager and use the take damage function
                //this is a different way in Unity and C# to call a function in a different class
                hurtEnemy.GetComponent<EnemyHealthManager>().TakeDamage();
            }
        }
    }
        
}
