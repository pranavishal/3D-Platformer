using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import unity's built in game view camera system called the cinemachine
using Cinemachine;

public class CameraFunction : MonoBehaviour
{
    /*
     * the cinemachine is a built in camera system that is designed to be moved round and follow a specific rule set that is optimized for gameplay
     */
    //create an instance of the camera
    public static CameraFunction instance;
    //create an object for this specific camera type
    public CinemachineBrain theCMBrain;

    private void Awake()
    {
        //set this to be a reference to the class
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
