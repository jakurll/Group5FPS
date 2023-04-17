using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
  public GameObject player; //Player Target
  public GameObject patrol; //Patrol Position
  public float speed = 0.02f;
  public bool run = false; //Chase the player
  public bool seekPatrol = false;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (player != null && run)
    {
      Run();
    }

    if (player == null && seekPatrol)
    {
      SeekPatrol();
    }
  }

  //Check if player is in box collider
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      player = other.gameObject;
      run = true;
      seekPatrol = false;
    }
  }

  //Check if player left box collider
  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      player = null;
      run = false;
    }
    else
    {
      seekPatrol = true;
    }
  }

  //Look and run towards player
  private void Run()
  {
    transform.LookAt(player.transform.position);
    transform.position = (transform.forward * speed) + transform.position;
  }

  //Look and run towards patrol spot
  private void SeekPatrol()
  {
    transform.LookAt(patrol.transform.position);
    transform.position = (transform.forward * speed) + transform.position;
    transform.LookAt(patrol.transform.forward);
  }
}
