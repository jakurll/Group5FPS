using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void FindObjects()
    {
        GameObject[] objects;
        objects = GameObject.FindGameObjectsWithTag("Enemy");

        //If there are no more enemies, spawn key pickup
        if (objects.Length == 0)
        {
            void OnTriggerEnter(Collider other)
            {
                //Check if player has the key
                PlayerInventory inventory = other.GetComponent<PlayerInventory>();
                if (inventory != null)
                {
                    if (inventory.hasKey == true)
                    {

                    }
                    inventory.hasKey = true;
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(true);
            }
        }
    }
}
