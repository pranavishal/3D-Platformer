using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*A very large, flat plane is created beneath the poosition of the player
     *the player can only come into contact with the plane if the player walks and falls off the edge
     * when this happens, the player's position should be restored to 0,0,0 (coordinates)
     */
    private void OnTriggerEnter (Collider playerContact)
    {

        if (playerContact.tag == "Player")
        {
            //deactivates the player object
            RhodriObject.instance.gameObject.SetActive(false);
            //moves the player to the specified position
            RhodriObject.instance.transform.position = Vector3.zero;
            //reactivates the player
            RhodriObject.instance.gameObject.SetActive(true);

        }
    }
}
