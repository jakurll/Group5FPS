using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
  public GameObject player; //Player Target
  public GameObject patrol; //Patrol Position
  public float speed = 0.15f;
  public bool run = false; //Chase the player
  public bool seekPatrol = false;
  private Animator _animator;

  public Rigidbody EnemyRigidbody;

  // Start is called before the first frame update
  void Start()
  {
    //Grab animator for animations
    _animator = GetComponent<Animator>();

    //Freeze Y coordinate so enemy does not float
    EnemyRigidbody = GetComponent<Rigidbody>();
    //This locks the RigidBody so that it does not move or rotate in the Y axis.
    EnemyRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;

    //Enemy stand up straight
    transform.Rotate(-90, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    if (player != null && run)
    {
      Run();
      transform.Rotate(-90, 0, 0);
    }

    if (player == null && seekPatrol)
    {
      SeekPatrol();
      transform.Rotate(-90, 0, 0);
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
      _animator.SetBool("isRunning", true);
    }
  }

  //Check if player left box collider
  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      player = null;
      run = false;
      seekPatrol = true;
      _animator.SetBool("isRunning", false);
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
