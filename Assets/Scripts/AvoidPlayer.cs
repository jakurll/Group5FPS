using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayer : MonoBehaviour
{
  private UnityEngine.AI.NavMeshAgent Agent;
  public GameObject Player;
  public float EnemyDistance = 4.0f;

  // Start is called before the first frame update
  void Start()
  {
    Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
  }

  // Update is called once per frame
  void Update()
  {
    float distance = Vector3.Distance(transform.position, Player.transform.position);

    //Run away from player
    if (distance < EnemyDistance)
    {
      Vector3 dirToPlayer = transform.position - Player.transform.position;
      Vector3 newPos = transform.position + dirToPlayer;
      Agent.SetDestination(newPos);
    }
  }
}
