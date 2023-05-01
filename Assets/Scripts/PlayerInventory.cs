using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false; //Player does or does not have the key

  //If the player collides into an object with the tag "Key" then set
  //hasKey to true
  public void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Key"))
    {
      hasKey = true;
    }
  }
}
