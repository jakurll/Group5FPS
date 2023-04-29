using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;

  public void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Key"))
    {
      hasKey = true;
    }
  }
}
