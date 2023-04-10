using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
  public GameObject player;
  public float speed = 0.1f;
  public bool run = false;

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
  }

  //Check if player is in box collider
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      player = other.gameObject;
      run = true;
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
  }

  //Look and run towards player
  private void Run()
  {
    transform.LookAt(player.transform.position);
    transform.position = (transform.forward * speed) + transform.position;
  }
}
