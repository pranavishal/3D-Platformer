using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using the import of unities AI system for the skeleton enemies
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    /*
     * this class is used as a test to learn about how the NavMeshAgent import and unity AI system works
     * this is not part of the final game, more of a lesson for Sahib and I
     */
    //create a target in which the enemy is trying to follow
    public Transform target;
    //create a navmesh agent for the enemy
    public NavMeshAgent agentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        //Use the pre built set destination function to move towards the destination
        agentEnemy.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        //resuse the same command in the start funciton for every new frame
        //this is important because realistically this object is going to be chasing a player and the player will not be standing still from when the game started
        //this being in the update void allows the destination the navmeshagent is moving towards be updated as the player moves
        agentEnemy.SetDestination(target.position);

    }
}
