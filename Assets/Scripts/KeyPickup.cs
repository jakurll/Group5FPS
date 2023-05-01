using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{

  public GameObject key; //Get key asset
  public bool pickedUpKey = false; //Player has collided with key collider

  void Start()
  {
    key.SetActive(false); //Key is not visible at the start of the game
  }

  void Update()
  {
    //Create an array based on how many objects have the tag "Enemy"
    GameObject[] gameObjects;
    gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

    //If the array is empty then set the key to be visible
    if (gameObjects.Length == 0 && pickedUpKey == false)
    {
      key.SetActive(true);
    }

    //If the player has collided with the key then make the key not visible
    if (pickedUpKey == true) 
    {
      key.SetActive(false);
    }
  }

  public void OnTriggerEnter(Collider other)
  {
    //Check if player has the key in their inventory
    PlayerInventory inventory = other.GetComponent<PlayerInventory>();

    //If player collides with a game object with the tag "Key" then set
    //bool to be true for hasKey and pickedUpKey
    //Finally set key visibility to false
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
