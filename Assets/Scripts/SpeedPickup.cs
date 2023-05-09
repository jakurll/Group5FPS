using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
  //Get speedBoost value and get speed value from movement script
  public int speedLimit = 10;
  public GameObject player;

  Movement movement;

  void Awake()
  {


  }

  void Update()
  {

  }

  // Destroy the pickup game object and add
  // speed boost to movement speed of character
  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      player = other.gameObject;
      movement = FindObjectOfType<Movement>();
      StartCoroutine(SpeedDuration());
    }
    else
    {
      movement.speed = 5.0f;
    }
  }

  // Add a time limit to the speed boost
  IEnumerator SpeedDuration()
  {
    movement.speed = 10.0f;

    yield return new WaitForSeconds(5);

    Destroy(this.gameObject);
    movement.speed = 5.0f;
  }
}
