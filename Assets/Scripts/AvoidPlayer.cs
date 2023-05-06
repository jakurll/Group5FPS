using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayer : MonoBehaviour
{
  private UnityEngine.AI.NavMeshAgent Agent; //Enemy NavMeshAgent
  public GameObject Player; //Player object
  public float EnemyDistance = 4.0f; //Distance that the enemy will run away from the player
  private Animator animator; //Animation

  // Start is called before the first frame update
  void Start()
  {
    //Grab animator for animations
    animator = GetComponent<Animator>();

    //Get Enemy NavMeshAgent
    Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    //Set distance between the player and enemy
    float distance = Vector3.Distance(transform.position, Player.transform.position);

    //Run away from player if they are within distance
    if (distance < EnemyDistance)
    {
      Vector3 dirToPlayer = transform.position - Player.transform.position;
      Vector3 newPos = transform.position + dirToPlayer;
      Agent.SetDestination(newPos);
      animator.SetBool("sadRunning", true);
    }
    //Otherwise go idle
    else
    {
      animator.SetBool("sadRunning", false);
    }
  }
}
