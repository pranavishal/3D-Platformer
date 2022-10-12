using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhodriObject : MonoBehaviour
{
    /*
     * This script is to control the player in the game
     * player should be able to run, jump and hit enemies
     * other aspects are included such as the animation system in Unity in where the player's animation is different based on whether they run, jump or are doing nothing
     * particle effects for when the player is dying is included
     * when the player is hit by enemies or spikes, they are flung back
     * when a player is damaged they are also given what is called "invincibility frames" in game development terms so that they cannot be damaged in quick succession
     */
     //create an instnce of this object
    public static RhodriObject instance;
    // create a float for the speed the player will have
    public float runSpeed;
    //create a float for the amount of height a player can jump
    public float jumpHeight;
    //helps make realistic gravity
    public float gravityEnhancer = 5f;
   //make  float in order to control how fast a player rotates when tturning in the game
    public float rotateSpeed;
    //this is a function built into unity, which controls the 3 dimensions of movement a player can move along, (the 3 axis)
    private Vector3 direction;
    //boolean to see if the player object should be invincible
    public bool isInvincible = false;
    //a gameObject to represent the invincibility particle effect
    public GameObject invincibleEffect;
    //boolean to determine if the level has been completed. If so, the player shouldn't be able to move
    public bool hasWon = false;
    //signals a package to be sent to the player object in which several actions regarding the player's physical space can be done
    public CharacterController controlHub;
    //create a camera object for the player which will follow the player around and allow for movement and rotation to see surroundings
    private Camera camera;
    //create an object to store in the model of the player
    //this is a royalty free pre built model
    //in order to create an original model, a 3d model creator needs to be used
    //the most popular platform for this is blender
    //blender was used to fit the sword into the players hand 
    public GameObject playerModel;

    
    //create an animator object in order to control how the player animates when undergoing different actions
    public Animator animationPlayerObject;
    //boolean for if the player his hit
    public bool isHit;
    //time is knocked back after taking damage
    public float isHitLength = 0.5f;
    //this will essentially be a copy of hit length
    //when the player is damaged, this copy will go to 0, and then will revert to the isHitLength variable afterwards
    private float hitCounter;
    //A Vector2 is similar to a vector3 just one less dimension
    //this is called for when the player needs to be knocked back, and the force they will be knocked back with
    public Vector2 power;
    //this is an array of objects which are composed of the 3d components a player has
    public GameObject[] partsOfPlayer;
    //initialize the instance
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {   //makes this set to the main camera built into unity
        camera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit && !hasWon)
        {
            //stores the new value of the direction the player is along the y axis
            float yStore = direction.y;
            //Unity has a lot of built in mechanisms for this, where the horizontal and vertical plane have default settings and defined controls which allow the player to move alont the x and z axis
            //direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            //makes the player move forward or backward relative to where the player is facing, not just increasing the z value
            direction = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            //makes the diagonal movement along x and z axis not faster as the vectors used in both planes would increase speed
            direction.Normalize();
            //move the player along these directions at a pace multiplied by the run speed which is defined in unity
            direction = direction * runSpeed;
            //set the y axis to yStore. This is done so that the value of direction.y isn't set to 0 after it is redefined 
            direction.y = yStore;
            //another built in function of unity where the space bar has predefined controls to jump
            if (controlHub.isGrounded)
            {   //makes it so that gravity isn't being applied if the user is on the ground
                direction.y = 0f;

                if (Input.GetButtonDown("Jump"))
                {   //set the y axis amount to the jump height which is defined in Unity
                    direction.y = jumpHeight;
                }
            }
            //add that to the product of the unity built in gravity method, the gravity enhancer, and delta time
            //delta time allows the player to move by time rather than frame. We don't want the player to move by every frame because then the game would run different depending on the hardware of the player
            direction.y += Physics.gravity.y * Time.deltaTime * gravityEnhancer;
            //below is the standard way of incorporating character movement within a 3d game
            //transform.position = transform.position + (direction * Time.deltaTime * runSpeed);
            //same concept of delta time used for the character controller
            controlHub.Move(direction * Time.deltaTime);
            //if statement in which the condition is if there is some input being given by the user to move the character along the x and z axis
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                //this takes a 4 digit quaternian value and turns it into a vector3 which is useful because the math with a quaternian can get much too complicated for our understanding
                //when rotating the player based on the camera angle, it is important to know that only the y value is being rotated. We don't want to rotate our character along the x and z plane
                transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);
                //makes the character face the direction they are moving in
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
                //playerModel.transform.rotation = newRotation;
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }
        //conditional for if a player ends up taking damage
        if (isHit)
        {
            //so your knockback time starts decreasing
            hitCounter -= Time.deltaTime;
            //stores the new value of the direction the player is along the y axis
            float yStore = direction.y;
            //makes the player move forward or backward relative to where the player is facing, not just increasing the z value
            direction = (playerModel.transform.forward * -power.x);
            //makes the diagonal movement along x and z axis not faster as the vectors used in both planes would increase speed
            direction.y = yStore;
            if (controlHub.isGrounded)
            {   //makes it so that gravity isn't being applied if the user is on the ground
                direction.y = 0f;

 
                }
            //re apply the gravity force onto the player
            direction.y += Physics.gravity.y * Time.deltaTime * gravityEnhancer;
            //make them move based on Time, not frame so that it is universal movement regardless of player framerate
            controlHub.Move(direction * Time.deltaTime);
        }
        //once the hit counter finally has reached 0, the invincibility frames should end, and the player object should function as normal
        if (hitCounter <= 0)
        {
            //the player is no longer being hit, and the conditional above is no longer met
            isHit = false;

        }
        //this statement controls the player's actions once they have collided with the level end collider
        if (hasWon)
        {
            //this sets the animation bool to be true, which will then lead the player to proceed into the winning animation
            animationPlayerObject.SetBool("Won", hasWon);
            //the following 2 lines make it so that the player can no longer move since their directions are now set to 0, and gravity is still being applied so they are on the ground
            direction = Vector3.zero;
            //this line was needed as the player would revert to the jumping animation once the level end object had been touched for unkown reasons
            direction.y += Physics.gravity.y * Time.deltaTime * gravityEnhancer;
            //tell the player to move, in which the vector is 0, meaning the player cannot move
            controlHub.Move(direction);
            
        }
        /*
         * this conditional is to see if the player has decided to attack the enemy
         * in unity, there are some built in keywords which correspond to specific keyboard functions
         * for example, "jump" responds to the space bar
         * these are flexible, and different keywords can be made
         * for this program keywords built into unity are used
         */

        //conditional to see if "Fire3" (the left shift) is pressed
        if (Input.GetButtonDown("Fire3"))
        {
            //make the player enter their attacking animation
            //a trigger is simply an action that will occur once, thus unlike a bool, the state in which its in (true or false) is not needed
            animationPlayerObject.SetTrigger("Attacking");
        }
        //this gets the float value of the speed paramter built into the animation system within unity
        //how the paramter was built was such that if the speed was above 0.1 in terms of the float value, the player would then be set to their running animation
        //otherwise (the float value was set to less than 0.1,), the player would be in their "idle" animation
            animationPlayerObject.SetFloat("Speed", (Mathf.Abs(direction.x)) + Mathf.Abs(direction.z));
        //this also checks to see if the player is on the ground and this is a boolean
        //this is neeed to determine whether or not the player should be in their jumping/falling animation
        //(character control object of some type).isGrounded is a built in function into unity to determine whether or not the character is on some form of plane or mesh collider
            animationPlayerObject.SetBool("OnGround", controlHub.isGrounded);
        
       
       
        
        
    }

    //void for when the player is hit
    public void Hit()
    {
        //set this boolean to true so the other conditional in the update void can run
        isHit = true;
        //set the hit counter to be the same amount as the hit length
        hitCounter = isHitLength;
        //move the player upwards when hit
        direction.y = power.y;
        //move them based off time and not framerate
        controlHub.Move(direction * Time.deltaTime);
    }
    //void to destroy the player object for whatever reason necessary
    //usually when dead, level won, or entering a level
    public void Destroy()
    {
        //Destroy(gameObject) is a built in function to destroy the whole object this script is attatched to
        Destroy(gameObject);
    }


}
