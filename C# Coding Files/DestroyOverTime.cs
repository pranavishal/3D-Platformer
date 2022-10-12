using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    /*
     * THIS CLASS IS NOT YET BEING IMPLEMENTED INTO THE GAME
     THIS WILL BE USED LATER ON AS FURTHER WORK IS DONE ON THE GAME
     */
    public float lifespan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifespan);
    }
}
