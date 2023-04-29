using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{

  public GameObject key;
  public bool pickedUpKey = false;

  void Start()
  {
    key.SetActive(false);
  }

  void Update()
  {
    GameObject[] gameObjects;
    gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
    if (gameObjects.Length == 0 && pickedUpKey == false)
    {
      key.SetActive(true);
    }
    if (pickedUpKey == true) 
    {
      key.SetActive(false);
    }
  }

  public void OnTriggerEnter(Collider other)
  {
    //Check if player has the key
    PlayerInventory inventory = other.GetComponent<PlayerInventory>();
    if(other.gameObject.CompareTag("Key"))
    {
      pickedUpKey = true;
      if (inventory != null)
      {
        inventory.hasKey = true;
        key.SetActive(false);
      }
    }
  }
}
