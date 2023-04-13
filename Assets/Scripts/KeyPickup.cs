using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if(inventory != null)
        {
            if(inventory.hasKey == true)
            {

            }
            inventory.hasKey = true;
            Destroy(this.gameObject);
        }
    }
}
