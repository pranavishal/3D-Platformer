using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    /*
     * this script is what controls enemy behaviour
     * using NavMeshAgents, and the unity AI system, the enemies will have a basic behaviour
     * they will move between patrol points and when the player is within a certain distance of the enemy object, the enemy will move between the player
     */
    // create an instance of the class for reference
    public static EnemyController instance;
    //create an array of Transforms which are basically positions in the scene
    //these will be what the skeletons move from
    public Transform[] skeletonBaseAreas;
    //have an integer to represent the current base the skeleton is at as to determine its next one
    public int currentPoint;
    //create a NavMeshAgent
    public NavMeshAgent agent;
    //create an animator for the skeletons diffferent actions
    public Animator anim;
    //use an enum to direct the skeletons actions
    //an enum is a variable which has a string, with an underlying value which is an integer
    public enum AIState
    {
        //the states are for when the skeleton is idle, moving around to the points, chasing the player, or attacking the player
        stationaryPosition, onTheMoving, pursuingPlayer, playerDamager
    };
    //utilize the variable into the current state
    public AIState actionRightNow;
    //create a float for the time taken at each patrol point
    //this is the base value, which means a counter will be set equal to this and counted down and reset back to this value
    public float waitAtPoint = 30f;
    //counter to the base
    private float waitCounter;
    //a float to represent the amount of distance that must be met between the enemy and the player in order for the enemy to stop patrolling and chase the player
    public float playerInPursuitArea;
    //same concept as the chase range, except this is used for when the enemy is close enough to hit the player
    public float playerDamagerReach = 1f;
    //a float to make the enemy wait for a bit before going into the next attack
    //this is the base value which means a counter will be set to this value, and counted down and reset again
    public float timeBetweenAttacks = 2f;
    //this is a counter for timeBetweenAttacks
    private float attackCounter;

    //initialize the instance
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //set the wait counter to be the same as wait at point
        waitCounter = waitAtPoint;

    }


    // Update is called once per frame
    void Update()
    {

        //built in function which takes the overall distance from the enemy (since this is what this script is attatched to) to the player
        float vectorRangeFromPlayer = Vector3.Distance(transform.position, RhodriObject.instance.transform.position);
        /*
         * this is a switch statement
         * the basic premise of a switch statement is sort of similar to a conditional (if statement)
         * using the enum, if a certain state is met, a different set of code will be performed
         * once performed, the code will break to ensure that there is an end
         * in each of these states, another state can be activated meaning that the code under that state is now run once the break is reached
         */
        //make a switch statement of the actionRIghtNow enum
        switch (actionRightNow)
        {
            //for the case in which the AIState is in the stationaryposition (the enemy is not moving)
            case AIState.stationaryPosition:
                //set the animation boolean to false to ensure that the enemy isn't in the moving animation when they shouldn't be
                anim.SetBool("IsMoving", false);
                //take the wait counter and shorten it through time
                //this will be the enemy waiting at the base point
                if (waitCounter > 0)
                {
                    //use the Time.deltaTime function to decrease the waitCounter float
                    waitCounter -= Time.deltaTime;
                }
                //once the wait counter is at 0
                else
                {
                    //change the state of the enum to now be moving because the enemy has been at the base point for the alloted time amount
                    actionRightNow = AIState.onTheMoving;
                    //use the prebuilt set destination function to go to the next point which is in the array
                    agent.SetDestination(skeletonBaseAreas[currentPoint].position);
                }
                //if the distance to player amount is smaller or the same as the area in which the enemy senses the player and starts to pursue him
                //change the state
                if (vectorRangeFromPlayer <= playerInPursuitArea)
                {
                    //go to the state that chases the player
                    actionRightNow = AIState.pursuingPlayer;
                    //set isMoving boolean to true because the skeleton will be pursuing the player
                    //this is necessary because the skeleton could be at the base point being idle when the player comes within range
                    anim.SetBool("IsMoving", true);
                }
                //break the case
                break;
            //the next case is for when the enemy is now going to be on the move
            case AIState.onTheMoving:

                //
                //if the remaning distance to the patrol point is 0.1 or smaller
                if (agent.remainingDistance <= 0.1f)
                {
                    //set the new base point
                    //this help give the enemies more fluid movement
                    currentPoint++;
                    //this is to avoid an index out of bounds error if the current base is the last one on the array
                    if (currentPoint >= skeletonBaseAreas.Length)

                    {
                        //set the current point to go back to 0 so the skeletons go back to the first base point
                        currentPoint = 0;
                    }

                    //error with the line below
                    //agent.SetDestination(patrolPoints[currentPoint].position);

                    //set the enum to be the stationaryPoaition state
                    actionRightNow = AIState.stationaryPosition;
                    //reset the wait counter
                    waitCounter = waitAtPoint;
                }
                //if statement to change the enum state if the player gets too close to the enemy
                if (vectorRangeFromPlayer <= playerInPursuitArea)
                {
                    actionRightNow = AIState.pursuingPlayer;
                }
                //make sure the skeleton is in the moving animation
                anim.SetBool("IsMoving", true);
                //break the case
                break;

            //case for when the enemy is chasing the player
            case AIState.pursuingPlayer:
                //change the destination the enemy is approaching to be the player instead of the base area
                agent.SetDestination(RhodriObject.instance.transform.position);
                //if statement to determine whether the skeleton should stop chasing the player, and finally attack since it is close enough
                if (vectorRangeFromPlayer <= playerDamagerReach)
                {
                    //change the state to the playerDamager 
                    actionRightNow = AIState.playerDamager;
                    //set the animation for attack
                    anim.SetTrigger("Attack");
                    //and turn is moving to false since the running animation shouldn't coincide with the attacking animation
                    anim.SetBool("IsMoving", false);
                    //make the velocity of the AI enemy 0
                    agent.velocity = Vector3.zero;
                    //set the boolean function in unity to true now that the velocity is 0
                    agent.isStopped = true;
                    //set the attack counter
                    attackCounter = timeBetweenAttacks;
                }
                //if the player gets too far away from the enemy
                if (vectorRangeFromPlayer > playerInPursuitArea)
                {
                    //return it to it's idle statessss
                    actionRightNow = AIState.stationaryPosition;
                    //set the waitCOunter
                    waitCounter = waitAtPoint;
                    //make the enemy stop and revert back to its position
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }
                //break the case
                break;
            //case for when the enemy is attacking the player and causing damage
            case AIState.playerDamager:
                //make sure the enemy is facing the player when attacking using the unity built in .LookAt function
                transform.LookAt(RhodriObject.instance.transform, Vector3.up);
                //quaternion to make sure the skeleton doesnt angle himself along the y axis 
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                //set the attackcounter to go down with the passing of time
                attackCounter -= Time.deltaTime;
                //conditional for once the attack counter has depleted
                if (attackCounter <= 0)
                {   //redo the animation since the conditional checks to see if the player is still within attack range
                    if (vectorRangeFromPlayer <= playerDamagerReach)
                    {   //set the trigger animation
                        anim.SetTrigger("Attack");
                        //set the counter again
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        //return it to it's idle statessss
                        actionRightNow = AIState.stationaryPosition;
                        //set the wait counter
                        waitCounter = waitAtPoint;
                        //set the boolean to false so the enemy now has permission to move again
                        agent.isStopped = false;

                    }
                }
                //break the case
                break;
        }




    }
}
